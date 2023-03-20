namespace ClimbSociety.Models
{
    public class Developer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, int> Skills { get; set; }
    }
}
