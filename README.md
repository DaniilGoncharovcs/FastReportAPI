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
