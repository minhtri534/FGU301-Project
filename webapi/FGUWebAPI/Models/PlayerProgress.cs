namespace FGUWebAPI.Models
{
    public class PlayerProgress
    {
        public int Id { get; set; }
        public required string PlayerName { get; set; }
        public int LevelCompleted { get; set; }
        public int TotalCoins { get; set; }
        public int Score { get; set; }
        public int PlayTimeSeconds { get; set; }
        public DateOnly CompletedAt { get; set; }
    }
}
