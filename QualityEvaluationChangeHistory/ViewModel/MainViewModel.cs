using GalaSoft.MvvmLight;
using QualityEvaluationChangeHistory.BusinessLogic.Data;
using QualityEvaluationChangeHistory.BusinessLogic.Evaluation;
using QualityEvaluationChangeHistory.Controls;
using QualityEvaluationChangeHistory.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualityEvaluationChangeHistory.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private const bool DataFromRepository = true;
        //private const string RepositoryPath = @"C:\Users\walzeflo\source\repos\SAPApps\SapApps_Dsp";
        private const string RepositoryPath = @"C:\Users\walzeflo\source\repos\heidelpayDotNET";
        private const string GitDataPath = @"C:\Users\walzeflo\source\repos\BachelorArbeit\DSP\repo.txt";

        public MainViewModel()
        {

        }

        public List<FileMetricOverTime> FileMetricsOverTime { get; private set; }
        public ValuesOverTimeViewModel ValuesOverTimeViewModel { get; private set; }
        public ColumnChartViewModel ColumnChartViewModel { get; private set; }

        public List<FileChangeFrequency> FileChangeFrequencies { get; private set; }
        public List<GitCommit> GitCommits { get; private set; }

        internal async Task Init()
        {
            using (LoadingRing loadingRing = LoadingRing.Show("Laden"))
            {
                GitCommits = await Task.Run(() => GetCommits());
                FileChangeFrequencies = await Task.Run(() => CalculateFileChangeFrequency());
                FileMetricsOverTime = await Task.Run(() => CalculateFileMetricsOverTime());
            }

            ValuesOverTimeViewModel = new ValuesOverTimeViewModel(FileMetricsOverTime);
            ColumnChartViewModel = new ColumnChartViewModel(FileChangeFrequencies);
            CalculateFileCoupling();

            WriteCommitsToFileIfNeeded(GitCommits);

            RefreshUi();
        }

        private void RefreshUi()
        {
            RaisePropertyChanged(nameof(ValuesOverTimeViewModel));
            RaisePropertyChanged(nameof(ColumnChartViewModel));
            RaisePropertyChanged(nameof(FileChangeFrequencies));
        }

        private List<FileMetricOverTime> CalculateFileMetricsOverTime()
        {
            FileMetricOverTimeEvaluator fileMetricOverTimeEvaluator = new FileMetricOverTimeEvaluator();
            List<FileMetricOverTime> fileMetricsOverTime = fileMetricOverTimeEvaluator
                .GetFileMetricOverTime(GitCommits, FileChangeFrequencies.Take(5).Select(x => x.FilePath));

            return fileMetricsOverTime;
        }

        private static void WriteCommitsToFileIfNeeded(List<GitCommit> gitCommits)
        {
            if (DataFromRepository)
            {
                GitDataToFileSaver gisDataToFileSaver = new GitDataToFileSaver();
                gisDataToFileSaver.SaveCommitsToFile(gitCommits, GitDataPath);
            }
        }

        private void CalculateFileCoupling()
        {
            FileCouplingEvaluator fileCouplingEvaluator = new FileCouplingEvaluator(GitCommits, FileChangeFrequencies);
            List<FileCouple> fileCouples = fileCouplingEvaluator
                .CalculateFileCouples()
                .OrderByDescending(x => x.GitCommits.Count)
                .ToList();
        }

        private static List<GitCommit> GetCommits()
        {
            IGitDataProvider gitDataProvider = new GitDataProviderFactory(RepositoryPath, GitDataPath)
                .GetGitDataProvider(DataFromRepository);

            return gitDataProvider.GetCommits();
        }

        private List<FileChangeFrequency> CalculateFileChangeFrequency()
        {
            FileChangeFrequencyEvaluator fileChangeFrequencyEvaluator = new FileChangeFrequencyEvaluator();

            return fileChangeFrequencyEvaluator.GetFileChangeFrequencies(GitCommits);
        }
    }
}
