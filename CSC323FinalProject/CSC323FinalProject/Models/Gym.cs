/*
 * Kristen Kamouh - 20241747, Christy Khalifé - 20231256
 * submission date: 9/7/2025
 * instructor: Dr. Pierre Antoine Akiki
 * 
 * Final Project - CSC 323
 * 
 * File: Gym.cs
 * Description: represents a gym that manages members, 
            coaches, dieticians, and membership plans
 */


using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CSC323FinalProject.Models
{
    public class Gym : IGym
    {
        private List<GymMember> _members = new();
        private List<MembershipPlan> _membersPlan = new();
        private List<GymCoach> _coach = new();
        private List<Dietician> _dietician = new();

        private string _gymName;
        private string _gymLocation;

        public Gym(string gymName, string gymLocation)
        {
            GymName = gymName;
            GymLocation = gymLocation;
        }


        public string GymName
        {
            get { return _gymName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Gym name cannot be null or empty.",
                        "Gym Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                _gymName = value;
            }
        }

        public string GymLocation
        {
            get { return _gymLocation; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Gym location cannot be null or empty.",
                        "Gym Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                _gymLocation = value;
            }
        }

        public IMembershipPlan GetMembershipPlan 
        { 
            get
            {
                if (_membersPlan.Count > 0)
                {
                    return _membersPlan[0];
                }
                return null;
            }
        }
        
        public List<IMembershipPlan> GetAllMembershipPlans()
        {
            List<IMembershipPlan> plans = new List<IMembershipPlan>();
            foreach (var plan in _membersPlan)
            {
                plans.Add(plan);
            }
            return plans;
        }

        public void AddMember(GymMember member)
        {
            if (member == null)
            {
                MessageBox.Show("Member cannot be null or empty.",
                    "Member Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            _members.Add(member);
            MessageBox.Show(
                $"{member.Name} has been added successfully.",
                "Member Added",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public void RemoveMember(GymMember member)
        {
            if (member == null)
            {
                MessageBox.Show("Member cannot be null or empty.",
                    "Member Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            _members.Remove(member);
            MessageBox.Show(
                $"{member.Name} has been removed successfully.",
                "Member Removed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        
        public void AddPremiumMember(PremiumGymMembers member)
        {
            if (member == null)
            {
                MessageBox.Show("Member cannot be null or empty.",
                    "Member Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            _members.Add(member);
            MessageBox.Show(
                $"{member.Name} has been added as a premium member successfully.",
                "Premium Member Added",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        
        public void AddRegularMember(RegularGymMembers member)
        {
            if (member == null)
            {
                MessageBox.Show("Member cannot be null or empty.",
                    "Member Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            _members.Add(member);
            MessageBox.Show(
                $"{member.Name} has been added as a regular member successfully.",
                "Regular Member Added",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public void AddCoach(GymCoach coach)
        {
            if (coach == null)
            {
                MessageBox.Show("Coach cannot be null or empty.",
                    "Coach Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            _coach.Add(coach);
            MessageBox.Show(
                $"{coach.Name} has been added as a coach successfully.",
                "Coach Added",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public void RemoveCoach(GymCoach coach)
        {
            if (coach == null)
            {
                MessageBox.Show("Coach cannot be null or empty.",
                    "Coach Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            _coach.Remove(coach);
            MessageBox.Show(
                $"{coach.Name} has been removed successfully.",
                "Coach Removed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public void AddDietician(Dietician dietician)
        {
            if (dietician == null)
            {
                MessageBox.Show("Dietician cannot be null or empty.",
                    "Dietician Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            _dietician.Add(dietician);
            MessageBox.Show(
                $"{dietician.Name} has been added as a dietician successfully.",
                "Dietician Added",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public void RemoveDietician(Dietician dietician)
        {
            if (dietician == null)
            {
                MessageBox.Show("Dietician cannot be null or empty.",
                    "Dietician Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            _dietician.Remove(dietician);
            MessageBox.Show(
                $"{dietician.Name} has been removed successfully.",
                "Dietician Removed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public void AddMembershipPlan(MembershipPlan plan)
        {
            if (plan == null)
            {
                MessageBox.Show("Plan cannot be null or empty.",
                    "Plan Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            _membersPlan.Add(plan);
            MessageBox.Show(
                $"{plan.PlanName} has been added successfully.",
                "Plan Added",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public void RemoveMembershipPlan(MembershipPlan plan)
        {
            if (plan == null)
            {
                MessageBox.Show("Plan cannot be null or empty.",
                    "Plan Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            _membersPlan.Remove(plan);
            MessageBox.Show(
               $"{plan.PlanName} has been removed successfully.",
               "Plan Removed",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information
           );
        }
        
        public List<PremiumGymMembers> GetPremiumMembers()
        {
            List<PremiumGymMembers> premiumMembers = new List<PremiumGymMembers>();
            foreach (var member in _members)
            {
                if (member is PremiumGymMembers premiumMember)
                {
                    premiumMembers.Add(premiumMember);
                }
            }
            return premiumMembers;
        }
        
        public List<RegularGymMembers> GetRegularMembers()
        {
            List<RegularGymMembers> regularMembers = new List<RegularGymMembers>();
            foreach (var member in _members)
            {
                if (member is RegularGymMembers regularMember)
                {
                    regularMembers.Add(regularMember);
                }
            }
            return regularMembers;
        }

        public List<GymCoach> GetCoaches()
        {
            return new List<GymCoach>(_coach);
        }
        
        public List<Dietician> GetDieticians()
        {
            return new List<Dietician>(_dietician);
        }

        public void DisplayAllMembers()
        {
            StringBuilder sb = new();

            sb.AppendLine("Members available:");
            sb.AppendLine("=================");
            
            if (_members.Count == 0)
            {
                sb.AppendLine("No members found.");
            }
            else
            {
                int count = 1;
                foreach (var member in _members)
                {
                    sb.AppendLine($"Member #{count}:");
                    sb.AppendLine($"- Name: {member.Name}");
                    sb.AppendLine($"- Email: {member.MemberEmail?.EmailAddress ?? "N/A"}");
                    sb.AppendLine($"- Phone: {member.MemberPhoneNumber.ToString() ?? "N/A"}");
                    sb.AppendLine($"- Membership: {member.MembershipPlan?.PlanName ?? "N/A"}");
                    sb.AppendLine($"- Joined: {member.MemberJoiningDate.ToShortDateString()}");
                    sb.AppendLine($"- Age: {member.Age}");
                    sb.AppendLine($"- Height: {member.MemberHeight}m");
                    sb.AppendLine($"- Weight: {member.MemberWeight}kg");
                    
                    if (member is PremiumGymMembers premium)
                    {
                        sb.AppendLine($"- Type: Premium Member");
                        sb.AppendLine($"- Guest Passes: {premium.GuestPasses}");
                    }
                    else if (member is RegularGymMembers regular)
                    {
                        sb.AppendLine($"- Type: Regular Member");
                        sb.AppendLine($"- Additional Fees: {regular.AdditionalFeesBalance:C}");
                        sb.AppendLine($"- Auto Renewal: {(regular.AutoSubscriptionRenewal ? "Yes" : "No")}");
                    }
                    
                    sb.AppendLine(); // Empty line between members
                    count++;
                }
            }

            MessageBox.Show(
                sb.ToString(), 
                "Gym Members", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information
            );
        }

        public void DisplayAllCoaches()
        {
            StringBuilder sb = new();

            sb.Append("Coaches available: ");
            foreach (var coach in _coach)
            {
                sb.AppendLine(coach.ToString());
            }

            MessageBox.Show(
                sb.ToString(),
                "Gym Coaches",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public void DisplayAllDieticians()
        {
            StringBuilder sb = new();

            sb.Append("Dieticians available: ");
            foreach (var dietician in _dietician)
            {
                sb.AppendLine(dietician.ToString());
            }

            MessageBox.Show(
                sb.ToString(),
                "Gym Dieticians",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public void DisplayAllPlans()
        {
            StringBuilder sb = new();

            sb.Append("Membership Plans available: ");
            foreach (var plan in _membersPlan)
            {
                sb.AppendLine(plan.ToString());
            }

            MessageBox.Show(
                sb.ToString(),
                "Gym Membership Plans",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        
        public void DisplayMembersByType(string memberType)
        {
            StringBuilder sb = new();
            
            if (string.Equals(memberType, "premium", StringComparison.OrdinalIgnoreCase))
            {
                var premiumMembers = GetPremiumMembers();
                sb.AppendLine($"Premium Members ({premiumMembers.Count}):");
                foreach (var member in premiumMembers)
                {
                    sb.AppendLine(member.ToString());
                }
            }
            else if (string.Equals(memberType, "regular", StringComparison.OrdinalIgnoreCase))
            {
                var regularMembers = GetRegularMembers();
                sb.AppendLine($"Regular Members ({regularMembers.Count}):");
                foreach (var member in regularMembers)
                {
                    sb.AppendLine(member.ToString());
                }
            }
            else
            {
                sb.AppendLine("Invalid member type. Use 'premium' or 'regular'.");
            }
            
            MessageBox.Show(
                sb.ToString(),
                $"{memberType} Members",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}