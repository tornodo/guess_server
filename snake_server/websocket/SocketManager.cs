using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace snake_server.websocket
{
    public class SocketManager
    {
        // 设置字典初始化默认容量
        private static readonly int _captcity = 10;
        private static readonly ConcurrentDictionary<string, WebSocket> ClientSockets = 
            new ConcurrentDictionary<string, WebSocket>(_captcity, _captcity);


        public static bool AddSocket(string key, WebSocket socket)
        {
            return ClientSockets.TryAdd(key, socket);
        }

        public static async Task RemoveSocket(string key)
        {
            if (ClientSockets.TryRemove(key, out var socket))
            {
                await socket.CloseAsync(
                    closeStatus: WebSocketCloseStatus.NormalClosure,
                    statusDescription: "Closed by user",
                    cancellationToken: CancellationToken.None
                ).ConfigureAwait(false);
            }
        }

        public static ConcurrentDictionary<string, WebSocket> GetAllClients()
        {
            return ClientSockets;
        }
    }
}