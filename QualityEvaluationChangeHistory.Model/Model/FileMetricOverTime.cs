using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace QualityEvaluationChangeHistory.Model.Model
{
    [DataContract]
    public class FileMetricOverTime
    {
        public FileMetricOverTime()
        {

        }

        public FileMetricOverTime(string filePath)
        {
            FilePath = filePath;
            FileMetricForCommits = new List<FileMetricForCommit>();
        }

        [DataMember]
        public string FilePath { get; set; }

        [DataMember]
        public List<FileMetricForCommit> FileMetricForCommits { get; set; }
    }
}

