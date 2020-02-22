using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QualityEvaluationChangeHistory.Controls
{
    /// <summary>
    /// Interaction logic for LoadingRing.xaml
    /// </summary>
    public partial class LoadingRing : UserControl, IDisposable
    {
        private Popup _containingPopup;
        private bool _disposedValue = false;

        private LoadingRing(Popup containingPopup)
        {
            InitializeComponent();
            _containingPopup = containingPopup;
        }

        public static LoadingRing Show(string progressText)
        {
            // create a popup with the size of the apps window.
            Popup popup = new Popup()
            {
                Height = SystemParameters.WorkArea.Height,
                Width = SystemParameters.WorkArea.Width,
            };

            // create the busy-indicating dialog as a child, having the same size as the app.
            LoadingRing loadingRing = new LoadingRing(popup)
            {
                Height = popup.Height,
                Width = popup.Width
            };

            loadingRing.ProgressText.Text = progressText;
            loadingRing.RootPanel.Height = SystemParameters.WorkArea.Height;
            loadingRing.RootPanel.Width = SystemParameters.WorkArea.Width;

            // set the child of the popop
            popup.Child = loadingRing;
            popup.VerticalAlignment = VerticalAlignment.Center;
            popup.AllowsTransparency = true;

            popup.Placement = PlacementMode.Center;
            popup.IsOpen = true;

            return loadingRing;
        }

        public void Close()
        {
            _containingPopup.IsOpen = false;
        }

        void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                    Close();

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
