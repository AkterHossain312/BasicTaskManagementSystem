namespace Framework.Model
{
    public class ApplicationLog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime TimeStampUtc { get; set; }
        public string Category { get; set; }
        public string Level { get; set; }
        public string Text { get; set; }
        public string StakeTrace { get; set; }
        public string ErrorMessage { get; set; }
    }
}