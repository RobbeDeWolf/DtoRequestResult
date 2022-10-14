namespace Vives.Services.Model
{
    public class ServiceMessage
    {
        public string Code { get; set; } = null!;
        public string Message { get; set; } = null!;
        public ServiceMessageType Type { get; set; }
    }
}
