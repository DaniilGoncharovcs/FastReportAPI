using FastReportAPI.Data;
using FastReportAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastReprotsAPI.Tests
{
    public class TestHelper
    {
        public readonly TemplatesContext TemplatesContext;
        public readonly IFastReportService FastReportService;
        public readonly IWebHostEnvironment WebHostEnvironment;
        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<TemplatesContext>();
            builder.UseInMemoryDatabase(databaseName: "TemplatesDbTest");

            var dbContextOptions = builder.Options;
            TemplatesContext = new TemplatesContext(dbContextOptions);
            TemplatesContext.Database.EnsureDeleted();
            TemplatesContext.Database.EnsureCreated();

            WebHostEnvironment = new EnvironmentHelper();
            FastReportService = new FastReportService(WebHostEnvironment);
        }
    }
}
