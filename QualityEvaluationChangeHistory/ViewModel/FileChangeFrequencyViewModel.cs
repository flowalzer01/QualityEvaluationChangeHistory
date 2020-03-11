using GalaSoft.MvvmLight;
using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QualityEvaluationChangeHistory.ViewModel
{
    public class FileChangeFrequencyViewModel : ViewModelBase
    {
        private readonly int _filesToShow;

        public FileChangeFrequencyViewModel(List<FileChangeFrequency> fileChangeFrequencies, int filesToShow)
        {
            _filesToShow = filesToShow;
            FileChanges = GetFileChangeFrequenciesToShow(fileChangeFrequencies);
            LinesChanged = GetLinesChangedToShow(fileChangeFrequencies);

            FileChangeFrequencyColumnChartViewModel = new FileChangeFrequencyColumnChartViewModel(FileChanges);

            RaisePropertyChanged(nameof(FileChanges));
            RaisePropertyChanged(nameof(LinesChanged));
            RaisePropertyChanged(nameof(FileChangeFrequencyColumnChartViewModel));
        }


        public List<FileChangeFrequency> FileChanges { get; }
        public List<FileChangeFrequency> LinesChanged { get; }
        public FileChangeFrequencyColumnChartViewModel FileChangeFrequencyColumnChartViewModel { get; }

        private List<FileChangeFrequency> GetFileChangeFrequenciesToShow(List<FileChangeFrequency> fileChangeFrequencies)
        {
            return fileChangeFrequencies
                .OrderByDescending(x => x.FileChanges)
                .Take(_filesToShow)
                .ToList();
        }

        private List<FileChangeFrequency> GetLinesChangedToShow(List<FileChangeFrequency> fileChangeFrequencies)
        {
            return fileChangeFrequencies
                .OrderByDescending(x => x.LinesChanged)
                .Take(_filesToShow)
                .ToList();
        }
    }
}
