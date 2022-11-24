using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVM.Interface
{
    public interface IDialogService
    {
        void ShowMessage(string message);	// показ сообщения
        string FilePath { get; set; }	// путь к выбранному файлу
        bool OpenFileDialog();	// открытие файла
        bool SaveFileDialog();	// сохранение файла
    }
}
