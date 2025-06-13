namespace DataLayer.Models
{
    public class Team
    {
        public int? Id { get; set; }
        public string? Country { get; set; }
        public string? Alternate_Name { get; set; }
        public string? Fifa_Code { get; set; }
        public int? Group_Id { get; set; }
        public string? Group_Letter { get; set; }

        // Results fields
        public int? Wins { get; set; }
        public int? Draws { get; set; }
        public int? Losses { get; set; }
        public int? Games_Played { get; set; }
        public int? Points { get; set; }
        public int? Goals_For { get; set; }
        public int? Goals_Against { get; set; }
        public int? Goal_Differential { get; set; }
    }
}
