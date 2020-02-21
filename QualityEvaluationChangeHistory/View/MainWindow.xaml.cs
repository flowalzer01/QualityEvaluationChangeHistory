using CommonServiceLocator;
using QualityEvaluationChangeHistory.Model;
using QualityEvaluationChangeHistory.ViewModel;
using System.Collections.Generic;
using System.Windows;

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
            var vm = ServiceLocator.Current.GetInstance<MainViewModel>();
            vm.Init().ConfigureAwait(false);
        }
    }
}
