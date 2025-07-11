/*
 * Kristen Kamouh - 20241747, Christy khalife - 20231256
 * submission date: 9/7/2025
 * instructor: Dr. Pierre Antoine Akiki
 * 
 * Final Project - CSC 323
 * 
 * File: Dietician.cs
 * Description: represents a dietician who can create meal plans, 
               calculate calories, and schedule consultations for gym members.
 */

using System;
using System.Collections.Generic;
using System.Text;
using CSC323FinalProject.Enums;

namespace CSC323FinalProject.Models
{
    public class Dietician : IStaffMembers
    {
        private List<string> Certifications = new();
        private string _name;
        private decimal _salary;

        private const int CALORIES_PER_DAY = 2000;

        public DietitianType Specialization { get; set; }

        public decimal ConsultationRate { get; set; }

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
                    MessageBox.Show("Name cannot be null or empty.",
                        "Name Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
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
                    MessageBox.Show("Salary cannot be null or empty.",
                        "Salary Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                _salary = value;
            }
        }

        public List<string> GetCertifications()
        {
            return Certifications;
        }

        public string CreateMealPlan(GymMember member)
        {
            StringBuilder mealPlan = new();

            if (member == null)
            {
                MessageBox.Show("Member cannot be null or empty.",
                        "Member Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                );
            }

            if (CalculateCalories(member) > CALORIES_PER_DAY)
            {
                mealPlan.Append("Reduce calories and workout more.");
            }
            else if (CalculateCalories(member) < CALORIES_PER_DAY)
            {
                mealPlan.Append("Increase calories and focus on strength training.");
            }
            else
            {
                mealPlan.Append("Maintain current diet and exercise routine.");
            }

            return mealPlan.ToString();
        }

        public int CalculateCalories(GymMember member)
        {
            if (member == null)
            {
                MessageBox.Show("Member cannot be null or empty.",
                        "Member Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }

            return (int)(member.Age * 10 + member.MemberWeight * 5 - member.MemberHeight * 2);
        }

        public void ScheduleConsultation(GymMember member, DateTime date)
        {
            if (member == null)
            {
                MessageBox.Show("Member cannot be null or empty.",
                        "Member Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }

            if (date < DateTime.Now)
            {
                MessageBox.Show("Member cannot be null or empty.",
                        "Member Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }

            MessageBox.Show($"Consultation scheduled for {member.Name} on {date.ToShortDateString()}.");
        }
        
        public void PerformDuties()
        {
            MessageBox.Show($"{Name} is performing duties as a dietician.");
        }
    }
}
