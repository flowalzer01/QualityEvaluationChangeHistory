using QualityEvaluationChangeHistory.BusinessLogic.Evaluation.Interface;
using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace QualityEvaluationChangeHistory.BusinessLogic.WareHouse.Evaluation
{
    public class FileMetricOverFileChangeFrequencyWareHouse : IFileMetricOverFileChangeFrequencyEvaluator
    {
        private readonly string _wareHouseProjectPath;

        public FileMetricOverFileChangeFrequencyWareHouse(string wareHouseProjectPath)
        {
            _wareHouseProjectPath = wareHouseProjectPath;
        }

        public List<FileMetricOverFileChangeFrequency> GetFileMetricOverFileChangeFrequencies(List<FileChangeFrequency> fileChangeFrequencies, List<FileMetric> fileMetrics)
        {
            string jsonString = File.ReadAllText(Path.Combine(_wareHouseProjectPath, WareHouseWriter.FileMetricOverFileChangeFrequencyFileName));
            return JsonSerializer.Deserialize<List<FileMetricOverFileChangeFrequency>>(jsonString);
        }
    }
}
