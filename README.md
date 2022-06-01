# FastReportAPI
Web API для работы с шаблонами FastReport.
Запуск:

1) необходимо в решении назначить запускаемые проекты и выбрать там FastReport.Server и FastReport.Client

![image](https://user-images.githubusercontent.com/94749778/171412816-65c86eb5-4e60-4493-91d0-dd83062947d8.png)

2)запускаем проект и у нас откроется 2 окна в браузере, одно для сервера, второе для клиента

![image](https://user-images.githubusercontent.com/94749778/171413008-55c837aa-0c37-45c1-98db-c0320f7a566d.png)

![image](https://user-images.githubusercontent.com/94749778/171413116-5b728c43-63cb-43a1-aba5-4c9cf15f7e38.png)

Как работать в Клиенте:

1) Добавляем свой шаблон в БД:

![image](https://user-images.githubusercontent.com/94749778/171413513-2da6472b-4a75-473c-aa3c-b4365b3ddb97.png)



2) Загружаем файл шаблона на сервер. Он должен быть с расширением .frx. 

![image](https://user-images.githubusercontent.com/94749778/171413938-d77d3bca-f862-43ba-b600-b67fd41a9c55.png)

3) В списке шаблонов выбераем нужный для вас и жмем кнопку "Заполнить шаблон"

![image](https://user-images.githubusercontent.com/94749778/171414192-2679802a-2511-41b7-9d17-ab1e01fea642.png)

Вводим данные в формате JSON (Образцы JSON находятся в FastReport.Server/TestJson) и выбираем нужное расширение. После этого возвращаемся на главную страницу.

!!!Если вы не загрузили файл .frx, то экспорт производится не будет

Как работать в API:

1) Добавляем свой шаблон через данный метод

![image](https://user-images.githubusercontent.com/94749778/171413685-b16d2389-74bb-418b-8888-10f2f5c5fa8b.png)

2) Загружаем файл шаблона на сервер. Он должен быть с расширением .frx
 
![image](https://user-images.githubusercontent.com/94749778/171415077-dc07f131-b441-4ae1-b9f5-4f9f8e6fb9b7.png)

3) Для экспорта нужно эспользовать данный метод

![image](https://user-images.githubusercontent.com/94749778/171415316-bb20e845-3dc0-4064-b700-fedeca64e402.png)

- Поле Id - это id экспортируемого шаблона, чтобы узнать его воспользуетесь методом GET, который вернет список всех щаблонов.
- Поле Dictionary - это поле для ввода данных JSON
- Поле format - это поле выбора расширения экспортируемого файла. Для Pdf это 0, Html - 1 и т.д. Список всех расширений можно просмотреть здесь
https://github.com/DaniilGoncharovcs/FastReportAPI/blob/master/FastReportAPI/Services/ExportFormat.cs

- Поле imageFormat является необязательным. Его нужно использовать, когда вы экспортируете изображение, по умолчанию экспорт производится в .png
