# trx-converter
Visual Studio テスト結果ファイル (.trx) を CSVファイルに変換するツール

# Usage
- .trxファイルをCSVファイルに変換します。また、テスト結果が失敗だったテストケースを対象に、プレイリストファイル (.playlist) に出力します。
- ファイル名は ワイルドカード (*) を指定可能です。 

```
TrxConverter.ConsoleApp.exe <変換対象.trxファイルパス>
```

# Description
## .trx と CSV のマッピングルール
.trx と CSV のマッピングルールは下記の通りです。

|  .trx file  |  CSV  |
| ---- | ---- |
| UnitTest > TestMethod > className | TestClassName |
| UnitTest > TestMethod > className (クラス名部分)  | TestShortClassName |
| UnitTestResult > testName | TestCaseName |
| UnitTest > TestCategoryItem > TestCategory | TestCategory |
| UnitTestResult > outcome | OutCome |
| UnitTestResult > duration | Duration |
| UnitTestResult > startTime | StartTime |
| UnitTestResult > endTime | EndTime |
| UnitTestResult > Output > ErrorInfo > Message | ErrorMessage |
| UnitTestResult > Output > ErrorInfo > StackTrace | StackTrace |
| UnitTestResult > Output > StdOut | StdOut |
| UnitTestResult > InnerResults > UnitTestResult > testName | ParameterTestCaseName |
| UnitTestResult > InnerResults > UnitTestResult > outcome | ParameterTestOutCome |
| UnitTestResult > InnerResults > UnitTestResult > duration | ParameterTestDuration |
| UnitTestResult > InnerResults > UnitTestResult > startTime | ParameterTestStartTime |
| UnitTestResult > InnerResults > UnitTestResult > endTime | ParameterTestEndTime |
| UnitTestResult > InnerResults > UnitTestResult > Output > ErrorInfo > Message | ParameterTestErrorMessage |
| UnitTestResult > InnerResults > UnitTestResult > Output > ErrorInfo > StackTrace | ParameterTestStackTrace |
| UnitTestResult > InnerResults > UnitTestResult > Output > StdOut | ParameterStdOut |

## .playlistのファイル形式

```xml
<Playlist Version="1.0">
  <Add Test="{TestClassName}.{TestCaseName(※パラメータ部分を除外した名称)}" />
  <!-- ・・・以下、失敗したテストケースを出力します。 -->
</Playlist>
```

# Sample Site
- trx-converter を REST API に組込んだサンプルサイトは[こちら](https://trx-converter-api-frhkeegua3cacccw.japaneast-01.azurewebsites.net/swagger/index.html) です。
  - 詳細は TrxConverter.Api をご覧下さい。