using System;
using System.Collections.Generic;
using System.Linq;
using TrxConverter.CommonLibrary.Models;

namespace TrxConverter.CommonLibrary.Logic
{
    public static class ConvertLogic
    {
        public static List<TestReportLine> Convert(TestRun testRun)
        {
            var report = new List<TestReportLine>();
            foreach (var result in testRun.Results ?? Enumerable.Empty<UnitTestResult>())
            {
                var definition = testRun.TestDefinitions?.FirstOrDefault(_ => _.Id == result.TestId);

                var line = new TestReportLine
                {
                    TestClassName = definition?.TestMethod?.ClassName,
                    TestShortClassName = definition?.TestMethod?.ClassName?.Split('.').LastOrDefault(),
                    TestCaseName = result.TestName,
                    TestCategory = string.Join(",", definition?.TestCategory?.Select(_ => _.TestCategory) ?? Enumerable.Empty<string>()),
                    OutCome = result.Outcome,
                    Duration = DateTime.TryParse(result.Duration, out var parsedDuration) ? parsedDuration : (DateTime?)null,
                    StartTime = DateTime.TryParse(result.StartTime, out var parsedStartTime) ? parsedStartTime : (DateTime?)null,
                    EndTime = DateTime.TryParse(result.EndTime, out var parsedEndTime) ? parsedEndTime : (DateTime?)null,
                    ErrorMessage = result.Output?.ErrorInfo?.Message,
                    StackTrace = result.Output?.ErrorInfo?.StackTrace,
                    StdOut = result.Output?.StdOut
                };

                if (result.InnerResults == null)
                {
                    report.Add(line);
                    continue;
                }

                foreach (var resultInner in result.InnerResults)
                {
                    var inner = line.Clone();
                    inner.ParameterTestCaseName = resultInner.TestName;
                    inner.ParameterTestOutCome = resultInner.Outcome;
                    inner.ParameterTestStartTime = DateTime.TryParse(resultInner.StartTime, out var parsedParameterStartTime) ? parsedParameterStartTime : (DateTime?)null;
                    inner.ParameterTestEndTime = DateTime.TryParse(resultInner.EndTime, out var parsedParameterEndTime) ? parsedParameterEndTime : (DateTime?)null;
                    inner.ParameterTestDuration = DateTime.TryParse(resultInner.Duration, out var parsedParameterDuration) ? parsedParameterDuration : (DateTime?)null;
                    inner.ParameterTestErrorMessage = resultInner.Output?.ErrorInfo?.Message;
                    inner.ParameterTestStackTrace = resultInner.Output?.ErrorInfo?.StackTrace;
                    inner.ParameterTestStdOut = resultInner.Output?.StdOut;
                    report.Add(inner);
                }
            }
            return report;
        }
    }
}
