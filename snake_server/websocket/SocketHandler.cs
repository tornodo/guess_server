using System.Collections.Concurrent;
using System.Net.WebSockets;
using snake_server.user;

namespace snake_server.websocket
{
    public class SocketHandler
    {
        private static readonly int ErrorRetryCounts = 5;
        private static readonly ConcurrentDictionary<string, ChatRoom> Rooms = new ConcurrentDictionary<string, ChatRoom>();
        private static readonly ConcurrentDictionary<string, User> Users = new ConcurrentDictionary<string, User>();
        
        public static void HandleProtocol(WebSocket socket, Protocol protocol)
        {
            switch (protocol.Type)
            {
                    case Protocol.Types.ProtocolType.Chat:
                        break;
                    case Protocol.Types.ProtocolType.CreateRoom:
                        _handleCreateRoom(socket, protocol);
                        break;
                    case Protocol.Types.ProtocolType.GameBegin:
                        break;
                    case Protocol.Types.ProtocolType.GameEnd:
                        break;
                    case Protocol.Types.ProtocolType.JoinRoom:
                        _handleJoinRoom(socket, protocol);
                        break;
                    case Protocol.Types.ProtocolType.LeaveRoom:
                        break;
                    case Protocol.Types.ProtocolType.Login:
                        break;
                    case Protocol.Types.ProtocolType.Paint:
                        break;
            }
        }

        private static bool _hasUser(string key)
        {
            return Users.ContainsKey(key);
        }

        private static bool _hasRoom(string key)
        {
            return Rooms.ContainsKey(key);
        }


        /// <summary>
        /// 创建房间
        /// </summary>
        /// <param name="socket">连接的websocket</param>
        /// <param name="protocol">解析后的协议</param>
        private static void _handleCreateRoom(WebSocket socket, Protocol protocol)
        {
            var user = new User(
                    protocol.Key,
                    protocol.Name,
                    socket,
                    0);
            ChatRoom room;
            if (_hasRoom(user.Key))
            {
                Rooms.TryGetValue(user.Key, out room);
            }
            else
            {
                room = new ChatRoom(user.Key);
            }

            if (room.IsFull())
            {
                // 房间已经满了
                return;
            }
            room.AddUser(user);
        }

        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="socket">连接的websocket</param>
        /// <param name="protocol">解析后的协议</param>
        private static void _handleJoinRoom(WebSocket socket, Protocol protocol)
        {
            User user;
            if (_hasUser(protocol.Key))
            {
                Users.TryGetValue(protocol.Key, out user);
            }
            else
            {
                user = new User(
                    protocol.Key,
                    protocol.Name,
                    socket,
                    -1);
            }
            ChatRoom room;
            if (_hasRoom(protocol.RoomKey))
            {
                Rooms.TryGetValue(protocol.RoomKey, out room);
            }
            else
            {
                // 房间还没有被创建，现在创建
                room = new ChatRoom(protocol.RoomKey);
            }
            room.AddUser(user);
        }
    }
}