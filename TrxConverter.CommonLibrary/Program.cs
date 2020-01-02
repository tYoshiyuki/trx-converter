using System;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using TrxConverter.CommonLibrary.Logic;
using TrxConverter.CommonLibrary.Models;

namespace TrxConverter.CommonLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
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
                return;
            }

            try
            {
                Console.WriteLine(".trxファイルの変換処理を開始します。");

                var input = args.First();
                Console.WriteLine($"対象ファイルパス: [{input}]");

                var fileName = Path.GetFileName(input);
                if (fileName == null) throw new ArgumentNullException(nameof(fileName));

                var dirName = Path.GetDirectoryName(input);
                if (dirName == null) throw new ArgumentNullException(nameof(dirName));

                foreach (var file in Directory.EnumerateFiles(dirName, fileName, SearchOption.TopDirectoryOnly))
                {
                    var fs = new FileStream(file, FileMode.Open);
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(TestRun));
                    var model = (TestRun)serializer.Deserialize(fs);

                    var report = ConvertLogic.Convert(model);

                    using (var writer = new StreamWriter($"{Path.GetFileNameWithoutExtension(file)}.csv", false, new UTF8Encoding(true)))
                    using (var csv = new CsvWriter(writer))
                    {
                        csv.Configuration.RegisterClassMap<TestReportLineMap>();
                        csv.WriteRecords(report);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("変換処理に失敗しました。");
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);
            }

            Console.WriteLine("変換処理が完了しました。");

        }
    }
}
