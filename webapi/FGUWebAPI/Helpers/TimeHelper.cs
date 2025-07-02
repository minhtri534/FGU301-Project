namespace FGUWebAPI.Helpers
{
    public static class TimeHelper
    {
        public static string FormatTimeFromSeconds(int totalSeconds)
        {
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            return $"{minutes}m {seconds}s";
        }

        public static int ParseTimeToSeconds(string input)
        {
            string[] parts = input.Split(':');
            if (parts.Length != 2)
                throw new FormatException("Time format must be mm:ss");

            int minutes = int.Parse(parts[0]);
            int seconds = int.Parse(parts[1]);
            return (minutes * 60) + seconds;
        }
    }
}
