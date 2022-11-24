using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Commands
{
    class WindowCommands
    {
        public static RoutedCommand Exit { get; set; }


        static WindowCommands()
        {
            Exit = new RoutedCommand("Exit", typeof(MainWindow));
        }
        
    }
}
