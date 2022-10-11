using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace Idyfa.Core.Extensions;

public static class CoreExts
{
    private static readonly Random _random = new Random(Guid.NewGuid().GetHashCode());
    
    /// <summary>
    /// Checks if the argument is null.
    /// </summary>
    public static void CheckArgumentIsNull(this object o, string name) {
        if (o == null)
            throw new ArgumentNullException(name);
    }

    /// <summary>
    /// Checks if a reference is null.
    /// </summary>
    public static void CheckReferenceIsNull(this object o, string name)
    {
        if (o == null)
            throw new NullReferenceException(name);
    }

    public static bool IsNullOrEmpty(this string input)
        => string.IsNullOrWhiteSpace(input);

    public static bool IsNotNullOrEmpty(this string input)
        => !IsNullOrEmpty(input);
    
    public static T ToEnum<T>(this string value) 
        => (T)Enum.Parse(typeof(T), value, true);
    
    public static int GenerateRandomNumber(int min, int max)
        => _random.Next(min, max);
    
    /// <summary>
    /// returns true if the <paramref name="obj"/> is null.
    /// </summary>
    /// <param name="obj">object to check</param>
    /// <returns>true if is null</returns>
    public static bool IsNull(this object obj) => obj == null;
    
    /// <summary>
    /// returns true if the <paramref name="obj"/> is NOT null.
    /// </summary>
    /// <param name="obj">object to check</param>
    /// <returns>true if not null</returns>
    public static bool IsNotNull(this object obj) => obj != null;
    
    public static string GenerateToken(int len = 11) { //TODO : Refactor this to improve it's performance
        var builder = new StringBuilder();
        foreach (var c in Constants._ALPHANUMERICS.OrderBy(_ => Guid.NewGuid()).Take(len))
            builder.Append(c);

        return builder.ToString();
    }
    
    public static string ToStringDecimal(this decimal d, byte decimals) {
        var fmt = (decimals > 0) ? "0." + new string('0', decimals) : "0";
        return d.ToString(fmt);
    }

    public static string ToStringDecimal(this decimal? d, byte decimals) {
        if (!d.HasValue) return "";
        return ToStringDecimal(d.Value, decimals);
    }
    
    public static int GetDecimalPlacesCount(this decimal value) {
        var split = value.ToString(CultureInfo.InvariantCulture).Split('.');

        if (split.Length < 2)
            return 0;

        return split[1].TrimEnd('0').Length;
    }
    
    public static decimal CalculatePercentage(this long sourceVal, long targetVal) {

        return ((sourceVal - targetVal) / Math.Abs(sourceVal)) * 100;
    }

    public static int CalcuateAge(this DateTime startDate) {
        var today = DateTime.Today;
        var age = today.Year - startDate.Year;
        var diffMonths = today.Month - startDate.Month;
        var diffDays = today.Day - startDate.Day;

        if(diffDays < 0) {
            diffMonths--;
        }
        if(diffMonths < 0) {
            age--;
        }

        return age;
    }
        
    public static bool IsAValidInstagramUserName(this string userName)
        => Regex.IsMatch(userName, @"@(?:(?:[\w][\.]{0,1})*[\w]){1,29}", RegexOptions.IgnoreCase);

    public static bool IsAValidTelegramUserName(this string userName) 
        => Regex.IsMatch(userName.TrimStart('@'),
            @"^(?=.{5,32}$)(?!.*__)(?!^(telegram|admin|support))[a-z][a-z0-9_]*[a-z0-9]$",
            RegexOptions.IgnoreCase);

    public static string MakeSlug(this string slug) =>
        slug == null
            ? null
            : Regex.Replace(slug,
                @"[^A-Za-z0-9\u0600-\u06FF_\.~]+", "-");
    
    public static bool IsDigit(this char c)
        => c is >= '0' and <= '9';
    
    public static bool IsLower(char c)
        => c is >= 'a' and <= 'z';

    public static bool IsUpper(char c)
        => c is >= 'A' and <= 'Z';

    public static bool IsLetterOrDigit(char c)
        => IsLower(c) || IsUpper(c) || IsDigit(c);

    public static bool IsDigit(this string str)
        => str.All(IsDigit);

    public static bool IsLower(this string str)
        => str.All(IsLower);

    public static bool IsUpper(this string str)
        => str.All(IsUpper);

    public static bool ContainsDigit(this string str)
        => str.Any(IsDigit);
}