using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using ModelLayer.Controllers;

namespace ViewModelLayer
{
    public class AddStudentViewModel : BaseViewModel
    {
        private IStudentsController _studentsController;
        private ICommand _addStudentCommand;
        private string _name = "";
        private string _group = "";
        private string _speciality = "";
        private bool _isAddButtonEnabled = false;

        public AddStudentViewModel(IStudentsController studentsController)
        {
            _studentsController = studentsController;

            PropertyChanged += ValidateFormPropertyChanged;
        }

        private void ValidateFormPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Name) || e.PropertyName == nameof(Group) ||
                e.PropertyName == nameof(Speciality))
            {
                ValidateForm();
            }
        }

        public ICommand AddStudentCommand =>
            _addStudentCommand ?? (_addStudentCommand = new RelayCommand(AddStudent));

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public string Group
        {
            get => _group;
            set => SetField(ref _group, value);
        }

        public string Speciality
        {
            get => _speciality;
            set => SetField(ref _speciality, value);
        }

        public bool IsAddButtonEnabled
        {
            get => _isAddButtonEnabled;
            set => SetField(ref _isAddButtonEnabled, value);
        }

        private void AddStudent()
        {
            _studentsController.Add(Name, Speciality, Group);
            Name = "";
            Speciality = "";
            Group = "";
        }

        private void ValidateForm()
        {
            Debug.Print("Валидация");
            IsAddButtonEnabled = Name.Length > 0 && Group.Length > 0 && Speciality.Length > 0;
        }
    }
}