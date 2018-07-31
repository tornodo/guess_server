using System.Collections.Generic;

namespace snake_server.user
{
    public class ChatRoom
    {
        // 房间最大容量
        private static readonly int RoomCapacity = 8;
        private User Owner { set; get; }
        private readonly LinkedList<User> UserList = new LinkedList<User>();

        public ChatRoom(User user)
        {
            this.Owner = user;
        }

        public bool IsFull()
        {
            return this.UserList.Count == RoomCapacity;
        }

        public bool AddUser(User user)
        {
            if (this.IsFull())
            {
                return false;
            }

            UserList.AddLast(user);
            return true;
        }

        public bool RemoveUser(User user)
        {
            return UserList.Remove(user);
        }
    }
}