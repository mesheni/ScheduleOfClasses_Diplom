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
        string audDir = @".\Auditoriums\";
        string teachDir = @".\Teachers\";
        string subDir = @".\Subjects\";
        string classDir = @".\Classes\";

        // Создаем список из аудиторий, учителей, предметов
        private List<Auditorium> auditoriums = new List<Auditorium>();
        private List<Teacher> teachers = new List<Teacher>();
        private List<Subject> subjects = new List<Subject>();
        private List<StudentsGroup> classes = new List<StudentsGroup>();
        private string filePathAuditorium = "auditoriums.xml"; // название файла, где будут храниться аудитории
        private string filePathTeacher = "teachers.xml"; // название файла, где будут храниться преподаватели
        private string filePathSubject = "subjects.xml"; // название файла, где будут храниться предметы
        private string filePathClasses = "classes.xml"; // название файла, где будут храниться классы


        public AddDataWindow() // Конструктор
        {
            InitializeComponent();
            LoadAuditoriums();
            InitDir();
        }

        private void InitDir()
        {
            if (!Directory.Exists(audDir))
            {
                Directory.CreateDirectory(audDir);
            }
            if (!Directory.Exists(teachDir))
            {
                Directory.CreateDirectory(teachDir);
            }
            if (!Directory.Exists(subDir))
            {
                Directory.CreateDirectory(subDir);
            }
            if (!Directory.Exists(classDir))
            {
                Directory.CreateDirectory(classDir);
            }
        }

        // Код для вкладки "Аудитории"
        #region
        private void AddAuditorium_Click(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр класса аудитории
            Auditorium auditorium = new Auditorium();

            // Заполняем данными из текстбоксов
            auditorium.Id = Convert.ToInt32(txtIdAuditorium.Text);
            auditorium.Name = txtNameAuditorium.Text;
            auditorium.NumberOfSeats = Convert.ToInt32(txtNumSeatsAuditorium.Text);
            foreach (Subject subject in subjects)
            {
                if (subject.Name == comSubjectAuditorium.Text)
                {
                    auditorium.Subject = subject;
                }
            }

            // Добавляем данные в список аудиторий
            auditoriums.Add(auditorium);
            // Обновляем DataGrid данными из списка аудиторий
            dgAuditoriums.ItemsSource = null;
            dgAuditoriums.ItemsSource = auditoriums;

            txtIdAuditorium.Text = "";
            txtNameAuditorium.Text = "";
            txtNumSeatsAuditorium.Text = "";
        }

        private void SaveAuditoriums_Click(object sender, RoutedEventArgs e) // Сохранение в xml файле
        {
            // Создаем объект xml сериализации и передаем наш список аудиторий
            XmlSerializer serializer = new XmlSerializer(typeof(List<Auditorium>));
            // Записываем в файл наши данные
            using (FileStream stream = File.Create($"{audDir}{filePathAuditorium}"))
            {
                serializer.Serialize(stream, auditoriums);
                MessageBox.Show("Данные успешно сохранены!", "Успех!");
            }
        }

        private void LoadAuditoriums() // Загрузка данных из файла в DataGrid
        {
            if (File.Exists($"{audDir}{filePathAuditorium}")) // Проверяем, существует ли файл
            {
                // Создаем объект xml сериализации и передаем ему список аудиторий
                XmlSerializer serializer = new XmlSerializer(typeof(List<Auditorium>));
                using (FileStream stream = File.OpenRead($"{audDir}{filePathAuditorium}")) // Читаем из файла
                {
                    auditoriums = serializer.Deserialize(stream) as List<Auditorium>;
                }
                dgAuditoriums.ItemsSource = auditoriums; // Прочитанную информацию помещаем в DataGrid
            }
            foreach (Subject i in subjects)
            {
                comSubjectAuditorium.Items.Add(i.Name);
                comSubjectTeacher.Items.Add(i.Name);
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

        // Функция загрузки данных из файла в DataGrid при переходе на вкладку "Аудитории"
        private void TabItemAuditorium_Initialized(object sender, EventArgs e)
        {
            if (File.Exists($"{audDir}{filePathAuditorium}")) // Проверяем, существует ли файл
            {
                // Создаем объект xml сериализации и передаем ему список аудиторий
                XmlSerializer serializer = new XmlSerializer(typeof(List<Auditorium>));
                using (FileStream stream = File.OpenRead($"{audDir}{filePathAuditorium}")) // Читаем из файла
                {
                    auditoriums = serializer.Deserialize(stream) as List<Auditorium>;
                }
                dgAuditoriums.ItemsSource = auditoriums; // Прочитанную информацию помещаем в DataGrid
            }
        }
        #endregion
        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        // Код для вкладки "Преподаватели"
        #region
        // Добавление новой строки в DataGrid по нажатию кнопки "Добавить"
        private void AddButTeacher_AddDataW_Click(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр класса учителей
            Teacher teacher = new Teacher();

            // Заполняем данными из текстбоксов
            teacher.Id = Convert.ToInt32(txtIdTeacher.Text);
            teacher.Name = txtNameTeacher.Text;
            foreach (Subject subject in subjects)
            {
                if (subject.Name == comSubjectTeacher.Text)
                {
                    teacher.Subject = subject;
                }
            }

            // Добавляем данные в список аудиторий
            teachers.Add(teacher);
            // Обновляем DataGrid данными из списка аудиторий
            dgTeachers.ItemsSource = null;
            dgTeachers.ItemsSource = teachers;

            txtIdTeacher.Text = "";
            txtNameTeacher.Text = "";
        }

        // Функция сохранения данных учителей в xml файле
        private void SaveButTeacher_AddDataW_Click(object sender, RoutedEventArgs e)
        {
            // Создаем объект xml сериализации и передаем наш список аудиторий
            XmlSerializer serializer = new XmlSerializer(typeof(List<Teacher>));
            // Записываем в файл наши данные
            using (FileStream stream = File.Create($"{teachDir}{filePathTeacher}"))
            {
                serializer.Serialize(stream, teachers);
                MessageBox.Show("Данные успешно сохранены!", "Успех!");
            }
        }

        // Функция удаления записи из DataGrid по нажатию кнопки Delete
        private void DeleteTeacher_Click(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) // Если нажата кнопка Delete
            {
                if (dgTeachers.SelectedIndex != -1)
                {
                    // Выводим сообщение
                    MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        // Удаляем строку из списка аудиторий
                        teachers.RemoveAt(dgTeachers.SelectedIndex);
                        // Перезаписываем DataGrid
                        dgTeachers.ItemsSource = null;
                        dgTeachers.ItemsSource = teachers;
                    }
                }
            }
        }

        // Функция загрузки данных из файла в DataGrid при переходе на вкладку "Учителя"
        private void TabItemTeacher_Initialized(object sender, EventArgs e)
        {
            if (File.Exists($"{teachDir}{filePathTeacher}")) // Проверяем, существует ли файл
            {
                // Создаем объект xml сериализации и передаем ему список учителей
                XmlSerializer serializer = new XmlSerializer(typeof(List<Teacher>));
                using (FileStream stream = File.OpenRead($"{teachDir}{filePathTeacher}")) // Читаем из файла
                {
                    teachers = serializer.Deserialize(stream) as List<Teacher>;
                }
                dgTeachers.ItemsSource = teachers; // Прочитанную информацию помещаем в DataGrid
            }
        }
        #endregion
        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        // Код для вкладки "Предметы"
        #region
        // Добавление новой строки в DataGrid по нажатию кнопки "Добавить"
        private void AddButSubject_AddDataW_Click(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр класса учителей
            Subject subject = new Subject();

            // Заполняем данными из текстбоксов
            subject.Id = Convert.ToInt32(txtIdSubject.Text);
            subject.Name = txtNameSubject.Text;

            // Добавляем данные в список аудиторий
            subjects.Add(subject);

            // Обновляем DataGrid данными из списка аудиторий
            dgSubjects.ItemsSource = null;
            dgSubjects.ItemsSource = subjects;

            txtIdSubject.Text = "";
            txtNameSubject.Text = "";
        }

        // Функция сохранения данных предметов в xml файле
        private void SaveButSubject_AddDataW_Click(object sender, RoutedEventArgs e)
        {
            // Создаем объект xml сериализации и передаем наш список аудиторий
            XmlSerializer serializer = new XmlSerializer(typeof(List<Subject>));
            // Записываем в файл наши данные
            using (FileStream stream = File.Create($"{subDir}{filePathSubject}"))
            {
                serializer.Serialize(stream, subjects);
                MessageBox.Show("Данные успешно сохранены!", "Успех!");
            }
        }

        // Функция удаления записи из DataGrid по нажатию кнопки Delete
        private void DeleteSubject_Click(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) // Если нажата кнопка Delete
            {
                if (dgSubjects.SelectedIndex != -1)
                {
                    // Выводим сообщение
                    MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        // Удаляем строку из списка предметов
                        subjects.RemoveAt(dgSubjects.SelectedIndex);
                        // Перезаписываем DataGrid
                        dgSubjects.ItemsSource = null;
                        dgSubjects.ItemsSource = subjects;
                    }
                }
            }
        }

        // Функция загрузки данных из файла в DataGrid при переходе на вкладку "Предметы"
        private void TabItemSubject_Initialized(object sender, EventArgs e)
        {
            if (File.Exists($"{subDir}{filePathSubject}")) // Проверяем, существует ли файл
            {
                // Создаем объект xml сериализации и передаем ему список предметов
                XmlSerializer serializer = new XmlSerializer(typeof(List<Subject>));
                using (FileStream stream = File.OpenRead($"{subDir}{filePathSubject}")) // Читаем из файла
                {
                    subjects = serializer.Deserialize(stream) as List<Subject>;
                }
                dgSubjects.ItemsSource = subjects; // Прочитанную информацию помещаем в DataGrid
            }
        }
        #endregion
        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        
        // Код для вкладки "Классы"
        #region
        private void AddButClasses_AddDataW_Click(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр класса классов
            StudentsGroup classSchool = new StudentsGroup();

            // Заполняем данными из текстбоксов
            classSchool.Id = Convert.ToInt32(txtIdClass.Text);
            classSchool.Name = txtNameClass.Text;
            classSchool.NumberOfStudents = Convert.ToInt32(txtNumberStudentsClass.Text);

            // Добавляем данные в список классов
            classes.Add(classSchool);

            // Обновляем DataGrid данными из списка классов
            dgClasses.ItemsSource = null;
            dgClasses.ItemsSource = classes;

            txtIdClass.Text = "";
            txtNameClass.Text = "";
            txtNumberStudentsClass.Text = "";
        }

        private void SaveButClasses_AddDataW_Click(object sender, RoutedEventArgs e)
        {
            // Создаем объект xml сериализации и передаем наш список аудиторий
            XmlSerializer serializer = new XmlSerializer(typeof(List<StudentsGroup>));
            // Записываем в файл наши данные
            using (FileStream stream = File.Create($"{classDir}{filePathClasses}"))
            {
                serializer.Serialize(stream, classes);
                MessageBox.Show("Данные успешно сохранены!", "Успех!");
            }
        }

        private void TabItemClasses_Initialized(object sender, EventArgs e)
        {
            if (File.Exists($"{classDir}{filePathClasses}")) // Проверяем, существует ли файл
            {
                // Создаем объект xml сериализации и передаем ему список предметов
                XmlSerializer serializer = new XmlSerializer(typeof(List<StudentsGroup>));
                using (FileStream stream = File.OpenRead($"{classDir}{filePathClasses}")) // Читаем из файла
                {
                    classes = serializer.Deserialize(stream) as List<StudentsGroup>;
                }
                dgClasses.ItemsSource = classes; // Прочитанную информацию помещаем в DataGrid
            }
        }

        private void DeleteClasses_Click(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) // Если нажата кнопка Delete
            {
                if (dgClasses.SelectedIndex != -1)
                {
                    // Выводим сообщение
                    MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление записи", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        // Удаляем строку из списка классов
                        classes.RemoveAt(dgClasses.SelectedIndex);
                        // Перезаписываем DataGrid
                        dgClasses.ItemsSource = null;
                        dgClasses.ItemsSource = classes;
                    }
                }
            }
        }
        #endregion
        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    }
}
