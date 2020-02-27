using QualityEvaluationChangeHistory.BusinessLogic.Evaluation.Interface;
using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace QualityEvaluationChangeHistory.BusinessLogic.WareHouse.Evaluation
{
    public class FileMetricOverTimeWareHouse : IFileMetricOverTimeEvaluator
    {
        private readonly string _wareHouseProjectPath;

        public FileMetricOverTimeWareHouse(string wareHouseProjectPath)
        {
            _wareHouseProjectPath = wareHouseProjectPath;
        }

        public List<FileMetricOverTime> GetFileMetricOverTime(IEnumerable<GitCommit> gitCommits, IEnumerable<string> filePaths)
        {
            string jsonString = File.ReadAllText(Path.Combine(_wareHouseProjectPath, WareHouseWriter.FileMetricOverTimeDataFilename));
            return JsonSerializer.Deserialize<List<FileMetricOverTime>>(jsonString);
        }
    }
}
