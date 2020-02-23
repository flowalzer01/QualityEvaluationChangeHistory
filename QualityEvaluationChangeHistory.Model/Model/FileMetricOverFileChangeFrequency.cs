using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.Model.Model
{
    public class FileMetricOverFileChangeFrequency
    {
        public FileMetricOverFileChangeFrequency(FileChangeFrequency fileChangeFrequency, FileMetric fileMetric)
        {
            FileChangeFrequency = fileChangeFrequency;
            FileMetric = fileMetric;
        }

        public FileChangeFrequency FileChangeFrequency { get; }
        public FileMetric FileMetric { get; }
    }
}
