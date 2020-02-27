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
        internal const string FileChangeFrequencyDataFilename = "FileChangeFrequencyData.json";
        internal const string FileMetricOverTimeDataFilename = "FileMetricOverTimeData.json";
        internal const string FileMetricOverFileChangeFrequencyFileName = "FileMetricOverFileChangeFrequencyData.json";

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
            Write(GetFileNameInDateWareHouseProject(GitDataFileName), json);
        }

        public void WriteFileChangeFrequenciesToWareHouse(List<FileChangeFrequency> fileChangeFrequencies)
        {
            string json = JsonSerializer.Serialize(fileChangeFrequencies);
            Write(GetFileNameInDateWareHouseProject(FileChangeFrequencyDataFilename), json);
        }

        public void WriteFileMetricOverTimeToWareHouse(List<FileMetricOverTime> fileMetricsOverTime)
        {
            string json = JsonSerializer.Serialize(fileMetricsOverTime);
            Write(GetFileNameInDateWareHouseProject(FileMetricOverTimeDataFilename), json);
        }

        public void WriteFileMetricOverFileChangeFrequencyToWareHouse(List<FileMetricOverFileChangeFrequency> fileMetricOverFileChangeFrequencies)
        {
            string json = JsonSerializer.Serialize(fileMetricOverFileChangeFrequencies);
            Write(GetFileNameInDateWareHouseProject(FileMetricOverFileChangeFrequencyFileName), json);
        }

        private string GetFileNameInDateWareHouseProject(string fileName)
        {
            return Path.Combine(GetProjectFolderPath(), fileName);
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
