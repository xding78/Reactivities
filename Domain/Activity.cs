namespace Domain
{
    public class Activity
    {
        // all properties need to be `public`
        public Guid Id { get; set; } // "Id" is reserved as the PK in the database
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
    }
}