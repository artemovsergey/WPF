using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVM.Models;
using MVVM.Interface;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace MVVM.Service
{
    public class JsonFileService : IFileService
    {
        public List<Phone> Open(string filename)
        {
            List<Phone> phones = new List<Phone>();
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<Phone>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                phones = jsonFormatter.ReadObject(fs) as List<Phone>;
            }

            return phones;
        }

        public void Save(string filename, List<Phone> phonesList)
        {
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<Phone>));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, phonesList);
            }
        }
    }
}
