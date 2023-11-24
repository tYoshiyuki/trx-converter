using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using TrxConverter.CommonLibrary.Models;

namespace TrxConverter.CommonLibrary.Logic
{
    /// <summary>
    /// ファイル出力ロジッククラス
    /// </summary>
    public static class OutputFileLogic
    {
        /// <summary>
        /// CSVファイルを出力します。
        /// </summary>
        /// <param name="reportLines"><see cref="TestReportLine"/>のリスト</param>
        /// <param name="path">出力先パス</param>
        public static void OutputCsvFile(IEnumerable<TestReportLine> reportLines, string path)
        {
            using (var writer = new StreamWriter(path, false, new UTF8Encoding(true)))
            using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                csv.Context.RegisterClassMap<TestReportLineMap>();
                csv.WriteRecords(reportLines);
            }
        }

        /// <summary>
        /// プレイリストファイルを出力します。
        /// テスト結果がNGだったテストケースを対象とします。
        /// </summary>
        /// <param name="reportLines"><see cref="TestReportLine"/>のリスト</param>
        /// <param name="path">出力先パス</param>
        public static void OutputPlaylistFile(IEnumerable<TestReportLine> reportLines, string path)
        {
            if (reportLines is null)
            {
                return;
            }

            var ngLines = reportLines.Where(x => x.OutCome == "Failed").ToList();
            if (ngLines.Any())
            {
                using (var writer = new StreamWriter(path, false, new UTF8Encoding(true)))
                {
                    writer.WriteLine("<Playlist Version=\"1.0\">");
                    foreach (var line in ngLines)
                    {
                        // HACK パラメタライズドテストの場合 テストケース名 + (パラメータ名) となるため、不要な文字列をトリミングする
                        writer.WriteLine($"  <Add Test=\"{line.TestClassName}.{line.TestCaseName.Split(' ').First()}\" />");
                    }
                    writer.WriteLine("</Playlist>");
                }
            }
        }
    }
}
