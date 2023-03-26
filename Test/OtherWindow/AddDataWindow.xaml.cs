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
        // Создаем список из аудиторий и учителей
        private List<Auditorium> auditoriums = new List<Auditorium>();
        private List<Teacher> teachers = new List<Teacher>();
        private string filePathAuditorium = "auditoriums.xml"; // название файла, где будут храниться аудитории
        private string filePathTeacher = "teachers.xml"; // название файла, где будут храниться преподаватели


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

            txtNumber.Text = "";
            txtSubject.Text = "";
            txtCapacity.Text = "";
            txtBuilding.Text = "";
        }

        private void SaveAuditoriums_Click(object sender, RoutedEventArgs e) // Сохранение в xml файле
        {
            // Создаем объект xml сериализации и передаем наш список аудиторий
            XmlSerializer serializer = new XmlSerializer(typeof(List<Auditorium>));
            // Записываем в файл наши данные
            using (FileStream stream = File.Create(filePathAuditorium))
            {
                serializer.Serialize(stream, auditoriums);
            }
        }

        private void LoadAuditoriums() // Загрузка данных из файла в DataGrid
        {
            if (File.Exists(filePathAuditorium)) // Проверяем, существует ли файл
            {
                // Создаем объект xml сериализации и передаем ему список аудиторий
                XmlSerializer serializer = new XmlSerializer(typeof(List<Auditorium>));
                using (FileStream stream = File.OpenRead(filePathAuditorium)) // Читаем из файла
                {
                    auditoriums = serializer.Deserialize(stream) as List<Auditorium>;
                }
                dgAuditoriums.ItemsSource = auditoriums; // Прочитанную информацию помещаем в DataGrid
            }
        }
        // Функция удаления записи из DataGrid по нажатию кнопки Delete
        private void DeleteAuditorium_Click(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) // Если нажата кнопка Delete
            {
                if (dgAuditoriums.SelectedIndex != -1)
                {
                    // Выводим сообщение
                    MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        // Удаляем строку из списка аудиторий
                        auditoriums.RemoveAt(dgAuditoriums.SelectedIndex);
                        // Перезаписываем DataGrid
                        dgAuditoriums.ItemsSource = null;
                        dgAuditoriums.ItemsSource = auditoriums;
                    }
                }
            }
        }
        // Добавление новой строки в DataGrid по нажатию кнопки "Добавить"
        private void AddButTeacher_AddDataW_Click(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр класса учителей
            Teacher teacher = new Teacher();

            // Заполняем данными из текстбоксов
            teacher.Name = txtNameT.Text;
            teacher.Surname = txtSurnameT.Text;
            teacher.Gender = txtGenderT.Text;
            teacher.Subject = txtSubjectT.Text;
            teacher.Email = txtEmailT.Text;
            teacher.Phone = txtPhoneT.Text;
            teacher.Address = txtAddressT.Text;
            teacher.BirthDate = txtBirthDate.Text;

            // Добавляем данные в список аудиторий
            teachers.Add(teacher);
            // Обновляем DataGrid данными из списка аудиторий
            dgTeachers.ItemsSource = null;
            dgTeachers.ItemsSource = teachers;

            txtNameT.Text = "";
            txtSurnameT.Text = "";
            txtGenderT.Text = "";
            txtSubjectT.Text = "";
            txtEmailT.Text = "";
            txtPhoneT.Text = "";
            txtAddressT.Text= "";
            txtBirthDate.Text = "";
        }

        // Функция сохранения данных учителей в xml файле
        private void SaveButTeacher_AddDataW_Click(object sender, RoutedEventArgs e)
        {
            // Создаем объект xml сериализации и передаем наш список аудиторий
            XmlSerializer serializer = new XmlSerializer(typeof(List<Teacher>));
            // Записываем в файл наши данные
            using (FileStream stream = File.Create(filePathTeacher))
            {
                serializer.Serialize(stream, teachers);
            }
        }

        // Функция загрузки данных из файла в DataGrid при переходе на вкладку "Аудитории"
        private void TabItemAuditorium_Initialized(object sender, EventArgs e)
        {
            if (File.Exists(filePathAuditorium)) // Проверяем, существует ли файл
            {
                // Создаем объект xml сериализации и передаем ему список аудиторий
                XmlSerializer serializer = new XmlSerializer(typeof(List<Auditorium>));
                using (FileStream stream = File.OpenRead(filePathAuditorium)) // Читаем из файла
                {
                    auditoriums = serializer.Deserialize(stream) as List<Auditorium>;
                }
                dgAuditoriums.ItemsSource = auditoriums; // Прочитанную информацию помещаем в DataGrid
            }
        }

        // Функция загрузки данных из файла в DataGrid при переходе на вкладку "Учителя"
        private void TabItemTeacher_Initialized(object sender, EventArgs e)
        {
            if (File.Exists(filePathTeacher)) // Проверяем, существует ли файл
            {
                // Создаем объект xml сериализации и передаем ему список учителей
                XmlSerializer serializer = new XmlSerializer(typeof(List<Teacher>));
                using (FileStream stream = File.OpenRead(filePathTeacher)) // Читаем из файла
                {
                    teachers = serializer.Deserialize(stream) as List<Teacher>;
                }
                dgTeachers.ItemsSource = teachers; // Прочитанную информацию помещаем в DataGrid
            }
        }
    }
}
