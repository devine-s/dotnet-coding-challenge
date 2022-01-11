using CMS.challenge.data.Cache;
using CMS.challenge.data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading.Tasks;

namespace CMS.challenge.common
{
    public class ValidationClass
    {
        public static bool EmailExists(string emailAddress, ISimpleObjectCache<Guid, User> SimpleObjectCache, Guid id)
        {
            if (id == null) return false;

            var userRecords = SimpleObjectCache.GetAllAsync();
            foreach (User user in userRecords.Result)
            {
                if (user.Id == id) continue;
                if (user.Email == emailAddress) return true;
            }

            return false;
        }

        public static bool UserExists(Guid userID, ISimpleObjectCache<Guid, User> SimpleObjectCache)
        {
            if (userID == null) return false;

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
            if ((firstName == null) || (firstName == "")) return false;

            if (firstName.Length > 128) return false;
            return true;
        }
        public static bool IsValidLastName(string lastName)
        {
            if ((lastName == null) || (lastName == "")) return true;

            if (lastName.Length > 128) return false;
            return true;
        }
        public static bool IsValidEmail(string email)
        {
            if ((email == null) || (email == "")) return false;

            return (new EmailAddressAttribute().IsValid(email));
        }
        public static bool IsValidDateOfBirth(DateTime dateOfBirth)
        {
            string date = dateOfBirth.ToString();
            try
            { 
                DateTime dt = DateTime.Parse(date);

                return true;

            }
            catch
            {
                return false;
            }
        }
        public static bool Is18OrOlder(DateTime dateOfBirth)
        {
            int test = CalculateAge(dateOfBirth);
            if (test > 17)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int CalculateAge(DateTime dateOfBirth)
        {
            int age = new DateTime(DateTime.Now.Subtract(dateOfBirth).Ticks).Year - 1;

            return age;
        }

        public static List<Error> ValidInput(User user, ISimpleObjectCache<Guid, User> _simpleObjectCache, bool userMustExist)
        {
            List<Error> errorArray = new List<Error>();

            if ((!userMustExist) && (ValidationClass.UserExists(user.Id, _simpleObjectCache)))
            {
                Error userExistsError = new Error();
                userExistsError.message = "User already exists";
                userExistsError.field = "Id";
                errorArray.Add(userExistsError);
            }

            if ((userMustExist) && (!ValidationClass.UserExists(user.Id, _simpleObjectCache)))
            {
                Error userExistsError = new Error();
                userExistsError.message = "User doesn't exist";
                userExistsError.field = "Id";
                errorArray.Add(userExistsError);
            }

            //var userRecords = _simpleObjectCache.GetAllAsync();
            if (ValidationClass.EmailExists(user.Email, _simpleObjectCache, user.Id))
            {
                Error emailExistsError = new Error();
                emailExistsError.message = "Email already exists";
                emailExistsError.field = "Email";
                errorArray.Add(emailExistsError);
            }

            if (!ValidationClass.IsValidFirstName(user.FirstName))
            {
                Error firstNameError = new Error();
                firstNameError.message = "First name is blank or greater than 128 characters";
                firstNameError.field = "FirstName";
                errorArray.Add(firstNameError);
            }

            if (!ValidationClass.IsValidLastName(user.LastName))
            {
                Error lastNameError = new Error();
                lastNameError.message = "Last name is blank or greater than 128 characters";
                lastNameError.field = "LastName";
                errorArray.Add(lastNameError);
            }

            if (!ValidationClass.IsValidEmail(user.Email))
            {
                Error emailValidError = new Error();
                emailValidError.message = "Email isn't valid";
                emailValidError.field = "Email";
                errorArray.Add(emailValidError);
            }

            if (!ValidationClass.IsValidDateOfBirth(user.DateOfBirth))
            {
                Error dateOfBirthValidError = new Error();
                dateOfBirthValidError.message = "Date of birth isn't valid";
                dateOfBirthValidError.field = "DateOfBirth";
                errorArray.Add(dateOfBirthValidError);
            }

            if (!ValidationClass.Is18OrOlder(user.DateOfBirth))
            {
                Error dateOfBirthValidError = new Error();
                dateOfBirthValidError.message = "User is under 18 years old";
                dateOfBirthValidError.field = "DateOfBirth";
                errorArray.Add(dateOfBirthValidError);
            }

            return errorArray;
        }
    }
}
