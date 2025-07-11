/*
 * Kristen Kamouh - 20241747, Christy Khalife - 20231256
 * Submission date: 9/7/2025
 * Instructor: Dr. Pierre Antoine Akiki
 * 
 * Final Project - CSC 323
 * 
 * File: IMembershipPlan.cs
 * Description: Interface for membership plans defining the required properties and methods.
 */

using System;
using CSC323FinalProject.Enums;

namespace CSC323FinalProject.Models
{
    public interface IMembershipPlan
    {
        string PlanName { get; set; }
        
        decimal MonthlyFee { get; set; }
        
        int DurationInMonths { get; set; }
        
        DateTime StartDate { get; set; }
        
        DateTime EndDate { get; set; }
        
        PlanType PlanType { get; set; }
        
        int PersonalTrainerSessions { get; set; }
        
        int NutritionConsultations { get; set; }
        
        decimal ApplyDiscountedPercentage { get; set; }
        
        bool IsSpecialPromotion { get; set; }

        decimal CalculateTotalCost();
        
        string GetPlanDetails();
    }
}