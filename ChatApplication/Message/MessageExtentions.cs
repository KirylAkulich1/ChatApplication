using System.Text.Json;

namespace ChatApplication.Messages
{
    public static class MessageExtentions
    {
        public static string ToJson(this Message self)
        {
            return JsonSerializer.Serialize<Message>(self);
        }
    }
}