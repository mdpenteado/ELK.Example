namespace ELK.Example.ELK.Example.Adapter.Logs.Driven.DTO.ELK
{
    public class ELKLogDTO
    {
        public Guid RegisterId { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public string Body { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}