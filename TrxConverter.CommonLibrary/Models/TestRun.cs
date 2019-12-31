using System;
using System.Xml.Serialization;

namespace TrxConverter.CommonLibrary.Models
{
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    [XmlRoot(Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010", IsNullable = false)]
    public class TestRun
    {
        public TestRunTimes Times { get; set; }

        public TestSettings TestSettings { get; set; }

        [XmlArrayItem("UnitTestResult", IsNullable = false)]
        public UnitTestResult[] Results { get; set; }

        [XmlArrayItem("UnitTest", IsNullable = false)]
        public UnitTest[] TestDefinitions { get; set; }

        [XmlArrayItem("TestEntry", IsNullable = false)]
        public TestEntry[] TestEntries { get; set; }

        [XmlArrayItem("TestList", IsNullable = false)]
        public TestList[] TestLists { get; set; }

        public ResultSummary ResultSummary { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("runUser")]
        public string RunUser { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class TestRunTimes
    {
        [XmlAttribute("creation")]
        public DateTime Creation { get; set; }

        [XmlAttribute("queuing")]
        public DateTime Queuing { get; set; }

        [XmlAttribute("start")]
        public DateTime Start { get; set; }

        /// <remarks/>
        [XmlAttribute("finish")]
        public DateTime Finish { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class TestSettings
    {
        public Deployment Deployment { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class Deployment
    {
        [XmlAttribute("runDeploymentRoot")]
        public string RunDeploymentRoot { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class UnitTestResult
    {
        public Output Output { get; set; }

        [XmlArrayItem("UnitTestResult", IsNullable = false)]
        public InnerUnitTestResult[] InnerResults { get; set; }

        [XmlAttribute("executionId")]
        public string ExecutionId { get; set; }

        [XmlAttribute("testId")]
        public string TestId { get; set; }

        [XmlAttribute("testName")]
        public string TestName { get; set; }

        [XmlAttribute("computerName")]
        public string ComputerName { get; set; }

        [XmlAttribute("duration")]
        public string Duration { get; set; }

        [XmlAttribute("startTime")]
        public string StartTime { get; set; }

        [XmlAttribute("endTime")]
        public string EndTime { get; set; }

        [XmlAttribute("testType")]
        public string TestType { get; set; }

        [XmlAttribute("outcome")]
        public string Outcome { get; set; }

        [XmlAttribute("testListId")]
        public string TestListId { get; set; }

        [XmlAttribute("relativeResultsDirectory")]
        public string RelativeResultsDirectory { get; set; }

        [XmlAttribute("resultType")]
        public string ResultType { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class Output
    {
        public ErrorInfo ErrorInfo { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class ErrorInfo
    {
        public string Message { get; set; }

        public string StackTrace { get; set; }
    }

    /// <remarks/>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class InnerUnitTestResult
    {
        public Output Output { get; set; }

        /// <remarks/>
        [XmlAttribute("executionId")]
        public string ExecutionId { get; set; }

        /// <remarks/>
        [XmlAttribute("parentExecutionId")]
        public string ParentExecutionId { get; set; }

        /// <remarks/>
        [XmlAttribute("testId")]
        public string TestId { get; set; }

        /// <remarks/>
        [XmlAttribute("testName")]
        public string TestName { get; set; }

        /// <remarks/>
        [XmlAttribute("computerName")]
        public string ComputerName { get; set; }

        /// <remarks/>
        [XmlAttribute("duration")]
        public string Duration { get; set; }

        /// <remarks/>
        [XmlAttribute("startTime")]
        public string StartTime { get; set; }

        /// <remarks/>
        [XmlAttribute("endTime")]
        public string EndTime { get; set; }

        /// <remarks/>
        [XmlAttribute("testType")]
        public string TestType { get; set; }

        /// <remarks/>
        [XmlAttribute("outcome")]
        public string Outcome { get; set; }

        /// <remarks/>
        [XmlAttribute("testListId")]
        public string TestListId { get; set; }

        /// <remarks/>
        [XmlAttribute("relativeResultsDirectory")]
        public string RelativeResultsDirectory { get; set; }

        /// <remarks/>
        [XmlAttribute("dataRowInfo")]
        public byte DataRowInfo { get; set; }

        /// <remarks/>
        [XmlAttribute("resultType")]
        public string ResultType { get; set; }
    }

    /// <remarks/>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class UnitTest
    {
        [XmlArrayItem("TestCategoryItem", IsNullable = false)]
        public TestCategoryItem[] TestCategory { get; set; }

        public Execution Execution { get; set; }

        public TestMethod TestMethod { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("storage")]
        public string Storage { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }
    }

    /// <remarks/>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class TestCategoryItem
    {
        [XmlAttribute]
        public string TestCategory { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class Execution
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class TestMethod
    {
        [XmlAttribute("codeBase")]
        public string CodeBase { get; set; }

        [XmlAttribute("adapterTypeName")]
        public string AdapterTypeName { get; set; }

        [XmlAttribute("className")]
        public string ClassName { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class TestEntry
    {
        [XmlAttribute("testId")]
        public string TestId { get; set; }

        [XmlAttribute("executionId")]
        public string ExecutionId { get; set; }

        [XmlAttribute("testListId")]
        public string TestListId { get; set; }
    }

    /// <remarks/>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class TestList
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class ResultSummary
    {
        public Counters Counters { get; set; }

        [XmlAttribute("outcome")]
        public string Outcome { get; set; }
    }

    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class Counters
    {
        [XmlAttribute("total")]
        public byte Total { get; set; }

        [XmlAttribute("executed")]
        public byte Executed { get; set; }

        [XmlAttribute("passed")]
        public byte Passed { get; set; }

        [XmlAttribute("failed")]
        public byte Failed { get; set; }

        [XmlAttribute("error")]
        public byte Error { get; set; }

        [XmlAttribute("timeout")]
        public byte Timeout { get; set; }

        [XmlAttribute("aborted")]
        public byte Aborted { get; set; }

        [XmlAttribute("inconclusive")]
        public byte Inconclusive { get; set; }

        [XmlAttribute("passedButRunAborted")]
        public byte PassedButRunAborted { get; set; }

        [XmlAttribute("notRunnable")]
        public byte NotRunnable { get; set; }

        [XmlAttribute("notExecuted")]
        public byte NotExecuted { get; set; }

        [XmlAttribute("disconnected")]
        public byte Disconnected { get; set; }

        [XmlAttribute("warning")]
        public byte Warning { get; set; }

        [XmlAttribute("completed")]
        public byte Completed { get; set; }

        [XmlAttribute("inProgress")]
        public byte InProgress { get; set; }

        [XmlAttribute("pending")]
        public byte Pending { get; set; }
    }
}
