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
            foreach (var result in testRun.Results)
            {
                var definition = testRun.TestDefinitions.FirstOrDefault(_ => _.Id == result.TestId);

                var line = new TestReportLine
                {
                    TestClassName = definition?.TestMethod?.ClassName,
                    TestCaseName = result.TestName,
                    TestCategory = string.Join(",", definition?.TestCategory?.Select(_ => _.TestCategory) ?? (string[])Enumerable.Empty<string>()),
                    OutCome = result.Outcome,
                    Duration = DateTime.Parse(result.Duration),
                    StartTime = DateTime.Parse(result.StartTime),
                    EndTime = DateTime.Parse(result.EndTime),
                    ErrorMessage = result.Output?.ErrorInfo?.Message,
                    StackTrace = result.Output?.ErrorInfo?.StackTrace
                };

                var lines = new List<TestReportLine> { line };
                foreach (var resultInner in result.InnerResults ?? (InnerUnitTestResult[])Enumerable.Empty<InnerUnitTestResult>())
                {
                    var inner = line.Clone();
                    inner.ParameterTestCaseName = resultInner.TestName;
                    inner.ParameterTestOutCome = resultInner.Outcome;
                    inner.ParameterTestStartTime = DateTime.Parse(resultInner.StartTime);
                    inner.ParameterTestEndTime = DateTime.Parse(resultInner.EndTime);
                    inner.ParameterTestDuration = DateTime.Parse(resultInner.Duration);
                    inner.ParameterTestErrorMessage = resultInner.Output?.ErrorInfo?.Message;
                    inner.ParameterTestStackTrace = resultInner.Output?.ErrorInfo?.StackTrace;
                    lines.Add(inner);
                }
                report.AddRange(lines);
            }
            return report;
        }
    }
}
