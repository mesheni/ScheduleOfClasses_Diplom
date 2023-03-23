using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Xml.Serialization;
using Test.Model;

namespace Test.OtherWindow
{
    /// <summary>
    /// Логика взаимодействия для AddDataWindow.xaml
    /// </summary>
    public partial class AddDataWindow : Window
    {

        private List<Auditorium> auditoriums = new List<Auditorium>();
        private string filePath = "auditoriums.xml";

        public AddDataWindow()
        {
            InitializeComponent();
            LoadAuditoriums();
        }

        private void AddAuditorium_Click(object sender, RoutedEventArgs e)
        {
            string number = txtNumber.Text;
            string subject = txtSubject.Text;
            string capacity = txtCapacity.Text;
            string building = txtBuilding.Text;

            Auditorium auditorium = new Auditorium(number, subject, capacity, building);
            auditoriums.Add(auditorium);
            dgAuditoriums.ItemsSource = null;
            dgAuditoriums.ItemsSource = auditoriums;
        }

        private void SaveAuditoriums_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Auditorium>));
            using (FileStream stream = File.Create(filePath))
            {
                serializer.Serialize(stream, auditoriums);
            }
        }

        private void LoadAuditoriums()
        {
            if (File.Exists(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Auditorium>));
                using (FileStream stream = File.OpenRead(filePath))
                {
                    auditoriums = (List<Auditorium>)serializer.Deserialize(stream);
                }
                dgAuditoriums.ItemsSource = auditoriums;
            }
        }
    }
}
