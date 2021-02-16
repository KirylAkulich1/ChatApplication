using System.IO;
using System.Net;

namespace ChatApplication.MessageService
{
    public class FileStorage : IMessageStorage
    {
        private string _path;
        public FileStorage(string path)
        {
            _path = path;
            
        }
        public void Save(string text)
        {
            using FileStream fileStream = File.Open(_path, FileMode.Append);
            using var fw=new StreamWriter(fileStream);
            fw.WriteLine(text);
        }

        public string[] Read()
        {
            if (File.Exists(_path))
                return File.ReadAllLines(_path);
            else
                return null;
        }
    }
}