using Microsoft.AspNetCore.Mvc;
using FGUWebAPI.Models;
using FGUWebAPI.Data;

namespace FGUWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgressController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProgressController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ API lưu dữ liệu từ Unity
        [HttpPost("save")]
        public async Task<IActionResult> SaveProgress([FromBody] PlayerProgressDto dto)
        {
            var progress = new PlayerProgress
            {
                PlayerName = dto.PlayerName,
                LevelCompleted = dto.LevelCompleted,
                TotalCoins = dto.TotalCoins,
                Score = dto.Score,
                PlayTimeSeconds = dto.PlayTimeSeconds,
                CompletedAt = DateOnly.FromDateTime(DateTime.Today)
            };

            _context.PlayerProgress.Add(progress);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Progress saved successfully" });
        }

        [HttpGet("leaderboard")]
        public IActionResult GetLeaderboard()
        {
            var topPlayers = _context.PlayerProgress
                .OrderByDescending(p => p.Score)
                .Take(10)
                .Select(p => new
                {
                    p.PlayerName,
                    p.Score,
                    p.LevelCompleted,
                    p.TotalCoins,
                    p.PlayTimeSeconds,
                    p.CompletedAt
                })
                .ToList();

            return Ok(topPlayers);
        }
    }
}
