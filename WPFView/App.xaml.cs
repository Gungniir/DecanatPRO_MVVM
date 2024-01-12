using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ModelLayer;
using Ninject;

namespace WPFView
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel _kernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _kernel = new StandardKernel(new SimpleConfigModule());

            MainWindow mainWindow = _kernel.Get<MainWindow>();
            mainWindow.Show();
        }
    }
}
