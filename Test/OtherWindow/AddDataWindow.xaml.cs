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
        // Создаем список из аудиторий
        private List<Auditorium> auditoriums = new List<Auditorium>();
        private string filePath = "auditoriums.xml"; // название файла, гду будут храниться аудитории

        public AddDataWindow() // Конструктор
        {
            InitializeComponent();
            LoadAuditoriums();
        }

        private void AddAuditorium_Click(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр класса аудитории
            Auditorium auditorium = new Auditorium();

            // Заполняем данными из текстбоксов
            auditorium.Number = txtNumber.Text;
            auditorium.Subject = txtSubject.Text;
            auditorium.Capacity = txtCapacity.Text;
            auditorium.Building = txtBuilding.Text;

            // Добавляем данные в список аудиторий
            auditoriums.Add(auditorium);
            // Обновляем DataGrid данными из списка аудиторий
            dgAuditoriums.ItemsSource = null;
            dgAuditoriums.ItemsSource = auditoriums;
        }

        private void SaveAuditoriums_Click(object sender, RoutedEventArgs e) // Сохранение в xml файле
        {
            // Создаем объект xml сериализации и передаем наш список аудиторий
            XmlSerializer serializer = new XmlSerializer(typeof(List<Auditorium>));
            // Записываем в файл наши данные
            using (FileStream stream = File.Create(filePath))
            {
                serializer.Serialize(stream, auditoriums);
            }
        }

        private void LoadAuditoriums() // Загрузка данных из файла в DataGrid
        {
            if (File.Exists(filePath)) // Проверяем, существует ли файл
            {
                // Создаем объект xml сериализации и передаем ему список аудиторий
                XmlSerializer serializer = new XmlSerializer(typeof(List<Auditorium>));
                using (FileStream stream = File.OpenRead(filePath)) // Читаем из файла
                {
                    auditoriums = serializer.Deserialize(stream) as List<Auditorium>;
                }
                dgAuditoriums.ItemsSource = auditoriums; // Прочитанную информацию помещаем в DataGrid
            }
        }
    }
}
