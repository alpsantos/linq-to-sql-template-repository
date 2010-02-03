using Codevil.TemplateRepository.Entities;

namespace Codevil.TemplateRepository.Factories
{
    public interface IEntityFactory
    {
        object Create(object row);
    }
}
