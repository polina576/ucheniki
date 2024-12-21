using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace uchen
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        private List<Student> students = new List<Student>()
        {
            new Student { LastName = "Иванов", Initials = "И.И.", Class = 10, Grades = new List<Grade> { new Grade { Subject = "Алгебра", Score = 4 }, new Grade { Subject = "Алгебра", Score = 5 }, new Grade { Subject = "Алгебра", Score = 4 }, new Grade { Subject = "Геометрия", Score = 5 }, new Grade { Subject = "Геометрия", Score = 4 }, new Grade { Subject = "Информатика", Score = 4 }, new Grade { Subject = "Информатика", Score = 5 } } },
            new Student { LastName = "Петров", Initials = "П.П.", Class = 10, Grades = new List<Grade> { new Grade { Subject = "Алгебра", Score = 3 }, new Grade { Subject = "Алгебра", Score = 4 }, new Grade { Subject = "Геометрия", Score = 3 }, new Grade { Subject = "Геометрия", Score = 4 }, new Grade { Subject = "Информатика", Score = 5 }, new Grade { Subject = "Информатика", Score = 4 }, new Grade { Subject = "Информатика", Score = 5 } } },
            new Student { LastName = "Сидоров", Initials = "С.С.", Class = 11, Grades = new List<Grade> { new Grade { Subject = "Алгебра", Score = 4 }, new Grade { Subject = "Алгебра", Score = 5 }, new Grade { Subject = "Геометрия", Score = 4 }, new Grade { Subject = "Геометрия", Score = 5 }, new Grade { Subject = "Информатика", Score = 5 }, new Grade { Subject = "Информатика", Score = 4 }, new Grade { Subject = "Информатика", Score = 5 } } },
            new Student { LastName = "Кузнецов", Initials = "К.К.", Class = 9, Grades = new List<Grade> { new Grade { Subject = "Алгебра", Score = 5 }, new Grade { Subject = "Алгебра", Score = 4 }, new Grade { Subject = "Геометрия", Score = 4 }, new Grade { Subject = "Геометрия", Score = 5 } } },
            new Student { LastName = "Алексеев", Initials = "А.А.", Class = 9, Grades = new List<Grade> { new Grade { Subject = "Информатика", Score = 3 }, new Grade { Subject = "Информатика", Score = 4 } } },
            new Student { LastName = "Борисов", Initials = "Б.Б.", Class = 10, Grades = new List<Grade> { new Grade { Subject = "Алгебра", Score = 4 }, new Grade { Subject = "Алгебра", Score = 3 }, new Grade { Subject = "Алгебра", Score = 5 }, new Grade { Subject = "Геометрия", Score = 5 }, new Grade { Subject = "Геометрия", Score = 4 } } },
            new Student { LastName = "Васильев", Initials = "В.В.", Class = 11, Grades = new List<Grade> { new Grade { Subject = "Алгебра", Score = 3 }, new Grade { Subject = "Алгебра", Score = 4 }, new Grade { Subject = "Геометрия", Score = 4 }, new Grade { Subject = "Геометрия", Score = 5 }, new Grade { Subject = "Информатика", Score = 5 }, new Grade { Subject = "Информатика", Score = 3 } } },
            new Student { LastName = "Яковлев", Initials = "Я.Я.", Class = 11, Grades = new List<Grade> { } }
        };

        public MainWindow()
        {
            InitializeComponent();
            InitializeClassComboBox();
        }
        private void InitializeClassComboBox()
        {
            var distinctClasses = students.Select(s => s.Class).Distinct().OrderBy(c => c);
            foreach (var cls in distinctClasses)
            {
                ClassComboBox.Items.Add(cls);
            }
            if (ClassComboBox.Items.Count > 0)
                ClassComboBox.SelectedIndex = 0;
        }
        private void ClassComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StudentsListBox.Items.Clear();
            AverageGradeListBox.Items.Clear();

            if (ClassComboBox.SelectedItem == null)
                return;

            int selectedClass = (int)ClassComboBox.SelectedItem;
            var studentsOfClass = students.Where(s => s.Class == selectedClass)
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.Initials)
                .ToList();


            foreach (var student in studentsOfClass)
            {
                StudentsListBox.Items.Add($"{student.LastName} {student.Initials}");
            }
        }
        private void StudentsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AverageGradeListBox.Items.Clear();

            if (StudentsListBox.SelectedItem == null) return;


            string selectedStudentName = (string)StudentsListBox.SelectedItem;
            var student = students.FirstOrDefault(s => $"{s.LastName} {s.Initials}" == selectedStudentName);

            if (student == null) return;

            AverageGradeListBox.Items.Add($"Фамилия: {student.LastName}, Инициалы: {student.Initials}");

            var subjectGroups = student.Grades.GroupBy(g => g.Subject);

            foreach (var subjectGroup in subjectGroups)
            {
                string subjectName = subjectGroup.Key;
                string scores = string.Join(", ", subjectGroup.Select(g => g.Score));
                double average = subjectGroup.Average(g => g.Score);
                AverageGradeListBox.Items.Add($"  {subjectName}: ({scores}) - {average:F2}");
            }
            double averageGrade = 0;
            if (student.Grades.Any())
            {
                averageGrade = student.Grades.Average(g => g.Score);
            }
            AverageGradeListBox.Items.Add($"Общий средний балл: {averageGrade:F2}");
        }
        private string GetSubjectAverage(IEnumerable<dynamic> subjectAverages, string subject)
        {
            var avg = subjectAverages.FirstOrDefault(item => item.Subject == subject);
            if (avg == null)
                return "0.00";
            else
                return $"{avg.Average:F2}";

        }
    }
    }

