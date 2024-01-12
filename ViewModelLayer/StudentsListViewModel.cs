using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using ModelLayer.Controllers;
using ModelLayer.Models;

namespace ViewModelLayer
{
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

            _students = new ObservableCollection<Student>(_studentsController.GetAll());
            _studentsController.Added += (o, student) => Students.Add(student);
        }

        public ICommand FillStudentsCommand =>
            _fillStudentsCommand ?? (_fillStudentsCommand = new RelayCommand(FillStudents));

        public ICommand DeleteSelectedStudentsCommand => _deleteSelectedStudentsCommand ??
                                                         (_deleteSelectedStudentsCommand =
                                                             new RelayCommand(DeleteSelectedStudents));

        public ObservableCollection<Student> SelectedStudents
        {
            get => _selectedStudents;
            set => SetField(ref _selectedStudents, value);
        }

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