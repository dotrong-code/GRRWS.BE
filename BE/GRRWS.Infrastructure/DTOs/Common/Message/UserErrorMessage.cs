using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DTOs.Common.Message
{
    public static class UserErrorMessage
    {
        public static Error FieldIsEmpty(string nameField)
            => Error.Validation("User.Empty", $"The '{nameField}' is required.");
        public static Error BirthdayCannotBeInFuture()
       => Error.Validation("User.BirthdayInFuture", "The 'Date of Birth' cannot be in the future.");
        public static Error UserMustBeAtLeast18()
        => Error.Validation("User.AgeRequirement", "The user must be at least 18 years old.");

        public static Error UserNotExist()
            => Error.NotFound("User.Exist", $"Invalid email or password");

        public static Error UserNoCreated()
            => Error.Conflict("User.Exist", $"User is not created!!");

        public static Error UserNameInValidFormat()
            => Error.Validation("User.UserName.Format", $"The username is invalid.");
        public static Error UserNameInValidLength()
            => Error.Validation("User.UserName.Length", $"User name must be at least 3 characters.");
        public static Error UserNameIsExist()
            => Error.Validation("User.UserName.Exist", $"The user name is exist.");

        public static Error UserNotFound()
       => Error.Validation("User.NotFound", "The specified user was not found.");
        public static Error EmailInValidFormat()
            => Error.Validation("User.Email.Format", $"The email is invalid.");
        public static Error EmailIsExist()
            => Error.Validation("User.Email.Exist", $"The email is exist.");
        public static Error PhoneInvalidFormat()
            => Error.Validation("User.Phone.Format", $" number must be 10-11 digits.");

        public static Error PasswordInValidLength()
            => Error.Validation("User.Password.Length", $"The password must be at least 3 characters.");
        public static Error PasswordInValidUppercase()
            => Error.Validation("User.Password.Uppercase", $"Password must start with a strong character (uppercase letter).");
        public static Error PasswordInValidSpecialChar()
            => Error.Validation("User.Password.SpecialChar", $"Password must contain at least one special character.");
        public static Error UserDeleteFailed()
        => Error.Validation("User.DeleteFailed", "Failed to delete user.");
        public static Error UserUpdateFailed()
        => Error.Validation("User.UpdateFailed", "Failed to update user.");
        public static Error UserCreationFailed()
        => Error.Validation("User.UpdateFailed", "Failed to create user.");


        public static Error LicenseNumberInvalidFormat()
       => Error.Validation("User.LicenseNumberInvalidFormat", "The 'License Number' must be in the format XX123456."); // Ví dụ định dạng

        public static Error SpecialtyInvalidLength()
            => Error.Validation("User.SpecialtyInvalidLength", "The 'Specialty' must be at least 3 characters long.");

        public static Error QualificationsInvalidLength()
            => Error.Validation("User.QualificationsInvalidLength", "The 'Qualifications' must be at least 5 characters long.");

    }
}
