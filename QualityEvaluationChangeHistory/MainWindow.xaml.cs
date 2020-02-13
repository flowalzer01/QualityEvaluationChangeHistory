using QualityEvaluationChangeHistory.Data;
using QualityEvaluationChangeHistory.Evaluation;
using QualityEvaluationChangeHistory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public MainWindow()
        {
            InitializeComponent();
            CalculateFileChangeFrequency();
        }

        private void CalculateFileChangeFrequency()
        {
            GitDataProvider gitDataProvider = new GitDataProvider(@"C:\Users\walzeflo\source\repos\heidelpayJava");
            FileChangeFrequencyEvaluator fileChangeFrequencyEvaluator = new FileChangeFrequencyEvaluator();

            List<FileChangeFrequency> fileChangeFrequencies = fileChangeFrequencyEvaluator.GetFileChangeFrequencies(gitDataProvider.GetCommits());
            PlotGraph(fileChangeFrequencies);
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
