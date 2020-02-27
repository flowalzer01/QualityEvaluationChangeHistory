using Newtonsoft.Json;
using QualityEvaluationChangeHistory.Configuration;
using QualityEvaluationChangeHistory.RoslynFileMetric.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityEvaluationChangeHistory.RoslynFileMetric
{
    public class FileMetricsWriter
    {
        internal void PrepareWareHouse()
        {
            CreateWareHouseRootFolderIfNeeded();
            CreateWareHouseProjectFolderIfNeeded();
        }

        internal void WriteToFile(List<FileMetric> fileMetrics)
        {
            using (StreamWriter file = File.CreateText(GetFileNameInDateWareHouseProject()))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, fileMetrics);
            }
        }

        private void CreateWareHouseRootFolderIfNeeded()
        {
            if (!Directory.Exists(Constants.WareHousePath))
                Directory.CreateDirectory(Constants.WareHousePath);
        }

        private void CreateWareHouseProjectFolderIfNeeded()
        {
            if (!Directory.Exists(Constants.WareHouseProjectPath))
                Directory.CreateDirectory(Constants.WareHouseProjectPath);
        }

        private string GetFileNameInDateWareHouseProject()
        {
            return Path.Combine(Constants.WareHouseProjectPath, Constants.FileMetricFileName);
        }
    }
}
