using System;

namespace Codevil.TemplateRepository.Factories
{
    public class MappingNotFoundException : Exception
    {
        public MappingNotFoundException(string message) : base(message)
        {
        }
    }
}