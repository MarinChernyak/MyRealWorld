﻿namespace MyRealWorld.Models.Authentication
{
    public class GmailOptions
    {
        public const string GMailOptionsKey = "GmailOptions";

        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password{ get; set; }
    }
}
