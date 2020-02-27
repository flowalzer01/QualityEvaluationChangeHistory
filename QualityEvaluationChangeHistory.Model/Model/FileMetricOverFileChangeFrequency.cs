using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace QualityEvaluationChangeHistory.Model.Model
{
    [DataContract]
    public class FileMetricOverFileChangeFrequency
    {
        public FileMetricOverFileChangeFrequency()
        {

        }

        public FileMetricOverFileChangeFrequency(FileChangeFrequency fileChangeFrequency, FileMetric fileMetric)
        {
            FileChangeFrequency = fileChangeFrequency;
            FileMetric = fileMetric;
        }

        [DataMember]
        public FileChangeFrequency FileChangeFrequency { get; set; }

        [DataMember]
        public FileMetric FileMetric { get; set; }
    }
}
