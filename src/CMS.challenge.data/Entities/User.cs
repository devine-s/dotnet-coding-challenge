using System;

namespace CMS.challenge.data.Entities
{
    public class User
    {
        /// <summary>
        /// ID of the User.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// First name of the User.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of the User.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// User's D.O.B.
        /// </summary>
        private DateTime dateOfBirth;
        public DateTime DateOfBirth {
            get
            {
                return dateOfBirth;
            }
            set
            {
                dateOfBirth = value;
                Age = new DateTime(DateTime.Now.Subtract(dateOfBirth).Ticks).Year - 1;
            }
        }

        public int Age { get; set; }
    }
}