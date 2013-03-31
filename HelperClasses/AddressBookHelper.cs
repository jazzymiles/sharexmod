using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareX.HelperClasses
{
    public static class AddressBookHelper
    {
        public static string CurrentRecipient { get; set; }
        private const int ADDRESSBOOK_LIMIT = 10;

        /// <summary>
        /// Add email to the Address Book and remove the last item if the count surpasses limit
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>True if added successfully</returns>
        public static bool AddEmail(string email)
        {
            string result = SettingsManager.ConfigUser.AddressBook.SingleOrDefault(s => s == email);
            if (string.IsNullOrEmpty(result))
            {
                SettingsManager.ConfigUser.AddressBook.Add(email);

                if (SettingsManager.ConfigUser.AddressBook.Count > ADDRESSBOOK_LIMIT)
                {
                    SettingsManager.ConfigUser.AddressBook.RemoveAt(SettingsManager.ConfigUser.AddressBook.Count - 1);
                }
                return true;
            }
            return false;
        }
    }
}