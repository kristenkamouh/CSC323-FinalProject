/*
 * Kristen Kamouh - 20241747, Christy Khalife - 20231256
 * Submission date: 9/7/2025
 * Instructor: Dr. Pierre Antoine Akiki
 * 
 * Final Project - CSC 323
 * 
 * File: RegularMembers.cs
 * Description: Represents a member with a regular membership plan.
 */

using System;

namespace CSC323FinalProject.Models
{
    public class RegularGymMembers : GymMember
    {
        private decimal _additionalFeesBalance;

        public decimal AdditionalFeesBalance 
        { 
            get => _additionalFeesBalance;
            set => _additionalFeesBalance = Math.Max(0, value);
        }

        public RegularGymMembers() : base()
        {
            AdditionalFeesBalance = 0;
        }

        public RegularGymMembers(string name, Email email, PhoneNumber phoneNumber,
            IMembershipPlan membershipPlan, DateTime joiningDate, decimal height,
            decimal weight, DateTime dateOfBirth, bool autoSubscriptionRenewal = false)
            : base(name, email, phoneNumber, membershipPlan, joiningDate, height, weight, dateOfBirth, autoSubscriptionRenewal)
        {
            AdditionalFeesBalance = 0;
        }

        public override decimal CalculateDiscount()
        {
            // Regular members get a flat 5% discount
            return 0.05m;
        }

        public override string GetMembershipBenefits()
        {
            return "Access to gym equipment during regular hours. Limited access to group classes.";
        }

        public bool PremiumUpgradeAvailable()
        {
            // Regular members can upgrade to premium membership
            return AdditionalFeesBalance == 0; // Only allow upgrade if no outstanding fees
        }

        public decimal GetMonthlyFee()
        {
            // Assuming a base monthly fee for regular members
            decimal baseMonthlyFee = 50.00m; // Example base fee
            return baseMonthlyFee - (baseMonthlyFee * CalculateDiscount());
        }
        
        public void AddAdditionalFee(decimal amount, string reason)
        {
            if (amount <= 0)
            {
                MessageBox.Show("Fee must be positive.",
                    "Fee Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            AdditionalFeesBalance += amount;
            MessageBox.Show($"Added {amount:C} fee to {Name}'s account. Reason: {reason}");
        }
        
        public bool PayAdditionalFees(decimal amount)
        {
            if (amount <= 0)
            {
                MessageBox.Show("Fee must be positive.",
                    "Fee Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            if (amount > AdditionalFeesBalance)
            {
                // Overpayment scenario
                decimal change = amount - AdditionalFeesBalance;
                AdditionalFeesBalance = 0;
                MessageBox.Show($"Payment of {amount:C} processed. Change: {change:C}");
                return true;
            }
            
            // Normal payment scenario
            AdditionalFeesBalance -= amount;
            MessageBox.Show($"Payment of {amount:C} processed. Remaining balance: {AdditionalFeesBalance:C}");
            return true;
        }
        
        public override string ToString()
        {
            return $"{base.ToString()}, Type: Regular, " +
                   $"Monthly Fee: {GetMonthlyFee():C}, " +
                   $"Additional Fees: {AdditionalFeesBalance:C}, " +
                   $"Premium Upgrade Available: {(PremiumUpgradeAvailable() ? "Yes" : "No")}";
        }
    }
}