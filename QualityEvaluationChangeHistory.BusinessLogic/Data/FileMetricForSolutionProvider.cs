using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace QualityEvaluationChangeHistory.BusinessLogic.Data
{
    /// <summary>
    /// Always reads data from ware house, because roslyn will not work under .net core
    /// So the project in QualityEvaluationChangeHistory.RoslynFileMetric will have to be run first :/
    /// </summary>
    public class FileMetricForSolutionProvider
    {
        private readonly string _fileMetricFileName;
        private readonly string _wareHouseProjectPath;

        public FileMetricForSolutionProvider(string wareHouseProjectPath, string fileMetricFileName)
        {
            _fileMetricFileName = fileMetricFileName;
            _wareHouseProjectPath = wareHouseProjectPath;
        }

        public List<FileMetric> GetFileMetrics()
        {
            string jsonString = File.ReadAllText(Path.Combine(_wareHouseProjectPath, _fileMetricFileName));
            return JsonSerializer.Deserialize<List<FileMetric>>(jsonString);
        }
    }
}
