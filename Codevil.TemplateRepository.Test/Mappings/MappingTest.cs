using Codevil.TemplateRepository.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codevil.TemplateRepository.Test.Mappings
{
    [TestClass]
    public class MappingTest 
    {
        [TestMethod]
        public void ShouldCallUpdateWhenCreating()
        {
            EntityMappings.AddMapping(new DummyIntToDummyStringMapping());
            var mapping = EntityMappings.GetMappingForEntity(typeof(DummyString));

            var entity = (DummyString) mapping.CreateEntity(new DummyInt { Int = 666 });
            Assert.AreEqual("666", entity.String);

            var row = (DummyInt)mapping.CreateRow(new DummyString { String = "666" });
            Assert.AreEqual(666, row.Int);
        }
    }
}
