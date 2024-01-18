using Core.Utilities.Security.Jwt;

namespace Business.Authentication.Constants
{
    public class AuthMessages
    {
        public static string UserNotFound = "User not found.";
        public static string UserAlreadyExists = "User already exists.";
        public static string UserRegistered = "User registered successfully.";
        public static string UserLoginSuccessful = "User login successful.";
        public static string UserLoginFailed = "User login failed.";
        public static string UserPasswordWrong = "User password wrong.";
        public static string UserImageSizeInvalid = "Image size must be less than 2MB";
        public static string UserImageExtensionInvalid = "Image extension must be .jpg, .jpeg or .png";
        public static string UserClaimsNotFound  = "Claims not found.";
    }
}
