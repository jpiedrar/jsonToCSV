public class TestRun
{
    [JsonProperty("testName")]
    public string TestName { get; set; }

    [JsonProperty("testResult")]
    public string TestResult { get; set; }

    [JsonProperty("runTime")]
    public double RunTime { get; set; }
}