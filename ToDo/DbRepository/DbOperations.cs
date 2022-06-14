using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToDo.Models;

namespace ToDo.DbRepository
{
    internal class DbOperations
    {
        private readonly string path;

        public DbOperations(string path)
        {
            this.path = path;
        }

        public BindingList<ToDoModel>? Load()
        {
            var isFileExists = File.Exists(path);
            if (!isFileExists)
            {
                File.CreateText(path).Dispose();
                return new BindingList<ToDoModel>();
            }

            using (var reader = File.OpenText(path))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<ToDoModel>>(fileText);
            }
        }

        public void Save(object toDoDataList)
        {
            using (StreamWriter writer = File.CreateText(path))
            {
                string output = JsonConvert.SerializeObject(toDoDataList);
                writer.Write(output);
            }
        }
    }
}
