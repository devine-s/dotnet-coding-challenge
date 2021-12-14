using CMS.challenge.data.Cache;
using CMS.challenge.data.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace CMS.challenge.common
{
    public class ValidationClass
    {
        public static bool EmailExists(string emailAddress, ISimpleObjectCache<Guid, User> SimpleObjectCache)
        {
            var existingRecord = SimpleObjectCache.GetAllAsync();
            foreach (User user in existingRecord.Result)
            {
                if (user.Email == emailAddress) return true;
            }

            return false;
        }

        public static bool UserExists(Guid userID, ISimpleObjectCache<Guid, User> SimpleObjectCache)
        {
            var existingRecord = SimpleObjectCache.GetAsync(userID);
            if (existingRecord.Result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsValidFirstName(string firstName)
        {
            if (firstName.Length > 128) return false;
            return true;
        }
        public static bool IsValidLastName(string lastName)
        {
            if (lastName.Length > 128) return false;
            return true;
        }
        public static bool IsValidEmail(string email)
        {
            return true;
        }
        public static bool IsValidDateOfBirth(DateTime dateOfBirth)
        {
            return false;
        }

        public static List<Error> IsValidInput(User user, ISimpleObjectCache<Guid, User> SimpleObjectCache)
        {
            List<Error> errorArray = new List<Error>();

            Error error = new Error();
            error.message = "Test Message";
            Error error1 = new Error();
            error.message = "Test Message1";

            errorArray.Add(error);
            errorArray.Add(error1);

            return errorArray;
        }
    }
}
