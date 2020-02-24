using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.BusinessLogic.Data.Interface
{
    public interface IGitDataProvider
    {
        List<GitCommit> GetCommits();
    }
}
