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
        [Theory]
        [MemberData(nameof(TestData))]
        public void 正常系(string path)
        {
            // Arrange・Act・Assert
            Assert.True(Program.Main(new[] { path }));
        }

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { TestFileName01 };
            yield return new object[] { Path.Combine(".", TestFileName02) };
            yield return new object[] { Path.Combine(Directory.GetCurrentDirectory(), TestFileName03) };
        }
    }
}
