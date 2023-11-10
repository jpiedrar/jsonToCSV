public class TestSuite
{
    [JsonProperty("testKey")]
    public string TestKey { get; set; }

    [JsonProperty("start")]
    public DateTime Start { get; set; }

    [JsonProperty("finish")]
    public DateTime Finish { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("tests")]
    public List<Test> Tests { get; set; }
}