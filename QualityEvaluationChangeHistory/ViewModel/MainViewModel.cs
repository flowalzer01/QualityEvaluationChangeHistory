using GalaSoft.MvvmLight;
using QualityEvaluationChangeHistory.BusinessLogic.Data;
using QualityEvaluationChangeHistory.BusinessLogic.Data.Interface;
using QualityEvaluationChangeHistory.BusinessLogic.Evaluation;
using QualityEvaluationChangeHistory.BusinessLogic.Factory;
using QualityEvaluationChangeHistory.BusinessLogic.WareHouse;
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
        private readonly DataProviderFactory _dataProviderFactory;
        private readonly WareHouseWriter _wareHouseWriter;

        public MainViewModel()
        {
            _dataProviderFactory = new DataProviderFactory(Constants.DataFromWareHouse, Constants.WareHouseProjectPath, Constants.RepositoryPath);
            _wareHouseWriter = new WareHouseWriter(Constants.WareHousePath, Constants.WareHouseProjectName);
        }

        public List<FileMetricOverTime> FileMetricsOverTime { get; private set; }
        public List<FileMetricOverFileChangeFrequency> FileMetricOverFileChangeFrequencies { get; private set; }
        public List<FileChangeFrequency> FileChangeFrequencies { get; private set; }
        public List<GitCommit> GitCommits { get; private set; }
        public List<FileMetric> FileMetricsForSolution { get; private set; }
        public FileChangeFrequencyViewModel FileChangeFrequencyViewModel { get; private set; }
        public FileMetricOverTimeViewModel FileMetricOverTimeViewModel { get; private set; }
        public FileMetricOverFileChangeFrequencyViewModel FileMetricOverFileChangeFrequencyViewModel { get; private set; }

        internal async Task Init()
        {
            _wareHouseWriter.CreateWareHouseRootFolderIfNeeded();
            _wareHouseWriter.CreateWareHouseProjectFolderIfNeeded();

            using (LoadingRing loadingRing = LoadingRing.Show("Laden"))
            {
                GitCommits = await Task.Run(() => GetCommits());
                FileMetricsForSolution = await Task.Run(() => GetFileMetricsForSolution());
                FileChangeFrequencies = await Task.Run(() => CalculateFileChangeFrequency());
                FileMetricOverFileChangeFrequencies = await Task.Run(() => CalculateFileMetricOverFileChangeFrequencies());
                FileMetricsOverTime = await Task.Run(() => CalculateFileMetricsOverTime());

                WriteToWareHouse();
            }

            FileMetricOverFileChangeFrequencyViewModel = new FileMetricOverFileChangeFrequencyViewModel(FileMetricOverFileChangeFrequencies);
            FileChangeFrequencyViewModel = new FileChangeFrequencyViewModel(FileChangeFrequencies);
            FileMetricOverTimeViewModel = new FileMetricOverTimeViewModel(FileMetricsOverTime);


            RefreshUi();
        }

        private void WriteToWareHouse()
        {
            if (!Constants.DataFromWareHouse)
            {
                _wareHouseWriter.WriteCommitsToWareHouse(GitCommits);
            }
        }

        private void RefreshUi()
        {
            RaisePropertyChanged(nameof(FileChangeFrequencyViewModel));
            RaisePropertyChanged(nameof(FileMetricOverTimeViewModel));
            RaisePropertyChanged(nameof(FileMetricOverFileChangeFrequencyViewModel));
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

        private List<FileMetricOverTime> CalculateFileMetricsOverTime()
        {
            FileMetricOverTimeEvaluator fileMetricOverTimeEvaluator = new FileMetricOverTimeEvaluator();
            List<FileMetricOverTime> fileMetricsOverTime = fileMetricOverTimeEvaluator
                .GetFileMetricOverTime(GitCommits, FileChangeFrequencies.Take(5).Select(x => x.FilePath));

            return fileMetricsOverTime;
        }

        private void CalculateFileCoupling()
        {
            FileCouplingEvaluator fileCouplingEvaluator = new FileCouplingEvaluator(Constants.FileCouplingCombinationSize, Constants.FileCouplingFilesToLookAt,
                GitCommits, FileChangeFrequencies);
            List<FileCouple> fileCouples = fileCouplingEvaluator
                .CalculateFileCouples()
                .OrderByDescending(x => x.GitCommits.Count)
                .ToList();
        }

        private List<GitCommit> GetCommits()
        {
            IGitDataProvider gitDataProvider = _dataProviderFactory.GetGitDataProvider();
            return gitDataProvider.GetCommits();
        }

        private List<FileChangeFrequency> CalculateFileChangeFrequency()
        {
            FileChangeFrequencyEvaluator fileChangeFrequencyEvaluator = new FileChangeFrequencyEvaluator(Constants.FileChangeFrequencyNumberOfFiles);

            return fileChangeFrequencyEvaluator.GetFileChangeFrequencies(GitCommits);
        }
    }
}
