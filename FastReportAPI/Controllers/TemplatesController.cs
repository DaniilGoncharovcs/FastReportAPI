namespace FastReportAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TemplatesController : Controller
{
    readonly TemplatesContext _context;
    readonly IWebHostEnvironment _environment;
    public TemplatesController(TemplatesContext context, IWebHostEnvironment environment)
        => (_context,_environment) = (context,environment);

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
    
    public async Task<IActionResult> Post(Template template)
    {
        if (template != null)
        {
            await _context.AddAsync(template);
            await _context.SaveChangesAsync();
            return Ok();
        }
        return BadRequest();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Template template)
    {
        var dbTemplate = await _context.Templates.FirstOrDefaultAsync(t => t.Id == id);
        if (dbTemplate == null) return NotFound("Такого шаблона нет");
        if (template != null)
        {
            dbTemplate.Path = template.Path;
            await _context.SaveChangesAsync();
            return Ok();
        }
        return BadRequest("путь к файлу не может быть пустым");
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var dbTemplate = await _context.Templates.FirstOrDefaultAsync(t => t.Id == id);
        if (dbTemplate == null) return NotFound("Такого шаблона нет");
        _context.Templates.Remove(dbTemplate);
        await _context.SaveChangesAsync();
        return Ok();
    }
}