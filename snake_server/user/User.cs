using System.Net.WebSockets;

namespace snake_server.user
{
    public class User
    {
        public User(string key, string name, WebSocket socket)
        {
            this.Key = key;
            this.Name = name;
            this.Socket = socket;
            this.Offline = false;
        }
        private string Key { get; }
        private string Name { set; get; }
        private WebSocket Socket { set; get;}
        private bool Offline { set; get; }
    }
}