﻿namespace CMS_back.Authentication
{
    public class ChangePassword
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string OTP { get; set; }
    }
}
