using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace QualityEvaluationChangeHistory.Model.Model
{
    [DataContract]
    public class FileMetric
    {
        private bool _isEmpty;

        public FileMetric()
        {
            _isEmpty = false;
        }

        private FileMetric(bool isEmpty)
        {
            _isEmpty = isEmpty;
        }

        [DataMember]
        public string FilePath { get; set; }

        [DataMember]
        public int CyclomaticComplexity { get; set; }

        [DataMember]
        public double MaintainabilityIndex { get; set; }

        [DataMember]
        public int LinesOfCode { get; set; }

        public bool IsEmpty => _isEmpty;

        public static FileMetric Empty => new FileMetric(true);

    }
}
