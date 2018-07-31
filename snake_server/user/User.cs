using System.Net.WebSockets;

namespace snake_server.user
{
    public class User
    {
        public User(string key, string name, WebSocket socket, int seat)
        {
            this.Key = key;
            this.Name = name;
            this.Socket = socket;
            this.seat = seat;
            this.Offline = false;
        }
        private string Key { get; }
        private string Name { set; get; }
        private int seat { set; get; }
        private WebSocket Socket { set; get;}
        private bool Offline { set; get; }
    }
}