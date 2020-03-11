using GalaSoft.MvvmLight;
using QualityEvaluationChangeHistory.BusinessLogic.Data;
using QualityEvaluationChangeHistory.BusinessLogic.Data.Interface;
using QualityEvaluationChangeHistory.BusinessLogic.Evaluation;
using QualityEvaluationChangeHistory.BusinessLogic.Evaluation.Interface;
using QualityEvaluationChangeHistory.BusinessLogic.Factory;
using QualityEvaluationChangeHistory.BusinessLogic.WareHouse;
using QualityEvaluationChangeHistory.Configuration;
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
        private readonly EvaluatorFactory _evaluatorFactory;
        private readonly WareHouseWriter _wareHouseWriter;

        public MainViewModel()
        {
            _dataProviderFactory = new DataProviderFactory(Constants.DataFromWareHouse, Constants.WareHouseProjectPath, Constants.RepositoryPath);
            _evaluatorFactory = new EvaluatorFactory(Constants.EvaluationDataFromWareHouse, Constants.WareHouseProjectPath);
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
                await Task.Run(() => WriteDataToWareHouse());
                FileMetricsForSolution = await Task.Run(() => GetFileMetricsForSolution());
                FileChangeFrequencies = await Task.Run(() => CalculateFileChangeFrequency());
                FileMetricOverFileChangeFrequencies = await Task.Run(() => CalculateFileMetricOverFileChangeFrequencies());
                FileMetricsOverTime = await Task.Run(() => CalculateFileMetricsOverTime());

                WriteEvaluationDataToWareHouse();
            }

            FileChangeFrequencyViewModel = new FileChangeFrequencyViewModel(FileChangeFrequencies, Constants.FileChangeFrequencyNumberOfFiles);
            FileMetricOverTimeViewModel = new FileMetricOverTimeViewModel(FileMetricsOverTime);
            FileMetricOverFileChangeFrequencyViewModel = new FileMetricOverFileChangeFrequencyViewModel(FileMetricOverFileChangeFrequencies);

            RefreshUi();
        }

        private void WriteDataToWareHouse()
        {
            if (!Constants.DataFromWareHouse)
            {
                _wareHouseWriter.WriteCommitsToWareHouse(GitCommits);
            }
        }

        private void WriteEvaluationDataToWareHouse()
        {
            if (!Constants.EvaluationDataFromWareHouse)
            {
                _wareHouseWriter.WriteFileChangeFrequenciesToWareHouse(FileChangeFrequencies);
                _wareHouseWriter.WriteFileMetricOverTimeToWareHouse(FileMetricsOverTime);
                _wareHouseWriter.WriteFileMetricOverFileChangeFrequencyToWareHouse(FileMetricOverFileChangeFrequencies);
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
            IFileMetricOverFileChangeFrequencyEvaluator fileMetricOverFileChangeFrequencyEvaluator = _evaluatorFactory.GetFileMetricOverFileChangeFrequencyEvaluator();
            return fileMetricOverFileChangeFrequencyEvaluator.
                GetFileMetricOverFileChangeFrequencies(FileChangeFrequencies.Take(Constants.FileChangeFrequencyNumberOfFiles).ToList(), FileMetricsForSolution);
        }

        private List<FileMetric> GetFileMetricsForSolution()
        {
            FileMetricForSolutionProvider fileMetricForSolutionProvider = new FileMetricForSolutionProvider(Constants.WareHouseProjectPath, Constants.FileMetricFileName);
            return fileMetricForSolutionProvider.GetFileMetrics();
        }

        private List<FileMetricOverTime> CalculateFileMetricsOverTime()
        {
            IFileMetricOverTimeEvaluator fileMetricOverTimeEvaluator = _evaluatorFactory.GetFileMetricOverTimeEvaluator();
            List<FileMetricOverTime> fileMetricsOverTime = fileMetricOverTimeEvaluator
                .GetFileMetricOverTime(GitCommits, FileChangeFrequencies.Take(Constants.FileMetricsOverTimeNumberOfFiles).Select(x => x.FilePath));

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
            IFileChangeFrequencyEvaluator fileChangeFrequencyEvaluator = _evaluatorFactory.GetFileChangeFrequencyEvaluator();
            return fileChangeFrequencyEvaluator.GetFileChangeFrequencies(GitCommits);
        }
    }
}
