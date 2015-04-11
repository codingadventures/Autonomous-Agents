 

namespace Assets.Scripts.Message
{
    public enum MessageType
    {
        HiHoneyImHome,
        StewsReady,
        BanditsFollowMe
    }

    public struct Telegram 
    {
        public double DispatchTime;
        public int Sender;
        public int Receiver;
        public MessageType MessageType;

        public Telegram(double dt, int s, int r, MessageType mt)
        {
            DispatchTime = dt;
            Sender = s;
            Receiver = r;
            MessageType = mt;
        }
    }
}
