using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace Idyfa.Core.Extensions;

public static class PersianExts
{
    private const string _IRAN_MOBILE_PREFIX = "98";
    public const string _PHONE_PATTERN = @"(?<Code>\+98|0|98)?(?<Number>9[0123456789]{9})";

    private static readonly System.Type _typeOfString = typeof(string);

    public const char ArabicYeChar = (char)1610;
    public const char PersianYeChar = (char)1740;

    public const char ArabicKeChar = (char)1603;
    public const char PersianKeChar = (char)1705;
    
    public static string CorrectYeKe(this string data) {
        return string.IsNullOrWhiteSpace(data) ?
            data :
            data.Replace(ArabicYeChar, PersianYeChar)
                .Replace(ArabicKeChar, PersianKeChar)
                .Trim();
    }

    public static string DeNormalizePhoneNumber(this string input) {
        string result = input;
        if(input.StartsWith("98") && input.Length > 10) {
            result = "0" + result.TrimStart('9').TrimStart('8');
        }
        return result;
    }

    public static string NormalizePhoneNumber(this string input) {
        if (input.IsNullOrEmpty())
            return null;

        if (input.StartsWith('+'))
            return input.TrimStart('+');

        if (input.StartsWith("09"))
            return $"{_IRAN_MOBILE_PREFIX}{input.TrimStart('0')}";

        return input;
    }
    
    public static bool IsNationalCodeValid(this string input) {
        // http://www.aliarash.com/article/codemeli/codemeli.htm

        if (input.IsNullOrEmpty() || input.Length != 10)
            return false;

        var mod = input.Take(9)
            .Select((current, index) => CharUnicodeInfo.GetDecimalDigitValue(current) * (10 - index))
            .Sum() % 11;

        var ctrl = CharUnicodeInfo.GetDecimalDigitValue(
            input.Skip(9).First()
        );

        return mod < 2 ? ctrl == mod : ctrl == 11 - mod;
    }

    public static string ToLocalPhoneNumber(this string input) {
        if (input.IsNullOrEmpty())
            return null;

        var regexMatch = Regex.Match(input, _PHONE_PATTERN);
        if (!regexMatch.Success)
            return null;

        var number = regexMatch.Groups["Number"].Value.Trim();

        return $"0{number}";
    }
}