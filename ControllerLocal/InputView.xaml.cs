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

namespace ControllerLocal
{
    /// <summary>
    /// Interaction logic for RegionInputControl.xaml
    /// </summary>
    public partial class InputView : UserControl
    {
        #region Fields

        private bool m_isMouseInside;
        private bool m_isChecked;

        #endregion

        #region Properties

        public SolidColorBrush InputBackground
        {
            get { return (SolidColorBrush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static readonly DependencyProperty InputBackgroundProperty =
            DependencyProperty.Register("InputBackground", typeof(SolidColorBrush),
            typeof(InputView), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        public bool IsChecked
        {
            get
            {
                return m_isChecked;
            }
            set
            {
                if (m_isChecked == value)
                {
                    return;
                }

                m_isChecked = value;
                InputBackground = m_isChecked
                    ? new SolidColorBrush(Colors.Black)
                    : new SolidColorBrush(Colors.White);
            }
        }

        #endregion

        #region Methods

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            m_isMouseInside = true;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                IsChecked = true;
            }
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            m_isMouseInside = false;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            IsChecked = !IsChecked;
        }

        #endregion

        #region Instance

        public InputView()
        {
            InitializeComponent();
        }

        #endregion
    }
}
