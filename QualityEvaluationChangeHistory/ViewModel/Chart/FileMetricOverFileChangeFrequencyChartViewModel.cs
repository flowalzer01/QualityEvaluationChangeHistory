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
                    Title = fileMetricOverFileChangeFrequency.FileChangeFrequency.FilePath,
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
    }
}
