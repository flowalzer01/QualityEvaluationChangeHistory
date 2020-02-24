using QualityEvaluationChangeHistory.BusinessLogic.Data.Interface;
using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace QualityEvaluationChangeHistory.BusinessLogic.WareHouse.Data
{
    public class GitDataFromWareHouseProvider : IGitDataProvider
    {
        private string _wareHouseProjectPath;

        public GitDataFromWareHouseProvider(string wareHouseProjectPath)
        {
            _wareHouseProjectPath = wareHouseProjectPath;
        }

        public List<GitCommit> GetCommits()
        {
            string jsonString = File.ReadAllText(Path.Combine(_wareHouseProjectPath, WareHouseWriter.GitDataFileName));
            return JsonSerializer.Deserialize<List<GitCommit>>(jsonString);
        }
    }
}
