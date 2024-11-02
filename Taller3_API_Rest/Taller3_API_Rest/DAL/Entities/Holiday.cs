namespace Taller3_API_Rest.DAL.Entities
{
    public class Holiday
    {
        public int Id { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int? DaysFromEaster { get; set; }
    }
}
