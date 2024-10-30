using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextFileAnalyzer
{
    public class TextAnalyzer
    {
        public TextAnalysis Analyze(string text)
        {
            var analysis = new TextAnalysis();
            analysis.TotalCharacters = text.Length;
            analysis.AlphabeticCharacters = text.Count(char.IsLetter);
            analysis.NumericCharacters = text.Count(char.IsDigit);
            analysis.Paragraphs = text.Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.None).Length;
            analysis.Sentences = Regex.Matches(text, @"[.!?]").Count;
            analysis.Words = Regex.Matches(text, @"\b\w+\b").Count;
            var words = Regex.Matches(text, @"\b\w+\b")
                .Cast<Match>()
                .Select(m => m.Value.ToLower())
                .ToList();
            analysis.WordFrequency = words.GroupBy(w => w)
                .ToDictionary(g => g.Key, g => g.Count());
            analysis.AverageWordLength = words.Average(w => w.Length);
            analysis.AverageSentenceLength = (double)analysis.Words / analysis.Sentences;
            analysis.TopWords = analysis.WordFrequency.OrderByDescending(kvp => kvp.Value)
                .Take(10)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return analysis;
        }
    }
}

