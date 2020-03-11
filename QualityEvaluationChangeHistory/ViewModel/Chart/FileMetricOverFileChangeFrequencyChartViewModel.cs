using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.ViewModel
{
    public class FileMetricOverFileChangeFrequencyChartViewModel : ViewModelBase
    {
        public SeriesCollection SeriesCollection { get; set; }

        public FileMetricOverFileChangeFrequencyChartViewModel(List<FileMetricOverFileChangeFrequency> fileMetricOverFileChangeFrequencies)
        {
            SeriesCollection = new SeriesCollection();

            foreach (var fileMetricOverFileChangeFrequency in fileMetricOverFileChangeFrequencies)
            {
                ScatterSeries scatterSeries = new ScatterSeries()
                {
                    Title = GetTitle(fileMetricOverFileChangeFrequency),
                    Values = new ChartValues<ObservablePoint>()
                    {
                        new ObservablePoint(fileMetricOverFileChangeFrequency.FileChangeFrequency.FileChanges,
                            fileMetricOverFileChangeFrequency.FileMetric.CyclomaticComplexity),
                    }
                };

                SeriesCollection.Add(scatterSeries);
            }

            RaisePropertyChanged(nameof(SeriesCollection));
        }

        private static string GetTitle(FileMetricOverFileChangeFrequency fileMetricOverFileChangeFrequency)
        {
            string fileMetricClassName = fileMetricOverFileChangeFrequency.FileMetric.Name;
            if (string.IsNullOrEmpty(fileMetricClassName))
                fileMetricClassName = "Not found";

            return $"{fileMetricOverFileChangeFrequency.FileChangeFrequency.FilePath},{Environment.NewLine}{fileMetricClassName}";
        }
    }
}
