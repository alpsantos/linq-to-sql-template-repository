using System.Data.Linq;
using Codevil.TemplateRepository.Factories;

namespace Codevil.TemplateRepository.Data.Factories
{
    public class BankDataContextFactory : DataContextFactory
    {
        public override DataContext Create()
        {
            return new BankDataContext(@"Data Source=localhost;Initial Catalog=template_repository_tests;Integrated Security=SSPI");
        }
    }
}
