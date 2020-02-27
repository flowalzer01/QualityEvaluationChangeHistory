using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace QualityEvaluationChangeHistory.Model.Model
{
    [DataContract]
    public class FileMetricForCommit
    {
        public FileMetricForCommit()
        {

        }
        public FileMetricForCommit(string sHA, DateTime time, double maintainabilityIndex, int cyclomaticComplexity, int linesOfCode)
        {
            SHA = sHA;
            Time = time;
            MaintainabilityIndex = maintainabilityIndex;
            CyclomaticComplexity = cyclomaticComplexity;
            LinesOfCode = linesOfCode;
        }

        [DataMember]
        public string SHA { get; set; }

        [DataMember]
        public DateTime Time { get; set; }

        [DataMember]
        public double MaintainabilityIndex { get; set; }

        [DataMember]
        public int CyclomaticComplexity { get; set; }

        [DataMember]
        public int LinesOfCode { get; set; }
    }
}
