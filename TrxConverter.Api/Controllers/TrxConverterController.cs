using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml;
using TrxConverter.Api.Models;
using TrxConverter.CommonLibrary.Logic;
using TrxConverter.CommonLibrary.Models;
using System.Text;

namespace TrxConverter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrxConverterController : ControllerBase
    {
        /// <summary>
        /// trxファイルをCSVファイルに変換します。
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

                var outputStream = new MemoryStream();
                var writer = new StreamWriter(outputStream, new UTF8Encoding(true));
                var csv = new CsvWriter(writer, CultureInfo.CurrentCulture);
                csv.Context.RegisterClassMap<TestReportLineMap>();
                await csv.WriteRecordsAsync(report);
                await writer.FlushAsync();
                outputStream.Position = 0;

                return File(outputStream, "application/octet-stream", fileDownloadName: Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.Ticks + ".csv");
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
