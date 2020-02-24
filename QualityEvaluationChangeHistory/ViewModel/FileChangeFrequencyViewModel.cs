using GalaSoft.MvvmLight;
using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QualityEvaluationChangeHistory.ViewModel
{
    public class FileChangeFrequencyViewModel : ViewModelBase
    {
        public FileChangeFrequencyViewModel(List<FileChangeFrequency> fileChangeFrequencies)
        {
            FileChangeFrequencies = fileChangeFrequencies;
            FileChangeFrequencyColumnChartViewModel = new FileChangeFrequencyColumnChartViewModel(FileChangeFrequencies);

            RaisePropertyChanged(nameof(FileChangeFrequencies));
            RaisePropertyChanged(nameof(FileChangeFrequencyColumnChartViewModel));
        }

        public List<FileChangeFrequency> FileChangeFrequencies { get; }
        public FileChangeFrequencyColumnChartViewModel FileChangeFrequencyColumnChartViewModel { get; }
    }
}
