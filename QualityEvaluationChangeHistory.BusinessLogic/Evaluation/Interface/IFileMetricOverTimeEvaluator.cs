using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.BusinessLogic.Evaluation.Interface
{
    public interface IFileMetricOverTimeEvaluator
    {
        List<FileMetricOverTime> GetFileMetricOverTime(IEnumerable<GitCommit> gitCommits, IEnumerable<string> filePaths);
    }
}
