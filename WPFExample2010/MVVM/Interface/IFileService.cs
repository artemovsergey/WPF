using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVM.Models;

namespace MVVM.Interface
{
    public interface IFileService
    {
        List<Phone> Open(string filename);
        void Save(string filename, List<Phone> phonesList);
    }
}
