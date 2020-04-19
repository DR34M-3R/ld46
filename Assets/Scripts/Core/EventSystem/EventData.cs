public class EventData
{
    public string Type;
    public object Data;

    public EventData(string type, object data)
    {
        Type = type;
        Data = data;
    }
}