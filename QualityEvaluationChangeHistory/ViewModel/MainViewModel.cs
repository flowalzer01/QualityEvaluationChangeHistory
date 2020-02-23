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
        public List<FileMetricOverFileChangeFrequency> FileMetricOverFileChangeFrequencies { get; private set; }
        public FileMetricOverTimeChartViewModel FileMetricOverTimeChartViewModel { get; private set; }
        public FileChangeFrequencyColumnChartViewModel FileChangeFrequencyColumnChartViewModel { get; private set; }
        public FileMetricOverFileChangeFrequencyViewModel FileMetricOverFileChangeFrequencyViewModel { get; private set; }
        public List<FileChangeFrequency> FileChangeFrequencies { get; private set; }
        public List<GitCommit> GitCommits { get; private set; }
        public List<FileMetric> FileMetricsForSolution { get; private set; }

        internal async Task Init()
        {
            using (LoadingRing loadingRing = LoadingRing.Show("Laden"))
            {
                GitCommits = await Task.Run(() => GetCommits());
                FileMetricsForSolution = await Task.Run(() => GetFileMetricsForSolution());
                FileChangeFrequencies = await Task.Run(() => CalculateFileChangeFrequency());
                FileMetricOverFileChangeFrequencies = await Task.Run(() => CalculateFileMetricOverFileChangeFrequencies());
                FileMetricsOverTime = await Task.Run(() => CalculateFileMetricsOverTime());
            }

            FileMetricOverTimeChartViewModel = new FileMetricOverTimeChartViewModel(FileMetricsOverTime);
            FileChangeFrequencyColumnChartViewModel = new FileChangeFrequencyColumnChartViewModel(FileChangeFrequencies);
            FileMetricOverFileChangeFrequencyViewModel = new FileMetricOverFileChangeFrequencyViewModel(FileMetricOverFileChangeFrequencies);
            CalculateFileCoupling();

            WriteCommitsToFileIfNeeded(GitCommits);

            RefreshUi();
        }

        private List<FileMetricOverFileChangeFrequency> CalculateFileMetricOverFileChangeFrequencies()
        {
            FileMetricOverFileChangeFrequencyEvaluator fileMetricOverFileChangeFrequencyEvaluator = new FileMetricOverFileChangeFrequencyEvaluator();
            return fileMetricOverFileChangeFrequencyEvaluator.
                GetFileMetricOverFileChangeFrequencies(FileChangeFrequencies, FileMetricsForSolution);
        }

        private List<FileMetric> GetFileMetricsForSolution()
        {
            FileMetricForSolutionProvider fileMetricForSolutionProvider = new FileMetricForSolutionProvider();
            return fileMetricForSolutionProvider
                .GetFileMetrics(@"C:\Users\walzeflo\source\repos\heidelpayDotNET\heidelpayDotNET_Metrics.json");
        }

        private void RefreshUi()
        {
            RaisePropertyChanged(nameof(FileChangeFrequencies));
            RaisePropertyChanged(nameof(FileMetricOverTimeChartViewModel));
            RaisePropertyChanged(nameof(FileChangeFrequencyColumnChartViewModel));
            RaisePropertyChanged(nameof(FileMetricOverFileChangeFrequencyViewModel));
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
