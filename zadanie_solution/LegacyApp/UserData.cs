using System;

namespace LegacyApp
{
    public abstract class UserData
    {
        public bool CheckUserData(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && IsValidEmail(email) &&
                   IsValidAge(dateOfBirth);
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        private bool IsValidAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            return age >= 21;
        }
    }
}