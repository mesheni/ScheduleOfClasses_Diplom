using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Win32;
using Test.GenClasses;
using Test.Model;
using Test.OtherWindow;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string audDir = @".\Auditoriums\";
        string teachDir = @".\Teachers\";
        string subDir = @".\Subjects\";
        string classDir = @".\Classes\";
        string scheduleDir = @".\Schedule\";

        private string filePathAuditorium = "auditoriums.xml"; // название файла, где будут храниться аудитории
        private string filePathTeacher = "teachers.xml"; // название файла, где будут храниться преподаватели
        private string filePathSubject = "subjects.xml"; // название файла, где будут храниться предметы
        private string filePathClasses = "classes.xml"; // название файла, где будут храниться классы
        public static string filePathSchedule = ""; // название файла расписания по умолчанию, где будут храниться расписание

        private List<Auditorium> auditoriums = new List<Auditorium>();
        private List<Teacher> teachers = new List<Teacher>();
        private List<Subject> subjects = new List<Subject>();
        private List<StudentsGroup> classes = new List<StudentsGroup>();

        public List<TextBlock> textBlocksAll = new List<TextBlock>();

        public List<TextBlock> textBlocksPon = new List<TextBlock>();
        public List<TextBlock> textBlocksVt = new List<TextBlock>();
        public List<TextBlock> textBlocksSr = new List<TextBlock>();
        public List<TextBlock> textBlocksChet = new List<TextBlock>();
        public List<TextBlock> textBlocksPyat = new List<TextBlock>();
        public List<TextBlock> textBlocksSub = new List<TextBlock>();

        //private void InitTable()
        //{
        //    textBlocksPon.Clear();
        //    textBlocksVt.Clear();
        //    textBlocksSr.Clear();
        //    textBlocksChet.Clear();
        //    textBlocksPyat.Clear();
        //    textBlocksSub.Clear();

        //    textBlocksPon.Add(TextBlockPon1);
        //    textBlocksPon.Add(TextBlockPon2);
        //    textBlocksPon.Add(TextBlockPon3);
        //    textBlocksPon.Add(TextBlockPon4);
        //    textBlocksPon.Add(TextBlockPon5);
        //    textBlocksPon.Add(TextBlockPon6);
        //    textBlocksPon.Add(TextBlockPon7);
        //    textBlocksPon.Add(TextBlockPon8);

        //    textBlocksVt.Add(TextBlockVt1);
        //    textBlocksVt.Add(TextBlockVt2);
        //    textBlocksVt.Add(TextBlockVt3);
        //    textBlocksVt.Add(TextBlockVt4);
        //    textBlocksVt.Add(TextBlockVt5);
        //    textBlocksVt.Add(TextBlockVt6);
        //    textBlocksVt.Add(TextBlockVt7);
        //    textBlocksVt.Add(TextBlockVt8);

        //    textBlocksSr.Add(TextBlockSr1);
        //    textBlocksSr.Add(TextBlockSr2);
        //    textBlocksSr.Add(TextBlockSr3);
        //    textBlocksSr.Add(TextBlockSr4);
        //    textBlocksSr.Add(TextBlockSr5);
        //    textBlocksSr.Add(TextBlockSr6);
        //    textBlocksSr.Add(TextBlockSr7);
        //    textBlocksSr.Add(TextBlockSr8);

        //    textBlocksChet.Add(TextBlockChet1);
        //    textBlocksChet.Add(TextBlockChet2);
        //    textBlocksChet.Add(TextBlockChet3);
        //    textBlocksChet.Add(TextBlockChet4);
        //    textBlocksChet.Add(TextBlockChet5);
        //    textBlocksChet.Add(TextBlockChet6);
        //    textBlocksChet.Add(TextBlockChet7);
        //    textBlocksChet.Add(TextBlockChet8);

        //    textBlocksPyat.Add(TextBlockPt1);
        //    textBlocksPyat.Add(TextBlockPt2);
        //    textBlocksPyat.Add(TextBlockPt3);
        //    textBlocksPyat.Add(TextBlockPt4);
        //    textBlocksPyat.Add(TextBlockPt5);
        //    textBlocksPyat.Add(TextBlockPt6);
        //    textBlocksPyat.Add(TextBlockPt7);
        //    textBlocksPyat.Add(TextBlockPt8);

        //    textBlocksSub.Add(TextBlockSb1);
        //    textBlocksSub.Add(TextBlockSb2);
        //    textBlocksSub.Add(TextBlockSb3);
        //    textBlocksSub.Add(TextBlockSb4);
        //    textBlocksSub.Add(TextBlockSb5);
        //    textBlocksSub.Add(TextBlockSb6);
        //    textBlocksSub.Add(TextBlockSb7);
        //    textBlocksSub.Add(TextBlockSb8);
            
        //}

        private void InitTableAll()
        {
            textBlocksAll.Clear();

            textBlocksAll.Add(TextBlockPon1);
            textBlocksAll.Add(TextBlockPon2);
            textBlocksAll.Add(TextBlockPon3);
            textBlocksAll.Add(TextBlockPon4);
            textBlocksAll.Add(TextBlockPon5);
            textBlocksAll.Add(TextBlockPon6);

            textBlocksAll.Add(TextBlockVt1);
            textBlocksAll.Add(TextBlockVt2);
            textBlocksAll.Add(TextBlockVt3);
            textBlocksAll.Add(TextBlockVt4);
            textBlocksAll.Add(TextBlockVt5);
            textBlocksAll.Add(TextBlockVt6);

            textBlocksAll.Add(TextBlockSr1);
            textBlocksAll.Add(TextBlockSr2);
            textBlocksAll.Add(TextBlockSr3);
            textBlocksAll.Add(TextBlockSr4);
            textBlocksAll.Add(TextBlockSr5);
            textBlocksAll.Add(TextBlockSr6);

            textBlocksAll.Add(TextBlockChet1);
            textBlocksAll.Add(TextBlockChet2);
            textBlocksAll.Add(TextBlockChet3);
            textBlocksAll.Add(TextBlockChet4);
            textBlocksAll.Add(TextBlockChet5);
            textBlocksAll.Add(TextBlockChet6);

            textBlocksAll.Add(TextBlockPt1);
            textBlocksAll.Add(TextBlockPt2);
            textBlocksAll.Add(TextBlockPt3);
            textBlocksAll.Add(TextBlockPt4);
            textBlocksAll.Add(TextBlockPt5);
            textBlocksAll.Add(TextBlockPt6);

            textBlocksAll.Add(TextBlockSb1);
            textBlocksAll.Add(TextBlockSb2);
            textBlocksAll.Add(TextBlockSb3);
            textBlocksAll.Add(TextBlockSb4);
            textBlocksAll.Add(TextBlockSb5);
            textBlocksAll.Add(TextBlockSb6);

        }

        public MainWindow()
        {
            InitializeComponent();
            InitDir();
            InitTableAll();
        }

        private void DisplaySchedule(Chromosome bestChromosome)
        {
            foreach(var item in textBlocksAll)
            {
                item.Text = "";
            }
            foreach (TimeSlot timeSlot in bestChromosome.Schedule)
            {
                foreach (var item in textBlocksAll)
                {
                    
                    if (item.Text == "")
                    {
                        item.Text = timeSlot.Subject.Name + "\n" + timeSlot.Auditorium.Name;
                        break;
                    }
                }
                
                //scheduleListBox.Items.Add($"Предмет: {timeSlot.Subject.Name}");
                //scheduleListBox.Items.Add($"Аудитория: {timeSlot.Auditorium.Name}");
            }
        }

        private void Load()
        {
            if (File.Exists($"{audDir}{filePathAuditorium}")) // Проверяем, существует ли файл
            {
                // Создаем объект xml сериализации и передаем ему список аудиторий
                XmlSerializer serializer = new XmlSerializer(typeof(List<Auditorium>));
                using (FileStream stream = File.OpenRead($"{audDir}{filePathAuditorium}")) // Читаем из файла
                {
                    auditoriums = serializer.Deserialize(stream) as List<Auditorium>;
                }
            }

            if (File.Exists($"{teachDir}{filePathTeacher}")) // Проверяем, существует ли файл
            {
                // Создаем объект xml сериализации и передаем ему список преподавателей
                XmlSerializer serializer = new XmlSerializer(typeof(List<Teacher>));
                using (FileStream stream = File.OpenRead($"{teachDir}{filePathTeacher}")) // Читаем из файла
                {
                    teachers = serializer.Deserialize(stream) as List<Teacher>;
                }
            }

            if (File.Exists($"{subDir}{filePathSubject}")) // Проверяем, существует ли файл
            {
                // Создаем объект xml сериализации и передаем ему список предметов
                XmlSerializer serializer = new XmlSerializer(typeof(List<Subject>));
                using (FileStream stream = File.OpenRead($"{subDir}{filePathSubject}")) // Читаем из файла
                {
                    subjects = serializer.Deserialize(stream) as List<Subject>;
                }
            }

            if (File.Exists($"{classDir}{filePathClasses}")) // Проверяем, существует ли файл
            {
                // Создаем объект xml сериализации и передаем ему список классов
                XmlSerializer serializer = new XmlSerializer(typeof(List<StudentsGroup>));
                using (FileStream stream = File.OpenRead($"{classDir}{filePathClasses}")) // Читаем из файла
                {
                    classes = serializer.Deserialize(stream) as List<StudentsGroup>;
                }
            }
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            AddDataWindow addDataWindow = new AddDataWindow();
            addDataWindow.ShowDialog();
        }

        private void AddSchedule_Click(object sender, RoutedEventArgs e)
        {
            Load();
            
            GenAlgorithm genAlgorithm = new GenAlgorithm(classes, teachers, subjects, auditoriums);

            int populationSize = 100;
            int offspringCount = 50;
            int maxGenerations = 100;
            double mutationRate = 0.05;

            List<Chromosome> finalPopulation = genAlgorithm.RunGeneticAlgorithm(populationSize, offspringCount, maxGenerations, mutationRate);
            Chromosome bestChromosome = finalPopulation.OrderByDescending(chromosome => chromosome.Fitness).First();

            DisplaySchedule(bestChromosome);
            // Анализируйте и используйте наилучшую хромосому для составления расписания

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
            if (!Directory.Exists(scheduleDir))
            {
                Directory.CreateDirectory(scheduleDir);
            }

        }

        private void SaveSchedule_Click(object sender, RoutedEventArgs e)
        {
            // Создаем новый документ XML
            XmlDocument xmlDoc = new XmlDocument();

            // Создаем корневой элемент
            XmlElement rootElement = xmlDoc.CreateElement("Table");
            xmlDoc.AppendChild(rootElement);

            // Перебираем элементы таблицы и сохраняем их в XML
            for (int row = 0; row < gridSchedule.RowDefinitions.Count; row++)
            {
                for (int column = 0; column < gridSchedule.ColumnDefinitions.Count; column++)
                {
                    // Получаем TextBlock в текущей ячейке
                    TextBlock textBlock = gridSchedule.Children
                        .OfType<TextBlock>()
                        .FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);

                    if (textBlock != null)
                    {
                        // Создаем элемент для ячейки
                        XmlElement cellElement = xmlDoc.CreateElement("Cell");

                        // Устанавливаем атрибуты строки и столбца
                        cellElement.SetAttribute("Row", row.ToString());
                        cellElement.SetAttribute("Column", column.ToString());

                        // Устанавливаем текст ячейки
                        cellElement.InnerText = textBlock.Text;

                        // Добавляем ячейку в корневой элемент
                        rootElement.AppendChild(cellElement);
                    }
                }
            }
            NameScheduleSaveWindow nameScheduleSaveWindow = new NameScheduleSaveWindow();
            nameScheduleSaveWindow.ShowDialog();

            if (filePathSchedule != "")
            {
                // Сохраняем XML файл
                xmlDoc.Save($"{scheduleDir}{filePathSchedule}.xml");
            }

        }

        private void LoadSchedule_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML files (*.xml)|*.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;

                try
                {
                    // Загрузка XML файла
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(fileName);

                    // Получение всех элементов Cell из XML
                    XmlNodeList cellNodes = xmlDoc.SelectNodes("//Cell");

                    // Обновление данных в таблице
                    foreach (XmlNode cellNode in cellNodes)
                    {
                        int row = int.Parse(cellNode.Attributes["Row"].Value);
                        int column = int.Parse(cellNode.Attributes["Column"].Value);
                        string text = cellNode.InnerText;

                        // Поиск TextBlock в таблице по координатам
                        TextBlock textBlock = GetTextBlockByCoordinates(row, column);
                        if (textBlock != null)
                        {
                            // Обновление содержимого TextBlock
                            textBlock.Text = text;
                        }
                    }

                    MessageBox.Show("Расписание загружено из файла " + fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке файла: " + ex.Message);
                }
            }
        }

        private TextBlock GetTextBlockByCoordinates(int row, int column)
        {
            foreach (UIElement element in gridSchedule.Children)
            {
                if (element is TextBlock textBlock && Grid.GetRow(textBlock) == row && Grid.GetColumn(textBlock) == column)
                {
                    return textBlock;
                }
            }

            return null;
        }

        private void HelpButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.ShowDialog();
        }

        private void ExitButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти? Несохраненные данные будут утеряны!", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}

