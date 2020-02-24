using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace QualityEvaluationChangeHistory.BusinessLogic.WareHouse
{
    public class WareHouseWriter
    {
        internal const string GitDataFileName = "GitData.json";

        private string _wareHousePath;
        private string _wareHouseProjectName;

        public WareHouseWriter(string wareHousePath, string wareHouseProjectName)
        {
            _wareHousePath = wareHousePath;
            _wareHouseProjectName = wareHouseProjectName;
        }

        public void CreateWareHouseRootFolderIfNeeded()
        {
            if (!Directory.Exists(_wareHousePath))
                Directory.CreateDirectory(_wareHousePath);
        }

        public void CreateWareHouseProjectFolderIfNeeded()
        {
            if (!Directory.Exists(GetProjectFolderPath()))
                Directory.CreateDirectory(GetProjectFolderPath());
        }

        public void WriteCommitsToWareHouse(List<GitCommit> gitCommits)
        {
            string json = JsonSerializer.Serialize(gitCommits);
            Write(GetFileNameInDateWareHouseProject(), json);
        }

        private string GetFileNameInDateWareHouseProject()
        {
            return Path.Combine(GetProjectFolderPath(), GitDataFileName);
        }

        private void Write(string fileName, string fileContent)
        {
            File.WriteAllText(fileName, fileContent);
        }

        private string GetProjectFolderPath()
        {
            return Path.Combine(_wareHousePath, _wareHouseProjectName);
        }
    }
}
