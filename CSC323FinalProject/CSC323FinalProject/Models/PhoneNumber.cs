/*
 * Kristen Kamouh - 20241747, Christy khalife - 20231256
 * submission date: 7/9/2025
 * instructor: Dr. Pierre Antoine Akiki
 * 
 * Final Project - CSC 323
 * 
 * File: PhoneNumber.cs
 * Description: represents a phone number with country code, area code, and number.
 */

using System;
using System.Text.RegularExpressions;

namespace CSC323FinalProject.Models
{
    public class PhoneNumber
    {
        private string _countryCode;
        private string _areaCode;
        private string _number;

        public string CountryCode 
        { 
            get => _countryCode;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Country code cannot be null or empty.",
                        "Phone number Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                _countryCode = value;
            }
        }
        
        public string AreaCode 
        { 
            get => _areaCode;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Area code cannot be null or empty.",
                        "Phone number Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                _areaCode = value;
            }
        }
        
        public string Number 
        { 
            get => _number;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Number cannot be null or empty.",
                        "Phone number Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                _number = value;
            }
        }

        public PhoneNumber() { }

        public PhoneNumber(string countryCode, string areaCode, string number)
        {
            CountryCode = countryCode;
            AreaCode = areaCode;
            Number = number;
        }

        public bool IsValid() 
        {
            // Check if all parts are present
            if (string.IsNullOrEmpty(CountryCode) || 
                string.IsNullOrEmpty(AreaCode) || 
                string.IsNullOrEmpty(Number))
                return false;
            
            // Check if digits only
            if (!IsDigitsOnly(CountryCode) || !IsDigitsOnly(AreaCode) || !IsDigitsOnly(Number))
                return false;
                
            // Check lengths
            if (AreaCode.Length != 3 || Number.Length != 7)
                return false;
                
            return true;
        }
        
        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
        
        public override string ToString()
        {
            return $"+{CountryCode} ({AreaCode}) {Number.Substring(0, 3)}-{Number.Substring(3)}";
        }
    }
}
