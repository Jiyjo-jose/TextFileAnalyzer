using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileAnalyzer
{
    public class AnalysisReport
    {
        public string FileName { get; set; }
        public DateTime AnalysisDate { get; set; }
        public TextAnalysis Statistics { get; set; }
        public TimeSpan ProcessingTime { get; set; }
    }
}
