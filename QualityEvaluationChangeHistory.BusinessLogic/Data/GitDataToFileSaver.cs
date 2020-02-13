using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace QualityEvaluationChangeHistory.BusinessLogic.Data
{
    public class GitDataToFileSaver
    {
        public void SaveCommitsToFile(List<GitCommit> gitCommits, string fileName)
        {
            string json = JsonSerializer.Serialize(gitCommits);
            Write(fileName, json);
        }

        private void Write(string fileName, string fileContent)
        {
            File.WriteAllText(fileName, fileContent);
        }
    }
}
