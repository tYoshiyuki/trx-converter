using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace TrxConverter.CommonLibrary.Test
{
    [Trait("Category", "Integration")]
    public class ProgramTests
    {
        private const string TestFileName01 = "sample-data-20200101.trx";
        private const string TestFileName02 = "sample-data-20200102.trx";
        private const string TestFileName03 = "sample-data-20200103.trx";
        private const string TestFileName04 = "sample-data-20200104-*.trx";
        private const string NgTestFileName01 = "sample-ngdata-20200101.trx";
        private const string NgTestFileName02 = "sample-ngdata-20200102.trx";
        private const string NgTestFileName03 = "sample-ngdata-20200103.trx";
        private const string NgTestFileName04 = "sample-ngdata-20200104-*.trx";
        private const string ExpectedConsoleOutText = @"
TrxConverter

Description:
    Visual Studio テスト結果ファイル (.trx) を CSVファイルに変換するツール

Usage:
    TrxConverter.ConsoleApp.exe <変換対象.trxファイルパス>

";

        [Theory]
        [MemberData(nameof(TestData))]
        public void 正常系_プレイリスト出力無(string path, string[] expectedList)
        {
            // Arrange・Act・Assert
            Assert.True(Program.Main(new[] { path }));

            foreach (var expected in expectedList)
            {
                Assert.True(File.Exists($"{Path.GetFileNameWithoutExtension(expected)}.csv"));
                Assert.False(File.Exists($"{Path.GetFileNameWithoutExtension(expected)}.playlist"));
            }
        }

        [Theory]
        [MemberData(nameof(NgTestData))]
        public void 正常系_プレイリスト出力有(string path, string[] expectedList)
        {
            // Arrange・Act・Assert
            Assert.True(Program.Main(new[] { path }));

            foreach (var expected in expectedList)
            {
                Assert.True(File.Exists($"{Path.GetFileNameWithoutExtension(expected)}.csv"));
                Assert.True(File.Exists($"{Path.GetFileNameWithoutExtension(expected)}.playlist"));
            }
        }

        [Fact]
        public void 正常系_引数がNULLの場合()
        {
            var originalOut = Console.Out;

            try
            {
                // Arrange
                using var stringWriter = new StringWriter();
                Console.SetOut(stringWriter);

                // Act
                Assert.True(Program.Main(null));

                // Assert
                var output = stringWriter.ToString();
                Assert.Equal(ExpectedConsoleOutText, output);
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void 正常系_引数が空の場合()
        {
            var originalOut = Console.Out;

            try
            {
                // Arrange
                using var stringWriter = new StringWriter();
                Console.SetOut(stringWriter);

                // Act
                Assert.True(Program.Main(new string[] { }));

                // Assert
                var output = stringWriter.ToString();
                Assert.Equal(ExpectedConsoleOutText, output);
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void 異常系_引数のファイルが存在しない場合()
        {
            var originalOut = Console.Error;

            try
            {
                // Arrange
                using var stringWriter = new StringWriter();
                Console.SetError(stringWriter);

                // Act
                Assert.False(Program.Main(new[]
                    { Path.Combine(Directory.GetCurrentDirectory(), Path.DirectorySeparatorChar.ToString()) }));

                // Assert
                var output = stringWriter.ToString();
                Assert.Contains("変換対象となる.trxファイルパスを指定して下さい", output);
            }
            finally
            {
                Console.SetError(originalOut);
            }
        }

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { TestFileName01, new [] { TestFileName01 } };
            yield return new object[] { Path.Combine(".", TestFileName02), new[] { TestFileName02 } };
            yield return new object[] { Path.Combine(Directory.GetCurrentDirectory(), TestFileName03), new[] { TestFileName03 } };
            yield return new object[] { TestFileName04, new[] { "sample-data-20200104-01.trx", "sample-data-20200104-02.trx", "sample-data-20200104-03.trx" } };
        }

        public static IEnumerable<object[]> NgTestData()
        {
            yield return new object[] { NgTestFileName01, new[] { NgTestFileName01 } };
            yield return new object[] { Path.Combine(".", NgTestFileName02), new[] { NgTestFileName02 } };
            yield return new object[] { Path.Combine(Directory.GetCurrentDirectory(), NgTestFileName03), new[] { NgTestFileName03 } };
            yield return new object[] { NgTestFileName04, new[] { "sample-ngdata-20200104-01.trx", "sample-ngdata-20200104-02.trx", "sample-ngdata-20200104-03.trx" } };
        }
    }
}
