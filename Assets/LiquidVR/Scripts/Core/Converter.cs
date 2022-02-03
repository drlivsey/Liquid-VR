namespace Liquid.Core
{
    public static class Converter
    {
        public static string SecondsToTime(int secondsCount)
        {
            return SecondsToTime((float)secondsCount);
        }

        public static string SecondsToTime(float secondsCount)
        {
            var hours = (int)(secondsCount / 3600);
            var minutes = (int)((secondsCount % 3600) / 60);
            var seconds = (int)((secondsCount % 3600) % 60);

            string ConvertToTimeFormat(int value)
            {
                return value < 10 ? $"0{value}" : $"{value}";
            }

            return $"{ConvertToTimeFormat(hours)}:{ConvertToTimeFormat(minutes)}:{ConvertToTimeFormat(seconds)}";
        }
    }
}