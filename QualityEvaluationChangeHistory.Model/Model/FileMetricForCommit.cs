using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.Model.Model
{
    public class FileMetricForCommit
    {
        public FileMetricForCommit(string sHA, DateTime time, double maintainabilityIndex, int cyclomaticComplexity, int linesOfCode)
        {
            SHA = sHA;
            Time = time;
            MaintainabilityIndex = maintainabilityIndex;
            CyclomaticComplexity = cyclomaticComplexity;
            LinesOfCode = linesOfCode;
        }

        public string SHA { get; set; }
        public DateTime Time { get; set; }
        public double MaintainabilityIndex { get; set; }
        public int CyclomaticComplexity { get; set; }
        public int LinesOfCode { get; set; }
    }
}
