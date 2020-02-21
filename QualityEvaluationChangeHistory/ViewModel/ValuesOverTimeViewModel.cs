using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using QualityEvaluationChangeHistory.Controls;
using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace QualityEvaluationChangeHistory.ViewModel
{
    public class ValuesOverTimeViewModel : ViewModelBase
    {
        public ValuesOverTimeViewModel(FileMetricOverTime fileMetricOverTime)
        {
            var dayConfig = Mappers.Xy<DateModel>()
                .X(dayModel => (double)dayModel.DateTime.Ticks / TimeSpan.FromDays(1).Ticks)
                .Y(dayModel => dayModel.Value);

            SeriesCollection = new SeriesCollection(dayConfig)
            {
                new LineSeries
                {
                    Values = new ChartValues<DateModel>(fileMetricOverTime.FileMetricForCommits.Select(x => new DateModel(x.Time, x.CyclomaticComplexity))),
                    Fill = Brushes.Transparent,
                    Title = fileMetricOverTime.FilePath
                }
            };

            Formatter = value => new DateTime((long)(value * TimeSpan.FromDays(1).Ticks)).ToString("d");

            RaisePropertyChanged(nameof(SeriesCollection));
            RaisePropertyChanged(nameof(Formatter));
        }

        public SeriesCollection SeriesCollection { get; private set; }
        public Func<double, string> Formatter { get; set; }
    }
}
