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

```csharp
 // находим диапазон с фото
                Word.Range range1 = aDoc.Content;
                range1.Find.Execute(FindText: "[Фото]");
                
                // добавляем рядом картинку
                Word.InlineShape ils = aDoc.InlineShapes.AddPicture(abiturient.ImagePath, false, true, Range: range1);
		
                // удаляем слово фото
                range1.Find.Execute(FindText: "[Фото]", ReplaceWith: "", Replace: Word.WdReplace.wdReplaceAll);
```