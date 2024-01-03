namespace SROMCapi.Models
{
    public class GetMonsters
    {
        public int Page { get; set; }

        public string Language { get; set; }

        public string Keyword { get; set; }

        public string Category { get; set; }

        public int Level { get; set; } = 0;
    }
}
