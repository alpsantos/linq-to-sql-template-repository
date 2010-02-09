using System;
using Codevil.TemplateRepository.Factories;
using Codevil.TemplateRepository.Model.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codevil.TemplateRepository.Test.Factories
{
    [TestClass]
    public class EntityMappingsTest
    {
        [TestMethod]
        [ExpectedException(typeof(MappingNotFoundException))]
        public void ShouldThrowMappingNotFoundExceptionWhenMappingIsNotRegistered()
        {
            EntityMappings.GetMappingForEntity(typeof(String));
        }
    }
}
