﻿namespace BaiThucHanhWeb.Model.DTO
{
    public class UserLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirPassword { get; set; }
    }
}
