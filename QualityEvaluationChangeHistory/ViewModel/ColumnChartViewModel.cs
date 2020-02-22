using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Wpf;
using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QualityEvaluationChangeHistory.ViewModel
{
    public class ColumnChartViewModel : ViewModelBase
    {
        public ColumnChartViewModel(List<FileChangeFrequency> fileChangeFrequencies)
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "File Changes",
                    Values = new ChartValues<int> (fileChangeFrequencies.Select(x => x.FileChanges)),
                    ScalesYAt = 0
                },
                new ColumnSeries
                {
                    Title = "Lines changed",
                    Values = new ChartValues<int> (fileChangeFrequencies.Select(x => x.FileChanges*10)),
                    ScalesYAt = 1
                }
            };

            Labels = fileChangeFrequencies.Select(x => x.FilePath).ToArray();
            Formatter = value => value.ToString("N");

            RaisePropertyChanged(nameof(SeriesCollection));
            RaisePropertyChanged(nameof(Formatter));
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}
