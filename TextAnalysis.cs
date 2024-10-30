using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileAnalyzer
{
    public class TextAnalysis
    {
        public int TotalCharacters { get; set; }
        public int AlphabeticCharacters { get; set; }
        public int NumericCharacters { get; set; }
        public int Paragraphs { get; set; }
        public int Sentences { get; set; }
        public int Words { get; set; }
        public Dictionary<string, int> WordFrequency { get; set; }
        public double AverageWordLength { get; set; }
        public double AverageSentenceLength { get; set; }
        // I added this property in the provided data structure for the top words in the file
        public Dictionary<string, int> TopWords { get; set; }
    }
}
