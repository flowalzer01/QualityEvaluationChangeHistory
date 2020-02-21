using GalaSoft.MvvmLight;
using QualityEvaluationChangeHistory.BusinessLogic.Data;
using QualityEvaluationChangeHistory.BusinessLogic.Evaluation;
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

        public FileMetricOverTime FileMetricOverTime { get; private set; }
        public ValuesOverTimeViewModel ValuesOverTimeViewModel { get; private set; }
        public List<FileChangeFrequency> FileChangeFrequencies { get; private set; }
        public List<GitCommit> GitCommits { get; private set; }

        internal async Task Init()
        {
            GitCommits = await Task.Run(() => GetCommits());
            FileChangeFrequencies = CalculateFileChangeFrequency();
            FileMetricOverTime = CalculateFileMetricsOverTime();
            ValuesOverTimeViewModel = new ValuesOverTimeViewModel(FileMetricOverTime);
            CalculateFileCoupling();

            WriteCommitsToFileIfNeeded(GitCommits);

            RefreshUi();
        }

        private void RefreshUi()
        {
            RaisePropertyChanged(nameof(ValuesOverTimeViewModel));
            RaisePropertyChanged(nameof(FileChangeFrequencies));
        }

        private FileMetricOverTime CalculateFileMetricsOverTime()
        {
            FileMetricOverTimeEvaluator fileMetricOverTimeEvaluator = new FileMetricOverTimeEvaluator();
            FileMetricOverTime fileMetricOverTime = fileMetricOverTimeEvaluator
                .GetFileMetricOverTime(GitCommits, FileChangeFrequencies.First().FilePath);

            return fileMetricOverTime;
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
