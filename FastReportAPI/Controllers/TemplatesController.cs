using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;

namespace FastReportAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TemplatesController : Controller
{
    readonly TemplatesContext _context;
    readonly IWebHostEnvironment _environment;
    readonly IFastReportService _fastReportService;
    public TemplatesController(TemplatesContext context, IWebHostEnvironment environment, IFastReportService service)
        => (_context,_environment,_fastReportService) = (context,environment,service);

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Template>>> Get()
        => Ok(await _context.Templates.ToListAsync());
    [HttpGet("{id}")]
    public async Task<ActionResult<Template>> GetById(int id)
    {
        var template = await _context.Templates.FirstOrDefaultAsync(t => t.Id == id);
        if (template == null) return NotFound("Пользователя с данным id нет");
        return Ok(template);
    }
    [HttpPost]
    
    public async Task<IActionResult> Post(TemplateViewModel template)
    {
        if (template != null)
        {
            var containsThisName = await _context.Templates.FirstOrDefaultAsync(t => t.Name == template.Name);
            if (containsThisName != null) return BadRequest("Шаблон с таким именем уже существует");

            var dbtemplate = new Template
            {
                Name = template.Name,
            };
            await _context.AddAsync(dbtemplate);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        return BadRequest();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TemplateViewModel template)
    {
        var dbTemplate = await _context.Templates.FirstOrDefaultAsync(t => t.Id == id);
        if (dbTemplate == null) return NotFound("Такого шаблона нет");
        if (template != null && template.Name != null)
        {
            dbTemplate.Name = template.Name;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        return BadRequest("Передан пустой шаблон");
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var dbTemplate = await _context.Templates.FirstOrDefaultAsync(t => t.Id == id);
        if (dbTemplate == null) return NotFound("Такого шаблона нет");
        _context.Templates.Remove(dbTemplate);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null) return BadRequest();
        string dir = Path.Combine($"{_environment.ContentRootPath}\\wwwroot\\Templates");
        string filePath = Path.Combine(dir, file.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        return Ok("Upload Successful");
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> Export(int id, string dictionary, ExportFormat format, string imageDefaultFormat = "Png")
    {
        var template = await _context.Templates.FirstOrDefaultAsync(t => t.Id == id);
        if (template == null) return NotFound("Шаблона с таким id нет");
        var filePath = template.Name;
        if (dictionary == null || format == null) return BadRequest("Не переданы параметры и/или формат выходного файла");
        
        var json = JsonConvert.DeserializeObject<Dictionary<string,dynamic>>(dictionary);
        filePath = _fastReportService.FillReport(json,filePath,format, imageDefaultFormat);

        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(filePath, out var contentType))
        {
            contentType = "application/octet-stream";
        }
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
        return File(bytes, contentType, Path.GetFileName(filePath));
    }
    
}