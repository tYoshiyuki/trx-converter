using System.Collections.Generic;
using System.IO;
using System.Threading;
using Xunit;

namespace TrxConverter.CommonLibrary.Test
{
    [Trait("Category", "Integration")]
    public class ProgramTests
    {
        private const string TestFileName = "sample-data-20200101.trx";

        [Theory]
        [MemberData(nameof(TestData))]
        public void 正常系(string path)
        {
            // Arrange
            var expected = Path.Combine(Directory.GetCurrentDirectory(), $"{Path.GetFileNameWithoutExtension(TestFileName)}.csv");
            if (File.Exists(expected))
            {
                File.Delete(expected);
                while (File.Exists(expected))
                {
                    Thread.Sleep(1000);
                }
            }

            // Act
            Program.Main(new[] { path });
            Thread.Sleep(5000);

            // Assert
            Assert.True(File.Exists(expected));
        }

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { TestFileName };
            yield return new object[] { Path.Combine(".", TestFileName) };
            yield return new object[] { Path.Combine(Directory.GetCurrentDirectory(), TestFileName) };
        }
    }
}
