# FastReportAPI
WebApi, которое позволяет получить из данных и шаблона отчёта требуемый документ.
Здесь используются следущий стек технологий:
- ASP NET CORE WEB API (сервер)
- Entity Framework Core (для взаимодействия с бд. В данном случае MySQL)
## БД
Создана бд со следующими таблицами

![image](https://user-images.githubusercontent.com/94749778/163531465-22647a0f-99d6-4a99-9465-114bf337537f.png)

- Таблица ***templates*** хранит id, путь к шаблону и его название. При инициализации БД в нее уже добавляется следующее значение:

![image](https://user-images.githubusercontent.com/94749778/163812931-0b96f83d-5e58-4623-802e-6ef5ea72bb02.png)

- Таблица ***efmigrationhistory*** хранит данные о миграциях нашей бд
## TemplatesController
В нем содержаться CRUD операции связанные с таблицей ***templates***
#### GET
В контроллере реализованы следующие методы GET:
```cs
[HttpGet]
    public async Task<ActionResult<IEnumerable<Template>>> Get()
        => Ok(await _context.Templates.ToListAsync());
```
Просто возвращает все данные из таблицы ***templates***

![image](https://user-images.githubusercontent.com/94749778/163813009-0e9f77ef-47a0-4ef8-af09-6698270d8a37.png)

```cs
[HttpGet("{id}")]
    public async Task<ActionResult<Template>> GetById(int id)
    {
        var template = await _context.Templates.FirstOrDefaultAsync(t => t.Id == id);
        if (template == null) return NotFound("Пользователя с данным id нет");
        return Ok(template);
    }
```
Возвращает данные об одной записи по id

![image](https://user-images.githubusercontent.com/94749778/163813027-6d44d6ec-a916-4a01-a27d-7102bda2bc57.png)

Если записи с данным id нет

![image](https://user-images.githubusercontent.com/94749778/163533083-b2f4f1a0-6046-4e02-87b7-2e3399ccbe22.png)

```cs
[HttpGet("[action]")]
    public async Task<IActionResult> DownloadFile(int id, [FromQuery] Dictionary<string, string> dictionary, string format)
    {
        var template = await _context.Templates.FirstOrDefaultAsync(t => t.Id == id);
        if (template == null) return NotFound("Шаблона с таким id нет");

        var filePath = template.Name;
        filePath = _fastReportService.FillReport(dictionary,filePath,format);

        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(filePath, out var contentType))
        {
            contentType = "application/octet-stream";
        }
        var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
        return File(bytes, contentType, Path.GetFileName(filePath));
    }
```
На вход принимает список атрибутов шаблона, его id и расширение скачиваемого файла
Создает файл заполненного документа из шаблона(id хранится в БД) и скачивает его
Передадим следующие параметры:

![image](https://user-images.githubusercontent.com/94749778/163782376-8a05fa17-0303-4ecc-a0f9-1ee68b5696eb.png)

И на выходе получим готовый файл

![image](https://user-images.githubusercontent.com/94749778/163782623-0df6b726-fd44-42bb-a4b0-9eaa14c90b70.png)
#### POST
В контроллере реализованы следующие методы POST:
```cs
[HttpPost]
public async Task<IActionResult> Post(TemplateViewModel template)
    {
        if (template != null)
        {
            var dbtemplate = new Template
            {
                Name = template.Name,
            };
            await _context.AddAsync(dbtemplate);
            await _context.SaveChangesAsync();
            return Ok();
        }
        return BadRequest();
    }
```
Введем данные

![image](https://user-images.githubusercontent.com/94749778/163813492-4dc5c5d9-e1b4-4e9b-abb1-962175f4a6d9.png)

И просмотрим что хранится в бд

![image](https://user-images.githubusercontent.com/94749778/163813652-72057207-1c63-4886-96e0-32b3ea73e0c4.png)

```cs
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
```

Данный метод загружает выбраный файл в папку wwwroot
Загрузим новый шаблон документа

![image](https://user-images.githubusercontent.com/94749778/163813903-4a189f90-516f-4b82-a4f1-0ff2d5986e62.png)

Получим следующее сообщение 

![image](https://user-images.githubusercontent.com/94749778/163813981-2a3a282d-5c51-4b7b-9a0b-c6153ce94c73.png)

Проверем наличие файла

![image](https://user-images.githubusercontent.com/94749778/163814370-a4813264-da88-4720-9485-cd277a9547bd.png)


