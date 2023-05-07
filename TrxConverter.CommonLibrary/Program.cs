using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CsvHelper;
using TrxConverter.CommonLibrary.Logic;
using TrxConverter.CommonLibrary.Models;

namespace TrxConverter.CommonLibrary
{
    public static class Program
    {
        public static bool Main(string[] args)
        {
            var result = true;
            if (args == null || args.Length == 0)
            {
                Console.WriteLine();
                Console.WriteLine("TrxConverter");
                Console.WriteLine();
                Console.WriteLine("Description:");
                Console.WriteLine("    Visual Studio テスト結果ファイル (.trx) を CSVファイルに変換するツール");
                Console.WriteLine();
                Console.WriteLine("Usage:");
                Console.WriteLine("    TrxConverter.ConsoleApp.exe <変換対象.trxファイルパス>");
                Console.WriteLine();
                return true;
            }

            try
            {
                Console.WriteLine(".trxファイルの変換処理を開始します。");

                var input = args.First();
                Console.WriteLine($"対象ファイルパス: [{input}]");

                var fileName = Path.GetFileName(input);
                if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("変換対象となる.trxファイルパスを指定して下さい");

                var dirName = Path.GetDirectoryName(input);
                if (string.IsNullOrEmpty(dirName)) dirName = Directory.GetCurrentDirectory();

                foreach (var file in Directory.EnumerateFiles(dirName, fileName, SearchOption.TopDirectoryOnly))
                {
                    using (var fs = new FileStream(file, FileMode.Open))
                    using (var xml = XmlReader.Create(fs))
                    {
                        var serializer = new XmlSerializer(typeof(TestRun));
                        var model = (TestRun)serializer.Deserialize(xml);

                        var report = ConvertLogic.Convert(model);

                        using (var writer = new StreamWriter($"{Path.GetFileNameWithoutExtension(file)}.csv", false, new UTF8Encoding(true)))
                        using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                        {
                            csv.Context.RegisterClassMap<TestReportLineMap>();
                            csv.WriteRecords(report);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("変換処理に失敗しました。");
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);
                result = false;
            }

            Console.WriteLine("変換処理が完了しました。");
            return result;
        }
    }
}
