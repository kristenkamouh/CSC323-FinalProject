/*
 * Kristen Kamouh - 20241747, Christy khalife - 20231256
 * submission date: 9/7/2025
 * instructor: Dr. Pierre Antoine Akiki
 * 
 * Final Project - CSC 323
 * 
 * File: Email.cs
 * Description: represents the email address of a user and provides validation for it.
 */

using System;
using System.Text.RegularExpressions;

namespace CSC323FinalProject.Models
{
    public class Email
    {
        private string _emailAddress;

        public string EmailAddress 
        { 
            get => _emailAddress;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Email address cannot be null or empty.",
                        "Email Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                _emailAddress = value;
            }
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(EmailAddress))
                return false;

            // Basic email validation
            return EmailAddress.Contains("@") && 
                   EmailAddress.Contains(".") && 
                   EmailAddress.IndexOf("@") < EmailAddress.LastIndexOf(".");
        }
        
        public Email() { }
        
        public Email(string emailAddress)
        {
            EmailAddress = emailAddress;
        }
        
        public override string ToString()
        {
            return EmailAddress;
        }
    }
}
