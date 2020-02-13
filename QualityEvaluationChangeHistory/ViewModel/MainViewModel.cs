using GalaSoft.MvvmLight;
using QualityEvaluationChangeHistory.Data;
using QualityEvaluationChangeHistory.Evaluation;
using QualityEvaluationChangeHistory.Model;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualityEvaluationChangeHistory.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private const bool DataFromRepository = true;
        private const string RepositoryPath = @"C:\Users\walzeflo\source\repos\heidelpayJava";
        private const string GitDataPath = @"C:\Users\walzeflo\source\repos\heidelpayJava";
        private WpfPlot _wpfPlot;

        public MainViewModel()
        {

        }

        internal async Task Init(WpfPlot wpfPlot)
        {
            _wpfPlot = wpfPlot;
            List<GitCommit> gitCommits = await Task.Run(() => GetCommits());
            List<FileChangeFrequency> fileChangeFrequencies = CalculateFileChangeFrequency(gitCommits);
            CalculateFileCoupling(gitCommits, fileChangeFrequencies);
            PlotGraph(fileChangeFrequencies);
        }

        private void CalculateFileCoupling(List<GitCommit> gitCommits, List<FileChangeFrequency> fileChangeFrequencies)
        {
            FileCouplingEvaluator fileCouplingEvaluator = new FileCouplingEvaluator(gitCommits, fileChangeFrequencies);
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

        private List<FileChangeFrequency> CalculateFileChangeFrequency(List<GitCommit> gitCommits)
        {
            FileChangeFrequencyEvaluator fileChangeFrequencyEvaluator = new FileChangeFrequencyEvaluator();

            return fileChangeFrequencyEvaluator.GetFileChangeFrequencies(gitCommits);
        }

        private void PlotGraph(List<FileChangeFrequency> fileChangeFrequencies)
        {
            int pointCount = fileChangeFrequencies.Count;
            double[] Xs = new double[pointCount];
            double[] fileChanges = new double[pointCount];
            string[] labels = new string[pointCount];

            int i = 0;
            foreach (FileChangeFrequency fileChangeFrequency in fileChangeFrequencies)
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
        }

    }
}
