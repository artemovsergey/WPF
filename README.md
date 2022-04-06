# WPF
Learning WPF

## Компоновка

В WPF компоновка осуществляется при помощи специальных контейнеров. Фреймворк предоставляет нам следующие контейнеры: **Grid**, **UniformGrid**, **StackPanel**, **WrapPanel**, **DockPanel** и **Canvas**.

В WPF при компоновке и расположении элементов внутри окна нам надо придерживаться следующих принципов:

- Нежелательно указывать явные размеры элементов (за исключеним минимальных и максимальных размеров). Размеры должны определяться контейнерами.
- Нежелательно указывать явные позицию и координаты элементов внутри окна. Позиционирование элементов всецело должно быть прерогативой контейнеров. И контейнер сам должен определять, как элемент будет располагаться. Если нам надо создать сложную систему компоновки, то мы можем вкладывать один контейнер в другой, чтобы добиться максимально удобного расположения элементов управления.

### Grid
Это наиболее мощный и часто используемый контейнер, напоминающий обычную таблицу. Он содержит столбцы и строки, количество которых задает разработчик. Для определения строк используется свойство RowDefinitions, а для определения столбцов - свойство ColumnDefinitions:

```xaml

<Grid.RowDefinitions>
    <RowDefinition></RowDefinition>
    <RowDefinition></RowDefinition>
    <RowDefinition></RowDefinition>
</Grid.RowDefinitions>
<Grid.ColumnDefinitions>
    <ColumnDefinition></ColumnDefinition>
    <ColumnDefinition></ColumnDefinition>
    <ColumnDefinition></ColumnDefinition>
</Grid.ColumnDefinitions>

```
Чтобы задать позицию элемента управления с привязкой к определенной ячейке Grid, в разметке элемента нужно прописать значения свойств Grid.Column и Grid.Row, тем самым указывая, в каком столбце и строке будет находиться элемент. Кроме того, если мы хотим растянуть элемент управления на несколько строк или столбцов, то можно указать свойства Grid.ColumnSpan и Grid.RowSpan, как в следующем примере:

```xaml
<Window x:Class="LayoutApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:LayoutApp"
        mc:Ignorable="d"
        Title="Grid" Height="250" Width="350">
    <Grid ShowGridLines="True">
        <Grid.RowDefinitions>
            <RowDefinition></RowDefinition>
            <RowDefinition></RowDefinition>
            <RowDefinition></RowDefinition>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition></ColumnDefinition>
            <ColumnDefinition></ColumnDefinition>
            <ColumnDefinition></ColumnDefinition>
        </Grid.ColumnDefinitions>
        <Button Grid.Column="0" Grid.Row="0" Content="Строка 0 Столбец 0"  />
        <Button Grid.Column="0" Grid.Row="1" Content="Объединение трех столбцов" Grid.ColumnSpan="3"  />
        <Button Grid.Column="2" Grid.Row="2" Content="Строка 2 Столбец 2"  />
    </Grid>
</Window>

```
**Примечание**: Атрибут ShowGridLines="True" у элемента Grid задает видимость сетки, по умолчанию оно равно False.

## Установка размеров
Здесь столбец или строка занимает то место, которое им нужно

```xaml
<ColumnDefinition Width="Auto" />
<RowDefinition Height="Auto" />
```
Также абсолютные размеры можно задать в пикселях, дюймах, сантиметрах или точках:

# UniformGrid
Аналогичен контейнеру Grid контейнер UniformGrid, только в этом случае все столбцы и строки одинакового размера и используется упрощенный синтаксис для их определения:

```xaml
<UniformGrid Rows="2" Columns="2">
    <Button Content="Left Top" />
    <Button Content="Right Top" />
    <Button Content="Left Bottom" />
    <Button Content="Right Bottom" />
</UniformGrid>
```
**Примечание**: по умолчанию контейнер только один в элементе Window 


## GridSplitter

```xaml
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*" />
        <ColumnDefinition Width="Auto" />
        <ColumnDefinition Width="*" />
    </Grid.ColumnDefinitions>
    <Button Grid.Column="0" Content="Левая кнопка" />
    <GridSplitter Grid.Column="1" ShowsPreview="False" Width="3"
        HorizontalAlignment="Center" VerticalAlignment="Stretch" />
    <Button Grid.Column="2" Content="Правая кнопка" />
</Grid>
```

## StackPanel
Это более простой элемент компоновки. Он располагает все элементы в ряд либо по горизонтали, либо по вертикали в зависимости от ориентации. Например,

```xaml
<Window x:Class="LayoutApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:LayoutApp"
        mc:Ignorable="d"
        Title="StackPanel" Height="300" Width="300">
    <Grid>
        <StackPanel>
            <Button Background="Blue" Content="1" />
            <Button Background="White" Content="2" />
            <Button Background="Red" Content="3" />
        </StackPanel>
    </Grid>
</Window>
```

## DockPanel
Этот контейнер прижимает свое содержимое к определенной стороне внешнего контейнера. Для этого у вложенных элементов надо установить сторону, к которой они будут прижиматься с помощью свойства DockPanel.Dock. Например,

<Window x:Class="LayoutApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:LayoutApp"
        mc:Ignorable="d"
        Title="DockPanel" Height="250" Width="300">
    <DockPanel LastChildFill="True">
        <Button DockPanel.Dock="Top" Background="AliceBlue" Content="Верхняя кнопка" />
        <Button DockPanel.Dock="Bottom" Background="BlanchedAlmond" Content="Нижняя кнопка" />
        <Button DockPanel.Dock="Left" Background="Aquamarine" Content="Левая кнопка" />
        <Button DockPanel.Dock="Right" Background="DarkGreen" Content="Правая кнопка" />
        <Button Background="LightGreen" Content="Центр" />
    </DockPanel>
</Window>

Контейнер DockPanel особенно удобно использовать для создания стандартных интерфейсов, где верхнюю и левую часть могут занимать какие-либо меню, нижнюю - строка состояния, правую - какая-то дополнительная информация, а в центре будет находиться основное содержание.

## WrapPanel

Эта панель, подобно StackPanel, располагает все элементы в одной строке или колонке в зависимости от того, какое значение имеет свойство Orientation - Horizontal или Vertical. Главное отличие от StackPanel - если элементы не помещаются в строке или столбце, создаются новые столбец или строка для не поместившихся элементов.

```xaml
<Window x:Class="LayoutApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:LayoutApp"
        mc:Ignorable="d"
        Title="WrapPanel" Height="250" Width="300">
    <WrapPanel>
        <Button Background="AliceBlue" Content="Кнопка 1" />
        <Button Background="Blue" Content="Кнопка 2" />
        <Button Background="Aquamarine" Content="Кнопка 3" Height="30"/>
        <Button Background="DarkGreen" Content="Кнопка 4" Height="20"/>
        <Button Background="LightGreen" Content="Кнопка 5"/>
        <Button Background="RosyBrown" Content="Кнопка 6" Width="80" />
        <Button Background="GhostWhite" Content="Кнопка 7" />
    </WrapPanel>
</Window>
```

## Canvas

Контейнер Canvas является наиболее простым контейнером. Для размещения на нем необходимо указать для элементов точные координаты относительно сторон Canvas. Для установки координат элементов используются свойства Canvas.Left, Canvas.Right, Canvas.Bottom, Canvas.Top. Например, свойство Canvas.Left указывает, на сколько единиц от левой стороны контейнера будет находиться элемент, а свойство Canvas.Top - насколько единиц ниже верхней границы контейнера находится элемент.

<Window x:Class="Layout.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="MainWindow" Height="300" Width="300">
    <Grid>
        <Canvas Background="Lavender">
            <Button Background="AliceBlue" Content="Top 20 Left 40" Canvas.Top="20" Canvas.Left="40" />
            <Button Background="LightSkyBlue" Content="Top 20 Right 20" Canvas.Top="20" Canvas.Right="20"/>
            <Button Background="Aquamarine" Content="Bottom 30 Left 20" Canvas.Bottom="30" Canvas.Left="20"/>
            <Button Background="LightCyan" Content="Bottom 20 Right 40" Canvas.Bottom="20" Canvas.Right="40"/>
        </Canvas>
    </Grid>
</Window>

## Элементы управления 
https://metanit.com/sharp/wpf/5.1.php

## DependencyObject и свойства зависимостей
https://metanit.com/sharp/wpf/13.php

## Модель событий
https://metanit.com/sharp/wpf/6.php

## Команды
https://metanit.com/sharp/wpf/7.1.php

## Кисти
https://metanit.com/sharp/wpf/8.php

## Ресурсы
https://metanit.com/sharp/wpf/9.php

## Привязка
https://metanit.com/sharp/wpf/11.php

## Стили, триггеры и темы
https://metanit.com/sharp/wpf/10.php

## Приложение и класс Application
https://metanit.com/sharp/wpf/3.php

## Шаблоны элементов управления
https://metanit.com/sharp/wpf/12.php

## Работа с данными
https://metanit.com/sharp/wpf/14.1.php

## Взаимодействие с базой данных (SQL Server)

Для операции добавления новой записи в таблицу применим хранимую процедуру

Итак, мы создали базу данных и таблицу, и сделаем последний шаг - добавим в базу данных харнимую процедуру, которая осуществляет добавление нового объекта в базу данных. Для этого выберем в узле базы данных пункт Programmability->Stored Procedures. Нажмем на этот узел правой кнопкой мыши и в контекстном меню выберем пункт Stored Procedure...:

```sql

CREATE PROCEDURE [dbo].[sp_InsertPhone]
    @title nvarchar(50),
    @company nvarchar(50),
    @price int,
    @Id int out
AS
    INSERT INTO Phones (Title, Company, Price)
    VALUES (@title, @company, @price)
   
    SET @Id=SCOPE_IDENTITY()
GO

```

Добавим в проект файл ```app.config```. Настройки для подключения базы данных

```xml

<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<startup>
		<supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8" />
	</startup>
	<connectionStrings>
		<add name="DefaultConnection"
			 connectionString="Data Source=localhost;Initial Catalog=MobileStore;Integrated Security=True"
			providerName="System.Data.SqlClient"/>
	</connectionStrings>
</configuration>

```

Атрибут connectionString собственно хранит строку подключения. Он состоит из трех частей:

Data Source=localhost: указывает на название сервера. По умолчанию для MS SQL Server Express используется "localhost"

Initial Catalog=mobiledb: название базы данных. Так как база данных называется mobiledb, то соответственно здесь данное название и указываем

Integrated Security=True: задает режим аутентификации

Так как мы будем подключаться к базе данных MS SQL Server, то соответственно мы будем использовать провайдер для SQL Server, функциональность которого заключена в пространстве имен System.Data.SqlClient.

Далее, вывод данных в DataGrid. AutoGenerateColumns="False" позволяет делать привязку к нужным столбцам

```xaml

        <DataGrid AutoGenerateColumns="False" x:Name="phonesGrid">
            <DataGrid.Columns>
                <DataGridTextColumn Binding="{Binding Title}" Header="Модель" Width="120"/>
                <DataGridTextColumn Binding="{Binding Company}" Header="Производитель" Width="125"/>
                <DataGridTextColumn Binding="{Binding Price}" Header="Цена" Width="80"/>
            </DataGrid.Columns>
        </DataGrid>

```

Вся работа с бд производится стандартными средствами ADO.NET и прежде всего классом SqlDataAdapter. Вначале мы получаем в конструкторе строку подключения, которая определена выше в файле app.config:

```csharp
connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
```

Чтобы задействовать эту функциональность, нам надо добавить в проект библиотеку **System.Configuration.dll**.

Далее в обработчике загрузки окна Window_Loaded создаем объект SqlDataAdapter:


```csharp 
adapter = new SqlDataAdapter(command);
```

В качестве команды для добавления объекта устанавливаем ссылку на хранимую процедуру:

```csharp
adapter.InsertCommand = new SqlCommand("sp_InsertPhone", connection);
```
Получаем данные из БД и осуществляем привязку:

```csharp
adapter.Fill(phonesTable);
phonesGrid.ItemsSource = phonesTable.DefaultView;
```

За обновление отвечает метод UpdateDB():

```csharp

private void UpdateDB()
{
    SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter);
    adapter.Update(phonesTable);
}

```
Чтобы обновить данные через SqlDataAdapter, нам нужна команда обновления, которую можно получить с помощью объекта SqlCommandBuilder. Для самого обновления вызывается метод adapter.Update().

Причем не важно, что мы делаем в программе - добавляем, редактируем или удаляем строки. Метод adapter.Update сделает все необходимые действия. Дело в том, что при загрузке данных в объект DataTable система отслеживает состояние загруженных строк. В методе adapter.Update() состояние строк используется для генерации нужных выражений языка SQL, чтобы выполнить обновление базы данных.

В обработчике кнопки обновления просто вызывается этот метод UpdateDB, а в обработчике кнопки удаления предварительно удаляются все выделенные строки.

Таким образом, мы можем вводить в DataGrid новые данные, редактировать там же уже существующие, сделать множество изменений, и после этого нажать на кнопку обновления, и все эти изменения синхронизируются с базой данных.

Причем важно отметить действие хранимой процедуры - при добавлении нового объекта данные уходят на сервер, и процедура возвращает нам id добавленной записи. Этот id играет большую роль при генерации нужного sql-выражения, если мы захотим эту запись изменить или удалить. И если бы не хранимая процедура, то нам пришлось бы после добавления данных загружать заново всю таблицу в datagrid, только чтобы у новой добавленной записи был в datagrid id. И хранимая процедура избавляет нас от этой работы.


```csharp

private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM users";
            usersTable = new DataTable();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

                //установка команды на добавление для вызова хранимой процедуры
                adapter.InsertCommand = new SqlCommand("sp_InsertUsers", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 10, "name"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@age", SqlDbType.Int, 10, "age"));
                
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 0, "id");
                parameter.Direction = ParameterDirection.Output;

                connection.Open();
                adapter.Fill(usersTable);
                usersGrid.ItemsSource = usersTable.DefaultView;  // Заметь, что не DataSource, а ItemSource, чтобы Binding работал в xaml
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

```


```csharp

 private void UpdateDB()
        {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter);
            adapter.Update(usersTable);
            MessageBox.Show("Данные обновлены");
        }

```

```csharp

private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateDB();
            
        }
        
```




```csharp

private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (usersGrid.SelectedItems != null)
            {
                for (int i = 0; i < usersGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = usersGrid.SelectedItems[i] as DataRowView;
                    if (datarowView != null)
                    {
                        DataRow dataRow = (DataRow)datarowView.Row;
                        dataRow.Delete();
                    }
                }
            }
            UpdateDB();
        }

```

## WPF Entity Framework CRUD

1
App.config

Создание строки подключения и провайдеров для Entity Framework

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>

	<configSections>
		<!-- For more information on Entity Framework configuration, visit http://go.microsoft.com/fwlink/?LinkID=237468 -->
		<section name="entityFramework" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false" />
	</configSections>

	<startup>
		<supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8" />
	</startup>
	
	<connectionStrings>
		<add name="DefaultConnection"
			 connectionString="Data Source=localhost;Initial Catalog=MobileStore;Integrated Security=True"
			providerName="System.Data.SqlClient"/>
	</connectionStrings>
	
  <entityFramework>
    <defaultConnectionFactory type="System.Data.Entity.Infrastructure.SqlConnectionFactory, EntityFramework" />
    <providers>
      <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
    </providers>
  </entityFramework>
	
	
</configuration>

```

2 Далее надо создать класс модели

3 Для взаимодействия с базой данных через Entity Framework нам нужен контекст данных, поэтому добавим в папку Models еще один класс, который назовем AppContext:

```csharp


using Microsoft.EntityFrameworkCore;
using System.Configuration;


public class AppContext : DbContext
    {
  

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        
    }

    public DbSet<User> Users { get; set; }

    }



```

Класс контекста наследуется от класса DbContext. В своем конструкторе он передает в конструктор базового класса название строки подключения из файла App.config. Также в контексте данных определяется свойство по типу DbSet<Phone> - через него мы будем взаимодействовать с таблицей, которая хранит объекты Phone.

	
4 В разметки Xaml
	

```xaml

	<DataGrid AutoGenerateColumns="False" x:Name="phonesGrid">
            <DataGrid.Columns>
                <DataGridTextColumn Binding="{Binding Title}" Header="Модель" Width="100"/>
                <DataGridTextColumn Binding="{Binding Company}" Header="Производитель" Width="110"/>
                <DataGridTextColumn Binding="{Binding Price}" Header="Цена" Width="70"/>
            </DataGrid.Columns>
        </DataGrid>
	
```
	
5 Теперь определим в файле кода c# привязку данных и обработчики кнопок:

	
```csharp


using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace WpfApp
{
	
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
	
    public partial class MainWindow2 : Window
    {


        AppContext db;

        public MainWindow2()
        {
            InitializeComponent();
            db = new AppContext();
            db.Users.Load();
            usersGrid.ItemsSource = db.Users.Local.ToBindingList();
            this.Closing += MainWindow2_Closing;
        }


        private void MainWindow2_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }
	
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
	
            if (usersGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < usersGrid.SelectedItems.Count; i++)
                {
                    User user = usersGrid.SelectedItems[i] as User;
                    if (user != null)
                    {
                        db.Users.Remove(user);
                    }
                }
            }
            db.SaveChanges();
        }


    }
}

	
	
```
	
## Документы
https://metanit.com/sharp/wpf/15.php
								  
## Работа с графикой
https://metanit.com/sharp/wpf/17.php
								  
## Трехмерная графика						  
https://metanit.com/sharp/wpf/18.1.php								  

## Анимация
https://metanit.com/sharp/wpf/16.php
								  
## Окна
https://metanit.com/sharp/wpf/20.1.php

## Паттерн MVVM
https://metanit.com/sharp/wpf/22.1.php


![](Patterns.png)
## SQLite в WPF
https://metanit.com/sharp/wpf/21.1.php
								  
								  
## MaterialDesign

App.xaml
								  
```xaml
<Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml" />
                <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml" />
                <ResourceDictionary Source="pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml" />
                <ResourceDictionary Source="pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Lime.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>								  
```							
Подключение

```xaml

xmlns:materialDesign="http://materialdesigninxaml.net/winfx/xaml/themes"
        
        TextElement.Foreground="{DynamicResource MaterialDesignBody}"
        TextElement.FontWeight="Regular"
        TextElement.FontSize="13"
        TextOptions.TextFormattingMode="Ideal"
        TextOptions.TextRenderingMode="Auto"
        Background="{DynamicResource MaterialDesignPaper}"
        FontFamily="{DynamicResource MaterialDesignFont}"

```

## РЕАЛИЗАЦИЯ ГРАФИКОВ С ПОМОЩЬЮ КОМПОНЕНТА CHART (SYSTEM.WINDOWS.FORMS.DATAVISUALIZATION)
https://nationalteam.worldskills.ru/skills/realizatsiya-grafikov-s-pomoshchyu-komponenta-chart-system-windows-forms-datavisualization/


## ПРОГРАММНАЯ РАБОТА С ТАБЛИЦАМИ EXCEL С ПОМОЩЬЮ БИБЛИОТЕКИ MICROSOFT.OFFICE.INTEROP.EXCEL
https://nationalteam.worldskills.ru/skills/programmnaya-rabota-s-tablitsami-excel-s-pomoshchyu-biblioteki-microsoft-office-interop-excel/

## Программная работа с документами Word с помощью библиотеки Microsoft.Office.Interop.Word
https://nationalteam.worldskills.ru/skills/programmnaya-rabota-s-dokumentami-word-s-pomoshchyu-biblioteki-microsoft-office-interop-word/

## РЕАЛИЗАЦИЯ ПОЛЬЗОВАТЕЛЬСКИХ ЭЛЕМЕНТОВ УПРАВЛЕНИЯ (USERCONTROL)
https://nationalteam.worldskills.ru/skills/realizatsiya-polzovatelskikh-elementov-upravleniya-usercontrol/


## Page навигация в WPF


Переход с помощью Navigate можно только по Page, а не по Window

Чтобы получить доступ к фрейму из другой страницы можно создать класс посредник Manager, который будет хранить в статическом поле объект фрейма

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls; // for Frame

namespace WpfApp1
{
    public class ManagerPages
    {

        public static Frame Mainframe { get; set; }

    }
}

```


При этом либо мы работаем через page, либо через окна

**Замечание**: при создание обрабочика кнопки в разметке XAML после нажатия F12 в коде создается обработчик

**Замечание**: чтобы отправить файлы в ресурсы надо выбрать проект и нажать кнопку с "ключиком" и открыть свойства проекта. Далее перейти в Ресурсы и создаться папка Resources и файл ```Properties/Resources.resx```, в котором можно добавлсять ресурсы. При этом в свойствах отдельного ресурса Действия при сборке должны быть выбраны Ресурс

## Стилизация приложения

Все стили можно прописать в отдельном файле App.xaml

```xaml
<Application x:Class="WpfApp1.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:WpfApp1"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        
        <Style TargetType="Button">
            <Setter Property="Margin" Value="15"></Setter>
            <Setter Property="Width" Value="100"></Setter>
            <Setter Property="Height" Value="50"></Setter>
            <Setter Property="Background" Value="White"></Setter>
        </Style>
         
    </Application.Resources>
    
</Application>

```

**Замечание**: можно создавать базу данных и таблицы в Visual Studio. Также при импорте данных в знаениях float SQL Server принимает значения с ,



								  
