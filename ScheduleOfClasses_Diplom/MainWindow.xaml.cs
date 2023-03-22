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
using ScheduleOfClasses_Diplom.Data;
using ScheduleOfClasses_Diplom.WindowAdd;

namespace ScheduleOfClasses_Diplom
{
    public static class ConfirmListBox
    {
        public static ListBox list;
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddClassBut_Click(object sender, RoutedEventArgs e)
        {
            School school = new School();
            // Создание нового объекта окна "AddClassWindow"
            AddClassWindow addClassWindow = new AddClassWindow();

            // Открытие окна "AddClassWindow" в модальном режиме
            bool? result = addClassWindow.ShowDialog();

            // Если пользователь нажал кнопку "Добавить класс" на окне "AddClassWindow", 
            // добавляем новый класс в школу
            if (result == true)
            {
                // Получение значений из текстовых полей
                string className = addClassWindow.ClassNameTextBox.Text;
                string teacherName = addClassWindow.TeacherNameTextBox.Text;
                string roomName = addClassWindow.RoomNameTextBox.Text;

                // Добавление нового класса в школу
                school.AddClass(className, teacherName, roomName);

                // Обновление списка классов в пользовательском интерфейсе
                ClassesListBox.ItemsSource = school.GetClasses();
                //ConfirmListBox.list = ClassesListBox;
            }
        }
    }
}
