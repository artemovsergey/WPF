using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Validation
{
    public class User : IDataErrorInfo
    {

        
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        
        // реализация через switch

        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Age":
                        if ((Age < 0) || (Age > 100))
                        {
                            error = "Возраст должен быть больше 0 и меньше 100";
                        }
                        break;
                    case "Name":
                        //Обработка ошибок для свойства Name
                        break;
                    case "Position":
                        //Обработка ошибок для свойства Position
                        break;
                }
                return error;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }





        /* реализация через dictionary

        Dictionary<string, string> errors;
        
        public bool IsValid {
            get 
            {
              return errors.Values.Any(x => x != null);
            }
        } 
         
        public string this[string columnName]{

            get
            {
               if(errors.ContainsKey(columnName)){
                  return errors[columnName];
               }
               else
               {
                  return null;
               };
            }
        }

        public int Age1
        {
            get
            {
             return Age1;
            }

            set
            {
                Age1 = value; // не забываем вашу реализацию INotifyPropertyChanged
                if (true) // если данные некорректны
                {
                    errors["Age1"] = "Текст сообщения об ошибке";
                }
                else
                {
                    errors["Age1"] = null;
                }
            }
         
         */

        }



}



