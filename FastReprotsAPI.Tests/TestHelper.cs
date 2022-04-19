using FastReportAPI.Data;
using Microsoft.EntityFrameworkCore;
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
        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<TemplatesContext>();
            builder.UseInMemoryDatabase(databaseName: "TemplatesDbTest");

            var dbContextOptions = builder.Options;
            TemplatesContext = new TemplatesContext(dbContextOptions);
            TemplatesContext.Database.EnsureDeleted();
            TemplatesContext.Database.EnsureCreated();
        }
    }
}
