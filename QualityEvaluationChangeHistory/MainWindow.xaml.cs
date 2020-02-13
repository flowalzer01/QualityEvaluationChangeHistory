using QualityEvaluationChangeHistory.Data;
using QualityEvaluationChangeHistory.Evaluation;
using QualityEvaluationChangeHistory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QualityEvaluationChangeHistory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const bool DataFromRepository = true;
        private const string RepositoryPath = @"C:\Users\walzeflo\source\repos\heidelpayJava";
        private const string GitDataPath = @"C:\Users\walzeflo\source\repos\heidelpayJava";

        public MainWindow()
        {
            InitializeComponent();
            List<GitCommit> gitCommits = GetCommits();
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

            wpfPlot1.plt.Title("File Change Frequency");
            wpfPlot1.plt.Grid(false);

            // customize barWidth and xOffset to squeeze grouped bars together
            wpfPlot1.plt.PlotBar(Xs, fileChanges, barWidth: 2.0);

            wpfPlot1.plt.Axis(null, null, 0, null);
            wpfPlot1.plt.Legend();

            wpfPlot1.plt.XTicks(Xs, labels);
            wpfPlot1.plt.Ticks(displayTickLabelsX: true);
            wpfPlot1.Render();
        }
    }
}
