using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using ModelLayer.Controllers;

namespace ViewModelLayer
{
    /// <summary>
    /// ViewModel окна добавления студентов с валидацией
    /// </summary>
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

            // Для валидации будем прослушивать изменения своих свойств
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

        /// <summary>
        /// Создать нового студента и очистить форму
        /// </summary>
        public ICommand AddStudentCommand =>
            _addStudentCommand ?? (_addStudentCommand = new RelayCommand(AddStudent));

        /// <summary>
        /// Имя нового студента
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        /// <summary>
        /// Группа нового студента
        /// </summary>
        public string Group
        {
            get => _group;
            set => SetField(ref _group, value);
        }

        /// <summary>
        /// Специальность нового студента
        /// </summary>
        public string Speciality
        {
            get => _speciality;
            set => SetField(ref _speciality, value);
        }

        /// <summary>
        /// Доступна ли для нажатия кнопка "Добавить"
        /// Это поле обновляется при изменения имени, группы или специальности после валидации
        /// </summary>
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
            IsAddButtonEnabled = Name.Length > 0 && Group.Length > 0 && Speciality.Length > 0;
        }
    }
}