using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.BusinessLogic.Evaluation.Interface
{
    public interface IFileMetricOverFileChangeFrequencyEvaluator
    {
        List<FileMetricOverFileChangeFrequency> GetFileMetricOverFileChangeFrequencies(List<FileChangeFrequency> fileChangeFrequencies, List<FileMetric> fileMetrics);
    }
}
