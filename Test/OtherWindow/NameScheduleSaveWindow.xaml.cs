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
using System.Windows.Shapes;

namespace Test.OtherWindow
{
    /// <summary>
    /// Логика взаимодействия для NameScheduleSaveWindow.xaml
    /// </summary>
    public partial class NameScheduleSaveWindow : Window
    {
        public NameScheduleSaveWindow()
        {
            InitializeComponent();
        }

        private void SaveBut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.filePathSchedule = TextBoxNameSchedule.Text;
            this.Close();
        }

        private void CancelBut_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
