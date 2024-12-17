namespace Task.Core.Consts
{
    public static class RegexPatterns
    {
        public const string AlphNumric = "(?=(.*[0-9]))((?=.*[A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z]))^.{8,}$";
        public const string CharactersOnlyEnglish = "^[a-zA-Z-_ ]*$";
        public const string MobileNumber = "^\\+((?:9[679]|8[035789]|6[789]|5[90]|42|3[578]|2[1-689])|9[0-58]|8[1246]|6[0-6]|5[1-8]|4[013-9]|3[0-469]|2[70]|7|1)(?:\\W*\\d){0,13}\\d$";
        public const string NumbersOnly = "^\\d+$";
    }
}
