/*
 * Kristen Kamouh - 20241747, Christy khalife - 20231256
 * submission date: 9/7/2025
 * instructor: Dr. Pierre Antoine Akiki
 * 
 * Final Project - CSC 323
 * 
 * File: GymCoach.cs
 * Description: represents a gym coach who can assign members, and create workout plans.
 */

using System;
using System.Collections.Generic;
using System.Text;
using CSC323FinalProject.Enums;

namespace CSC323FinalProject.Models
{
    public class GymCoach : IStaffMembers
    {
        private List<GymMember> AssignedMembers = new();
        private DateTime HireDate;
        private string _name;
        private decimal _salary;

        private const int CALORIES_BURNED_PER_HOUR = 250;

        public CoachType Specialization { get; set; }

        private int GetYearsOfExperience()
        {
            var today = DateTime.Today;
            var thisYearAnniversary = new DateTime(today.Year, HireDate.Month, HireDate.Day);

            var years = today.Year - HireDate.Year;
            if (today < thisYearAnniversary)
                years--;

            return years;
        }

        public int YearsOfExperience
        {
            get
            {
                return GetYearsOfExperience();
            }
        }

        public string Name 
        { 
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Coach Name cannot be null or empty.",
                        "Name Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                _name = value;
            }
        }
        
        public decimal Salary 
        {
            get
            {
                return _salary;
            }
            set
            {
                if (value < 0)
                {
                    MessageBox.Show("Salary cannot be negative",
                        "Salary Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                _salary = value;
            }
        }
        
        
        // TODO: Implement duty performance logic
        public void PerformDuties()
        {
            MessageBox.Show($"{Name} is performing duties as a {Specialization} coach.");
        }

        public void AssignMemberToCoach(GymMember member)
        {
            if (member == null)
            {
                MessageBox.Show("Member cannot be null or empty.",
                    "Member Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            AssignedMembers.Add(member);
        }

        public void RemoveMemberFromCoach(GymMember member)
        {
            if (member == null)
            {
                MessageBox.Show("Member cannot be null or empty.",
                    "Member Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            if (!AssignedMembers.Remove(member))
            {
                MessageBox.Show("Member not found in assigned members.",
                    "Member Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        public List<GymMember> GetAssignedMembers()
        {
            return new List<GymMember>(AssignedMembers);
        }

        public string CreateWorkoutPlan(GymMember member, int currentCaloriesBurntPerHouer)
        {
            StringBuilder sb = new();
            sb.AppendLine($"Creating workout plan for {member.Name}.");

            if (member == null)
            {
                MessageBox.Show("Member cannot be null or empty.", "Member Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (currentCaloriesBurntPerHouer > CALORIES_BURNED_PER_HOUR)
            {
                sb.AppendLine($"{member.Name} is in a calory deficit and should apply push/pull/legs.");
            }
            else
            {
                sb.AppendLine($"{member.Name} is in a calory surplus and should apply hypertrophy.");
            }

            string placeholder = sb.ToString();
            MessageBox.Show(placeholder, "Workout Plan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return placeholder;
        }
        
        public GymCoach() { }
        
        public GymCoach(string name, decimal salary, DateTime hireDate, CoachType specialization)
        {
            Name = name;
            Salary = salary;
            HireDate = hireDate;
            Specialization = specialization;
        }
    }
}
