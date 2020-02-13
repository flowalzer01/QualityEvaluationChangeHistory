using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.Data
{
    public class GitDataProviderFactory
    {
        private readonly string _repositoryPath;
        private readonly string _gitDataPath;

        public GitDataProviderFactory(string repositoryPath, string gitDataPath)
        {
            _repositoryPath = repositoryPath;
            _gitDataPath = gitDataPath;
        }

        public IGitDataProvider GetGitDataProvider(bool fromRepository)
        {
            if (fromRepository)
                return new GitDataRepositoryProvider(_repositoryPath);
            else
                return new GitDataFromFileProvider(_gitDataPath);
        }
    }
}
