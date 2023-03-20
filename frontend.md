
# Пример MaterialDesign

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

# Стили в ```App.xaml```

```Csharp

	 <Style TargetType="Button">
	    <Setter Property="Background" Value="#FF76E383" />
	    <Setter Property="FontFamily" Value="Comic Sans MS"/>
	    <Setter Property="FontSize" Value="10"/>
	</Style>

	<Style TargetType="ListBox">
	    <Setter Property="Background" Value="#FF498C51" />
	    <Setter Property="FontFamily" Value="Comic Sans MS"/>
	    <Setter Property="FontSize" Value="10"/>
	</Style>

	<Style TargetType="TextBox">
	    <Setter Property="FontFamily" Value="Comic Sans MS"/>
	    <Setter Property="FontSize" Value="10"/>
	</Style>

	<Style TargetType="TextBlock">
	    <Setter Property="FontFamily" Value="Comic Sans MS"/>
	    <Setter Property="FontSize" Value="10"/>
	</Style>

	<Style TargetType="Window">
	    <Setter Property="FontFamily" Value="Comic Sans MS"/>
	    <Setter Property="FontSize" Value="10"/>
	</Style>

	<SolidColorBrush x:Key="ColorPrimery" Color="White"></SolidColorBrush>
	<SolidColorBrush x:Key="ColorSecondary" Color="#FFFFFFE1"></SolidColorBrush>
	<SolidColorBrush x:Key="ColorAccent" Color="#FF76E383"></SolidColorBrush>

```

# Добавление или редактирование

```xml

 <Grid>
        <StackPanel Orientation="Horizontal"
                    VerticalAlignment="Center"
                    HorizontalAlignment="Center">

            <StackPanel VerticalAlignment="Center"
                        HorizontalAlignment="Center"
                        Margin="10">

                <StackPanel Name="idPanel">
                    <TextBlock Margin="1"
                               Height="20"
                               Width="100"
                               Text="Id"/>
                    <TextBox Name="idBox"
                             Margin="1"
                             Height="20"
                             IsReadOnly="True"
                             Text="{Binding Id}"/>

                </StackPanel>

                <StackPanel>
                    <TextBlock Margin="1" Height="20" Width="100" Text="Имя"/>
                    <TextBox Name="nameBox"
                             Margin="1"
                             Height="20"
                             Text="{Binding Name}"/>
                </StackPanel>

                <StackPanel>
                    <TextBlock  Margin="1"
                                Height="20"
                                Text="Категория"/>
                    <ComboBox Name="categoryBox"
                              Margin="1"
                              Height="20"
                              Width="100"
                              SelectedValue="{Binding Category}"/>
                </StackPanel>

                <StackPanel>
                    <TextBlock  Margin="1"
                                Height="20"
                                Text="Производитель"/>
                    <TextBox Name="manufacturerBox"
                             Margin="1"
                             Height="20"
                             Text="{Binding Manufacturer}"/>
                </StackPanel>

                <StackPanel>
                    <TextBlock  Margin="1"
                                Height="20"
                                Text="Стоимость"/>
                    <TextBox Name="costBox"
                             Margin="1"
                             Height="20">
                        <TextBox.Text>
                            <Binding Path="Cost">
                                <Binding.ValidationRules>
                                    <ExceptionValidationRule />
                                    <DataErrorValidationRule />
                                </Binding.ValidationRules>
                            </Binding>
                        </TextBox.Text>

                    </TextBox>

                </StackPanel>

                <StackPanel>
                    <TextBlock  Margin="1"
                                Height="20"
                                Text="Скидка"/>
                    <TextBox Name="discountAmountBox" Margin="1" Height="20"
                         Text="{Binding DiscountAmount}"/>


                </StackPanel>

            </StackPanel>


            <StackPanel Margin="10"
                        VerticalAlignment="Center"
                        HorizontalAlignment="Center">


                <StackPanel>
                    <TextBlock Margin="1"
                               Height="20"
                               Width="100"
                               Text="Артикль"/>
                    <TextBox Name="articleBox"
                             Margin="1"
                             Height="20"
                             Text="{Binding ArticleNumber}"/>
                </StackPanel>


                <StackPanel>
                    <TextBlock  Margin="1"
                                Height="20"
                                Text="Поставщик"/>
                    <TextBox Name="supplierBox" Margin="1" Height="20" Width="100"
                         Text="{Binding Supplier}"/>
                </StackPanel>

                <StackPanel>
                    <TextBlock  Margin="1"
                                Height="20"
                                Text="Ед.измерения"/>
                    <TextBox Name="unitBox" Margin="1" Height="20" Width="100"
                         Text="{Binding Unit}"/>
                </StackPanel>

                <StackPanel>
                    <TextBlock  Margin="1"
                                Height="20"
                                Text="Количество"/>
                    <TextBox Name="quantityInStockBox"
                             Margin="1"
                             Height="20">
                        <TextBox.Text>
                            <Binding Path="QuantityInStock">
                                <Binding.ValidationRules>
                                    <DataErrorValidationRule />
                                </Binding.ValidationRules>
                            </Binding>
                        </TextBox.Text>
                    </TextBox>

                </StackPanel>

                <StackPanel>
                    <TextBlock  Margin="1"
                                Height="20"
                                Text="Статус"/>
                    <TextBox Name="statusBox" Margin="1" Height="20"
                         Text="{Binding Status}"/>
                </StackPanel>

                <StackPanel>
                    <TextBlock  Margin="1"
                                Height="20"
                                Text="Макс.скидка"/>
                    <TextBox Name="maxDiscountBox" Margin="1" Height="20"
                         Text="{Binding MaxDiscount}"/>
                </StackPanel>

            </StackPanel>

            <StackPanel Margin="10"
                        VerticalAlignment="Center"
                        HorizontalAlignment="Center">

                <StackPanel>
                    <TextBlock  Margin="1"
                                Height="20"
                                Text="Описание"/>
                    <TextBox Name="descriptionBox"
                             Margin="1"
                             Height="200"
                             Width="150"
                             TextWrapping="Wrap"
                             Text="{Binding Description}"
                        />
                    <Button Margin="0 10 0 0"
                        Height="20"
                        Content="Сохранить"
                        Click="saveProductButtonClick"/>
                </StackPanel>
            </StackPanel>

            <StackPanel Margin="10"
                        VerticalAlignment="Center"
                        HorizontalAlignment="Center">

                <TextBlock  Margin="1" Height="20" Text="Фото"/>

                <TextBox Name="imageBox"
                         Margin="1"
                         Height="20"
                         IsReadOnly="True"
                         Visibility="Collapsed"
                         Text="{Binding Photo}"/>

                <Border Margin="1"
                        Height="200"
                        Width="150"
                        BorderBrush="#FF76E383"
                        BorderThickness="2"
                        >
                    <Image Name="imageBoxPath"
                           Margin="1"
                           
                           Source="{Binding ImagePath}">
                    </Image>
                </Border>





                <Button Height="20"
                        Margin="0 10 0 0"
                        Content="Добавить"
                        Click="AddImageToProduct"/>

            </StackPanel>


        </StackPanel>
    </Grid>

```

# Авторизация

```xml


<Grid>
        <StackPanel VerticalAlignment="Center"
                    HorizontalAlignment="Center">
            <StackPanel>
                <Image Name="logo"  Source="Resources/logo.png" Height="100" Width="100" RenderTransformOrigin="0.5,0.5"/>
            </StackPanel>

            <StackPanel>
                <TextBlock Margin="1" Height="20" Width="135" Text="Логин"/>
                <TextBox Name="loginBox" Margin="1" Height="20" />
            </StackPanel>

            <StackPanel>
                <TextBlock Margin="1" Height="20" Text="Пароль"/>
                <PasswordBox Name="passwordBox" Margin="1" Height="20"/>
            </StackPanel>

            <StackPanel>
                <Button Name="loginButton"
                        Margin="0 10 0 0"
                        Height="20"
                        IsDefault="True"
                        Content="Авторизация"
                        Click="loginButton_Click"/>
            </StackPanel>

            <StackPanel>
                <Button Margin="0 10 0 0" Height="20" Content="Гость"
                        Click="guestButtonClick"/>
            </StackPanel>

            <StackPanel>
                <TextBlock Name="captchaBlock"
                           Margin="0 10 0 0">
                    
                    <TextBlock.Effect>
                        <BlurEffect RenderingBias="Quality" KernelType="Box" Radius="0.5"/>
                    </TextBlock.Effect>

                </TextBlock>
                
                <TextBox Name="captchaBox"/>

            </StackPanel>

        </StackPanel>
    </Grid>


```
# MainWindow

```xml

<Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="*"/>
            <RowDefinition Height="4*"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>


        <Grid VerticalAlignment="Center" HorizontalAlignment="Stretch" >
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="*"/>
                <ColumnDefinition Width="*"/>
                <ColumnDefinition Width="*"/>
                <ColumnDefinition Width="*"/>
                <ColumnDefinition Width="Auto"/>
                <ColumnDefinition Width="Auto"/>
                <ColumnDefinition Width="2*"/>
                
                
            </Grid.ColumnDefinitions>

            <StackPanel 
                        VerticalAlignment="Center"
                        HorizontalAlignment="Center"
                        Grid.Column="0">
                <Image Source="Resources/logo.png" Height="100" Width="150"/>
            </StackPanel>


            <StackPanel Margin="1" Grid.Column="1" VerticalAlignment="Center"
                        HorizontalAlignment="Center">
                <TextBlock Margin="1" Text="Сортировка" Height="20" />
                <ComboBox Margin="1" Name="sortUserComboBox" Height="20" Width="150"
                          SelectionChanged="sortUserComboBox_SelectionChanged"/>
            </StackPanel>


            <StackPanel Margin="1" Grid.Column="2" VerticalAlignment="Center"
                        HorizontalAlignment="Center">
                <TextBlock Margin="1" Text="Фильтр" Height="20" />
                <ComboBox Margin="1" Name="filterUserComboBox" Height="20" Width="150"     
                          SelectionChanged="filterUserComboBox_SelectionChanged"/>
            </StackPanel>


            <StackPanel Margin="1" Grid.Column="3" VerticalAlignment="Center"
                        HorizontalAlignment="Center">
                <TextBlock Margin="1" Text="Поиск" Height="20" Width="100"/>
                <TextBox Margin="1" Name="searchBox" Width="100" Height="20"
                         TextChanged="searchBox_TextChanged" />
            </StackPanel>



            <StackPanel Margin="1"
                        VerticalAlignment="Center"
                        HorizontalAlignment="Center"
                        Grid.Column="4">
                <TextBlock Margin="1"
                           TextAlignment="Center"
                           Name="countProducts"
                           Height="20" Width="150"
                           Text="Количество: "/>

                <Button Margin="1"
                            Name="сlearButton"
                        
                            Width="100"
                            Height="20"
                            Content="Очистить"
                            Click="сlearButton_Click"
                             />
            </StackPanel>

            <StackPanel 
                        VerticalAlignment="Center"
                        HorizontalAlignment="Center"
                        Grid.Column="6">

                <TextBlock Name="statusUser" Margin="1" Text="Роль: Иванов Иван Иванович" Height="20" Width="200" TextAlignment="Center"/>
                <Button Margin="1" Content="Выход" Height="20" Width="100"
                        Click="exitButtonClick"/>

            </StackPanel>

        </Grid>

        <ListView Grid.Row ="1"
	              x:Name="productlistView"
	              ScrollViewer.HorizontalScrollBarVisibility="Disabled"
                  HorizontalContentAlignment="Center"
                  MouseDoubleClick="EditProduct_MouseDoubleClick"
                  >

            <ListView.ItemsPanel>
                <ItemsPanelTemplate>
                    <WrapPanel Orientation="Horizontal"
                               HorizontalAlignment="Center"/>
                </ItemsPanelTemplate>
            </ListView.ItemsPanel>

            <ListView.ItemTemplate>
                <DataTemplate>
                    <StackPanel Width="300">

                        <TextBlock Text="{Binding Name, StringFormat=Название: {0}}"
				                       VerticalAlignment="Center"
				                       TextAlignment="Center"
				                       TextWrapping="Wrap"
				                       HorizontalAlignment="Center"
				                       Margin="0"
                                       FontSize="15"
				                       />

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


                        <TextBlock Text="{Binding ArticleNumber, StringFormat=Артикль: {0}}"

				                       Margin="5 5"
                                       FontSize="15"
                                       />
                        <!--
                            <TextBlock Text="{Binding Description, StringFormat=Описание: {0}}"
				                       Margin="5 5"
                                       FontSize="15"
                                       />
                         -->

                        <TextBlock Text="{Binding Category, StringFormat=Категория: {0}}"
				                       Margin="5 5"
                                       FontSize="15"
                                       />
                        <TextBlock Text="{Binding Manufacturer, StringFormat=Производитель: {0}}"
				                       Margin="5 5"
                                       FontSize="15"
                                       />
                        <TextBlock Text="{Binding Cost, StringFormat=Стоимость: {0}}"
				                       Margin="5 5"
                                       FontSize="15"
                                       />
                        <TextBlock Text="{Binding DiscountAmount, StringFormat=Скидка: {0}%}"
				                       Margin="5 5"
                                       FontSize="15"
                                      />
                        <TextBlock Text="{Binding QuantityInStock, StringFormat=Количество на складе: {0}}"
				                       Margin="5 5"
                                       FontSize="15"
                                       />
                        <TextBlock Text="{Binding Status, StringFormat= Статус: {0}}"
				                       Margin="5 5"
                                       FontSize="15"
                                      />
                        <TextBlock Text="{Binding MaxDiscount, StringFormat= Максимальная скидка: {0}}"
				                       Margin="5 5"
                                       FontSize="15"
                                       />
                        <TextBlock Text="{Binding Supplier, StringFormat= Поставщик: {0}}"
				                       Margin="5 5"
                                       FontSize="15"
                                       />
                        <TextBlock Text="{Binding Unit, StringFormat= Ед.измерения: {0}}"
				                       Margin="5 5"
                                       FontSize="15"
                                       />




                    </StackPanel>

                </DataTemplate>
            </ListView.ItemTemplate>


            <ListView.ItemContainerStyle>
                <Style TargetType="ListViewItem">

                    <Style.Triggers>
                        <DataTrigger Binding="{Binding QuantityInStock}" Value="0">
                            <Setter Property="Background" Value="Gray" />
                        </DataTrigger>
                    </Style.Triggers>
                </Style>
            </ListView.ItemContainerStyle>

        </ListView>


        <StackPanel Grid.Row="2"
                    VerticalAlignment="Center"
                    HorizontalAlignment="Center">

            <StackPanel Orientation="Horizontal"
                        HorizontalAlignment="Center"
                        >

                <Button Name="addProduct" Margin="5"
                            Content="Добавить товар" Height="20" Width="100"
                            Click="addProductButtonClick"/>

                <Button Name="deleteProduct" Margin="5"
                            Content="Удалить товар" Height="20" Width="100"
                            Click="delUserButton"/>

            </StackPanel>

        </StackPanel>

    </Grid>

```

# Библиотека классов

```Csharp

namespace SF2022UserLib
{
    public class Calculations
    {
        public string[] AvailablePeriods(TimeSpan[] startTimes,
                                       int[] durations,
                                       TimeSpan beginWorkingTime,
                                       TimeSpan endWorkingTime,
                                       int consultationTime)
        {

            TimeSpan consultation = TimeSpan.FromMinutes(consultationTime);

            List<string> protectIntervals = new List<string> {

                startTimes[0].ToString(),
                startTimes[1].ToString(),
                startTimes[2].ToString(),
                startTimes[3].ToString(),
                startTimes[4].ToString()

            };

            List<int> durations1 = new List<int>{

                durations[0],
                durations[1],
                durations[2],
                durations[3],
                durations[4]

            };

            List<string> reserveTime = new List<string>(); // "10:00:00 60"

            List<string> Intervals = new List<string>();
            List<string> IntervalsReserve = new List<string>();

            for (int i = 0; i < protectIntervals.Count; i++)
            {
                reserveTime.Add($"{protectIntervals[i]} {durations1[i]}");
            }

            Console.WriteLine();
            Console.WriteLine("Резервные интервалы: ");

            foreach (string interval in reserveTime)
            {
                Console.WriteLine(interval);
            }

            Console.WriteLine();

            while (true)
            {

                // если начальное время не существует в резерве
                if (!protectIntervals.Contains(beginWorkingTime.ToString()))
                {

                    Console.WriteLine($"{beginWorkingTime:hh\\:mm} не содержится в резерве");

                    // если в период консультации есть резерв, то пропустить время для консультации
                    foreach (string str in protectIntervals)
                    {
                        TimeSpan span = TimeSpan.Parse(str);

                        bool result = (beginWorkingTime < span) && ((beginWorkingTime + consultation) > span);


                        if (result == true)
                        {
                            Console.WriteLine($"{span} находится между {beginWorkingTime:hh\\:mm} и {(beginWorkingTime + consultation):hh\\:mm}");
                            beginWorkingTime = span;
                            Console.WriteLine("Выход из цикла for");
                            Console.WriteLine($"{beginWorkingTime:hh\\:mm}");
                            break;

                        }
                    }

                    if (!protectIntervals.Contains(beginWorkingTime.ToString()))
                    {
                        // запись в массив разрешенных интервалов
                        Intervals.Add($"{beginWorkingTime:hh\\:mm}-{(beginWorkingTime + consultation):hh\\:mm}");
                        // обновление начала
                        beginWorkingTime = beginWorkingTime + consultation;
                        Console.WriteLine($"Начало: {beginWorkingTime:hh\\:mm}");
                    }

                }
                else
                {
                    Console.WriteLine($"{beginWorkingTime:hh\\:mm} содержится в резерве");
                    // если начало существует в резерве

                    // находим элемент, с которым начало совпало в резерве
                    foreach (string p in reserveTime)
                    {

                        if (beginWorkingTime == TimeSpan.Parse(p.Split(" ")[0]))
                        {
                            TimeSpan span = TimeSpan.Parse(p.Split(" ")[0]);
                            // вычисляем длительность резерва
                            // добавляем интервал в резервный массив
                            IntervalsReserve.Add($"{span:hh\\:mm}-{(span + TimeSpan.FromMinutes(Convert.ToInt32(p.Split(" ")[1]))):hh\\:mm}");
                            // вычисляем начало
                            beginWorkingTime = span + TimeSpan.FromMinutes(Convert.ToInt32(p.Split(" ")[1]));
                            Console.WriteLine($"Начало: {beginWorkingTime:hh\\:mm}");
                        }
                    }

                }

                if (beginWorkingTime >= endWorkingTime)
                {
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Разрешенные интервалы: ");

            int t = 0;
            string[] str1 = new string[Intervals.Count];

            foreach (string interval in Intervals)
            {
                Console.WriteLine(interval);
                str1[t] = interval;
                t++;
            }

            Console.WriteLine();
            Console.WriteLine("Зарезервированные интервалы: ");

            foreach (string interval in IntervalsReserve)
            {
                Console.WriteLine(interval);
            }

            return str1; // массив строк формата HH:mm - HH:mm
        }
    }
}

```



