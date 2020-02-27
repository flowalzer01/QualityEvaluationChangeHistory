using QualityEvaluationChangeHistory.BusinessLogic.Evaluation.Interface;
using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QualityEvaluationChangeHistory.BusinessLogic.Evaluation
{
    public class FileMetricOverFileChangeFrequencyEvaluator : IFileMetricOverFileChangeFrequencyEvaluator
    {
        public List<FileMetricOverFileChangeFrequency> GetFileMetricOverFileChangeFrequencies(List<FileChangeFrequency> fileChangeFrequencies, List<FileMetric> fileMetrics)
        {
            List<FileMetricOverFileChangeFrequency> fileMetricOverFileChangeFrequencies = new List<FileMetricOverFileChangeFrequency>();

            foreach (FileChangeFrequency fileChangeFrequency in fileChangeFrequencies)
            {
                FileMetric fileMetric = GetFileMetric(fileChangeFrequency.FilePath, fileMetrics);

                //Metric can be null, because the request file from git, could no longer exist in the current version of the repo
                if (fileMetric == null)
                    fileMetric = FileMetric.Empty;

                FileMetricOverFileChangeFrequency fileMetricOverFileChangeFrequency =
                    new FileMetricOverFileChangeFrequency(fileChangeFrequency, fileMetric);

                fileMetricOverFileChangeFrequencies.Add(fileMetricOverFileChangeFrequency);
            }

            return fileMetricOverFileChangeFrequencies;
        }

        private FileMetric GetFileMetric(string filePath, List<FileMetric> fileMetrics)
        {
            string escapedFilePath = filePath.Replace("/", @"\");

            return fileMetrics
                .Where(x => x.FilePath.EndsWith(escapedFilePath))
                .SingleOrDefault();
        }
    }
}
