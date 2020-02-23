using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace QualityEvaluationChangeHistory.BusinessLogic.Data
{
    public class FileMetricForSolutionProvider
    {
        public List<FileMetric> GetFileMetrics(string path)
        {
            string jsonString = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<FileMetric>>(jsonString);
        }
    }
}
