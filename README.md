# WPF

## Паттерны проектирования

![](Patterns.png)
## SQLite в WPF
https://metanit.com/sharp/wpf/21.1.php


## Компоновка

В WPF при компоновке и расположении элементов внутри окна нам надо придерживаться следующих принципов:

- Нежелательно указывать явные размеры элементов (за исключеним **минимальных** и **максимальных** размеров). Размеры должны определяться контейнерами.
- Нежелательно указывать явные позицию и координаты элементов внутри окна. Позиционирование элементов всецело должно быть прерогативой контейнеров. И контейнер сам должен определять, как элемент будет располагаться. Если нам надо создать сложную систему компоновки, то мы можем вкладывать один контейнер в другой, чтобы добиться максимально удобного расположения элементов управления.

**Примечание**: Атрибут ShowGridLines="True" у элемента Grid задает видимость сетки, по умолчанию оно равно False.Это полезно при разработке интерфейса, потом стоит отключать эту опцию.

## Конфигурация базы данных
Добавим в проект файл ```App.config```. Подключение для SQL Server и для SQLite

```xml

<?xml version="1.0" encoding="utf-8" ?>
<?xml version="1.0" encoding="utf-8"?>
<configuration>
	<configSections>
		<sectionGroup name="applicationSettings" type="System.Configuration.ApplicationSettingsGroup, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" >

			<!-- For more information on Entity Framework configuration, visit http://go.microsoft.com/fwlink/?LinkID=237468 -->
			<section name="entityFramework" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false" />


			<section name="AdmissionsCommitteeColledge.Properties.Settings"
                     type="System.Configuration.ClientSettingsSection, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                     requirePermission="false" />

		</sectionGroup>
	</configSections>


	<startup>
		<supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8" />
	</startup>

	<connectionStrings>
		<add name="DefaultConnection"
			 connectionString="Server=localhost;Database=ColledgeStore;Integrated Security=True;"
			 providerName="System.Data.SqlClient"/>

		<add name="ConnectionSQLite" connectionString="Data Source=.\ColledgeStore.db" providerName="System.Data.SQLite" />

	</connectionStrings>


	<!-- 

		<entityFramework>

			<defaultConnectionFactory type="System.Data.Entity.Infrastructure.SqlConnectionFactory, EntityFramework" />

			<providers>
				<provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
				<provider invariantName="System.Data.SQLite"  type="System.Data.SQLite.EF6.SQLiteProviderServices, System.Data.SQLite.EF6"/>
				<provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
				<provider invariantName="System.Data.SQLite.EF6" type="System.Data.SQLite.EF6.SQLiteProviderServices, System.Data.SQLite.EF6" />
			</providers>

		</entityFramework>



		<system.data>
			<DbProviderFactories>
				<add name="SQLite Data Provider (Entity Framework 6)" invariant="System.Data.SQLite.EF6" description=".NET Framework Data Provider for SQLite (Entity Framework 6)" type="System.Data.SQLite.EF6.SQLiteProviderFactory, System.Data.SQLite.EF6" />
				<add name="SQLite Data Provider" invariant="System.Data.SQLite" description=".NET Framework Data Provider for SQLite" type="System.Data.SQLite.SQLiteFactory, System.Data.SQLite" />
			</DbProviderFactories>
		</system.data>

	-->



</configuration>

```
## Взаимодействие с базой данных SQL Server через ADO.NET


Хранимая процедура, которая осуществляет добавление нового объекта в базу данных. 

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

Атрибут connectionString собственно хранит строку подключения. Он состоит из трех частей:

- Data Source=localhost: указывает на название сервера. По умолчанию для MS SQL Server Express используется "localhost"

- Initial Catalog=mobiledb: название базы данных.
- Integrated Security=True: задает режим аутентификации

Вывод данных в DataGrid. **AutoGenerateColumns="False"** позволяет делать привязку к нужным столбцам.

```xaml

        <DataGrid AutoGenerateColumns="False"
		  x:Name="phonesGrid">
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

	Причем не важно, что мы делаем в программе - добавляем, редактируем или удаляем строки. Метод adapter.Update сделает все необходимые действия. Дело в том, что при загрузке данных в объект DataTable система отслеживает состояние загруженных строк. В методе adapter.Update() состояние строк используется для генерации нужных выражений языка SQL, чтобы выполнить обновление базы данных. В обработчике кнопки обновления просто вызывается этот метод UpdateDB, а в обработчике кнопки удаления предварительно удаляются все выделенные строки.
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
            SqlCommandBuilder commandbuilder = new SqlCommandBuilder(adapter);
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

Метод удаления
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

## Entity Framework Core 6

Класс модели. Можно все модели поместить в папку Models

```csharp

{
    public class Product
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public int ProductTypeID { get; set; }
        public string ArticleNumber { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int ProductionPersonCount { get; set; }
        public int ProductionWorkshopNumber { get; set; }
        public decimal MinCostForAgent { get; set; }

    }
}

```

Для взаимодействия с базой данных через Entity Framework нам нужен контекст данных, поэтому добавим в папку Models еще один класс, который назовем AppContext:

```csharp

using Microsoft.EntityFrameworkCore;
using System.Configuration;


public partial class ColledgeStoreContext : DbContext
    {
        public ColledgeStoreContext() => Database.EnsureCreated();  // создает базу данных, а затем всю структуру


        public ColledgeStoreContext(DbContextOptions<ColledgeStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Abiturient> Abiturients { get; set; } = null!;
        public virtual DbSet<Specialty> Specialties { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.


                // Подключение SQL Server
                optionsBuilder.UseSqlServer("Server=localhost;Database=ColledgeStore;Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());


                // SQlite
                //optionsBuilder.UseSqlite(ConfigurationManager.ConnectionStrings["ConnectionSQLite"].ToString());
                //optionsBuilder.UseSqlite(@"DataSource=ColledgeStore.db;");

            }
        }
```
**Замечание**: Product - это класс модели, Products - это название таблицы в базе данных
Класс контекста наследуется от класса DbContext. В своем конструкторе он передает в конструктор базового класса название строки подключения из файла App.config. Также в контексте данных определяется свойство по типу DbSet<Phone> - через него мы будем взаимодействовать с таблицей, которая хранит объекты Phone.


В разметки Xaml
	

```xaml

	<DataGrid AutoGenerateColumns="False" x:Name="usersGrid">
            <DataGrid.Columns>
                <DataGridTextColumn Binding="{Binding Title}" Header="Модель" Width="100"/>
                <DataGridTextColumn Binding="{Binding Company}" Header="Производитель" Width="110"/>
                <DataGridTextColumn Binding="{Binding Price}" Header="Цена" Width="70"/>
            </DataGrid.Columns>
        </DataGrid>
	
```
	
Определим в файле кода c# привязку данных и возможные обработчики кнопок:

	
```csharp


using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace WpfApp
{
	
    public partial class MainWindow2 : Window
    {

        AppContext db;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                db = new AppContext();
                db.Product.Load();
                ProductGrid.ItemsSource = db.Product.Local.ToBindingList();
		// using Linq
		// ProductGrid.ItemsSource = db.Product.ToList(); 
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
            


        }

    }
}
```
	
**Замечание**: при таком подходе надо изначально создавать базу данных на сервере или в классе AppContext прописать создание базы данных автоматически
																  
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

## Подключение в разметке XAML

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


## Page навигация в WPF


Переход с помощью Navigate можно только по Page, а не по Window.

Чтобы получить доступ к фрейму из другой страницы можно создать класс посредник ProxyClass, который будет хранить в статическом поле объект фрейма.

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

**Замечание**: при создание обрабочика кнопки в разметке XAML после нажатия F12 в коде создается обработчик. В средствах VS можно выбрать - Перейти к определению.

**Замечание**: чтобы отправить файлы в ресурсы надо выбрать проект и нажать кнопку с "ключиком" и открыть свойства проекта. Далее перейти в Ресурсы и создаться папка Resources и файл ```Properties/Resources.resx```, в котором можно добавлять ресурсы. При этом в свойствах отдельного ресурса ```Действия при сборке``` должны быть выбраны ```Ресурс```

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

**Замечание**: можно создавать базу данных и таблицы в Visual Studio. Также при импорте данных в значениях float SQL Server принимает значения с запятой  - ,

## Scaffold базы данных
	
В консоли диспетчера пакетов Nuget прописать команду
Scaffold-DbContext "Server=localhost;Database=Users;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
	
**Замечание**: надо установить Microsoft.EntityFrameworkCore.Tools.

Команда создает модели из каждой сущности в базе данных, учитывая связи, а также создает класс контекста для работы с данными как с классами.



## Привязка данных Binding

<TextBox Name="textBox" Height="40" Width="100" Text="{Binding ElementName=textBlock,Path=Text,Mode=TwoWay}"   />


## Обновление объектов в таблице

Событие IsVisibleChanged="Page1_InVisibleChanged"

```Csharp
private void Page1_InVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AppContext db = new AppContext();
                db.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ProductGrid.ItemsSource = db.Users.ToList();
            }
        }
```

## Вызов контекста на кнопке Редактировать

```Csharp
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerPages.Mainframe.Navigate(new Page2((sender as Button).DataContext as User));
        }
```

## Кнопка удаления с диалогом

```Csharp

private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUsers = ProductGrid.SelectedItems.Cast<User>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить {selectedUsers.Count()} пользователей", "Внимание!",
                 MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AppContext db = new AppContext();
                    db.Users.RemoveRange(selectedUsers);
                    db.SaveChanges();
                    ProductGrid.ItemsSource = db.Users.ToList();
                    MessageBox.Show("Пользователи удалены!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                }
            }
        }
```

## Общий метод для поиска, фильтрации, сортировки

```Csharp

private void UpdateUser()
        {
           var currentProducts = db.Abiturients.ToList();
            productGrid.ItemsSource = currentProducts;


            // Сортировка
            if (SortCombobox.SelectedIndex > 0)
            {
                if (SortCombobox.SelectedItem == "FullName")
                    currentProducts = currentProducts.OrderBy(p => p.FullName).ToList();

                if (SortCombobox.SelectedItem == "Id")
                    currentProducts = currentProducts.OrderBy(p => p.Id).ToList();


                productGrid.ItemsSource = currentProducts;

            }


            // Поиск
            if (SearchBox.Text != "")
            {
                currentProducts = currentProducts.Where(p => p.FullName.Contains(SearchBox.Text)).ToList();
                productGrid.ItemsSource = currentProducts;

            };

            if (FilterComboBox.SelectedValue == "Все типы")
                productGrid.ItemsSource = currentProducts;

            // Фильтрация
            if (FilterComboBox.SelectedValue != null && FilterComboBox.SelectedValue != "Все типы")
            {
                currentProducts = currentProducts.Where(p => p.FullName.Trim() == FilterComboBox.SelectedValue.ToString()).ToList();
                productGrid.ItemsSource = currentProducts;

                MessageBox.Show(FilterComboBox.SelectedValue.ToString());
                // В базу сохраняеются с пробелами при nchar!!!

            }






            CountAbiturients.Text = $"Количество: {currentProducts.Count} из {db.Abiturients.ToList().Count}";
        }

```

## Задание начальных значений для списка

```Csharp
            var allTypes = db.Users.ToList();

            allTypes.Insert(0, new User { Login = "Все типы" });
            ComboBox.ItemsSource = allTypes;//.Select(p => p.Login);

            CheckBox.IsChecked = true;
            ComboBox.SelectedIndex = 0;
```

	## Список

```xaml
 <ComboBox Width="100"
	   Name="ComboBox"
	   DisplayMemberPath="Login"
	   SelectionChanged="ComboBox_SelectionChanged"></ComboBox>
```

## ListView

```xaml

<ListView Grid.Row ="0"
	  x:Name="ListView"
	  ScrollViewer.HorizontalScrollBarVisibility="Disabled" 			  HorizontalContentAlignment="Center" >



            <ListView.ItemsPanel>
                <ItemsPanelTemplate>
                    <WrapPanel Orientation="Horizontal" HorizontalAlignment="Center"/>
                </ItemsPanelTemplate>
            </ListView.ItemsPanel>


            <ListView.ItemTemplate>
                <DataTemplate>
                    <Grid>
                        <Grid.RowDefinitions>
                            <RowDefinition/>
                            <RowDefinition/>
                            <RowDefinition/>
                        </Grid.RowDefinitions>

                        
                        <Image Grid.Row="2"
			       HorizontalAlignment="Center"
			       Height="100"
			       Width="100">

                                <Image.Source>

                                <Binding Path="Password" >
   
                                    <Binding.TargetNullValue>
                                        <ImageSource>products/tire_0.jpg</ImageSource>
                                    </Binding.TargetNullValue>
                                    
                                </Binding>
                            </Image.Source>
                        </Image>
                        

                        <TextBlock Text="{Binding Login}"
				   VerticalAlignment="Center"
				   TextAlignment="Center"
				   TextWrapping="Wrap"
				   HorizontalAlignment="Center"
				   Margin="5 5"
                                   FontSize="10"
				   Grid.Row="0"/>
                        <TextBlock Text="{Binding Password, StringFormat={}{0}}"
				   VerticalAlignment="Center"
				   TextAlignment="Center"
				   TextWrapping="Wrap"
				   HorizontalAlignment="Center"
				   Margin="5 5"
                                   FontSize="10" Grid.Row="1"/>
                        
                    </Grid>
                </DataTemplate>
            </ListView.ItemTemplate>

        </ListView>

```


## Передача параметров в конструктор для текущего пользователя

```Csharp
private User _currentUser = new User();

        public Page2(User selectedUser)
        {
            InitializeComponent();

            if(selectedUser != null)
            {
                _currentUser = selectedUser;
            }

            DataContext = _currentUser;
        }
```

## Валидация 
```Csharp
private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder errors = new StringBuilder();
            AppContext db = new AppContext();

            if (string.IsNullOrWhiteSpace(_currentUser.Login))
                errors.AppendLine("Укажите логин");

            //
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            MessageBox.Show(_currentUser.Id.ToString());
            if (_currentUser.Id == 0)
            {
                db.Users.Add(_currentUser);   
            }
            else
            {
              db.Users.Update(_currentUser);
            }

            try
            {
                db.SaveChanges();
                MessageBox.Show("Пользователь добавлен!");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }

        }
```


В Visual Studio работа с базой данных. Связь двух таблиц внешним ключом

```SQL
CONSTRAINT [FK_Abiturients_Specialty] FOREIGN KEY ([specialty_id]) REFERENCES [dbo].[Specialty] ([Id])
```
Автоинкремент в SQL Server 

```SQL
[Id] int Identity(1,1)
```


## Алгоритм действий при создании нового проекта WPF .NET 6
1. Сделать интерфейс приложения с навигацией
2. Установить EF6, Material Design (Microsoft.EntityFrameworkCore,Microsoft.EntityFrameworkCore.SqlServer,MaterialDesignThemes). Можно создать package.config с нужными пакетами
3. Подключить базу данных для EF6, подключить Material Design
4. Создать модели, сделать структуру проекта
5. Создать базу данных в CУБД.
6. Восстановить модели по готовой базе данных при помощи команды Scaffold-DbContext "Server=localhost;Database=Name;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
7. Можно использовать подход CodeFirst


## SQLite

После добавления для файла базы данных установим опцию "Copy if newer", чтобы файл копировался при компиляции в каталог приложения:


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

Пример MaterialDesign

```xaml
<Window x:Class="Vosmerka.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        
        xmlns:local="clr-namespace:Vosmerka"
        xmlns:materialDesign="http://materialdesigninxaml.net/winfx/xaml/themes"
        mc:Ignorable="d"
        Title="Восьмерка" Height="500" Width="800">
    <Grid>
        <Border MinWidth="100" Margin="15" Background="AliceBlue" VerticalAlignment="Center" Padding="40" MaxHeight="400" CornerRadius="30">
            <Border.Effect>
                <DropShadowEffect BlurRadius="30" Color="LightGray" ShadowDepth="0"/>
            </Border.Effect>

            <StackPanel>
                <TextBlock Text="Пользователи" FontSize="30" FontWeight="Bold" Margin="0 0 0 20"/>

                <Grid Margin="0 0 0 30">
                    <Button HorizontalAlignment="Left" Content="Список"/>
                    <Button HorizontalAlignment="Right" Content="Войти" Style="{StaticResource MaterialDesignFlatButton}"/>

                </Grid>

                <TextBox Name="loginField" materialDesign:HintAssist.Hint="Введите логин" Style="{StaticResource MaterialDesignFloatingHintTextBox}"/>
                <PasswordBox Name="passwordField" materialDesign:HintAssist.Hint="Введите пароль" Style="{StaticResource MaterialDesignFloatingHintPasswordBox}"/>
                
                <TextBox Name="emailField" materialDesign:HintAssist.Hint="Введите email" Style="{StaticResource MaterialDesignFloatingHintTextBox}"/>
                <Button Name="createButton" Content="Создать" Margin="0 20" Click="createButton_Click"/>
            </StackPanel>
        </Border>


    </Grid>
</Window>

```


## Работа с Word

```Csharp
          //  using(AppContext db = new AppContext())
            {
                //var products = db.Products.ToList();

                //var application = new Word.Application();

                //Word.Document document = application.Documents.Add();


                // Создаем параграф для хранения страниц


                // Основной структурной единицей текста является параграф, представленный объектом
                // Paragraph. Все абзацы объединяются в коллекцию Paragraphs, причем новые параграфы
                // добавляются с помощью метода Add. Доступ к тексту предоставляет объект Range,
                // являющийся свойством Paragraph, а текстовое содержание абзаца доступно через
                // Range.Text. В данном случае для хранения ФИО каждого пользователя создается новый параграф

                /*foreach (var p in products)
                {
                    Word.Paragraph productParagraph = document.Paragraphs.Add();
                    Word.Range productRange = productParagraph.Range;


                  

                    // Добавляем названия страниц
                    productRange.Text = p.Title;
                    //productParagraph.set_Style("Title");
                    productRange.InsertParagraphAfter();

                    //Добавляем и форматируем таблицу для хранения информации о продуктах
                    Word.Paragraph tableParagraph = document.Paragraphs.Add();
                    Word.Range tableRange = tableParagraph.Range;
                    Word.Table paymentsTable = document.Tables.Add(tableRange, products.Count() + 1, 3);


                    //После создания параграфа для таблицы и получения его Range, добавляется таблица
                    //с указанием числа строк (по количеству категорий + 1) и столбцов. Последние две строчки
                    //касаются указания границ (внутренних и внешних) и выравнивания ячеек (по центру и по вертикали)

                    paymentsTable.Borders.InsideLineStyle = paymentsTable.Borders.OutsideLineStyle
                        = Word.WdLineStyle.wdLineStyleSingle;
                    paymentsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;


                    Word.Range cellRange;

                    cellRange = paymentsTable.Cell(1, 1).Range;
                    cellRange.Text = "Текст 1";
                    cellRange = paymentsTable.Cell(1, 2).Range;
                    cellRange.Text = "Текст 2";
                    cellRange = paymentsTable.Cell(1, 3).Range;
                    cellRange.Text = "Текст 3";

                    paymentsTable.Rows[1].Range.Bold = 1;
                    paymentsTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;*/



                    // Положение ячейки заносится в переменную cellRange. Метод AddPicture() класса
                    // InlineShape позволяет добавить изображение в ячейку. Иконки категорий размещаются
                    // в новой папке Assets, основные шаги создания которой изображены на скриншоте


                   /* for (int i = 0; i < products.Count(); i++)
                    {
                        var currentProduct = products[i];
                        cellRange = paymentsTable.Cell(i + 2, 1).Range;
                        
                        
                        
                        //Word.InlineShape imageShape = cellRange.InlineShapes.AddPicture(AppDomain.CurrentDomain.BaseDirectory
                          //  + "..\\..\\" + currentProduct.Id);

                        // Для первой колонки устанавливаются длина, ширина,
                        // а также горизонтальное выравнивание по центру

                        //imageShape.Width = imageShape.Height = 40;
                        //cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                        cellRange = paymentsTable.Cell(i + 2, 2).Range;
                        cellRange.Text = currentProduct.Title;


                    }*/

            }

```
## Применение глобального шрифта к страницам Page или Window
							    
```Csharp
Style = (Style)FindResource(typeof(Page));
```

## Количество элементов
```Csharp
CountAbiturients.Text = $"Количество: {db.Abiturients.Take(10).ToList().Count} из {db.Abiturients.ToList().Count}";
```

## Включение в выборку связнных записей
```Csharp
productGrid.ItemsSource = db.Abiturients.Include(p => p.Specialty).ToList();
//Без использования метода Include мы бы не могли бы получить связанную команду и ее свойства: p.Specialty.Name
```

## Переход на страницу и передача объекта
```Cshawrp
ProxyFrame.Mainframe.Navigate(new AddAbiturientPage(productGrid.SelectedItem as Abiturient));
```

## Переход на страницу Вперед
```Csharp
private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            productGrid.ItemsSource = db.Abiturients.Skip(step).Take(10).ToList();
            if (step + 10 < db.Abiturients.Count())
                step += 10;

            CountAbiturients.Text = $"Количество: {db.Abiturients.Skip(step).Take(10).ToList().Count} из {db.Abiturients.ToList().Count}";
        }	
```
						   
## Переход на страницу Назад
```Csharp
private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (step > 0)
                step -= 10;
            productGrid.ItemsSource = db.Abiturients.Skip(step).Take(10).ToList();

            CountAbiturients.Text = $"Количество: {db.Abiturients.Skip(step).Take(10).ToList().Count} из {db.Abiturients.ToList().Count}";
        }
```

## Очистка параметров
```Csharp
private void Clear_ButtonClick(object sender, RoutedEventArgs e)
        {
            SortCombobox.Text = "Сортировка";
            FilterComboBox.Text = "Все типы";
            SearchBox.Text = "";
        }
```

## Опции для DataGrid

```xaml
	Grid.Row="1"
            Margin="5"
            AutoGenerateColumns="False"
            x:Name="productGrid"
            
            MouseDoubleClick="Edit_MouseDoubleClick"
            IsReadOnly="True"
            GridLinesVisibility="None"
            SelectionMode="Extended"
            SelectionUnit="FullRow"
            
            ColumnWidth="Auto"
            HorizontalAlignment="Stretch" 
            VerticalAlignment="Stretch" 
            HorizontalContentAlignment="Stretch"
            EnableRowVirtualization="false"
            EnableColumnVirtualization="false"
            CanUserAddRows="False"
            CanUserReorderColumns="False"
            CanUserResizeColumns="True" IsSynchronizedWithCurrentItem="False"
```

## Триггеры

```xaml
<DataGrid.RowStyle>
                <Style TargetType="DataGridRow">

                   

                    <Style.Triggers>

                        <DataTrigger Binding="{Binding Ball}" Value="5" >
                            <Setter Property="Background" Value="Orange"/>
                        </DataTrigger>

                    </Style.Triggers>

                </Style>
            </DataGrid.RowStyle>
```

##  Вinding Stringformat даты
	
```xaml
Binding="{Binding BirthDay, StringFormat={}{0:dd.MM.yyyy}}"
```
	
## Опции окна Window
```xaml
 	Title="Главное меню"
        Height="700"
        Width="1100"
        
        Background="White"

        WindowStartupLocation="CenterScreen"

        MinHeight="500"
        MinWidth="900"
        Icon="emblscc.ico"
```
	
## Авторизация

```Csharp
using(ColledgeStoreContext db = new ColledgeStoreContext())
            {
                var currentUser = db.Users.Where(user => user.Login == LoginBox.Text && user.Password == PasswordBox.Password).FirstOrDefault();

                if (currentUser != null)
                {

                    ProxyFrame.CurrentUser = currentUser;



                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();



                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль");
                }



            }
```
	

## Дополнительный свойства на основе существующих свойств
```Csharp
        public string? Image { get; set; }
        public string? ImagePath
        {
            get
            {
                //string newRelativePath = $"images/{System.DateTime.Now.ToString("HHmmss")}_image.jpeg";
                return System.IO.Path.Combine(Environment.CurrentDirectory, Image);
            }
            
        }
```

## Binding по полному пути картинки
```Csharp
                                <Image>

                                    <Image.Source>

                                        <BitmapImage DecodePixelWidth="100" DecodePixelHeight="100"
                                        UriSource = "{Binding ImagePath}"             
                                                     
                                                     />
                                    </Image.Source>


                                </Image>
```

## DataPicker
```xaml
<DatePicker
SelectedDate="{Binding BirthDay}"
Name="birth_day"  />
```
	
## ComboBox
```xaml
<ComboBox

SelectedValue="{Binding Specialty}"
Text="{Binding Specialty.Name}"
Name="specialty_id"
Margin="1"
Height="30"
Width="150" 
IsEditable="True" />
```
	
```Csharp
 specialty_id.ItemsSource = db.Specialties.ToList(); // загрузка в комбобокс объектов специальностей
 specialty_id.DisplayMemberPath = "Name"; // отображение в списке объектов конкретные свойства, а не весь объект
```

## Изображение по абсолютному пути
```Csharp
BitmapImage image = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, $"{_currentAbiturient.Image}"), UriKind.Absolute));
ImagePicture.Source = image;
```
	
	
## Валидация

```Csharp
// Валидация полей

                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrWhiteSpace(full_name.Text))
                    errors.AppendLine("Укажите имя");
                if (string.IsNullOrWhiteSpace(specialty_id.Text))
                    errors.AppendLine("Укажите специальность");
                if (string.IsNullOrWhiteSpace(birth_day.Text))
                    errors.AppendLine("Укажите дату рождения");
                if (string.IsNullOrWhiteSpace(date_certificate.Text))
                    errors.AppendLine("Укажите дату выдачи аттестата");
                if (string.IsNullOrWhiteSpace(passport_issued.Text))
                    errors.AppendLine("Укажите дату выдачи паспорта");

                //
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }
```
	
## Замена . на , для SQL Server

```Csharp
Ball = Convert.ToDouble(ball.Text.Replace(".", ",")) // SQL Server принимает дробные значения с запятой
```
	
## Полные сообщения об ошибках
```Csharp
catch (Exception ex)
{
    MessageBox.Show($"Ошибка: {ex.InnerException.Message}");
}
```
	
## Open File Dialog и сохранение
```Csharp
private void ImageButton(object sender, RoutedEventArgs e)
        {


            

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            Nullable<bool> result = dlg.ShowDialog();
            dlg.Title = "Open Image";

            dlg.InitialDirectory = "./";

            if (result == true)
            {
                BitmapImage image = new BitmapImage(new Uri(dlg.FileName));
                ImagePicture.Source = image;
            }



            try
                {
                    if (result == true)
                    {

                        // покажем картинку на экран по абсолютному пути

                        if (_currentAbiturient.Image != null)
                        {
                            //BitmapImage image = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, $"{_currentAbiturient.Image}"), UriKind.Absolute));

                            //BitmapImage image = new BitmapImage(new Uri(dlg.FileName));
                            //ImagePicture.Source = image;
                        }

                        // скопируем  выбранную картинку в нужный каталог /images/

                        string newRelativePath = $"images/{System.DateTime.Now.ToString("HHmmss")}_image.jpeg";
                        File.Copy(dlg.FileName, System.IO.Path.Combine(Environment.CurrentDirectory, newRelativePath));

                        // сохраним в свойство модели Abiturient.Image относительный путь к новой картинке

                        _currentAbiturient.Image = newRelativePath;
                         MessageBox.Show("Изображение добавлено!");

                     }




                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


/*
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                {
                    MessageBox.Show(saveFileDialog.FileName); // полный путь
                    MessageBox.Show(saveFileDialog.SafeFileName); // имя файла
                }*/
        }
```
	
## Работа с Word. Поиск и замена значений
```Csharp
 try
            {
                Abiturient abiturient = UsersComboBox.SelectedItem as Abiturient;
                File.Copy(System.IO.Path.Combine(Environment.CurrentDirectory, "заявление.doc"), System.IO.Path.Combine(Environment.CurrentDirectory, $"заявление {abiturient.FullName}.doc"));

                Word.Application wordApp = new Microsoft.Office.Interop.Word.Application { Visible = false };
                Word.Document aDoc = wordApp.Documents.Open(Environment.CurrentDirectory + "/" + $"заявление {abiturient.FullName}.doc", ReadOnly: false, Visible: false); // файлу дать разрешения для записdи
                Word.Range range = aDoc.Content;

                //range.Find.ClearFormatting();










                range.Find.Execute(FindText: "[Фамилия]", ReplaceWith: abiturient.FullName.Split(" ")[0], Replace: Word.WdReplace.wdReplaceAll);
                range.Find.Execute(FindText: "[Имя]", ReplaceWith: abiturient.FullName.Split(" ")[1], Replace: Word.WdReplace.wdReplaceAll);
                range.Find.Execute(FindText: "[Отчество]", ReplaceWith: abiturient.FullName.Split(" ")[2], Replace: Word.WdReplace.wdReplaceAll);
                range.Find.Execute(FindText: "[Дата рождения]", ReplaceWith: abiturient.BirthDay, Replace: Word.WdReplace.wdReplaceAll);
                range.Find.Execute(FindText: "[Место рождения]", ReplaceWith: abiturient.PlaceOfBirth, Replace: Word.WdReplace.wdReplaceAll);
                range.Find.Execute(FindText: "[Гражданство]", ReplaceWith: abiturient.Citizenship, Replace: Word.WdReplace.wdReplaceAll);
                range.Find.Execute(FindText: "[серия] ", ReplaceWith: abiturient.SeriesNumberPassport.Split(" ")[0], Replace: Word.WdReplace.wdReplaceAll);
                range.Find.Execute(FindText: "[номер]", ReplaceWith: abiturient.SeriesNumberPassport.Split(" ")[1], Replace: Word.WdReplace.wdReplaceAll);
                range.Find.Execute(FindText: "[Кем и когда выдан]", ReplaceWith: abiturient.PassportIssued, Replace: Word.WdReplace.wdReplaceAll);
                range.Find.Execute(FindText: "[Адрес регистрации]", ReplaceWith: abiturient.RegistrationAddress, Replace: Word.WdReplace.wdReplaceAll);
                range.Find.Execute(FindText: "[Адрес фактического проживания]", ReplaceWith: abiturient.AddressActualResidence, Replace: Word.WdReplace.wdReplaceAll);
                range.Find.Execute(FindText: "[телефон]", ReplaceWith: abiturient.NumberPhone, Replace: Word.WdReplace.wdReplaceAll);



                range.Find.Execute(FindText: "[код] ", ReplaceWith: (db.Specialties.Find(abiturient.SpecialtyId) as Specialty).Code   , Replace: Word.WdReplace.wdReplaceAll);

                range.Find.Execute(FindText: "[наименование]", ReplaceWith: db.Specialties.Find(abiturient.SpecialtyId).Name, Replace: Word.WdReplace.wdReplaceAll);

                range.Find.Execute(FindText: "[образовательное учреждение]", ReplaceWith: abiturient.Education, Replace: Word.WdReplace.wdReplaceAll);
                range.Find.Execute(FindText: "[год окончания]", ReplaceWith: abiturient.SchoolGraduationYear, Replace: Word.WdReplace.wdReplaceAll);
                range.Find.Execute(FindText: "[номер аттестата]", ReplaceWith: abiturient.CertificateNumber, Replace: Word.WdReplace.wdReplaceAll);
                range.Find.Execute(FindText: "[дата выдачи]", ReplaceWith: abiturient.DateCertificate, Replace: Word.WdReplace.wdReplaceAll);
                range.Find.Execute(FindText: "[иностранный язык]", ReplaceWith: abiturient.Language, Replace: Word.WdReplace.wdReplaceAll);
                range.Find.Execute(FindText: "[национальность]", ReplaceWith: abiturient.Nationality, Replace: Word.WdReplace.wdReplaceAll);

                range.Find.Execute(FindText: "[общежитие]", ReplaceWith: abiturient.NeedHostel, Replace: Word.WdReplace.wdReplaceAll);


                range.Find.Execute(FindText: "[дата]", ReplaceWith: DateTime.Now.ToShortDateString(), Replace: Word.WdReplace.wdReplaceAll);



                if (abiturient.Specialty.Base == "9")
                {
                    if (range.Find.Execute("общего"))
                      
                      range.Font.Underline = Word.WdUnderline.wdUnderlineDouble;
                }
                else
                {
                    if (range.Find.Execute("среднего"))
                        
                        range.Font.Underline = Word.WdUnderline.wdUnderlineDouble;
                }


                // создаю новый range так как старый range становится весь другой. С этим можно разобраться

                Word.Range range1 = aDoc.Content;

                if (abiturient.Specialty.FormEducation == "очная")
                {
                    if (range1.Find.Execute("очное"))
                        
                        range1.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
                }
                else
                {
                    
                    if (range1.Find.Execute("заочное"))
                        
                         range1.Font.Underline = Word.WdUnderline.wdUnderlineSingle;
                }






                MessageBox.Show("Заявление создано!", MessageBoxButton.OK.ToString());
              
                // Надо сохранять в файл с правами записи
                string gesturefile = System.IO.Path.Combine(Environment.CurrentDirectory + "/" + $"заявление {abiturient.FullName}.doc");
                string gesturefilePdf = System.IO.Path.Combine(Environment.CurrentDirectory + "/" + $"заявление {abiturient.FullName}.pdf");


                if (PdfCheck.IsChecked == true)
                {
                    aDoc.SaveAs2(gesturefilePdf, Word.WdExportFormat.wdExportFormatPDF);
                }

                aDoc.Close();
                wordApp.Quit();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
```

## Добавление фото в Word
```Csharp
 // находим диапазон с фото
                Word.Range range1 = aDoc.Content;
                range1.Find.Execute(FindText: "[Фото]");
                
                // добавляем рядом картинку
                Word.InlineShape ils = aDoc.InlineShapes.AddPicture(abiturient.ImagePath, false, true, Range: range1);
                
                // удаляем слово фото
                range1.Find.Execute(FindText: "[Фото]", ReplaceWith: "", Replace: Word.WdReplace.wdReplaceAll);
```
	
## Style
```xaml
<Application.Resources>




        <Style TargetType="{x:Type Window}">
            <Setter Property="FontSize" Value="20"></Setter>
            <Setter Property="FontFamily" Value="Gabriola"></Setter>
        </Style>

        <Style TargetType="{x:Type Page}">
            <Setter Property="FontSize" Value="20"></Setter>
            <Setter Property="FontFamily" Value="Gabriola"></Setter>
        </Style>


        <Style TargetType="{x:Type Grid}">
            <Setter Property="ShowGridLines" Value="False"></Setter>
        </Style>







        <Style TargetType="Button">
            <Setter Property="Margin" Value="1"></Setter>
            <Setter Property="Width" Value="120"></Setter>
            <Setter Property="Height" Value="30"></Setter>
            <Setter Property="Background" Value="#BCDAF0"></Setter>
        </Style>

        <Style TargetType="StackPanel">
            <Setter Property="Margin" Value="10"></Setter>
            <Setter Property="HorizontalAlignment" Value="Center"></Setter>
            <Setter Property="VerticalAlignment" Value="Center"></Setter>
        </Style>

        <Style TargetType="WrapPanel">
            <Setter Property="Margin" Value="10"></Setter>
        </Style>

        <Style TargetType="TextBox">
            <Setter Property="Width" Value="150"></Setter>
            <Setter Property="Height" Value="30"></Setter>
            <Setter Property="Margin" Value="1"></Setter>
        </Style>

        <Style TargetType="DatePicker">
            <Setter Property="Width" Value="150"></Setter>
            <Setter Property="Height" Value="30"></Setter>
            <Setter Property="Margin" Value="1"></Setter>
        </Style>


    </Application.Resources>
```
