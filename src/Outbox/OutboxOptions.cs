namespace EasySharp.Outbox
{
    public class OutboxOptions
    {
        public bool Enable { get; set; } = false;
        public string OutboxType { get; set; }
        public bool DeleteAfter { get; set; }
    }
}
