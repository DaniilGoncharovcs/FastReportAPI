using Moq;
using FastReportAPI.Controllers;
using FastReportAPI.Data;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FastReprotsAPI.Tests
{
    public class TemplatesControllerTests
    {
        [Fact]
        public void Get_Templates_Returns_All_Templates()
        {
            //Arrange
            var mock = new Mock<TemplatesContext>();
            mock.Setup(context => context.Templates.ToList()).Returns(GetTestTemplates());
            var controller = new TemplatesController(mock.Object,null,null);

            //Act
            var result = controller.Get();
            //Assert
            var actionResult = Assert.IsType<ActionResult>(result);
            var model = Assert.IsAssignableFrom<List<Template>>(result.Result);
            Assert.Equal(GetTestTemplates().Count(), model.Count);
        }
        private List<Template> GetTestTemplates()
        {
            var users = new List<Template>
            {
                new Template { Id=1, Name="template1"},
                new Template { Id=2, Name="template2"},
                new Template { Id=3, Name="template3"},
                new Template { Id=4, Name="template4"}
            };
            return users;
        }
    }
}