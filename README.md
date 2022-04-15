# FastReportAPI
WebApi, которое позволяет получить из данных и шаблона отчёта требуемый документ.
Здесь используются следущий стек технологий:
- ASP NET CORE WEB API (сервер)
- Entity Framework Core (для взаимодействия с бд. В данном случае MySQL)
## БД
Создана бд со следующими таблицами
![image](https://user-images.githubusercontent.com/94749778/163531465-22647a0f-99d6-4a99-9465-114bf337537f.png)

- Таблица ***templates*** хранит id, путь к шаблону и его название. При инициализации БД в нее уже добавляется следующее значение:

![image](https://user-images.githubusercontent.com/94749778/163531649-ccabbe46-df89-44eb-810e-6417b323bad2.png)

- Таблица ***efmigrationhistory*** хранит данные о миграциях нашей бд
## TemplatesController
В нем содержаться CRUD операции связанные с таблицей ***templates***
#### GET
В контроллере реализовано два метода GET:
```cs
[HttpGet]
    public async Task<ActionResult<IEnumerable<Template>>> Get()
        => Ok(await _context.Templates.ToListAsync());
```
Просто возвращает все данные из таблицы ***templates***

![image](https://user-images.githubusercontent.com/94749778/163532857-99dfc7c2-4404-4d57-a663-2a1e52cbdc70.png)

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

![image](https://user-images.githubusercontent.com/94749778/163533039-5ad1b0be-83c1-483f-ac42-ab5cd6c67c94.png)

Если записи с данным id нет

![image](https://user-images.githubusercontent.com/94749778/163533083-b2f4f1a0-6046-4e02-87b7-2e3399ccbe22.png)
