// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using TextFileAnalyzer;

namespace TextFileAnalyzer
{
    class Program
    {
        static string selectedFilePath;
        static AnalysisReport lastReport;
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("=== Text File Analyzer ===");
                Console.WriteLine("1. Select File");
                Console.WriteLine("2. Analyze File");
                Console.WriteLine("3. Generate Report");
                Console.WriteLine("4. View Previous Analysis");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice (1-5): ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        SelectFile();
                        break;
                    case "2":
                        AnalyzeFile();
                        break;
                    case "3":
                        GenerateReport();
                        break;
                    case "4":
                        ViewPreviousAnalysis();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void SelectFile()
        {
            Console.Write("Enter file path or drag file here: ");
            try
            {
                selectedFilePath = Console.ReadLine();
                var fileInfo = new FileInfo(selectedFilePath);
                Console.WriteLine($"File selected: {fileInfo.Name} (size: {fileInfo.Length / 1024.0:F2} KB)");
                while (true)
                {
                    Console.WriteLine("1. Start Analysis");
                    Console.WriteLine("2. Select Different File");
                    Console.WriteLine("3. Return to Main Menu");
                    Console.Write("Choice: ");
                    var choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            AnalyzeFile();
                            return;
                        case "2":
                            SelectFile();
                            return;
                        case "3":
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: File not found.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: Permission denied.");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("Error: File path is too long.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
        static void AnalyzeFile()
        {
            if (string.IsNullOrEmpty(selectedFilePath))
            {
                Console.WriteLine("No file selected. Please select a file first.");
                return;
            }
            try
            {
                var processor = new FileProcessor(selectedFilePath);
                var analyzer = new TextAnalyzer();
                var stopwatch = Stopwatch.StartNew();
                var content = processor.ReadFile(progress =>
                    Console.Write($"\r[{"=".PadRight(progress / 5, '=')}] {progress}%"));
                var analysis = analyzer.Analyze(content);
                stopwatch.Stop();
                lastReport = new AnalysisReport
                {
                    FileName = Path.GetFileName(selectedFilePath),
                    AnalysisDate = DateTime.Now,
                    Statistics = analysis,
                    ProcessingTime = stopwatch.Elapsed
                };
                Console.WriteLine($"\nAnalysis complete in {stopwatch.Elapsed.TotalSeconds:F2} seconds.");
                Console.WriteLine("Basic Statistics:");
                Console.WriteLine($"- Characters: {analysis.TotalCharacters}");
                Console.WriteLine($"- Words: {analysis.Words}");
                Console.WriteLine($"- Sentences: {analysis.Sentences}");
                Console.WriteLine($"- Paragraphs: {analysis.Paragraphs}");
                GenerateReport();
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("Error: Out of memory.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
        static void GenerateReport()
        {
            if (lastReport == null)
            {
                Console.WriteLine("No analysis available. Please analyze a file first.");
                return;
            }
            Console.Write("Generate detailed report? (Y/N): ");
            var choice = Console.ReadLine();
            if (choice?.ToLower() == "y")
            {
                try
                {
                    var generator = new ReportGenerator();
                    generator.GenerateReport(lastReport, "console");
                    generator.GenerateReport(lastReport, "text");
                    generator.GenerateReport(lastReport, "html");
                    Console.WriteLine("Reports generated.");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }
        static void ViewPreviousAnalysis()
        {
            if (lastReport == null)
            {
                Console.WriteLine("No previous analysis available.");
                return;
            }
            var generator = new ReportGenerator();
            generator.GenerateReport(lastReport, "console");
        }
    }
}



