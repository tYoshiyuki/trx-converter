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
        private const string ExpectedConsoleOutText = @"
TrxConverter

Description:
    Visual Studio テスト結果ファイル (.trx) を CSVファイルに変換するツール

Usage:
    TrxConverter.ConsoleApp.exe <変換対象.trxファイルパス>

";

        [Theory]
        [MemberData(nameof(TestData))]
        public void 正常系(string path)
        {
            // Arrange・Act・Assert
            Assert.True(Program.Main(new[] { path }));
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
            yield return new object[] { TestFileName01 };
            yield return new object[] { Path.Combine(".", TestFileName02) };
            yield return new object[] { Path.Combine(Directory.GetCurrentDirectory(), TestFileName03) };
            yield return new object[] { TestFileName04 };
        }
    }
}
