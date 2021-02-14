using System.Xml.Linq;

namespace ChatApplication.MessageService
{
    public interface IMessageStorage
    {
        public void Save(string text);
        public string[] Read();
    }
}