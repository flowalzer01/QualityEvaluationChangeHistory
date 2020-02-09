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
        }
    }
}
