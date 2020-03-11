using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QualityEvaluationChangeHistory.RoslynFileMetric.Model
{
    [DataContract]
    public class FileMetric
    {
        public FileMetric(string filePath, string name, int cyclomaticComplexity, double maintainabilityIndex, int linesOfCode)
        {
            FilePath = filePath;
            Name = name;
            CyclomaticComplexity = cyclomaticComplexity;
            MaintainabilityIndex = maintainabilityIndex;
            LinesOfCode = linesOfCode;
        }

        [DataMember]
        public string FilePath { get; }

        [DataMember]
        public string Name { get; }

        [DataMember]
        public int CyclomaticComplexity { get; }

        [DataMember]
        public double MaintainabilityIndex { get; }

        [DataMember]
        public int LinesOfCode { get; }
    }
}
