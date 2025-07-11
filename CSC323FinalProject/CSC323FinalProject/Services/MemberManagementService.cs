using CSC323FinalProject.Models;
using CSC323FinalProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CSC323FinalProject.Services
{
    public class MemberManagementService
    {
        private readonly Gym _gym;
        // Dictionary to track auto-renewal status for members
        private Dictionary<string, bool> _autoRenewalStatus = new Dictionary<string, bool>();

        public MemberManagementService(Gym gym)
        {
            _gym = gym;
        }

        public void AddRegularMember()
        {
            try
            {
                // Get member name
                string name = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter member name:",
                    "Add New Member",
                    ""
                );

                if (string.IsNullOrEmpty(name))
                    return; // User canceled

                // Get email
                string emailStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter email address:",
                    "Add New Member",
                    "example@email.com"
                );

                // Get phone number
                string countryCode = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter country code:",
                    "Add New Member",
                    "1"
                );

                string areaCode = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter area code:",
                    "Add New Member",
                    "555"
                );

                string number = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter phone number:",
                    "Add New Member",
                    "1234567"
                );

                // Get plan information
                string planName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter plan name:",
                    "Add New Member",
                    "Regular Plan"
                );

                string feeStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter monthly fee (e.g., 50.00):",
                    "Add New Member",
                    "50.00"
                );

                string durationStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter duration in months:",
                    "Add New Member",
                    "12"
                );

                // Get physical information
                string heightStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter height in meters (e.g., 1.75):",
                    "Add New Member",
                    "1.75"
                );

                string weightStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter weight in kg (e.g., 70.0):",
                    "Add New Member",
                    "70.0"
                );

                string birthYearStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter birth year:",
                    "Add New Member",
                    "1990"
                );

                string birthMonthStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter birth month (1-12):",
                    "Add New Member",
                    "1"
                );

                string birthDayStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter birth day (1-31):",
                    "Add New Member",
                    "1"
                );

                // Parse values
                Email email = new Email(emailStr);
                PhoneNumber phoneNumber = new PhoneNumber(countryCode, areaCode, number);

                decimal fee = decimal.Parse(feeStr);
                int duration = int.Parse(durationStr);

                MembershipPlan plan = new(planName, fee, duration, Enums.PlanType.BasicAccess);
                // Set a default number of trainer sessions for regular members too
                plan.PersonalTrainerSessions = 2; // Regular members get 2 sessions per month

                DateTime join = DateTime.Now; // Current date as join date
                decimal height = decimal.Parse(heightStr);
                decimal weight = decimal.Parse(weightStr);

                int birthYear = int.Parse(birthYearStr);
                int birthMonth = int.Parse(birthMonthStr);
                int birthDay = int.Parse(birthDayStr);
                DateTime dateOfBirth = new DateTime(birthYear, birthMonth, birthDay);

                // Create and add member
                RegularGymMembers newMember = new(
                    name, email, phoneNumber, plan, join, height,
                    weight, dateOfBirth, false
                );

                _gym.AddMember(newMember);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error adding member: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        public void RemoveMember()
        {
            string memberName = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter the name of the member to remove:",
                "Remove Member",
                ""
            );

            if (!string.IsNullOrEmpty(memberName))
            {
                // Find the member with the given name
                GymMember memberToRemove = null;
                foreach (var member in _gym.GetRegularMembers().Cast<GymMember>().Concat(_gym.GetPremiumMembers()))
                {
                    if (member.Name.Equals(memberName, StringComparison.OrdinalIgnoreCase))
                    {
                        memberToRemove = member;
                        break;
                    }
                }

                // Remove the member if found
                if (memberToRemove != null)
                {
                    _gym.RemoveMember(memberToRemove);
                }
                else
                {
                    MessageBox.Show(
                        $"No member found with name '{memberName}'.",
                        "Member Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
        }

        public void PayAdditionalFees()
        {
            try
            {
                // Get member name
                string memberName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter member name to pay additional fees:",
                    "Pay Additional Fees",
                    ""
                );

                if (string.IsNullOrEmpty(memberName))
                    return; // User canceled

                // Find the member
                RegularGymMembers member = null;
                foreach (var m in _gym.GetRegularMembers())
                {
                    if (m.Name.Equals(memberName, StringComparison.OrdinalIgnoreCase))
                    {
                        member = m;
                        break;
                    }
                }

                if (member == null)
                {
                    MessageBox.Show(
                        $"No regular member found with name '{memberName}'.",
                        "Member Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Display current balance and ask for payment amount
                string paymentStr = Microsoft.VisualBasic.Interaction.InputBox(
                    $"Current additional fees balance: {member.AdditionalFeesBalance:C}\n\n" +
                    "Enter payment amount:",
                    "Pay Additional Fees",
                    member.AdditionalFeesBalance.ToString("F2")
                );

                if (string.IsNullOrEmpty(paymentStr))
                    return; // User canceled

                // Parse the payment amount
                if (!decimal.TryParse(paymentStr, out decimal paymentAmount) || paymentAmount <= 0)
                {
                    MessageBox.Show(
                        "Invalid payment amount. Please enter a positive number.",
                        "Input Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Process payment
                bool success = member.PayAdditionalFees(paymentAmount);
                
                if (success)
                {
                    MessageBox.Show(
                        $"Payment of {paymentAmount:C} processed successfully.\n" +
                        $"New balance: {member.AdditionalFeesBalance:C}",
                        "Payment Successful",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error processing payment: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        public void CheckPremiumUpgradeEligibility()
        {
            try
            {
                // Get member name
                string memberName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter regular member name to check for premium upgrade eligibility:",
                    "Premium Upgrade Check",
                    ""
                );

                if (string.IsNullOrEmpty(memberName))
                    return; // User canceled

                // Find the member
                RegularGymMembers member = null;
                foreach (var m in _gym.GetRegularMembers())
                {
                    if (m.Name.Equals(memberName, StringComparison.OrdinalIgnoreCase))
                    {
                        member = m;
                        break;
                    }
                }

                if (member == null)
                {
                    MessageBox.Show(
                        $"No regular member found with name '{memberName}'.",
                        "Member Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Check if eligible for premium upgrade
                bool isEligible = member.PremiumUpgradeAvailable();
                
                if (!isEligible)
                {
                    MessageBox.Show(
                        $"{member.Name} is not eligible for premium upgrade.\n" +
                        $"Outstanding additional fees: {member.AdditionalFeesBalance:C}\n\n" +
                        "All outstanding fees must be paid before upgrading.",
                        "Not Eligible for Upgrade",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Member is eligible, ask if they want to upgrade
                DialogResult result = MessageBox.Show(
                    $"{member.Name} is eligible for premium upgrade!\n\n" +
                    "Benefits of Premium Membership:\n" +
                    "- Access to all gym equipment and facilities 24/7\n" +
                    "- Free access to all group classes\n" +
                    "- Personal trainer sessions included\n" +
                    "- Priority booking for facilities\n\n" +
                    "Current monthly fee: " + member.GetMonthlyFee().ToString("C") + "\n" +
                    "Premium monthly fee: " + (member.GetMonthlyFee() * 1.75M).ToString("C") + "\n\n" +
                    "Would you like to upgrade to Premium?",
                    "Premium Upgrade Available",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // Create a new premium membership plan
                    var premiumPlan = new MembershipPlan(
                        member.MembershipPlan.PlanName + " Premium",
                        member.MembershipPlan.MonthlyFee * 1.75M, // 75% higher fee for premium
                        member.MembershipPlan.DurationInMonths,
                        PlanType.PremiumAccess // Set to PremiumAccess to get the correct session allocation
                    );

                    PremiumGymMembers premiumMember = new PremiumGymMembers(
                        member.Name,
                        member.MemberEmail,
                        member.MemberPhoneNumber,
                        premiumPlan, // Use the new premium plan instead of the old one
                        member.MemberJoiningDate,
                        member.MemberHeight,
                        member.MemberWeight,
                        DateTime.Now.AddYears(-member.Age) // Approximate birthdate from age
                    );
                    
                    // Remove regular member and add premium member
                    _gym.RemoveMember(member);
                    _gym.AddMember(premiumMember);

                    MessageBox.Show(
                        $"{memberName} has been upgraded to Premium status!",
                        "Upgrade Successful",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error checking upgrade eligibility: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        
        public void SetupAutoRenewal()
        {
            try
            {
                // Get member name
                string memberName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter regular member name to set up auto-renewal:",
                    "Auto-Renewal Setup",
                    ""
                );

                if (string.IsNullOrEmpty(memberName))
                    return; // User canceled

                // Find the member
                RegularGymMembers member = null;
                foreach (var m in _gym.GetRegularMembers())
                {
                    if (m.Name.Equals(memberName, StringComparison.OrdinalIgnoreCase))
                    {
                        member = m;
                        break;
                    }
                }

                if (member == null)
                {
                    MessageBox.Show(
                        $"No regular member found with name '{memberName}'.",
                        "Member Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Check if auto-renewal is already enabled (retrieve from dictionary)
                bool autoRenewalEnabled = false;
                _autoRenewalStatus.TryGetValue(member.Name, out autoRenewalEnabled);
                
                if (autoRenewalEnabled)
                {
                    // Ask if they want to disable auto-renewal
                    DialogResult disableResult = MessageBox.Show(
                        $"Auto-renewal is currently ENABLED for {member.Name}.\n\n" +
                        "Would you like to disable auto-renewal?",
                        "Auto-Renewal Status",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (disableResult == DialogResult.Yes)
                    {
                        _autoRenewalStatus[member.Name] = false;
                        MessageBox.Show(
                            "Auto-renewal has been disabled.",
                            "Auto-Renewal Disabled",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    return;
                }

                // Auto-renewal is currently disabled, ask if they want to enable it
                DialogResult enableResult = MessageBox.Show(
                    $"Auto-renewal is currently DISABLED for {member.Name}.\n\n" +
                    "Would you like to enable auto-renewal?\n\n" +
                    "This will automatically renew the membership when it expires.",
                    "Auto-Renewal Status",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (enableResult == DialogResult.Yes)
                {
                    _autoRenewalStatus[member.Name] = true;
                    MessageBox.Show(
                        "Auto-renewal has been enabled.",
                        "Auto-Renewal Enabled",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error setting up auto-renewal: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
