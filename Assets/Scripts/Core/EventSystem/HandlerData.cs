using Events;

namespace core
{
    
    public delegate void HandlerFunction(EventData e);
    
    public struct HandlerData
    {
        public HandlerFunction Handler;
        public bool Once;

        public HandlerData(HandlerFunction handler, bool once)
        {
            Handler = handler;
            Once = once;
        }
    }
}