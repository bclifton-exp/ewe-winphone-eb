using Ninject;

namespace Expedia.Injection
{
    public class ExpediaKernel
    {
        private static ExpediaKernel _instance;
        private readonly StandardKernel _kernel;

        protected ExpediaKernel()
        {
            _kernel = new StandardKernel();
            _kernel.Load(new ServiceModule(), new ViewModelModule());
        }

        public static ExpediaKernel Instance()
        {
            return _instance ?? (_instance = new ExpediaKernel());
        }

        public T Get<T>()
        {
            return _kernel.Get<T>();
        }
    }
}
