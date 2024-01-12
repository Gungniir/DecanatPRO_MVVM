using System.Windows;
using Ninject;

namespace Share
{
    public class WindowService
    {
        private readonly IKernel _kernel;

        public WindowService(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void OpenWindow<T>() where T : Window
        {
            var window = _kernel.Get<T>();
            window.Show();
        }
    }
}
