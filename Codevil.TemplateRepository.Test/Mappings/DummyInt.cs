using System.Data.Linq.Mapping;

namespace Codevil.TemplateRepository.Test.Mappings
{
    [Table(Name = TableName)]
    public class DummyInt
    {
        public const string TableName = "DummyInt";

        public int Int { get; set; }
    }
}
