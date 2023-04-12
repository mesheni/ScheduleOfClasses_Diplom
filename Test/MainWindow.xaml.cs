using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml.Serialization;
using Test.Model;
using Test.OtherWindow;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string filePathSubject = "subjects.xml";
        private List<Subject> subjects = new List<Subject>();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            AddDataWindow addDataWindow = new AddDataWindow();
            addDataWindow.ShowDialog();
        }
        
    }
}
