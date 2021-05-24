using ClubersProviderMobile.Prism.Interfaces;
using System;
using System.Net.Mail;

namespace ClubersProviderMobile.Prism.Helpers
{
    public class RegexHelper : IRegexHelper
    {
        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                var mail = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
