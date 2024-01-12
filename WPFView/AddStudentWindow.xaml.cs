using System.Windows;
using ViewModelLayer;

namespace WPFView
{
    /// <summary>
    /// Логика взаимодействия для AddStudentWindow.xaml
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        public AddStudentWindow(AddStudentViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = (AddStudentViewModel)DataContext;

            vm.AddStudentCommand.Execute(e);
            Close();
        }
    }
}
