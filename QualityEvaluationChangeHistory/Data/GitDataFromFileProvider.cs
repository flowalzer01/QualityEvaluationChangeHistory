using System;
using System.Collections.Generic;
using System.Text;
using QualityEvaluationChangeHistory.Model;

namespace QualityEvaluationChangeHistory.Data
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
