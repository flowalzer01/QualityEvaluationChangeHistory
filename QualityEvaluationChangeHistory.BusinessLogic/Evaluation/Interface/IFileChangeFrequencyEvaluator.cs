using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.BusinessLogic.Evaluation.Interface
{
    public interface IFileChangeFrequencyEvaluator
    {
        List<FileChangeFrequency> GetFileChangeFrequencies(List<GitCommit> gitCommits);
    }
}
