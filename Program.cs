using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Program
{
    public static void main()
    {
        string json = @"{
            ""tests"": [
                {
                    ""testKey"": ""Encora test"",
                    ""start"": ""2023-11-09T11:47:35+01:00"",
                    ""finish"": ""2023-11-09T11:50:56+01:00"",
                    ""comment"": """",
                    ""status"": ""PASSED"",
                    ""tests"": [
                        {
                            ""testName"": ""Login with valid credentials"",
                            ""testResult"": ""PASSED"",
                            ""runTime"": 0.0009
                        },
                        {
                            ""testName"": ""Login with invalid credentials"",
                            ""testResult"": ""PASSED"",
                            ""runTime"": 0.10
                        },
                        {
                            ""testName"": ""Username change"",
                            ""testResult"": ""PASSED"",
                            ""runTime"": 0.20
                        },
                        {
                            ""testName"": ""Profile picture change"",
                            ""testResult"": ""PASSED"",
                            ""runTime"": 0.134
                        }
                    ]
                }
            ]
        }";

        // Deserialize JSON to an object
        TestSuite testSuite = JsonConvert.DeserializeObject<TestSuite>(json);

        // Export metrics
        ExportMetrics(testSuite);
    }

    public static void ExportMetrics(TestSuite testSuite)
    {
        Console.WriteLine($"Test Key: {testSuite.TestKey}");
        Console.WriteLine($"Start Time: {testSuite.Start}");
        Console.WriteLine($"Finish Time: {testSuite.Finish}");
        Console.WriteLine($"Status: {testSuite.Status}");
        Console.WriteLine();

        foreach (var test in testSuite.Tests)
        {
            Console.WriteLine($"Test Name: {test.TestName}");
            Console.WriteLine($"Result: {test.TestResult}");
            Console.WriteLine($"Run Time: {test.RunTime} seconds");
            Console.WriteLine();
        }
    }

    public static void ExportMetricsToCSV(TestSuite testSuite, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write header
            writer.WriteLine("Test Name,Result,Run Time (seconds)");

            // Write data
            foreach (var test in testSuite.Tests)
            {
                writer.WriteLine($"{test.TestName},{test.TestResult},{test.RunTime}");
            }
        }

        Console.WriteLine($"Metrics exported to {filePath}");
    }

    public static void ExportMetricsToCsv(TestSuite testSuite, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Metric,Value");
            writer.WriteLine($"Total Number of Test Cases Executed,{CountTestCases(testSuite.Tests)}");
            writer.WriteLine($"Number of Test Cases Passed,{CountPassedTestCases(testSuite.Tests)}");
            writer.WriteLine($"Number of Test Cases Failed,{CountFailedTestCases(testSuite.Tests)}");
            writer.WriteLine($"Average Execution Time for All Test Cases,{CalculateAverageRunTime(testSuite.Tests)} seconds");
            writer.WriteLine($"Minimum Execution Time Among All Test Cases,{GetMinimumRunTime(testSuite.Tests)} seconds");
            writer.WriteLine($"Maximum Execution Time Among All Test Cases,{GetMaximumRunTime(testSuite.Tests)} seconds");
        }
    }

    public static int CountTestCases(List<Test> tests)
    {
        return tests.Count;
    }

    public static int CountPassedTestCases(List<Test> tests)
    {
        return tests.Count(test => test.TestResult == "PASSED");
    }

    public static int CountFailedTestCases(List<Test> tests)
    {
        return tests.Count(test => test.TestResult == "FAILED");
    }

    public static double CalculateAverageRunTime(List<Test> tests)
    {
        if (tests.Count == 0)
        {
            return 0;
        }

        double totalRunTime = 0;
        foreach (var test in tests)
        {
            totalRunTime += test.RunTime;
        }

        return totalRunTime / tests.Count;
    }

    public static double GetMinimumRunTime(List<Test> tests)
    {
        if (tests.Count == 0)
        {
            return 0;
        }

        return tests.Min(test => test.RunTime);
    }

    public static double GetMaximumRunTime(List<Test> tests)
    {
        if (tests.Count == 0)
        {
            return 0;
        }

        return tests.Max(test => test.RunTime);
    }
}
