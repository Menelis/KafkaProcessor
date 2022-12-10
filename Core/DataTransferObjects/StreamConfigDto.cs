namespace Core.DataTransferObjects
{
    public class StreamConfigDto
    {
        public long Id { get;}
        public string Name { get;}

        public StreamConfigDto(long id, string name)
        {
            Name = name;
            Id = id;
        }
        
    }
}