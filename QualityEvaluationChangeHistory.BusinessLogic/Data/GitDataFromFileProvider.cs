using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;

namespace QualityEvaluationChangeHistory.BusinessLogic.Data
{
    public class GitDataFromFileProvider : IGitDataProvider
    {
        private string gitDataPath;

        public GitDataFromFileProvider(string gitDataPath)
        {
            this.gitDataPath = gitDataPath;
        }

        public List<GitCommit> GetCommits()
        {
            throw new NotImplementedException();
        }
    }
}
