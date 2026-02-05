namespace System
{
    public static class NumberExtensions
    {  
        public static string FromSecondsToMMSS(this long @val)
        {
            var mins = val / 60;
            var rem = val % 60;
            return $"{mins.ToString("D2")}:{rem.ToString("D2")}";
        }

        public static string FromSecondsToHHMMSS(this long @val)
        {
            var hrs = val / 3600;
            var remS1 = val % 3600;
            var min = remS1 / 60;
            var sec = remS1 % 60;
            return $"{hrs.ToString("D2")}:{min.ToString("D2")}:{sec.ToString("D2")}";
        }
    }
}