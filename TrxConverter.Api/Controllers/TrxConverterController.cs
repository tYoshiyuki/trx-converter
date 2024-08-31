using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Xml.Serialization;
using System.Xml;
using TrxConverter.Api.Models;
using TrxConverter.CommonLibrary.Logic;
using TrxConverter.CommonLibrary.Models;
using TrxConverter.Api.Services;

namespace TrxConverter.Api.Controllers
{
    /// <summary>
    /// trxファイルをCSVファイル、プレイリストファイルに変換するコントローラです。
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TrxConverterController : ControllerBase
    {
        private readonly ITempFolderService tempFolderService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="tempFolderService"></param>
        public TrxConverterController(ITempFolderService tempFolderService)
        {
            this.tempFolderService = tempFolderService;
        }

        /// <summary>
        /// trxファイルをCSVファイル、プレイリストファイルに変換します。
        /// 変換結果はzip形式でアーカイブします。
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Convert")]
        [Produces("application/octet-stream", Type = typeof(FileResult))]
        public async Task<IActionResult> Convert([FromForm] ConvertRequest request)
        {
            try
            {
                var file = request.File;
                using var inputStream = new MemoryStream();
                await file.CopyToAsync(inputStream);
                inputStream.Position = 0;

                // 入力ファイルのデシリアライズ
                using var xml = XmlReader.Create(inputStream);
                var serializer = new XmlSerializer(typeof(TestRun));
                var model = (TestRun?)serializer.Deserialize(xml);

                // 変換処理の実行
                var report = ConvertLogic.Convert(model);

                // 一時フォルダを作成
                var tmpPath = tempFolderService.Create();

                // CSV, プレイリストを生成する
                var csvFilePath = Path.Combine(tmpPath, Path.GetFileNameWithoutExtension(file.FileName) + ".csv");
                OutputFileLogic.OutputCsvFile(report, csvFilePath);

                var playListFilPath = Path.Combine(tmpPath, Path.GetFileNameWithoutExtension(file.FileName) + ".playlist");
                OutputFileLogic.OutputPlaylistFile(report, playListFilPath);

                // zip形式へアーカイブする
                using var ms = new MemoryStream();
                using (var ar = new ZipArchive(ms, ZipArchiveMode.Create, true))　// NOTE アーカイブに失敗するため、明示的にスコープを区切ってDisposeする
                {
                    var directoryInfo = new DirectoryInfo(tmpPath);
                    var entries = directoryInfo.EnumerateFileSystemInfos("*", SearchOption.AllDirectories);
                    foreach (var entry in entries)
                    {
                        if (entry is not FileInfo) continue;

                        var entryName = entry.Name;
                        var zipEntry = ar.CreateEntry(entryName);
                        await using var zipStream = zipEntry.Open();
                        await using var fs = new FileStream(entry.FullName, FileMode.Open, FileAccess.Read);
                        await fs.CopyToAsync(zipStream);
                    }
                }

                // 一時フォルダを削除
                Directory.Delete(tmpPath, true);

                return File(ms.ToArray(), "application/zip", fileDownloadName: Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.Ticks + ".zip");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
    }
}
