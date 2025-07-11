/*
 * Kristen Kamouh - 20241747, Christy Khalife - 20231256
 * Submission date: 9/7/2025
 * Instructor: Dr. Pierre Antoine Akiki
 * 
 * Final Project - CSC 323
 * 
 * File: MembershipPlan.cs
 * Description: Concrete implementation of a gym membership plan.
 */

using CSC323FinalProject.Enums;
using System;

namespace CSC323FinalProject.Models
{
    public class MembershipPlan : IMembershipPlan
    {
        public string PlanName
        {
            get => _planName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    MessageBox.Show("Plan cannot be null or empty.",
                        "Plan Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                _planName = value;
            }
        }
        private string _planName;

        public decimal MonthlyFee
        {
            get => _monthlyFee;
            set
            {
                if (value < 0)
                {
                    MessageBox.Show("Monthly fee cannot be null or empty.",
                        "Monthly fee Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                _monthlyFee = value;
            }
        }
        private decimal _monthlyFee;

        public int DurationInMonths
        {
            get => _durationInMonths;
            set
            {
                if (value <= 0)
                {
                    MessageBox.Show("Duration must be positive",
                        "Duration Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                _durationInMonths = value;
            }
        }
        private int _durationInMonths;

        public decimal ApplyDiscountedPercentage { get; set; }
        
        public PlanType PlanType { get; set; }
        
        public int PersonalTrainerSessions { get; set; }
        
        public int NutritionConsultations { get; set; }
        
        public bool IsSpecialPromotion { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public MembershipPlan()
        {
            // Default constructor
            PersonalTrainerSessions = 0;
            NutritionConsultations = 0;
            ApplyDiscountedPercentage = 0;
            IsSpecialPromotion = false;
        }

        public MembershipPlan(string planName, decimal monthlyFee, int durationInMonths, PlanType planType)
        {
            PlanName = planName;
            MonthlyFee = monthlyFee;
            DurationInMonths = durationInMonths;
            PlanType = planType;
            
            // Set default values based on plan type
            if (planType == PlanType.PremiumAccess)
            {
                PersonalTrainerSessions = 4;
                NutritionConsultations = 1;
            }
            else
            {
                PersonalTrainerSessions = 0;
                NutritionConsultations = 0;
            }
            
            ApplyDiscountedPercentage = 0;
            IsSpecialPromotion = false;
        }

        public decimal CalculateTotalCost()
        {
            decimal total = MonthlyFee * DurationInMonths;
            
            if (ApplyDiscountedPercentage > 0)
            {
                total = total * (1 - ApplyDiscountedPercentage / 100m);
            }
            
            return total;
        }

        public string GetPlanDetails()
        {
            string planTypeStr = PlanType.ToString();
            string discount = ApplyDiscountedPercentage > 0 ? $" (with {ApplyDiscountedPercentage}% discount)" : "";
            string trainerSessions = PersonalTrainerSessions > 0 ? $", Includes {PersonalTrainerSessions} personal trainer sessions" : "";
            string nutritionConsults = NutritionConsultations > 0 ? $", Includes {NutritionConsultations} nutrition consultations" : "";
            
            return $"Plan: {PlanName}, Type: {planTypeStr}, Duration: {DurationInMonths} months, " +
                   $"Monthly Fee: ${MonthlyFee}{discount}, Total: ${CalculateTotalCost()}{trainerSessions}{nutritionConsults}";
        }
    }
}