using System.Net.Mail;

namespace Sample.Core.Extensions
{
    public static class StringValidationsExtension
    {
        public static bool IsValidEmailAddress(this string value)
        {
            try
            {
                var mailAddress = new MailAddress(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsSavePassword(this string value)
        {
            return !string.IsNullOrEmpty(value) && value.Length > 4;
        }
    }
}