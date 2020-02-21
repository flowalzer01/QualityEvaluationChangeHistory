using GalaSoft.MvvmLight;
using QualityEvaluationChangeHistory.BusinessLogic.Data;
using QualityEvaluationChangeHistory.BusinessLogic.Evaluation;
using QualityEvaluationChangeHistory.Model.Model;
using ScottPlot;
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
        private WpfPlot _wpfPlot;


        public MainViewModel()
        {

        }

        public List<FileChangeFrequency> FileChangeFrequencies { get; private set; }
        public List<GitCommit> GitCommits { get; private set; }

        internal async Task Init(WpfPlot wpfPlot)
        {
            _wpfPlot = wpfPlot;
            GitCommits = await Task.Run(() => GetCommits());
            FileChangeFrequencies = CalculateFileChangeFrequency();

            CalculateFileMetricsOverTime();
            CalculateFileCoupling();
            PlotGraph();

            WriteCommitsToFileIfNeeded(GitCommits);
        }

        private void CalculateFileMetricsOverTime()
        {
            FileMetricOverTimeEvaluator fileMetricOverTimeEvaluator = new FileMetricOverTimeEvaluator();
            FileMetricOverTime fileMetricOverTime = fileMetricOverTimeEvaluator
                .GetFileMetricOverTime(GitCommits, FileChangeFrequencies.First().FilePath);
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

            var commits = gitDataProvider.GetCommits();

            return commits;
        }

        private List<FileChangeFrequency> CalculateFileChangeFrequency()
        {
            FileChangeFrequencyEvaluator fileChangeFrequencyEvaluator = new FileChangeFrequencyEvaluator();

            return fileChangeFrequencyEvaluator.GetFileChangeFrequencies(GitCommits);
        }

        private void PlotGraph()
        {
            int pointCount = FileChangeFrequencies.Count;
            double[] Xs = new double[pointCount];
            double[] fileChanges = new double[pointCount];
            string[] labels = new string[pointCount];

            int i = 0;
            foreach (FileChangeFrequency fileChangeFrequency in FileChangeFrequencies)
            {
                Xs[i] = i;
                fileChanges[i] = fileChangeFrequency.FileChanges;
                labels[i] = "Vla";
                i++;
            }

            _wpfPlot.plt.Title("File Change Frequency");
            _wpfPlot.plt.Grid(false);

            // customize barWidth and xOffset to squeeze grouped bars together
            _wpfPlot.plt.PlotBar(Xs, fileChanges, barWidth: 2.0);

            _wpfPlot.plt.Axis(null, null, 0, null);
            _wpfPlot.plt.Legend();

            _wpfPlot.plt.XTicks(Xs, labels);
            _wpfPlot.plt.Ticks(displayTickLabelsX: true);
            _wpfPlot.Render();

            RaisePropertyChanged(nameof(FileChangeFrequencies));
        }

    }
}
