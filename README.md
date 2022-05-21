# WPF

## Компоновка

В WPF при компоновке и расположении элементов внутри окна нам надо придерживаться следующих принципов:

- Нежелательно указывать явные размеры элементов (за исключеним **минимальных** и **максимальных** размеров). Размеры должны определяться контейнерами.
- Нежелательно указывать явные позицию и координаты элементов внутри окна. Позиционирование элементов всецело должно быть прерогативой контейнеров. И контейнер сам должен определять, как элемент будет располагаться. Если нам надо создать сложную систему компоновки, то мы можем вкладывать один контейнер в другой, чтобы добиться максимально удобного расположения элементов управления.

**Примечание**: Атрибут ShowGridLines="True" у элемента Grid задает видимость сетки, по умолчанию оно равно False.

## Конфигурация базы данных
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
## Взаимодействие с базой данных SQL Server через ADO.NET

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

Атрибут connectionString собственно хранит строку подключения. Он состоит из трех частей:

Data Source=localhost: указывает на название сервера. По умолчанию для MS SQL Server Express используется "localhost"

Initial Catalog=mobiledb: название базы данных. Так как база данных называется mobiledb, то соответственно здесь данное название и указываем

Integrated Security=True: задает режим аутентификации

Так как мы будем подключаться к базе данных MS SQL Server, то соответственно мы будем использовать провайдер для SQL Server, функциональность которого заключена в пространстве имен System.Data.SqlClient.

Далее, вывод данных в DataGrid. **AutoGenerateColumns="False"** позволяет делать привязку к нужным столбцам

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

## Технология доступа к базе данных через фреймворк Entity Framework 6


Создадим файл **App.config**

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


Класс модели

Можно все модели поместить в папку Models

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


public class AppContext : DbContext
    {
  

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        
    }

    public DbSet<Product> Products { get; set; }

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
								  
**Замечание**: при таком подходе надо изначально создавать базу данных на сервере или в классе AppContext прописать создание базы данных автоматически
								  
								  
	
	



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

Подключение в разметке XAML

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


При этом либо мы работаем через Page, либо через окна Window

**Замечание**: при создание обрабочика кнопки в разметке XAML после нажатия F12 в коде создается обработчик. В средствах VS можно выбрать - Перейти к определению

**Замечание**: чтобы отправить файлы в ресурсы надо выбрать проект и нажать кнопку с "ключиком" и открыть свойства проекта. Далее перейти в Ресурсы и создаться папка Resources и файл ```Properties/Resources.resx```, в котором можно добавлсять ресурсы. При этом в свойствах отдельного ресурса ```Действия при сборке``` должны быть выбраны ```Ресурс```

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

Проблема для создания модели данных в проекте .NET 6
```Добавление ADO.Net Entity Framework дает «Целевая платформа проекта не содержит сборки среды выполнения Entity Framework»```

Проблема в том, что EF для .Net 6.0 работает только с командами. Коллега прислал мне эту ссылку:
Инструмент EF 6 работает только с проектом .NET Framework, вы должны добавить его в свое решение, а затем скопировать или связать с созданным кодом. Кроме того, файлы EDMX в проектах .NET Core не поддерживаются, но есть обходные пути.



**Решение**: В консоли диспетчера пакетов Nuget прописать команду
Scaffold-DbContext "Server=localhost;Database=Users;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models2
**Замечание**: надо установить Microsoft.EntityFrameworkCore.Tools.

Команда создает модели из каждой сущности в базе данных, учитывая связи, а также создает класс контекста для работы с данными как с классами.



## Привязка данных

<TextBox Name="textBox" Height="40" Width="100" Text="{Binding ElementName=textBlock,Path=Text,Mode=TwoWay}"   />


## Обновление

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
            var currentUsers = db.Users.ToList();

            if (ComboBox.SelectedIndex > 0)
            {
                currentUsers = currentUsers.Where(p => p.Login == ComboBox.SelectedItem.ToString()).ToList();
            }

            currentUsers = currentUsers.Where(p => p.Login.ToLower().Contains(SearchBox.Text.ToLower())).ToList();


            //if (CheckBox.IsChecked.Value)
            //  currentUsers = currentUsers.Where(p => p.Password == "1").ToList();


            ProductGrid.ItemsSource = currentUsers.OrderBy(p => p.Login).ToList();
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
 <ComboBox Width="100" Name="ComboBox" DisplayMemberPath="Login" SelectionChanged="ComboBox_SelectionChanged"></ComboBox>
```

## ListView

```xaml

<ListView Grid.Row ="0" x:Name="ListView" ScrollViewer.HorizontalScrollBarVisibility="Disabled" HorizontalContentAlignment="Center" >



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

                        
                        <Image Grid.Row="2"  HorizontalAlignment="Center" Height="100" Width="100">

                            

                                <Image.Source>

                                <Binding Path="Password" >

                                    
                                    
                                    <Binding.TargetNullValue>
                                        <ImageSource>products/tire_0.jpg</ImageSource>
                                    </Binding.TargetNullValue>
                                    
                                </Binding>
                            </Image.Source>
                        </Image>
                        

                        <TextBlock Text="{Binding Login}"  VerticalAlignment="Center" TextAlignment="Center" TextWrapping="Wrap" HorizontalAlignment="Center" Margin="5 5"
                                   FontSize="10" Grid.Row="0"/>
                        <TextBlock Text="{Binding Password, StringFormat={}{0}}"  VerticalAlignment="Center" TextAlignment="Center" TextWrapping="Wrap" HorizontalAlignment="Center" Margin="5 5"
                                   FontSize="10" Grid.Row="1"/>
                        

                                               





                    </Grid>
                </DataTemplate>
            </ListView.ItemTemplate>

        </ListView>

```

## Работа с изображениями через путь

```Csharp
AppContext db = new AppContext();
// чтобы картинка подгружалась из папки products
// сформировать в коде список с измененным полем Password, где путь до изображения
// в базе данных в поле дописать папку в путь
db.Users.ToList().ForEach(p => p.Password = "products/" + p.Password);
ListView.ItemsSource = db.Users.ToList();
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
1. Определиться как будет осуществляться навигация по проекту. Через окна или через страницы. Сделать интерфейс приложения с навигацией
2. Установить EF6, Material Design (Microsoft.EntityFrameworkCore,Microsoft.EntityFrameworkCore.SqlServer,MaterialDesignThemes)
3. Подключить базу данных для EF6, подключить Material Design
4. Создать модели, сделать структуру проекта
5. Создать базу данных в CУБД.
6. Восстановить модели по готовой базе данных при помощи команды Scaffold-DbContext "Server=localhost;Database=Name;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models









								  
