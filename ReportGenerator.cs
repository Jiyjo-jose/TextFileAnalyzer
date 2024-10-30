using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileAnalyzer
{
    public class ReportGenerator
    {
        public void GenerateReport(AnalysisReport report, string format = "console")
        {
            switch (format.ToLower())
            {
                case "console":
                    GenerateConsoleReport(report);
                    break;
                case "text":
                    GenerateTextReport(report);
                    break;
                case "html":
                    GenerateHtmlReport(report);
                    break;
                default:
                    throw new InvalidOperationException("Unsupported report format.");
            }
        }

        private void GenerateConsoleReport(AnalysisReport report)
        {
            Console.WriteLine(FormatReport(report));
        }

        private void GenerateTextReport(AnalysisReport report)
        {
            File.WriteAllText($"{report.FileName}_report.txt", FormatReport(report));
        }

        private void GenerateHtmlReport(AnalysisReport report)
        {
            var htmlReport = new StringBuilder();
            htmlReport.AppendLine("<html><body>");
            htmlReport.AppendLine($"<h1>Text Analysis Report</h1>");
            htmlReport.AppendLine($"<p><strong>File:</strong> {report.FileName}</p>");
            htmlReport.AppendLine($"<p><strong>Date:</strong> {report.AnalysisDate}</p>");
            htmlReport.AppendLine(
                $"<p><strong>Processing Time:</strong> {report.ProcessingTime.TotalSeconds} seconds</p>");
            htmlReport.AppendLine("<h2>Character Statistics</h2>");
            htmlReport.AppendLine($"<p>Total Characters: {report.Statistics.TotalCharacters}</p>");
            htmlReport.AppendLine($"<p>Alphabetic: {report.Statistics.AlphabeticCharacters}</p>");
            htmlReport.AppendLine($"<p>Numeric: {report.Statistics.NumericCharacters}</p>");
            htmlReport.AppendLine("<h2>Word Statistics</h2>");
            htmlReport.AppendLine($"<p>Total Words: {report.Statistics.Words}</p>");
            htmlReport.AppendLine($"<p>Average Word Length: {report.Statistics.AverageWordLength}</p>");
            htmlReport.AppendLine("<h2>Sentence Statistics</h2>");
            htmlReport.AppendLine($"<p>Total Sentences: {report.Statistics.Sentences}</p>");
            htmlReport.AppendLine($"<p>Average Sentence Length: {report.Statistics.AverageSentenceLength}</p>");
            htmlReport.AppendLine("<h2>Top 10 Words (excluding articles)</h2>");
            htmlReport.AppendLine("<ol>");
            foreach (var word in report.Statistics.TopWords)
            {
                htmlReport.AppendLine($"<li>{word.Key} ({word.Value} times)</li>");
            }
            htmlReport.AppendLine("</ol>");
            htmlReport.AppendLine("</body></html>");
            File.WriteAllText($"{report.FileName}_report.html", htmlReport.ToString());
        }

        private string FormatReport(AnalysisReport report)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== Text Analysis Report ===");
            sb.AppendLine($"File: {report.FileName}");
            sb.AppendLine($"Date: {report.AnalysisDate}");
            sb.AppendLine($"Processing Time: {report.ProcessingTime.TotalSeconds} seconds");
            sb.AppendLine();
            sb.AppendLine("1. Character Statistics");
            sb.AppendLine($"   - Total Characters: {report.Statistics.TotalCharacters}");
            sb.AppendLine($"   - Alphabetic: {report.Statistics.AlphabeticCharacters}");
            sb.AppendLine($"   - Numeric: {report.Statistics.NumericCharacters}");
            sb.AppendLine();
            sb.AppendLine("2. Word Statistics");
            sb.AppendLine($"   - Total Words: {report.Statistics.Words}");
            sb.AppendLine($"   - Average Word Length: {report.Statistics.AverageWordLength}");
            sb.AppendLine();
            sb.AppendLine("3. Sentence Statistics");
            sb.AppendLine($"   - Total Sentences: {report.Statistics.Sentences}");
            sb.AppendLine($"   - Average Sentence Length: {report.Statistics.AverageSentenceLength}");
            sb.AppendLine();
            sb.AppendLine("4. Top 10 Words (excluding articles)");
            int rank = 1;
            foreach (var word in report.Statistics.TopWords)
            {
                sb.AppendLine($"   {rank}. \"{word.Key}\" ({word.Value} times)");
                rank++;
            }
            return sb.ToString();
        }
    }
}
