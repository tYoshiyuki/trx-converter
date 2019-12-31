using System;
using System.Linq;
using TrxConverter.CommonLibrary.Logic;
using TrxConverter.CommonLibrary.Models;
using Xunit;

namespace TrxConverter.CommonLibrary.Test.Logic
{
    [Trait("Category", "Logic")]
    public class ConvertLogicTests
    {
        [Fact]
        public void Convert_正常系_対象が全て空の場合()
        {
            // Arrange
            var target = new TestRun();

            // Act
            var result = ConvertLogic.Convert(target);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public void Convert_正常系_UnitTestResultのみ存在する場合()
        {
            // Arrange
            var target = new TestRun
            {
                Results = GetUnitTestResultTestData()
            };

            // Act
            var result = ConvertLogic.Convert(target);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            var resultLine = result.First();
            var expected = target.Results.First();
            AssertUnitTestResult(expected, resultLine);
        }

        [Fact]
        public void Convert_正常系_UnitTestResultとTestDefinitionが存在する場合()
        {
            // TODO 実装途中
            // Arrange
            var target = new TestRun
            {
                Results = GetUnitTestResultTestData(),
                TestDefinitions = new[]
                {
                    new UnitTest
                    {
                        Id = "TestId001",
                        Name = "TestName001",
                        TestMethod = new TestMethod
                        {
                            ClassName = "TestClass001",
                            Name = "TestMethod001"
                        },
                        TestCategory = new []
                        {
                            new TestCategoryItem
                            {
                                TestCategory = "TestCategory001-A"
                            },
                            new TestCategoryItem
                            {
                                TestCategory = "TestCategory001-B"
                            }
                        }
                    }
                }
            };

            // Act
            var result = ConvertLogic.Convert(target);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count == 1);

            var resultLine = result.First();
            var expected = target.Results.First();
            AssertUnitTestResult(expected, resultLine);
        }

        private UnitTestResult[] GetUnitTestResultTestData()
        {
            return new[]
            {
                new UnitTestResult
                {
                    TestId = "TestId001",
                    TestName = "TestName001",
                    Outcome = "Outcome001",
                    Duration = "2019/12/31 10:00:00",
                    StartTime = "2019/12/31 11:00:00",
                    EndTime = "2019/12/31 12:00:00",
                    Output = new Output
                    {
                        ErrorInfo = new ErrorInfo
                        {
                            Message = "Message001",
                            StackTrace = "StackTrace001"
                        }
                    }
                }
            };
        }

        private void AssertUnitTestResult(UnitTestResult expected, TestReportLine resultLine)
        {
            Assert.Equal(expected.TestName, resultLine.TestCaseName);
            Assert.Equal(expected.Outcome, resultLine.OutCome);
            Assert.Equal(DateTime.Parse(expected.Duration), resultLine.Duration);
            Assert.Equal(DateTime.Parse(expected.StartTime), resultLine.StartTime);
            Assert.Equal(DateTime.Parse(expected.EndTime), resultLine.EndTime);
            Assert.Equal(expected.Output.ErrorInfo.Message, resultLine.ErrorMessage);
            Assert.Equal(expected.Output.ErrorInfo.StackTrace, resultLine.StackTrace);
        }
    }
}
