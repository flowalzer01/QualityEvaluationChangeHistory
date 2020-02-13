using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace QualityEvaluationChangeHistory.BusinessLogic.Data
{
    public class GitDataFromFileProvider : IGitDataProvider
    {
        private string _gitDataPath;

        public GitDataFromFileProvider(string gitDataPath)
        {
            _gitDataPath = gitDataPath;
        }

        public List<GitCommit> GetCommits()
        {
            string jsonString = File.ReadAllText(_gitDataPath);
            return JsonSerializer.Deserialize<List<GitCommit>>(jsonString);
        }
    }
}
