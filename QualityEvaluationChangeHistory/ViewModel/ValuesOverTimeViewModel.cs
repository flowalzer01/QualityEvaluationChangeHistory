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
        public ValuesOverTimeViewModel(IEnumerable<FileMetricOverTime> fileMetricsOverTime)
        {
            var dayConfig = Mappers.Xy<DateModel>()
                .X(dayModel => (double)dayModel.DateTime.Ticks / TimeSpan.FromDays(1).Ticks)
                .Y(dayModel => dayModel.Value);

            SeriesCollection = new SeriesCollection(dayConfig);
            SeriesCollection.AddRange(GetLineSeries(fileMetricsOverTime));

            Formatter = value => new DateTime((long)(value * TimeSpan.FromDays(1).Ticks)).ToString("d");

            RaisePropertyChanged(nameof(SeriesCollection));
            RaisePropertyChanged(nameof(Formatter));
        }

        public SeriesCollection SeriesCollection { get; private set; }
        public Func<double, string> Formatter { get; set; }

        private IEnumerable<LineSeries> GetLineSeries(IEnumerable<FileMetricOverTime> fileMetricsOverTime)
        {
            List<LineSeries> lineSeriesList = new List<LineSeries>();

            foreach(FileMetricOverTime fileMetricOverTime in fileMetricsOverTime)
            {
                lineSeriesList.Add(new LineSeries
                {
                    Values = new ChartValues<DateModel>(fileMetricOverTime.FileMetricForCommits.Select(x => new DateModel(x.Time, x.CyclomaticComplexity))),
                    Fill = Brushes.Transparent,
                    Title = fileMetricOverTime.FilePath
                });
            }

            return lineSeriesList;
        }
    }
}
