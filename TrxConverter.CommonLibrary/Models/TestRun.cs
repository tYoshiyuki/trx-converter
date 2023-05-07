using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace TrxConverter.CommonLibrary.Models
{
    [ExcludeFromCodeCoverage]
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

    [ExcludeFromCodeCoverage]
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

        [XmlAttribute("finish")]
        public DateTime Finish { get; set; }
    }

    [ExcludeFromCodeCoverage]
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

    [ExcludeFromCodeCoverage]
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
    public class Deployment
    {
        [XmlAttribute("runDeploymentRoot")]
        public string RunDeploymentRoot { get; set; }
    }

    [ExcludeFromCodeCoverage]
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

    [ExcludeFromCodeCoverage]
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class Output
    {
        public string StdOut { get; set; }
        public ErrorInfo ErrorInfo { get; set; }
    }

    [ExcludeFromCodeCoverage]
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class ErrorInfo
    {
        public string Message { get; set; }

        public string StackTrace { get; set; }
    }

    /// <remarks/>
    [ExcludeFromCodeCoverage]
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class InnerUnitTestResult
    {
        public Output Output { get; set; }

        [XmlAttribute("executionId")]
        public string ExecutionId { get; set; }

        [XmlAttribute("parentExecutionId")]
        public string ParentExecutionId { get; set; }

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

        [XmlAttribute("dataRowInfo")]
        public int DataRowInfo { get; set; }

        [XmlAttribute("resultType")]
        public string ResultType { get; set; }
    }

    [ExcludeFromCodeCoverage]
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
    [ExcludeFromCodeCoverage]
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class TestCategoryItem
    {
        [XmlAttribute]
        public string TestCategory { get; set; }
    }

    [ExcludeFromCodeCoverage]
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class Execution
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
    }

    [ExcludeFromCodeCoverage]
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

    [ExcludeFromCodeCoverage]
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
    [ExcludeFromCodeCoverage]
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class TestList
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }
    }

    [ExcludeFromCodeCoverage]
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class ResultSummary
    {
        public Counters Counters { get; set; }

        [XmlAttribute("outcome")]
        public string Outcome { get; set; }
    }

    [ExcludeFromCodeCoverage]
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")]
    public class Counters
    {
        [XmlAttribute("total")]
        public int Total { get; set; }

        [XmlAttribute("executed")]
        public int Executed { get; set; }

        [XmlAttribute("passed")]
        public int Passed { get; set; }

        [XmlAttribute("failed")]
        public int Failed { get; set; }

        [XmlAttribute("error")]
        public int Error { get; set; }

        [XmlAttribute("timeout")]
        public int Timeout { get; set; }

        [XmlAttribute("aborted")]
        public int Aborted { get; set; }

        [XmlAttribute("inconclusive")]
        public int Inconclusive { get; set; }

        [XmlAttribute("passedButRunAborted")]
        public int PassedButRunAborted { get; set; }

        [XmlAttribute("notRunnable")]
        public int NotRunnable { get; set; }

        [XmlAttribute("notExecuted")]
        public int NotExecuted { get; set; }

        [XmlAttribute("disconnected")]
        public int Disconnected { get; set; }

        [XmlAttribute("warning")]
        public int Warning { get; set; }

        [XmlAttribute("completed")]
        public int Completed { get; set; }

        [XmlAttribute("inProgress")]
        public int InProgress { get; set; }

        [XmlAttribute("pending")]
        public int Pending { get; set; }
    }
}
