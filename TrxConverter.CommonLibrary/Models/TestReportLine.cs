using System;
using CsvHelper.Configuration;

namespace TrxConverter.CommonLibrary.Models
{
    public class TestReportLine
    {
        public string TestClassName { get; set; }

        public string TestCaseName { get; set; }

        public string TestCategory { get; set; }

        public string OutCome { get; set; }

        public DateTime? Duration { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string ErrorMessage { get; set; }

        public string StackTrace { get; set; }

        public string ParameterTestCaseName { get; set; }

        public string ParameterTestOutCome { get; set; }

        public DateTime? ParameterTestDuration { get; set; }

        public DateTime? ParameterTestStartTime { get; set; }

        public DateTime? ParameterTestEndTime { get; set; }

        public string ParameterTestErrorMessage { get; set; }

        public string ParameterTestStackTrace { get; set; }

        public TestReportLine Clone()
        {
            return (TestReportLine)MemberwiseClone();
        }
    }

    public sealed class TestReportLineMap : ClassMap<TestReportLine>
    {
        public TestReportLineMap()
        {
            AutoMap();
            Map(m => m.Duration).TypeConverterOption.Format("HH:mm:ss");
            Map(m => m.StartTime).TypeConverterOption.Format("yyyy/MM/dd HH:mm:ss");
            Map(m => m.EndTime).TypeConverterOption.Format("yyyy/MM/dd HH:mm:ss");
            Map(m => m.ParameterTestDuration).TypeConverterOption.Format("HH:mm:ss");
            Map(m => m.ParameterTestStartTime).TypeConverterOption.Format("yyyy/MM/dd HH:mm:ss");
            Map(m => m.ParameterTestEndTime).TypeConverterOption.Format("yyyy/MM/dd HH:mm:ss");

        }
    }
}
