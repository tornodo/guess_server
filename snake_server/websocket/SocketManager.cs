using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using snake_server.user;

namespace snake_server.websocket
{
    public class SocketManager
    {
        // 设置字典初始化默认容量
        private static readonly int _captcity = 10;
        private static readonly ConcurrentDictionary<string, User> ClientSockets = 
            new ConcurrentDictionary<string, User>(_captcity, _captcity);


        public static bool AddSocket(string key, User user)
        {
            return ClientSockets.TryAdd(key, user);
        }

        public static async Task RemoveSocket(string key)
        {
            if (ClientSockets.TryRemove(key, out var user))
            {
                await user.Socket.CloseAsync(
                    closeStatus: WebSocketCloseStatus.NormalClosure,
                    statusDescription: "Closed",
                    cancellationToken: CancellationToken.None
                ).ConfigureAwait(false);
            }
        }

        public static ConcurrentDictionary<string, User> GetAllClients()
        {
            return ClientSockets;
        }
    }
}