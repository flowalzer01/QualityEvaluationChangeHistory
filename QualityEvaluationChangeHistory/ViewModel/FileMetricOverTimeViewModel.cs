using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.ViewModel
{
    public class FileMetricOverTimeViewModel : ViewModelBase
    {
        public FileMetricOverTimeViewModel(List<Model.Model.FileMetricOverTime> fileMetricsOverTime)
        {
            FileMetricOverTimeChartViewModel = new FileMetricOverTimeChartViewModel(fileMetricsOverTime);

            RaisePropertyChanged(nameof(FileMetricOverTimeChartViewModel));
        }

        public FileMetricOverTimeChartViewModel FileMetricOverTimeChartViewModel { get; }
    }
}
