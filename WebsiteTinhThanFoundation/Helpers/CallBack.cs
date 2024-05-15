using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebsiteTinhThanFoundation.Helpers
{
	public static class CallBack
	{
		public static bool IsValidUsername(string username)
		{
			string pattern = "^[A-Za-z]{3}_[A-Za-z]{3}$";
			return Regex.IsMatch(username, pattern);
		}

        public static DateTime ToTimeZone(this DateTime dateTime, string timeZoneId = "SE Asia Standard Time")
        {
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZone);
        }

        public static string GetEnumDisplayName(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null)
                return "";
            var displayAttribute = (DisplayAttribute?)Attribute.GetCustomAttribute(
                fieldInfo,
                typeof(DisplayAttribute)
            );

            string displayName = displayAttribute?.Name ?? value.ToString();


            return displayName;
        }
    }
}
