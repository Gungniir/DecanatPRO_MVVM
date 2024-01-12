using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using ModelLayer.Controllers;
using ModelLayer.Models;

namespace ViewModelLayer
{
    /// <summary>
    /// ViewModel для главного окна - списка студентов
    /// </summary>
    public class StudentsListViewModel : BaseViewModel
    {
        private IStudentsController _studentsController;
        private ICommand _fillStudentsCommand;
        private ICommand _deleteSelectedStudentsCommand;
        private ObservableCollection<Student> _students;
        private ObservableCollection<Student> _selectedStudents;

        public StudentsListViewModel(IStudentsController studentsController)
        {
            _studentsController = studentsController;

            // Получаем список студентов при первом запуске
            _students = new ObservableCollection<Student>(_studentsController.GetAll());

            // Так как студент создаётся в отдельном окне, которое напрямую с нами не связано,
            // то нужно слушать событие добавления студента в контроллере
            _studentsController.Added += (o, student) => Students.Add(student);
        }

        /// <summary>
        /// Добавить в таблицу случайных студентов
        /// </summary>
        public ICommand FillStudentsCommand =>
            _fillStudentsCommand ?? (_fillStudentsCommand = new RelayCommand(FillStudents));

        /// <summary>
        /// Удалить выбранных в таблице студентов
        /// </summary>
        public ICommand DeleteSelectedStudentsCommand => _deleteSelectedStudentsCommand ??
                                                         (_deleteSelectedStudentsCommand =
                                                             new RelayCommand(DeleteSelectedStudents));

        /// <summary>
        /// Выбранные студенты
        /// Заполняется в MainWindow при событии изменения выбора у ListView
        /// </summary>
        public ObservableCollection<Student> SelectedStudents
        {
            get => _selectedStudents;
            set => SetField(ref _selectedStudents, value);
        }

        /// <summary>
        /// Список студентов для отображения
        /// </summary>
        public ObservableCollection<Student> Students
        {
            get => _students;
            private set => SetField(ref _students, value);
        }

        private void FillStudents()
        {
            _studentsController.CreateRandomStudents();

            Students = new ObservableCollection<Student>(_studentsController.GetAll());
        }

        private void DeleteSelectedStudents()
        {
            foreach (var student in SelectedStudents)
            {
                Students.Remove(student);
                _studentsController.DeleteById(student.Id);
            }
        }
    }
}