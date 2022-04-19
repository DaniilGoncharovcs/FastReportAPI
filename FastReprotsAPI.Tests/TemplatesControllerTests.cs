using Castle.Core.Logging;
using FastReportAPI.Controllers;
using FastReportAPI.Data;
using FastReportAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FastReprotsAPI.Tests
{
    public class TemplatesControllerTests
    {
        private readonly TestHelper testHelper;
        public TemplatesControllerTests()
        {
            testHelper = new TestHelper();
        }
        [Fact]
        public async Task CreateIfTheNameIsNotExisted()
        {
            //Arrange
            var context = testHelper.TemplatesContext;
            context.Templates.AddRange(GetTestTemplates());
            await context.SaveChangesAsync();
            var templatesController = new TemplatesController(context, null, null);

            //Act
            var result = templatesController.Get();

            //Assert
            var actionResult = Assert.IsType<ActionResult>(result);
            var model = Assert.IsAssignableFrom<List<Template>>(result.Result.Value);
            Assert.Equal(GetTestTemplates().Count(),model.Count);
        }
        private List<Template> GetTestTemplates()
        {
            var templates = new List<Template>
            {
                new Template { Id=2, Name="template1"},
                new Template { Id=3, Name="template2"},
                new Template { Id=4, Name="template3"},
                new Template { Id=5, Name="template4"}
            };
            return templates;
        }
    }
}
