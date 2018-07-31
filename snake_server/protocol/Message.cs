namespace snake_server.protocol
{
    public class Message
    {
        public static readonly int MsgTypeChact = 1;
        public static readonly int MsgTypeGame = 2;
        private int id { set; get; }
        private int type { set; get; }
        private string content { set; get; }
    }
}