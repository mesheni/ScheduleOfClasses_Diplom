using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
using ScheduleOfClasses_Diplom.Data;

namespace ScheduleOfClasses_Diplom.WindowAdd
{
    /// <summary>
    /// Логика взаимодействия для AddClassWindow.xaml
    /// </summary>
    public partial class AddClassWindow : Window
    {
        public AddClassWindow()
        {
            InitializeComponent();
        }

        private void AddClassButton_Click(object sender, RoutedEventArgs e)
        {
            School school = new School();

            // Получение значений из текстовых полей
            string className = ClassNameTextBox.Text;
            string teacherName = TeacherNameTextBox.Text;
            string roomName = RoomNameTextBox.Text;

            // Проверка, что все поля заполнены
            if (string.IsNullOrEmpty(className) || string.IsNullOrEmpty(teacherName) || string.IsNullOrEmpty(roomName))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //// Создание нового объекта класса
            //SchoolClass newClass = new SchoolClass();

            //// Добавление нового класса в список классов школы
            //school.AddClass(newClass);

            //// Обновление списка классов на главном окне приложения
            //ClassesListBox.ItemsSource = school.GetClasses();

            // Закрытие окна
            this.DialogResult = true;
        }
    }
}
