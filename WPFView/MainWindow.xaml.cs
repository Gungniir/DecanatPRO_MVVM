using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ModelLayer.Models;
using ZedGraph;
using System.Drawing;
using System.Windows.Controls;
using Ninject;
using ViewModelLayer;

namespace WPFView
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly StudentsListViewModel _vm;
        private readonly IKernel _kernel;

        public MainWindow(StudentsListViewModel vm, IKernel kernel)
        {
            InitializeComponent();
            DataContext = vm;
            _vm = vm;
            _kernel = kernel;
            DrawZedGraph();


            // Нам нужно перерисовывать график при изменении внутри коллекции, а также при
            // обновлении всей коллекции (в этом случае нужно будет ещё раз повесить
            // обработчик события изменения коллекции)
            vm.Students.CollectionChanged += (sender, args) =>
                DrawZedGraph();

            vm.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName != nameof(StudentsListViewModel.Students)) return;

                vm.Students.CollectionChanged += (s, a) =>
                    DrawZedGraph();

                DrawZedGraph();
            };
        }

        private void DrawZedGraph()
        {
            var groups = _vm.Students.GroupBy(student => student.Speciality);
            GraphPane pane = ZedGraphControl.GraphPane;

            
            pane.CurveList.Clear();

            string[] names = groups.Select(group => group.Key).ToArray();
            double[] values = groups.Select(group => Convert.ToDouble(group.Count())).ToArray();

            BarItem curve = pane.AddBar("Гистограмма", null, values, Color.Blue);

            // Настроим ось X так, чтобы она отображала текстовые данные
            pane.XAxis.Type = AxisType.Text;

            // Уставим для оси наши подписи
            pane.XAxis.Scale.TextLabels = names;

            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            ZedGraphControl.AxisChange();

            // Обновляем график
            ZedGraphControl.Invalidate();
        }

        private void StudentsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _vm.SelectedStudents = new ObservableCollection<Student>(StudentsListView.SelectedItems.Cast<Student>());
        }

        private void ButtonOpenAddWindow_OnClick(object sender, RoutedEventArgs e)
        {
            var window = _kernel.Get<AddStudentWindow>();

            Hide();
            window.Closed += (o, args) => Show();
            window.Show();
        }
    }
}