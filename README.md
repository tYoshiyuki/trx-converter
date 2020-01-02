# trx-converter
Visual Studio テスト結果ファイル (.trx) を CSVファイルに変換するツール

# Usage
- .trxファイルをCSVファイルに変換します。
- ファイル名は ワイルドカード (*) を指定可能です。 

```
TrxConverter.ConsoleApp.exe <変換対象.trxファイルパス>
```

# Description 
.trx と CSV のマッピングルールは下記の通りです。

|  .trx file  |  CSV  |
| ---- | ---- |
| UnitTest > TestMethod > className | TestClassName |
| UnitTestResult > testName | TestCaseName |
| UnitTest > TestCategoryItem > TestCategory | TestCategory |
| UnitTestResult > outcome | OutCome |
| UnitTestResult > duration | Duration |
| UnitTestResult > startTime | StartTime |
| UnitTestResult > endTime | EndTime |
| UnitTestResult > Output > ErrorInfo > Message | ErrorMessage |
| UnitTestResult > Output > ErrorInfo > StackTrace | StackTrace |
| UnitTestResult > InnerResults > UnitTestResult > testName | ParameterTestCaseName |
| UnitTestResult > InnerResults > UnitTestResult > outcome | ParameterTestOutCome |
| UnitTestResult > InnerResults > UnitTestResult > duration | ParameterTestDuration |
| UnitTestResult > InnerResults > UnitTestResult > startTime | ParameterTestStartTime |
| UnitTestResult > InnerResults > UnitTestResult > endTime | ParameterTestEndTime |
| UnitTestResult > InnerResults > UnitTestResult > Output > ErrorInfo > Message | ParameterTestErrorMessage |
| UnitTestResult > InnerResults > UnitTestResult > Output > ErrorInfo > StackTrace | ParameterTestStackTrace |
