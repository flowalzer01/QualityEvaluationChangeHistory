using GalaSoft.MvvmLight;
using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.ViewModel
{
    public class FileMetricOverFileChangeFrequencyViewModel : ViewModelBase
    {
        public FileMetricOverFileChangeFrequencyViewModel(List<FileMetricOverFileChangeFrequency> fileMetricOverFileChangeFrequencies)
        {
            FileMetricOverFileChangeFrequency = fileMetricOverFileChangeFrequencies;
            FileMetricOverFileChangeFrequencyChartViewModel = new FileMetricOverFileChangeFrequencyChartViewModel(fileMetricOverFileChangeFrequencies);

            RaisePropertyChanged(nameof(FileMetricOverFileChangeFrequencyChartViewModel));
        }

        public List<FileMetricOverFileChangeFrequency> FileMetricOverFileChangeFrequency { get; }

        public FileMetricOverFileChangeFrequencyChartViewModel FileMetricOverFileChangeFrequencyChartViewModel { get; }
    }
}
