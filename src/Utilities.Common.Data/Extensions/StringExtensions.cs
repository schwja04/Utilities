namespace Utilities.Common.Data.Extensions
{
    public static class StringExtensions
    {
        public static bool TryParseBoolean(this string data, ref bool result)
        {
            if (string.IsNullOrWhiteSpace(data)) return false;

            if (bool.TryParse(data, out result)) return true;

            switch (data.Trim().ToUpperInvariant())
            {
                case "TRUE":
                case "T":
                case "YES":
                case "Y":
                    result = true;
                    break;

                case "FALSE":
                case "F":
                case "NO":
                case "N":
                    result = false;
                    break;

                default:
                    if (int.TryParse(data, out int boolAsInt))
                    {
                        result = boolAsInt != 0;
                        break;
                    }
                    return false;
            }

            return true;
        }
    }
}
