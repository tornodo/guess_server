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
            this.Seat = seat;
            this.Offline = false;
        }
        public string Key { get; }
        public string Name { set; get; }
        public int Seat { set; get; }
        public WebSocket Socket { set; get;}
        public bool Offline { set; get; }
    }
}