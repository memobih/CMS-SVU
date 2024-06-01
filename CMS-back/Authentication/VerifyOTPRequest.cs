namespace CMS_back.Authentication
{
    public class VerifyOTPRequest
    {
        public string Email { get; set; }
        public string OTP { get; set; }
    }
}
