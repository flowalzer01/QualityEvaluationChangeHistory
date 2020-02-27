using QualityEvaluationChangeHistory.BusinessLogic.Evaluation.Interface;
using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace QualityEvaluationChangeHistory.BusinessLogic.WareHouse.Evaluation
{
    public class FileChangeFrequencyWareHouse : IFileChangeFrequencyEvaluator
    {
        private readonly string _wareHouseProjectPath;

        public FileChangeFrequencyWareHouse(string wareHouseProjectPath)
        {
            _wareHouseProjectPath = wareHouseProjectPath;
        }

        public List<FileChangeFrequency> GetFileChangeFrequencies(List<GitCommit> gitCommits)
        {
            string jsonString = File.ReadAllText(Path.Combine(_wareHouseProjectPath, WareHouseWriter.FileChangeFrequencyDataFilename));
            return JsonSerializer.Deserialize<List<FileChangeFrequency>>(jsonString);
        }
    }
}
