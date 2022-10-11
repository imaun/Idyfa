using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace Idyfa.Core.Extensions;

public static class PersianExts
{
    private const string _IRAN_MOBILE_PREFIX = "98";
    public const string _PHONE_PATTERN = @"(?<Code>\+98|0|98)?(?<Number>9[0123456789]{9})";
    private static readonly Regex _matchIranianMobileNumber1 = new Regex(@"^(((98)|(\+98)|(0098)|0)(9){1}[0-9]{9})+$", 
        options: RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex _matchIranianMobileNumber2 = new Regex(@"^(9){1}[0-9]{9}$", 
        options: RegexOptions.Compiled | RegexOptions.IgnoreCase);
    
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

    public static bool IsAValidIranianPhoneNumber(this string? phoneNumber)
    {
        if (phoneNumber.IsNullOrEmpty())
            return false;

        phoneNumber = phoneNumber.ToEnglishNumbers();
        if (!phoneNumber.IsDigit())
            return false;

        return _matchIranianMobileNumber1.IsMatch(phoneNumber) ||
               _matchIranianMobileNumber2.IsMatch(phoneNumber);
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
    
    /// <summary>
    /// Converts English digits of a given string to their equivalent Persian digits.
    /// Based on : https://github.com/VahidN/DNTPersianUtils.Core/blob/34b9ae00ad3584bc9ef34033c6402d1b8ae7a148/src/DNTPersianUtils.Core/PersianNumbersUtils.cs
    /// </summary>
    /// <param name="data">English number</param>
    /// <returns></returns>
    public static string ToPersianNumbers(this string? data)
    {
        if (data is null)
        {
            return string.Empty;
        }

        var dataChars = data.ToCharArray();
        for (var i = 0; i < dataChars.Length; i++)
        {
            switch (dataChars[i])
            {
                case '0':
                case '\u0660':
                    dataChars[i] = '\u06F0';
                    break;

                case '1':
                case '\u0661':
                    dataChars[i] = '\u06F1';
                    break;

                case '2':
                case '\u0662':
                    dataChars[i] = '\u06F2';
                    break;

                case '3':
                case '\u0663':
                    dataChars[i] = '\u06F3';
                    break;

                case '4':
                case '\u0664':
                    dataChars[i] = '\u06F4';
                    break;

                case '5':
                case '\u0665':
                    dataChars[i] = '\u06F5';
                    break;

                case '6':
                case '\u0666':
                    dataChars[i] = '\u06F6';
                    break;

                case '7':
                case '\u0667':
                    dataChars[i] = '\u06F7';
                    break;

                case '8':
                case '\u0668':
                    dataChars[i] = '\u06F8';
                    break;

                case '9':
                case '\u0669':
                    dataChars[i] = '\u06F9';
                    break;
            }
        }

        return new string(dataChars);
    }

    /// <summary>
    /// Converts Persian and Arabic digits of a given string to their equivalent English digits.
    /// Based on : https://github.com/VahidN/DNTPersianUtils.Core/blob/34b9ae00ad3584bc9ef34033c6402d1b8ae7a148/src/DNTPersianUtils.Core/PersianNumbersUtils.cs
    /// </summary>
    /// <param name="data">Persian number</param>
    /// <returns></returns>
    public static string ToEnglishNumbers(this string? data)
    {
        if (data is null)
        {
            return string.Empty;
        }

        var dataChars = data.ToCharArray();
        for (var i = 0; i < dataChars.Length; i++)
        {
            switch (dataChars[i])
            {
                case '\u06F0':
                case '\u0660':
                    dataChars[i] = '0';
                    break;

                case '\u06F1':
                case '\u0661':
                    dataChars[i] = '1';
                    break;

                case '\u06F2':
                case '\u0662':
                    dataChars[i] = '2';
                    break;

                case '\u06F3':
                case '\u0663':
                    dataChars[i] = '3';
                    break;

                case '\u06F4':
                case '\u0664':
                    dataChars[i] = '4';
                    break;

                case '\u06F5':
                case '\u0665':
                    dataChars[i] = '5';
                    break;

                case '\u06F6':
                case '\u0666':
                    dataChars[i] = '6';
                    break;

                case '\u06F7':
                case '\u0667':
                    dataChars[i] = '7';
                    break;

                case '\u06F8':
                case '\u0668':
                    dataChars[i] = '8';
                    break;

                case '\u06F9':
                case '\u0669':
                    dataChars[i] = '9';
                    break;
            }
        }

        return new string(dataChars);
    }
    

}