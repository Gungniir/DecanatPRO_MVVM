using ModelLayer.Controllers;
using ModelLayer.Models;
using ModelLayer.Repositories;
using ModelLayer.Repositories.Entity;
using Ninject.Modules;

namespace ModelLayer;

public class SimpleConfigModule: NinjectModule
{
    public override void Load()
    {
        Bind<IRepository<Student>>().To<StudentEntityRepository>().InSingletonScope();
        Bind<IStudentsController>().To<StudentsController>().InSingletonScope();
    }
}