/*
 * Kristen Kamouh - 20241747, Christy khalife - 20231256
 * submission date: 9/7/2025
 * instructor: Dr. Pierre Antoine Akiki
 * 
 * Final Project - CSC 323
 * 
 * File: GymMember.cs
 * Description: represents a gym member with properties such as name, email, phone number, 
            membership plan, joining date, height, weight, age, and auto-renewal option.
 */

namespace CSC323FinalProject.Models
{
    public abstract class GymMember
    {
        private DateTime DateOfBirth;
        public string Name { get; set; }
        public Email MemberEmail { get; set; }
        public PhoneNumber MemberPhoneNumber { get; set; }
        public IMembershipPlan MembershipPlan { get; set; }
        public DateTime MemberJoiningDate { get; set; }
        public decimal MemberHeight { get; set; }
        public decimal MemberWeight { get; set; }

        public int Age
        {
            get
            {
                return GetAge();
            }
        }
        public bool AutoSubscriptionRenewal { get; set; }

        public abstract decimal CalculateDiscount();

        public abstract string GetMembershipBenefits();

        private int GetAge()
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;

            // Check if birthday hasn't occurred this year yet
            if (today.Month < DateOfBirth.Month ||
                (today.Month == DateOfBirth.Month && today.Day < DateOfBirth.Day))
            {
                age--;
            }

            return age;
        }

        public GymMember() { }

        public GymMember(string name, Email email, PhoneNumber phoneNumber,
            IMembershipPlan membershipPlan, DateTime joiningDate, decimal height,
            decimal weight, DateTime dateOfBirth
        )
        {
            Name = name;
            MemberEmail = email;
            MemberPhoneNumber = phoneNumber;
            MembershipPlan = membershipPlan;
            MemberJoiningDate = joiningDate;
            MemberHeight = height;
            MemberWeight = weight;
            DateOfBirth = dateOfBirth;
        }


        public GymMember(string name, Email email, PhoneNumber phoneNumber,
            IMembershipPlan membershipPlan, DateTime joiningDate, decimal height,
            decimal weight, DateTime dateOfBirth, bool autoSubscriptionRenewal
        )
        {
            Name = name;
            MemberEmail = email;
            MemberPhoneNumber = phoneNumber;
            MembershipPlan = membershipPlan;
            MemberJoiningDate = joiningDate;
            MemberHeight = height;
            MemberWeight = weight;
            DateOfBirth = dateOfBirth;
            AutoSubscriptionRenewal = autoSubscriptionRenewal;
        }
    }
}
