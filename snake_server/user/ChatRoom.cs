using System.Collections.Concurrent;
using System.Collections.Generic;

namespace snake_server.user
{
    public class ChatRoom
    {
        // 房间最大容量
        private const int RoomCapacity = 8;
        private string key { set; get; }
        private readonly ConcurrentDictionary<string, User> _users = new ConcurrentDictionary<string, User>();

        public ChatRoom(string key)
        {
            this.key = key;
        }

        public bool IsFull()
        {
            return this._users.Count == RoomCapacity;
        }

        public bool AddUser(User user)
        {
            if (this.IsFull())
            {
                return false;
            }

            return _users.TryAdd(user.Key, user);
        }

        public bool RemoveUser(User user)
        {
            return _users.TryRemove(user.Key, out user);
        }
    }
}