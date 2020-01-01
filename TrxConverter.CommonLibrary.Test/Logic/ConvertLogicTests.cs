using JetBrains.Annotations;
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
                Results = GetUnitTestResultTestData().Take(1).ToArray()
            };

            // Act
            var result = ConvertLogic.Convert(target);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);

            var resultLine = result.First();
            var expected = target.Results.First();
            AssertUnitTestResult(expected, resultLine);
        }

        [Fact]
        public void Convert_正常系_UnitTestResultとTestDefinitionが1件存在する場合()
        {
            // Arrange
            var target = new TestRun
            {
                Results = GetUnitTestResultTestData().Take(1).ToArray(),
                TestDefinitions = GetTestDefinitionTestData().Take(1).ToArray()
            };

            // Act
            var result = ConvertLogic.Convert(target);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);

            var resultLine = result.First();
            var expectedResult = target.Results.First();
            AssertUnitTestResult(expectedResult, resultLine);

            var expectedDefinition = target.TestDefinitions.First();
            AssertTestDefinition(expectedDefinition, resultLine);
        }

        [Fact]
        public void Convert_正常系_UnitTestResultとTestDefinitionが複数件存在する場合()
        {
            // Arrange
            var target = new TestRun
            {
                Results = GetUnitTestResultTestData().Take(3).ToArray(),
                TestDefinitions = GetTestDefinitionTestData().Take(3).ToArray()
            };

            // Act
            var result = ConvertLogic.Convert(target);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);

            var i = 0;
            foreach (var resultLine in result)
            {
                var expectedResult = target.Results.Skip(i).First();
                AssertUnitTestResult(expectedResult, resultLine);

                var expectedDefinition = target.TestDefinitions.Skip(i).First();
                AssertTestDefinition(expectedDefinition, resultLine);
                i++;
            }
        }

        [Fact]
        public void Convert_正常系_UnitTestResultとTestDefinitionとInnerUnitTestResultが複数件存在する場合()
        {
            // Arrange
            var target = new TestRun
            {
                Results = GetUnitTestResultTestData(),
                TestDefinitions = GetTestDefinitionTestData()
            };

            // Act
            var result = ConvertLogic.Convert(target);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count);

            var i = 0;
            foreach (var resultLine in result.Take(3))
            {
                var expectedResult = target.Results.Skip(i).First();
                AssertUnitTestResult(expectedResult, resultLine);

                var expectedDefinition = target.TestDefinitions.Skip(i).First();
                AssertTestDefinition(expectedDefinition, resultLine);
                i++;
            }

            var expectedResults = target.Results.Skip(3).First().InnerResults;
            i = 0;
            foreach (var resultLine in result.Skip(3))
            {
                var expected = expectedResults.Skip(i).First();
                AssertInnerUnitTestResult(expected, resultLine);
                i++;
            }
        }

        private static UnitTestResult[] GetUnitTestResultTestData()
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
                },
                new UnitTestResult
                {
                    TestId = "TestId002"
                },
                new UnitTestResult
                {
                    TestId = "TestId003",
                    Output = new Output(),
                    InnerResults = new []
                    {
                        new InnerUnitTestResult()
                    }
                },
                new UnitTestResult
                {
                    TestId = "TestId004",
                    Output = new Output
                    {
                        ErrorInfo = new ErrorInfo
                        {
                            Message = "Message004",
                            StackTrace = "StackTrace004"
                        }
                    },
                    InnerResults = new []
                    {
                        new InnerUnitTestResult
                        {
                            TestName = "InnerTestName001",
                            Outcome = "InnerOutcome001",
                            Duration = "2019/12/31 10:00:00",
                            StartTime = "2019/12/31 11:00:00",
                            EndTime = "2019/12/31 12:00:00",
                            Output = new Output
                            {
                                ErrorInfo = new ErrorInfo
                                {
                                    Message = "InnerMessage001",
                                    StackTrace = "InnerStackTrace001"
                                }
                            }
                        },
                        new InnerUnitTestResult
                        {
                            TestName = "InnerTestName002",
                            Outcome = "InnerOutcome002",
                            Duration = "2019/12/31 10:10:00",
                            StartTime = "2019/12/31 11:10:00",
                            EndTime = "2019/12/31 12:10:00",
                            Output = new Output
                            {
                                ErrorInfo = new ErrorInfo
                                {
                                    Message = "InnerMessage002",
                                    StackTrace = "InnerStackTrace002"
                                }
                            }
                        }
                    }
                }
            };
        }

        private static UnitTest[] GetTestDefinitionTestData()
        {
            return new[]
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
                    TestCategory = new[]
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
                },
                new UnitTest
                {
                    Id = "TestId002"
                },
                new UnitTest
                {
                    Id = "TestId003",
                    TestMethod = new TestMethod(),
                    TestCategory = new TestCategoryItem[] { }
                },
                new UnitTest
                {
                    Id = "TestId004",
                    Name = "TestName004",
                    TestMethod = new TestMethod
                    {
                        ClassName = "TestClass004",
                        Name = "TestMethod004"
                    },
                    TestCategory = new[]
                    {
                        new TestCategoryItem
                        {
                            TestCategory = "TestCategory004-A"
                        },
                        new TestCategoryItem
                        {
                            TestCategory = "TestCategory004-B"
                        }
                    }
                }
            };
        }

        [AssertionMethod]
        private static void AssertUnitTestResult(UnitTestResult expected, TestReportLine actual)
        {
            Assert.Equal(expected.TestName, actual.TestCaseName);
            Assert.Equal(expected.Outcome, actual.OutCome);
            AssertDateTime(expected.Duration, actual.Duration);
            AssertDateTime(expected.StartTime, actual.StartTime);
            AssertDateTime(expected.EndTime, actual.EndTime);
            Assert.Equal(expected.Output?.ErrorInfo?.Message, actual.ErrorMessage);
            Assert.Equal(expected.Output?.ErrorInfo?.StackTrace, actual.StackTrace);
        }

        [AssertionMethod]
        private static void AssertInnerUnitTestResult(InnerUnitTestResult expected, TestReportLine actual)
        {
            Assert.Equal(expected.TestName, actual.ParameterTestCaseName);
            Assert.Equal(expected.Outcome, actual.ParameterTestOutCome);
            AssertDateTime(expected.StartTime, actual.ParameterTestStartTime);
            AssertDateTime(expected.EndTime, actual.ParameterTestEndTime);
            AssertDateTime(expected.Duration, actual.ParameterTestDuration);
            Assert.Equal(expected.Output?.ErrorInfo?.Message, actual.ParameterTestErrorMessage);
            Assert.Equal(expected.Output?.ErrorInfo?.StackTrace, actual.ParameterTestStackTrace);
        }

        [AssertionMethod]
        private static void AssertTestDefinition(UnitTest expected, TestReportLine actual)
        {
            Assert.Equal(expected?.TestMethod?.ClassName, actual.TestClassName);
            Assert.Equal(string.Join(",", expected?.TestCategory?.Select(_ => _.TestCategory) ?? Enumerable.Empty<string>()), actual.TestCategory);
        }

        [AssertionMethod]
        private static void AssertDateTime(string expected, DateTime? actual)
        {
            if (DateTime.TryParse(expected, out var parsed))
            {
                Assert.Equal(parsed, actual);
            }
            else
            {
                Assert.Null(actual);
            }
        }
    }
}
