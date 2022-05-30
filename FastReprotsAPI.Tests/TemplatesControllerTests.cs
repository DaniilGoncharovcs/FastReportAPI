using FastReportAPI.Controllers;
using FastReportAPI.Data;
using FastReportAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            testTemplate.Name = "Акт возобновления качественного предоставления услуг";
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
            testTemplate.Name = "Акт возобновления качественного предоставления услуг";
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
        public async Task DeleteTemplateWithNonExistentId_NotFoundObjectResult()
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
        [Fact]
        public async Task ExportWithNonExistentId_ReturnsNotFoundObject()
        {
            //Arrange

            var context = testHelper.TemplatesContext;
            var templatesController = new TemplatesController(context, null, null);

            //Act

            var result = templatesController.Export(100,null,ExportFormat.Pdf).Result;

            //Assert

            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async Task ExportNullDictionaryOrNullFormat_ReturnsBadRequestObject()
        {
            //Arrange

            var context = testHelper.TemplatesContext;
            var templatesController = new TemplatesController(context, null, null);

            //Act

            var result = templatesController.Export(1,null,ExportFormat.Pdf).Result;

            //Assert

            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        /*[Fact]
        public async Task ExportFile_ReturnsPdf()
        {
            //Arrange
            var id = 1;
            var testData = new Dictionary<string, string>
            {
                { "Day","15" },
                { "Month","04" },
                { "Year","22" },
                { "Organization","Oraginization" },
                { "OrganizationPosition","Position" },
                { "Owner","Owner" },
                { "Address","Address" },
                { "InspectorPost","InspectorPost" },
                { "InspectorFio", "InspectorFio" },
                { "ConsumerFio", "ConsumerFio" },
                { "ResourceFio", "ResourceFio" },
                { "Hours", "Hours" },
                { "Minutes", "Minutes" },
                { "Service", "Service" },
                { "Cause", "Cause" },
                { "Flat", "Flat" },
                { "House", "House" },
                { "Street", "Street" },
                { "City", "City" }
            };
            var format = "pdf";
            var context = testHelper.TemplatesContext;
            var service = testHelper.FastReportService;
            var templatesController = new TemplatesController(context, null, service);
            


            //Act
            var result = (FileContentResult)templatesController.Export(id, testData, format).Result;
            var filesDir = testHelper.WebHostEnvironment.ContentRootPath;
            var testFile = Path.Combine(filesDir, $"testFile.pdf");
            var exportFile = Path.Combine(filesDir,
            $"wwwroot\\Files\\{context.Templates.FirstOrDefault(t => t.Id == id).Name}.pdf");

            //Assert
            Assert.IsType<FileContentResult>(result);

            int bytes1, bytes2;
            bool equal;
            using(var teststream = new FileStream(testFile, FileMode.Open, FileAccess.Read))
            {
                using (var stream = new FileStream(exportFile, FileMode.Open, FileAccess.Read)){
                    
                    if(teststream.Length != stream.Length)
                    {
                        equal = false;
                    }
                    else
                    {
                        do
                        {
                            bytes1 = teststream.ReadByte();
                            bytes2 = stream.ReadByte();
                        } while ((bytes1 == bytes2) && (bytes1 != -1));
                        Assert.Equal(bytes1, bytes2);
                    }

                }
            }

 
        }
        private string GetHash(Stream stream)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(stream), 0);
        }*/
    }
}
