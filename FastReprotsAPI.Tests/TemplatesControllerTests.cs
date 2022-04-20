using Castle.Core.Logging;
using FastReportAPI.Controllers;
using FastReportAPI.Data;
using FastReportAPI.Models;
using FastReportAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FastReprotsAPI.Tests
{
    public class TemplatesControllerTests
    {
        private readonly TestHelper testHelper;
        private TemplateViewModel testTemplate = new TemplateViewModel
        {
            Name = "TestTemplate"
        };
        public TemplatesControllerTests()
        {
            testHelper = new TestHelper();
        }
        [Fact]
        public async Task GetAllTemplates_ReturnsAllTemplates()
        {
            //Arrange

            var context = testHelper.TemplatesContext;
            var templatesController = new TemplatesController(context, null, null);

            //Act

            var result = (OkObjectResult)templatesController.Get().Result.Result;
            
            //Assert
            var model = (List<Template>)result.Value; 
            Assert.Equal(await context.Templates.ToListAsync(), model);
        }
        [Fact]
        public async Task GetTemplateById_ReturnTemplate()
        {
            //Arrange
            var id = 1;
            var context = testHelper.TemplatesContext;
            var templatesController = new TemplatesController(context, null, null);

            //Act
            var result = (OkObjectResult)templatesController.GetById(id).Result.Result;

            //Assert 
            var model = result.Value;
            Assert.Equal(await context.Templates.FirstOrDefaultAsync(m => m.Id == id), model);
        }
        [Fact]
        public async Task GetTemplateById_ReturnsNotFound()
        {
            //Arrange
            var id = 100;
            var context = testHelper.TemplatesContext;
            var templatesController = new TemplatesController(context, null, null);

            //Act
            var result = templatesController.GetById(id).Result.Result;

            //Assert 
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async Task PostIfTheNameIsNotExisted_ReturnsNoContentResult()
        {
            //Arrange

            var context = testHelper.TemplatesContext;
            var templatesController = new TemplatesController(context, null, null);
            
            //Act

            var result = templatesController.Post(testTemplate).Result;

            //Assert

            Assert.IsType<NoContentResult>(result);
        }
        
        [Fact]
        public async Task PostIfTheNameExisted_ReturnsBadRequestResult()
        {
            //Arrange
            testTemplate.Name = "Акт возобновления качественного предоставления услуг";
            var context = testHelper.TemplatesContext;
            var templatesController = new TemplatesController(context, null, null);
            
            //Act

            var result = templatesController.Post(testTemplate).Result;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task UpdateTemplate_ReturnsNoContentResult()
        {
            //Arrange
            var id = 1;
            testTemplate.Name = "шаблон1";
            var context = testHelper.TemplatesContext;
            var templatesController = new TemplatesController(context, null, null);

            //Act

            var result = templatesController.Update(id, testTemplate).Result;

            //Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task UpdateTemplateWithNonExistentId_RetrnsNotFoundObject()
        {
            //Arrange
            var id = 100;
            var context = testHelper.TemplatesContext;
            var templatesController = new TemplatesController(context, null, null);

            //Act

            var result = templatesController.Update(id, testTemplate).Result;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async Task UpdateTemplateWithNonExistentId_RetrnsBadRequest()
        {
            //Arrange
            var id = 1;
            testTemplate.Name = null;
            var context = testHelper.TemplatesContext;
            var templatesController = new TemplatesController(context, null, null);

            //Act

            var result = templatesController.Update(id, testTemplate).Result;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task DeleteTemplateWithId_NoContentResult()
        {
            //Arrange
            var id = 1;
            var context = testHelper.TemplatesContext;
            var templatesController = new TemplatesController(context, null, null);

            //Act

            var result = templatesController.Delete(id).Result;

            //Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task DeleteTemplateWithId_NotFoundObjectResult()
        {
            //Arrange
            var id = 100;
            var context = testHelper.TemplatesContext;
            var templatesController = new TemplatesController(context, null, null);

            //Act

            var result = templatesController.Delete(id).Result;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async Task UploadFile_ReturnsBadRequestObject()
        {
            //Arrange
            var context = testHelper.TemplatesContext;
            var templatesController = new TemplatesController(context, null, null);
            //Act
            var result = templatesController.UploadFile(null).Result;
            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
