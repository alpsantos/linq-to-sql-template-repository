using System;

namespace Codevil.TemplateRepository.Mappings
{
    public class InvalidRowTypeException : Exception
    {
        public InvalidRowTypeException(Type type)
            : base(string.Format("Type {0} is not a valid LINQ table", type))
        {
            
        }
    }
}
