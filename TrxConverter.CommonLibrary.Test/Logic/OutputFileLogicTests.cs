using System.Collections.Generic;
using System.IO;
using TrxConverter.CommonLibrary.Logic;
using TrxConverter.CommonLibrary.Models;
using Xunit;

namespace TrxConverter.CommonLibrary.Test.Logic
{
    [Trait("Category", "Logic")]
    public class OutputFileLogicTests
    {
        [Fact]
        public void OutputCsvFile_正常系()
        {
            // Arrange
            var reportLines = new List<TestReportLine>
            {
                new()
            };
            var path = "OutputCsvFile_正常系.csv";

            // Act
            OutputFileLogic.OutputCsvFile(reportLines, path);

            // Assert
            Assert.True(File.Exists(path));
        }

        [Fact]
        public void OutputPlaylistFile_正常系()
        {
            // Arrange
            var reportLines = new List<TestReportLine>
            {
                new()
                {
                    TestClassName = "TestClassName",
                    TestCaseName = "TestCaseName (ParameterName)",
                    OutCome = "Failed"
                }
            };
            var path = "OutputPlaylistFile_正常系.playlist";

            // Act
            OutputFileLogic.OutputPlaylistFile(reportLines, path);

            // Assert
            Assert.True(File.Exists(path));
        }

        [Fact]
        public void OutputPlaylistFile_正常系_プレイリスト出力無し()
        {
            // Arrange
            var reportLines = new List<TestReportLine>
            {
                new()
            };
            var path = "OutputPlaylistFile_正常系_プレイリスト出力無し.playlist";

            // Act
            OutputFileLogic.OutputPlaylistFile(reportLines, path);

            // Assert
            Assert.False(File.Exists(path));
        }

    }
}
