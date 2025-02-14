<h2>Запуск</h2>

1) Клонирование репозитория : https://github.com/tertotyer/CatsTaskProject/new/master?filename=README.md <br />
2) Запись пользовательского api ключа (<a href="https://thecatapi.com/signup">get API key</a>) в <b>user-secrets</b> <br/>
<b>Команды:</b>
```
dotnet user-secrets init
```
```
dotnet user-secrets set "x-api-key" "YOUR_API_KEY"
```
<p align="center">
   <img width="900" src="https://github.com/user-attachments/assets/316c0b43-ebc4-454d-b034-4cd6a22f796d" />
</p>

<h2>Описание</h2>
<b>Стек технологий:</b> WPF, .NET 8, ReactiveUI.
<img width="400" align="right" src="https://github.com/user-attachments/assets/3ed3dc7b-2ba8-48bb-bae6-f8ffbb1db01e" />

<h4>Работа с API</h4>
Осуществляется с помощью <b>HttpClient</b>.<br/> Все запросы к API выполняются асинхронно.;
<h4>Получение списка котов</h4>

- При запуске ПО асинхронно загружается 20 пород котов с одной основной картинкой из API;  <br/>

- При пролистывании списка пород вниз за 2 итерации пролистывания до последнего элемента отправляется асинхронный запрос к API на выборку 20 следующих пород;

<h4>Работа с изображениями</h4>

- После загрузки данных изображения сохраняются локально в папке "Images";
- В последующем при всех запросах уже имеющиеся картинки будут загружаться локально, минимизируя сетевые запросы;
- При отсутствии основной картинки у породы первоначально отправляется запрос к API на одну картинку породы. При отсутствии таковой локально загружается картинка-заглушка;

<h4>Поиск и фильтрация изображений</h4>

- Поиск выполняется по наличию введенной строки в имени пород : первоначально отображаются породы, имена которых начинаются с введенной строки;
- Если в локальный поиск выдал нулевой результат, то отправляется запрос к API для поиска пород с именем, содержащих введенную строку;
- При нажатии кнопки "Enter" или кнопки поиска "Изображение лупы" на пользовательском интерфейсе отправляется запрос к API для поиска пород с именем, содержащих введенную строку;
- Если запрос к API ни к чему не привел, дальнейший ввод в текстовое поле будет игнорироваться до того момента, пока выборка сможет быть сформирована;
- Фильтрация пород происходит по стране происхождения с возможность выбора одной или нескольких стран через выпадающий список;

<img width="400" align="left" src="https://github.com/user-attachments/assets/c1b5dcfa-956d-42ef-b2b2-e8c33a2b73a8" Margin="0 0 5 0"/>

<h4>Модальное окно</h4>

- При открытии окна происходит запрос к API для получения N числа изображений, относящихся к выбранной породе;
- N число картинок каждые 5 секунд заменяют друг друга по кругу. Также у пользователя есть возможность перелистывать их самому.

<b>To be continued...</b>