# WPF
## Паттерны проектирования

![](image/Patterns.png)



## App.config

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>


	<connectionStrings>
		<add 
			 name="DefaultConnection"
			 connectionString="Server=localhost,63027;Database=UserDatabase;Trusted_Connection=True"
			 providerName="System.Data.SqlClient"/>

		<add
			 name="ConnectionLocalDb"
			 connectionString="Server=(localdb)\mssqllocaldb;Database=UserDatabase;Trusted_Connection=True;"
			 providerName="System.Data.SqlClient"/>


		<add name="ConnectionSQLite"
			 connectionString="Data Source=FabricShop.db"
			 providerName="System.Data.SQLite" />

	</connectionStrings>


</configuration>
```

## UserContext

```Csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                // SQL Server connection with port
                //optionsBuilder.UseSqlServer("Server=localhost,63027;Database=UserDatabase;Trusted_Connection=True;");

                // SQL Server connection with localdb
                //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UserDatabase;Trusted_Connection=True;");

                // SQL Server connection from App.config
                //optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());

                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ConnectionLocalDb"].ToString());

                // SQlite
                //optionsBuilder.UseSqlite(ConfigurationManager.ConnectionStrings["ConnectionSQLite"].ToString());
                //optionsBuilder.UseSqlite(@"DataSource=ColledgeStore.db;");

            }
        }
```

## DataGrid определение

```csharp
    <DataGrid 
                AutoGenerateColumns="False"
        x:Name="productGrid"
                Grid.Row="1"
                Grid.RowSpan="1"
                IsReadOnly="True"
                SelectionMode="Single"
                RowDetailsVisibilityMode="VisibleWhenSelected"
                HorizontalContentAlignment="Left"
        >
        
        <DataGrid.Columns>
            <DataGridTemplateColumn Header="Фото" IsReadOnly="True" >
                <DataGridTemplateColumn.CellTemplate>
                    <DataTemplate>
                        <Image Height="100" Width="100" Source="{Binding ImagePath}" />
                    </DataTemplate>
                </DataGridTemplateColumn.CellTemplate>
            </DataGridTemplateColumn>


            <DataGridTextColumn Binding="{Binding Title}" Header="Название"/>
            
            <DataGridTextColumn Binding="{Binding Price}" Header="Цена"/>
            <DataGridTextColumn Binding="{Binding Category.Name}" Header="Категория" />
            
            <DataGridTextColumn Width="*" Header="Описание" Binding="{Binding Description}">
                    <DataGridTextColumn.ElementStyle>
                        <Style>
                            <Setter Property="TextBlock.TextWrapping" Value="Wrap" />
                            <Setter Property="TextBlock.TextAlignment" Value="Justify" />
                        </Style>
                    </DataGridTextColumn.ElementStyle>
            </DataGridTextColumn>
            
        </DataGrid.Columns>
    </DataGrid>

```


## Интерфейс INotifyPropertyChanged

```Csharp
  public class User : INotifyPropertyChanged
    {

        private int id;
        private string name;
        private int age;

        public int Id
        {
            get {return id; }

            set {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return name; }

            set
            {
                name = value;
                OnPropertyChanged("Name");
            }

        }

        public int Age
        {
            get { return age; }

            set
            {
                age = value;
                OnPropertyChanged("Age");
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
```

## Entity Framework Core 6. Cвязь моделей 1 : M

### Model User

```csharp

using System;
using System.Collections.Generic;

namespace FabricShop.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
       
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
```

### Модель Role

```csharp
using System;
using System.Collections.Generic;

namespace FabricShop.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; } = null!;
        
	public virtual ICollection<User> Users { get; set; }
    }
}
```
																  
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

## Подключение

```xml
xmlns:materialDesign="http://materialdesigninxaml.net/winfx/xaml/themes"
        
        TextElement.Foreground="{DynamicResource MaterialDesignBody}"
        TextElement.FontWeight="Regular"
        TextElement.FontSize="13"
        TextOptions.TextFormattingMode="Ideal"
        TextOptions.TextRenderingMode="Auto"
        Background="{DynamicResource MaterialDesignPaper}"
        FontFamily="{DynamicResource MaterialDesignFont}"
```

## Пример MaterialDesign

```xml
<Grid>
        <Grid>
            <Border MinWidth="100"
                    Margin="15"
                    Background="AliceBlue"
                    VerticalAlignment="Center"
                    Padding="40"
                    MaxHeight="400"
                    CornerRadius="30">
                
                <Border.Effect>
                    <DropShadowEffect BlurRadius="30"
                                      Color="LightGray"
                                      ShadowDepth="0"/>
                </Border.Effect>

                <StackPanel>
                    <TextBlock Text="База данных магазина компьютерной техники"
                               FontSize="30"
                               FontWeight="Bold"
                               Margin="0 0 0 20"/>
                    <TextBox Name="loginField"
                             materialDesign:HintAssist.Hint="Введите логин"
                             Style="{StaticResource MaterialDesignFloatingHintTextBox}"/>
                    <PasswordBox Name="passwordField"
                                 materialDesign:HintAssist.Hint="Введите пароль"
                                 Style="{StaticResource MaterialDesignFloatingHintPasswordBox}"/>

                    <TextBox Name="emailField"
                             materialDesign:HintAssist.Hint="Введите email"
                             Style="{StaticResource MaterialDesignFloatingHintTextBox}"/>
                    <Button Name="createButton"
                            Content="Создать"
                            Margin="0 20"
                            />
                </StackPanel>
            </Border>

        </Grid>
    </Grid>

```


## Page в WPF

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

## Scaffold базы данных

**Замечание**: надо установить ```Microsoft.EntityFrameworkCore.Tools```.

В консоли диспетчера пакетов Nuget прописать команду
```Scaffold-DbContext "Server=localhost;Database=Users;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models```
	
Команда создает модели из каждой сущности в базе данных, учитывая связи, а также создает класс контекста для работы с данными как с классами.

```Scaffold-DbContext "Data Source=.\ComputerDatabase.db" Microsoft.EntityFrameworkCore.Sqlite -OutputDir Models```

**Примечание**: если будет делать Scaffold для SQLite, ему нужна база из проекта, а не в Debug. При инициализации контекста база данных SQLite создается в Debug по умолчанию.


В консоле диспетчера пакетов для SQLServer

```
Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=UserDatabase;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models2
```

## Обновление объектов базы данных в DataGrid на странице

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

## Задание начальных значений для списка

```csharp
var allTypes = db.Users.ToList();

allTypes.Insert(0, new User { Login = "Все типы" });
ComboBox.ItemsSource = allTypes;//.Select(p => p.Login);

ComboBox.SelectedIndex = 0;
```

## Список combobox в xaml

```xml
 <ComboBox Width="100"
	   Name="ComboBox"
	   DisplayMemberPath="Login"
	   SelectionChanged="ComboBox_SelectionChanged"></ComboBox>
```

## ListView

```xml
<ListView Grid.Row ="0"
	  x:Name="ListView"
	  ScrollViewer.HorizontalScrollBarVisibility="Disabled"HorizontalContentAlignment="Center" >
	  
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

## SQL Server create foreign key

```SQL
CONSTRAINT [FK_Abiturients_Specialty] FOREIGN KEY ([specialty_id]) REFERENCES [dbo].[Specialty] ([Id])
```
## Автоинкремент в SQL Server 

```SQL
[Id] int Identity(1,1)
```


## Применение глобального шрифта к страницам Page или Window
							    
```Csharp
Style = (Style)FindResource(typeof(Page));
```

## Количество элементов
```Csharp
CountAbiturients.Text = $"Количество: {db.Abiturients.Take(10).ToList().Count} из {db.Abiturients.ToList().Count}";
```

## Include relation

```Csharp
productGrid.ItemsSource = db.Abiturients.Include(p => p.Specialty).ToList();
//Без использования метода Include мы бы не могли бы получить связанную команду и ее свойства: p.Specialty.Name
```

## Переход на страницу и передача объекта
```Csharp
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

## Очистка параметров сортировки, фильтрации и поиска

```Csharp
private void Clear_ButtonClick(object sender, RoutedEventArgs e)
    {
        SortCombobox.Text = "Сортировка";
        FilterComboBox.Text = "Все типы";
        SearchBox.Text = "";
    }
```

## Атрибуты для DataGrid

```xml
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

## Триггеры в DataGrid на Row

```xml
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

## Вinding даты через Stringformat
	
```xml
    Binding="{Binding BirthDay, StringFormat={}{0:dd.MM.yyyy}}"
```
	
## Атрибуты окна Window
```xml
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

	
## Валидация

```Csharp
    
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


## Дополнительное свойство на основе существующего свойства

```Csharp       
    public string? Image { get; set; }
    public string? ImagePath { get { return System.IO.Path.Combine(Environment.CurrentDirectory, $"images/{Image}"); }  }
```

## Binding по полному пути картинки

```xml
    <Image>
        <Image.Source>
            <BitmapImage DecodePixelWidth="100" DecodePixelHeight="100"
            UriSource = "{Binding ImagePath}"/>
        </Image.Source>
    </Image>
```

## DataPicker

```xml
    <DatePicker SelectedDate="{Binding BirthDay}"  Name="birth_day"/>
```
	
## ComboBox

```xml
<ComboBox SelectedValue="{Binding Specialty}"
          Text="{Binding Specialty.Name}"
          Name="specialty_id"
          Margin="1"
          Height="30"
          Width="150" 
          IsEditable="True" />
```
	
```csharp
    specialty_id.ItemsSource = db.Specialties.ToList(); // загрузка в комбобокс объектов специальностей
    specialty_id.DisplayMemberPath = "Name"; // отображение в списке объектов конкретные свойства, а не весь объект
```

## Изображение по абсолютному пути

```Csharp
    BitmapImage image = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, $"{_currentAbiturient.Image}"), UriKind.Absolute));
    ImagePicture.Source = image;
```
		
## Замена . на , для SQL Server

```csharp
    Ball = Convert.ToDouble(ball.Text.Replace(".", ",")) // SQL Server принимает дробные значения с запятой
```
	
## Полные сообщения об ошибках

```csharp
    catch (Exception ex)
    {
        MessageBox.Show($"Ошибка: {ex.InnerException.Message}");
    }
```

## Captcha

```Csharp
    public static class CaptchaBuild
        {
            public static string Refresh()
            {

                string captcha = "A1fd";

                Random rand = new Random();

                for (int i = 0; i < 4; i++)
                {
                    captcha += (char)rand.Next('A', 'Z' + 1);
                }

                return captcha;
            }
        }								  
```
				  
```csharp
	CaptchaBox.Visibility = Visibility.Visible;
	CaptchaText.Visibility = Visibility.Visible;
	Captcha.Visibility = Visibility.Visible;
	LoginButtonName.IsEnabled = false;

	MessageBox.Show(CaptchaBuild.Refresh());
	Captcha.Text = CaptchaBuild.Refresh();
	Captcha.IsReadOnly = true;				  
```

```csharp
	private void CaptchaBox_TextChanged(object sender, TextChangedEventArgs e)
		{
		    if (CaptchaBox.Text == Captcha.Text)
		    {
			CaptchaBox.Visibility = Visibility.Collapsed;
			CaptchaText.Visibility = Visibility.Collapsed;
			Captcha.Visibility = Visibility.Collapsed;
			LoginButtonName.IsEnabled = true;
		    }
		    else
		    {
			Captcha.Text = CaptchaBuild.Refresh();
			disableButton();

		    }
		}
```

## Асинхронная задача для выключения кнопки

```csharp
	async void disableButton()
		{
		    LoginButtonName.IsEnabled = false;
		    await Task.Delay(TimeSpan.FromSeconds(10));
		    LoginButtonName.IsEnabled = true;
		}
```

## App.xaml. Стили и ресурсы для приложения

```xml
    <Application.Resources>

        <SolidColorBrush x:Key="ColorPrimery" Color="White"></SolidColorBrush>
        <SolidColorBrush x:Key="ColorSecondary" Color="#FFFFFFE1"></SolidColorBrush>
        <SolidColorBrush x:Key="ColorAccent" Color="#FF76E383"></SolidColorBrush>


        <Style TargetType="{x:Type Window}">
            <Setter Property="FontSize" Value="15"></Setter>
            <Setter Property="FontFamily" Value="Comic Sans MS"></Setter>
            <Setter Property="Background" Value="White">
            </Setter>
        </Style>

        <Style TargetType="{x:Type Page}">
            <Setter Property="FontSize" Value="20"></Setter>
            <Setter Property="FontFamily" Value="Comic Sans MS"></Setter>
        </Style>

        <Style TargetType="{x:Type DataGrid}">
            
            <Setter Property="Background" Value="#FF76E383">
            </Setter>
        </Style>

        <Style TargetType="Button">
            <Setter Property="Margin" Value="4"></Setter>
            <Setter Property="Width" Value="120"></Setter>
            <Setter Property="Height" Value="30"></Setter>
            <Setter Property="Background" Value="#FF498C51"></Setter>
        </Style>

        <Style TargetType="StackPanel">
            <Setter Property="Margin" Value="15"></Setter>
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

## Триггер для ListView

```xml
    <ListView.ItemContainerStyle>
        <Style TargetType="ListViewItem">
            
            <Style.Triggers>
                <DataTrigger Binding="{Binding QuantityInStock}" Value="0">
                    <Setter Property="Background" Value="Gray" />
                </DataTrigger>
            </Style.Triggers>
        </Style>
    </ListView.ItemContainerStyle>
```

## Border

```xml
    <Border
        CornerRadius="3"
        BorderThickness="2"
        Width="800"
        Height="Auto"
        BorderBrush="{StaticResource ColorAccent}"

        <Border.Effect>
            <DropShadowEffect BlurRadius="30"
            Color="LightGray"
            ShadowDepth="0"/>
        </Border.Effect>
    </Border>
```

**Замечание**: в Border может быть только один элемент.

## Роли 

```csharp
    if (Proxy.CurrentUser != null)
        {
            nameUser.Text = Proxy.CurrentUser.Name + " " + Proxy.CurrentUser.Surname;
            roleUser.Text = $"Ваша роль: {Proxy.CurrentUser.RoleNavigation.RoleName}";
            AddProduct.Visibility = Visibility.Visible;
            DeleteProduct.Visibility = Visibility.Visible;
        }
    else
    {
        nameUser.Text = "Вы зашли как гость!";
    }
```

## Выборка по столбцу Select в EF

```csharp
    var Suppliers = db.Products.Select(p => p.Supplier).Distinct().ToList();
```

## Начальные значения для сортировки и фильтрации

```csharp
    SortComboBox.ItemsSource = new List<String>() { "Цена", "По убыванию", "По возрастанию" };   //.Select(p => p.Login);
    SortComboBox.SelectedIndex = 0;

    Suppliers.Insert(0, "Все производители");
    FilterComboBox.ItemsSource = Suppliers;
    FilterComboBox.SelectedIndex = 0;
```
**Замечания**: событие SelectionChanged выбора срабатывает после смены значения в списке, т.е вначале первое значение стоит поставить нейтральное


## Обновленный общий метод для сортировки, фильтрации и поиска

```csharp
    private void UpdateProducts()
        {
            using(FabricShopContext db = new FabricShopContext())
            {
                var currentProducts = db.Products.ToList();
                ListViewProduct.ItemsSource = currentProducts;

                // Сортировка
                if (SortComboBox.SelectedIndex > 0)
                {
                    
                    if (SortComboBox.SelectedItem == "По возрастанию")
                    {
                        currentProducts = currentProducts.OrderBy(p => p.Cost).ToList();
                    }

                    if (SortComboBox.SelectedItem == "По убыванию")
                    {
                        currentProducts = currentProducts.OrderByDescending(p => p.Cost).ToList();
                    }
                    ListViewProduct.ItemsSource = currentProducts;
                }

                // Поиск
                if (Search.Text != "")
                {
                    currentProducts = currentProducts.Where(p => p.Name.Contains(Search.Text) || p.Description.Contains(Search.Text) || 			p.Category.Contains(Search.Text)).ToList();
                    ListViewProduct.ItemsSource = currentProducts;
                };

                if (FilterComboBox.SelectedValue == "Все производители")
                    ListViewProduct.ItemsSource = currentProducts;
                // Фильтрация
                if (FilterComboBox.SelectedValue != null && FilterComboBox.SelectedValue != "Все производители")
                {
                    currentProducts = currentProducts.Where(p => p.Supplier.Trim() == FilterComboBox.SelectedValue.ToString()).ToList();
                    ListViewProduct.ItemsSource = currentProducts;

                    //MessageBox.Show(FilterComboBox.SelectedValue.ToString());
                    // В базу сохраняеются с пробелами при nchar!!!
                }

                CountBlock.Text = $"Количество: {currentProducts.Count} из {db.Products.ToList().Count}";
            }

        }
```

## Добавление изображения

```Csharp
private void AddImageToProduct(object sender, RoutedEventArgs e)
    {
        Stream myStream;
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        if(dlg.ShowDialog() == true)
        {
            if ( (myStream = dlg.OpenFile()) != null )
            {
                string strfilename = dlg.FileName;
                string filetext = File.ReadAllText(strfilename);

                dlg.DefaultExt = ".png";
                dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                dlg.Title = "Open Image";
                dlg.InitialDirectory = "./";
                
                BitmapImage image = new BitmapImage(new Uri(dlg.FileName));
                ImageBox.Source = image;

                try
                {
                        string newRelativePath = $"{System.DateTime.Now.ToString("HHmmss")}_{dlg.SafeFileName}";
                        File.Copy(dlg.FileName, System.IO.Path.Combine(Environment.CurrentDirectory, $"images/{newRelativePath}"));
                        ImagePath = newRelativePath;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }       
            myStream.Dispose();
            
        }
    }
```


## Перезапуск localDb в Visual Studio

1. Open Nuget Console in Tools -> Nuget Package Manager -> Package Manager Console

2. Stop LocalDB Instance typing: sqllocaldb stop MSSQLLocalDB

3. Try to update the table

4. Start LocalDB Instance typing: sqllocaldb start MSSQLLocalDB



## Подключение LocalDb в Dbeaver

https://github.com/dbeaver/dbeaver/issues/2959


# Миграции для WPF

## Установка Nuget-пакетов:
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools

## Обновление пакета инструментов в консоле:
dotnet tool update --global dotnet-ef

## Создание миграции:
dotnet ef migrations add Itinial

## Применение миграции
dotnet ef database update

### Результат:
В папке с проектом будет создана папка Migration

**Замечание**: если база данных уже существует:
dotnet ef migration add Initial
комментирование методов Up и Down
dotnet ef database update 

