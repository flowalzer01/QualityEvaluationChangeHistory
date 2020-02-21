using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.Model.Model
{
    public class FileMetricOverTime
    {
        public FileMetricOverTime(string filePath)
        {
            FilePath = filePath;
            FileMetricForCommits = new List<FileMetricForCommit>();
        }

        public string FilePath { get; set; }
        public List<FileMetricForCommit> FileMetricForCommits { get; set; }
    }
}

