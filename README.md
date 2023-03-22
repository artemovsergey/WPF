# WPF
## Паттерны проектирования

![](image/Patterns.png)



# App.config

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

		<add name="PostgreSQL"
			 connectionString="Host=localhost;Port=5432;Database=Keeper;Username=postgres;Password=root"
			 providerName="System.Data.Npgsql" />

	</connectionStrings>

</configuration>
```

# UserContext

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

# Загрузка ```View``` вручную в ```App.cs```

- Переместить MainWindow.xaml в папку ```Views```

- Удалить в файле App.xaml опацию ```Startup```

- Подключение в представление объекта модели представления 

```xml
xmlns:local="clr-namespace:TNC.WPF"
```

```xml
<Window.DataContext>
   <vm:UserViewModel/>
</Window.DataContext>
```

```Csharp

public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow mv = new MainWindow();
            //MainWindowViewModel vm = new MainWindowViewModel();
            //mv.DataContext = vm;
            mv.Show();
        }

    }

```

# Команда как свойство во ```ViewModel``` и определение команды в конструкторе

```Csharp

#region Команда CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; set; }

        private void OnCloseApplicationCommandExecuteed(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseApplicationCommandExecute(object p)
        {
            return true;
        }
        #endregion

        #region Команда MessageCommand
        private TestCommand messageCommand;
        public TestCommand MessageCommand
        {
            get
            {
                if (messageCommand == null)
                    messageCommand = new TestCommand();
                return messageCommand;
            }
            set
            {
                messageCommand = value;
            }
        } 
        #endregion


        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuteed, CanCloseApplicationCommandExecute);
        }

```

# Команда ```LambdaCommand```

```Csharp

internal class LambdaCommand : Command
    {

        // если поля помечены readonly, то они будут работать быстрее
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        // в конструкторе надо получить два делегата Action и Func
        public LambdaCommand(Action<object> Execute, Func<object, bool>  CanExecute = null) 
        {
            _execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _canExecute = CanExecute;
        }

        public override bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
                
        public override void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
    
```

# Отдельная команда TestCommand
```Csharp
public class TestCommand : ICommand
    {

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object? parameter)
        {
            return 2 > 1;
        }
        public void Execute(object? parameter)
        {
            MessageBox.Show(parameter.ToString());
        }
    }
```

# Команда LambdaCommand через obj
```Csharp
#region LambdaCommand через obj
        private LambdaCommand addCommand;
        public LambdaCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new LambdaCommand(obj =>
                  {
                      // obj - это CommandParametr
                      MessageBox.Show(obj.ToString());
                  }, 

                  // условие при котором команда будет активна
                  (obj) => (bool)(App.Current.MainWindow.FindName("language") as RadioButton).IsChecked)
                  );   
            }
        }
        #endregion
```


# Базовая ```ViewModel```

```Csharp

internal abstract class ViewModel : INotifyPropertyChanged, IDisposable 
    {

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        // разрешить кольцевые обновления свойств без зацикливания
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
        #endregion

        #region Disposable
        // деструктор
        ~ViewModel()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private bool _Disposed;

        protected virtual void Dispose(bool Disposing)
        {
            if (!Disposing || _Disposed) return;
            _Disposed = true;
            // Освобождение управляемых ресурсов 
        } 
        #endregion

    }

```

# Базовый класс ```Command```

```Csharp

// команды тоже как свойства ViewModel

    internal abstract class Command : ICommand
    {

        /*
         CanExecuteChanged уведомляет любые источники команд
         (такие как Button или CheckBox), привязанные к этому ICommand,
         об изменении значения, возвращаемого CanExecute.
         Источники команд заботятся об этом, потому что обычно им необходимо
         соответствующим образом обновлять свой статус (например, кнопка отключится, если CanExecute() вернет false).
         */

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested+= value;
            remove => CommandManager.RequerySuggested-=value;
        }

        // возможно ли выполнить команду?
        public abstract bool CanExecute(object? parameter);
        

        // логика команды
        public abstract void Execute(object? parameter);    
        
    }

```

# Связь команд с событиями 

Подключить ```Microsoft.Xaml.Behavior.Wpf```

```xml
xmlns:i="http://schemas.microsoft.com/xaml/behaviors"
```

```Csharp
                <i:Interaction.Triggers>
                    <i:EventTrigger EventName="MouseDoubleClick">
                        <i:InvokeCommandAction
                        Command="{Binding EditUserCommand}"
                        CommandParameter="{Binding ElementName=userList, Path=SelectedItem}"/>
                    </i:EventTrigger>
                </i:Interaction.Triggers>
```

# Команда ```TestCommand``` как отдельный класс

```Csharp

internal class TestCommand : Command
    {

        /*
          при загрузке окна он проверит возвращаемое значение CanExecute,
          и если он вернет true, то он включит элемент управления кнопки,
          и метод Execute готов к использованию, 
          в противном случае элемент управления кнопки отключен.
         */

        public override bool CanExecute(object? parameter)
        {
            return true;
        }


        // CommandParameter отправляется как для событий CanExecute, так и для событий Execute.

        public override void Execute(object? parameter)
        {
            //MessageBox.Show("Логика команды");

            MessageBox.Show(CanExecute(null).ToString());

            new Window1(parameter).Show();

            Application.Current.MainWindow.Close();

        }
    }

```

**Замечание**: дизайнер WPF работает с бинарниками, поэтому каждый раз как что-то поменяли в коде надо пересобрать проект


# Вынесение команды в ресурсы как синглтон

```xml

    <Window.Resources>
        <command:TestCommand x:Key="TestCommand"/>
    </Window.Resources>
```

# Команда на клавиши

```xml

<Window.InputBindings>
        <KeyBinding Modifiers="Ctrl" Key="Q" Command="{Binding CloseApplicationCommand}"/>
</Window.InputBindings>

```

# Подключение команды в XAML

```xml
<!-- Подключение команды как свойства ViewModel  -->
 Command="{Binding CloseApplicationCommand}" CommandParameter="{Binding ElementName=items, Path=SelectedItem}"

```

# Подключение команды в разметке XAML как контент
```xml

<!-- Отдельная команда в отдельном классе            
 Лучше вынести в отдельные ресурсы, ибо может быть расточительно для памяти -->
                
<Button.Command>
  <command:TestCommand/>
</Button.Command>

```

# Применение команд через ```InputBinding```
```xml
            <Rectangle Width="45"
                               Height="45"
                               Fill="Green"
                               RadiusX="3"
                               RadiusY="3">

                <Rectangle.InputBindings>
                    <MouseBinding Command="{Binding CloseApplicationCommand}" MouseAction="LeftDoubleClick" />
                </Rectangle.InputBindings>
            </Rectangle>

            <Rectangle Width="45"
                               Height="45"
                               Fill="Blue"
                               RadiusX="3"
                               RadiusY="3">

                <Rectangle.InputBindings>
                    <MouseBinding Command="{StaticResource TestCommand}" MouseAction="LeftDoubleClick" />
                </Rectangle.InputBindings>
            </Rectangle>
```

# DataGrid определение

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


# Интерфейс INotifyPropertyChanged

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

# Entity Framework Core 6. Cвязь моделей 1 : M

# Model User

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

# Модель Role

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
																  
# MaterialDesign

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

# Подключение MaterialDesign
 
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

# Scaffold

**Замечание**: надо установить ```Microsoft.EntityFrameworkCore.Tools```.

В консоли диспетчера пакетов Nuget прописать команду

```Scaffold-DbContext "Server=localhost;Database=Users;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models```
	
Команда создает модели из каждой сущности в базе данных, учитывая связи, а также создает класс контекста для работы с данными как с классами.

```Scaffold-DbContext "Data Source=.\ComputerDatabase.db" Microsoft.EntityFrameworkCore.Sqlite -OutputDir Models```

**Примечание**: если Scaffold для SQLite, ему нужна база из проекта, а не в ```Debug```. При инициализации контекста база данных ```SQLite``` создается в ```Debug``` по умолчанию.


В консоле диспетчера пакетов для SQLServer

```
Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=UserDatabase;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models2
```

# Кнопка удаления с диалогом

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

# Задание начальных значений для списка

```csharp
var allTypes = db.Users.ToList();

allTypes.Insert(0, new User { Login = "Все типы" });
ComboBox.ItemsSource = allTypes;//.Select(p => p.Login);

ComboBox.SelectedIndex = 0;
```

# Список ```Combobox``` в xaml

```xml
 <ComboBox Width="100"
	   Name="ComboBox"
	   DisplayMemberPath="Login"
	   SelectionChanged="ComboBox_SelectionChanged"></ComboBox>
```

# ```ListView```

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

# Внешний ключ SQL Server

```SQL
CONSTRAINT [FK_Abiturients_Specialty] FOREIGN KEY ([specialty_id]) REFERENCES [dbo].[Specialty] ([Id])
```

# Применение глобального шрифта к страницам Page или Window
							    
```Csharp
Style = (Style)FindResource(typeof(Page));
```

# Include

```Csharp
productGrid.ItemsSource = db.Abiturients.Include(p => p.Specialty).ToList();
//Без использования метода Include мы бы не могли бы получить связанную команду и ее свойства: p.Specialty.Name
```

# Переход на страницу Вперед

```Csharp
private void ForwardButton_Click(object sender, RoutedEventArgs e)
    {
        productGrid.ItemsSource = db.Abiturients.Skip(step).Take(10).ToList();
        if (step + 10 < db.Abiturients.Count())
            step += 10;

        CountAbiturients.Text = $"Количество: {db.Abiturients.Skip(step).Take(10).ToList().Count} из {db.Abiturients.ToList().Count}";
    }	
```
						   
# Переход на страницу Назад

```Csharp
private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (step > 0)
            step -= 10;
        productGrid.ItemsSource = db.Abiturients.Skip(step).Take(10).ToList();

        CountAbiturients.Text = $"Количество: {db.Abiturients.Skip(step).Take(10).ToList().Count} из {db.Abiturients.ToList().Count}";
    }
```

# Атрибуты для DataGrid

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

# Триггеры в DataGrid на Row

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

# Вinding даты через Stringformat
	
```xml
    Binding="{Binding BirthDay, StringFormat={}{0:dd.MM.yyyy}}"
```
	
# Авторизация логика

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

	
# Валидация

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


# Binding по полному пути картинки

```xml
    <Image>
        <Image.Source>
            <BitmapImage DecodePixelWidth="100" DecodePixelHeight="100"
            UriSource = "{Binding ImagePath}"/>
        </Image.Source>
    </Image>
```

# DataPicker

```xml
    <DatePicker SelectedDate="{Binding BirthDay}"  Name="birth_day"/>
```
	
# ComboBox

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

# Изображение по абсолютному пути

```Csharp
    BitmapImage image = new BitmapImage(new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, $"{_currentAbiturient.Image}"), UriKind.Absolute));
    ImagePicture.Source = image;
```
		
# Замена . на , для SQL Server

```csharp
    Ball = Convert.ToDouble(ball.Text.Replace(".", ",")) // SQL Server принимает дробные значения с запятой
```
	
# Полные сообщения об ошибках

```csharp
    catch (Exception ex)
    {
        MessageBox.Show($"Ошибка: {ex.InnerException.Message}");
    }
```

# Captcha

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

# Асинхронная задача для выключения кнопки

```csharp
	async void disableButton()
		{
		    LoginButtonName.IsEnabled = false;
		    await Task.Delay(TimeSpan.FromSeconds(10));
		    LoginButtonName.IsEnabled = true;
		}
```

# Триггер для ListView

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

# Border

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


# Выборка по столбцу Select в EF

```csharp
    var Suppliers = db.Products.Select(p => p.Supplier).Distinct().ToList();
```

# Начальные значения для сортировки и фильтрации

```csharp
    SortComboBox.ItemsSource = new List<String>() { "Цена", "По убыванию", "По возрастанию" };   //.Select(p => p.Login);
    SortComboBox.SelectedIndex = 0;

    Suppliers.Insert(0, "Все производители");
    FilterComboBox.ItemsSource = Suppliers;
    FilterComboBox.SelectedIndex = 0;
```
**Замечания**: событие SelectionChanged выбора срабатывает после смены значения в списке, т.е вначале первое значение стоит поставить нейтральное



# Добавление изображения

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

# Перезапуск localDb в Visual Studio

1. Open Nuget Console in Tools -> Nuget Package Manager -> Package Manager Console

2. Stop LocalDB Instance typing: sqllocaldb stop MSSQLLocalDB

3. Try to update the table

4. Start LocalDB Instance typing: sqllocaldb start MSSQLLocalDB


# Подключение LocalDb в Dbeaver

https://github.com/dbeaver/dbeaver/issues/2959


# Миграции

## Установка Nuget-пакетов

Microsoft.EntityFrameworkCore.Design

Microsoft.EntityFrameworkCore.Tools

## Обновление пакета инструментов в консоле:
dotnet tool update --global dotnet-ef

## Создание миграции:
dotnet ef migrations add Itinial

## Применение миграции
dotnet ef database update

## Результат:
В папке с проектом будет создана папка Migration

**Замечание**: если база данных уже существует:
dotnet ef migration add Initial
комментирование методов Up и Down
dotnet ef database update 

# Application.Current.MainWindow

Application.MainWindow автоматически устанавливается в первое окно, открытое в приложении. Когда окно закрывается, оно устанавливается либо на следующее открытое окно, либо на ноль, если открытых окон нет.

# TextBlock

```xml

<TextBlock.Effect>
 <BlurEffect RenderingBias="Quality" KernelType="Box" Radius="0.5"/>
</TextBlock.Effect>

```



# Image в ListView

```xml

 <Border BorderBrush="#FF498C51" BorderThickness="2">
                            <Image 
			                       HorizontalAlignment="Center"
			                       Height="200"
			                       Width="200"
                                   ToolTip="{Binding Description}">

                                <Image.Source>
                                    <BitmapImage UriSource = "{Binding ImagePath,TargetNullValue=Resources/picture.png}"
                                                 CacheOption="OnLoad"
                                                 CreateOptions="IgnoreImageCache"
                                                 
                                                 >
                                    </BitmapImage>

                                </Image.Source>
                            </Image>
                        </Border>

```

# UpdateProducts

```Csharp

 using (SportStoreContext db = new SportStoreContext())
            {

                var currentProducts = db.Products.ToList();
                productlistView.ItemsSource = currentProducts;

                //Сортировка
                if (sortUserComboBox.SelectedIndex != -1)
                {
                    if (sortUserComboBox.SelectedValue == "По убыванию цены")
                    {
                        currentProducts = currentProducts.OrderByDescending(u => u.Cost).ToList();

                    }

                    if (sortUserComboBox.SelectedValue == "По возрастанию цены")
                    {
                        currentProducts = currentProducts.OrderBy(u => u.Cost).ToList();

                    }
                }


                // Фильтрация
                if (filterUserComboBox.SelectedIndex != -1)
                {
                    if (db.Products.Select(u => u.Manufacturer).Distinct().ToList().Contains(filterUserComboBox.SelectedValue))
                    {
                        currentProducts = currentProducts.Where(u => u.Manufacturer == filterUserComboBox.SelectedValue.ToString()).ToList();
                    }
                    else
                    {
                        currentProducts = currentProducts.ToList();
                    }
                }

                // Поиск

                if (searchBox.Text.Length > 0)
                {

                    currentProducts = currentProducts.Where(u => u.Name.Contains(searchBox.Text) || u.Description.Contains(searchBox.Text)).ToList();

                }

                productlistView.ItemsSource = currentProducts;

                countProducts.Text = $"Количество: {currentProducts.Count} из {db.Products.ToList().Count}";

            }

```

# MainWindow

```Csharp

MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

```

# Добавление изображения

```Csharp

 private void AddImageToProduct(object sender, RoutedEventArgs e)
        {

            Stream myStream;

            if (currentProduct != null)
            {
                oldImage = System.IO.Path.Combine(Environment.CurrentDirectory, $"images/{currentProduct.Photo}");
            }
            else
            {
                oldImage = null;
            }

            // проверяем, есть ли изображение у товара и запоминаем путь к изображению сейчас

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                if ((myStream = dlg.OpenFile()) != null)
                {
                    dlg.DefaultExt = ".png";
                    dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                    dlg.Title = "Open Image";
                    dlg.InitialDirectory = "./";

                    // Предпросмотр изображения
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    image.UriSource = new Uri(dlg.FileName);
                    
                    //MessageBox.Show($"Изображения: {image.Width} на {image.Height} пикселей. Размер будет приведен к 200 на 300 пикселей! ");
                    //image.DecodePixelWidth = 300;
                    //image.DecodePixelHeight = 200;
                    imageBoxPath.Source = image;
                    image.EndInit();

                    try
                    {
                        
                        newImage = dlg.SafeFileName;
                        //MessageBox.Show($"newImage: {newImage}");
                        newImagePath = dlg.FileName;
                        //MessageBox.Show($"newImagePath: {newImagePath}");
                        //MessageBox.Show(File.Exists(newImagePath).ToString());

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                myStream.Dispose();
            }

        }

```

# Свойство ImagePath

```Csharp

    public virtual string? ImagePath { 
        
        get {
            if (File.Exists(System.IO.Path.Combine(Environment.CurrentDirectory, $"images/{Photo}")))
            {
                return System.IO.Path.Combine(Environment.CurrentDirectory, $"images/{Photo}");
            }
            else
            {
                Photo = "picture.png";
                return null;
            }
        }
    
    }

```

# Каскадное удаление или обновление SQLServer

```sql

CREATE TABLE [dbo].[RelatedProducts] (
    [Id]              INT IDENTITY (1, 1) NOT NULL,
    [ProductId]       INT NOT NULL,
    [RelatedProdutId] INT NOT NULL,
    CONSTRAINT [PK_RelatedProducts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__RelatedProducts__ProductId__571DF1D5] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK__RelatedProducts__RelatedProdutId__571DF1D5] FOREIGN KEY ([RelatedProdutId]) REFERENCES [dbo].[Product] ([Id])
);

```

# Конвертеры привязки
```Csharp
public class TitleConverter : IValueConverter
    {

        // source to binding
        // от источника к чему привязываемся (свойствоа VM) к приемнику (xaml)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"Компания:{(string)value}";
        }

        // binding to source
        // от интерфейса(xaml) к свойству VM
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value;
        }

    }
 ```   
 
 ```xml
 Title="{Binding Title,Converter={StaticResource ResourceKey=titleconverter}}"
```

```xml
<Application.Resources>
	<cnv:TitleConverter x:Key="titleconverter" />
</Application.Resources>
```

# Генератор кода

```Csharp
public static class CodeGeneration
    {
        public static string Refresh()
        {
            string code = "";
            Random rnd = new Random();
            int n;
            string st = "@#+-#$%^&*!ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
           
                for (int j = 0; j < 8; j++)
                {
                    n = rnd.Next(0, st.Length - 1);
                    code += st.Substring(n, 1);
                }

            return code;
        }
    }
```    
    
    
# Применение фокуса при MVVM
 
 ```Csharp
 public static class FocusExtension
    {
        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached(
                "IsFocused", typeof(bool), typeof(FocusExtension),
                new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));

        private static void OnIsFocusedPropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var uie = (UIElement)d;
            if ((bool)e.NewValue)
            {
                uie.Focus(); // Don't care about false values.
            }
        }
    }
 ```  

```xml
focus:FocusExtension.IsFocused="{Binding PasswordFocus}"
```

```Csharp
   public virtual bool PasswordFocus
        {
            get { return _passwordFocus; }

            set
            {
                Set(ref _passwordFocus, value);
            }

        }
```


# PasswordBox Binding MVVM
 
 ```xml
 <KeyBinding Key="Return" Command="{Binding LoginCommand}"
                                 CommandParameter="{Binding ElementName=passwordName}"/>
```

# Image

```xml
 <Image Source="pack://application:,,,/Resources/Media/volume.png"/>
```







