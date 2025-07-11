/*
 * Kristen Kamouh - 20241747, Christy Khalife - 20231256
 * Submission date: 9/7/2025
 * Instructor: Dr. Pierre Antoine Akiki
 * 
 * Final Project - CSC 323
 * 
 * File: PremiumGymMembers.cs
 * Description: Represents a member with a premium membership plan.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace CSC323FinalProject.Models
{
    public class PremiumGymMembers : GymMember
    {
        // Session managers
        private readonly ITrainerSessionManager _trainerManager;
        private readonly IDieticianSessionManager _dieticianManager;
        
        // Guest passes
        public int GuestPasses { get; private set; } = 3;

        public PremiumGymMembers(string name, Email email, PhoneNumber phoneNumber,
            IMembershipPlan membershipPlan, DateTime joiningDate, decimal height,
            decimal weight, DateTime dateOfBirth)
            : base(name, email, phoneNumber, membershipPlan, joiningDate, height, weight, dateOfBirth)
        {
            // Initialize session managers
            _trainerManager = new TrainerSessionManager();
            _dieticianManager = new DieticianSessionManager();
        }

        // Constructor with dependency injection for testing
        public PremiumGymMembers(string name, Email email, PhoneNumber phoneNumber,
            IMembershipPlan membershipPlan, DateTime joiningDate, decimal height,
            decimal weight, DateTime dateOfBirth,
            ITrainerSessionManager trainerManager, IDieticianSessionManager dieticianManager)
            : base(name, email, phoneNumber, membershipPlan, joiningDate, height, weight, dateOfBirth)
        {
            _trainerManager = trainerManager ?? throw new ArgumentNullException(nameof(trainerManager));
            _dieticianManager = dieticianManager ?? throw new ArgumentNullException(nameof(dieticianManager));
        }

        public override decimal CalculateDiscount()
        {
            return 0.15m; // Premium members get 15% discount
        }

        public override string GetMembershipBenefits()
        {
            return "Unlimited gym access, free group classes, monthly dietician consultation, and premium facilities access.";
        }

        // Trainer Session Management

        public void BookTrainerSession(GymCoach coach, DateTime sessionTime)
        {
            _trainerManager.BookSession(this, coach, sessionTime);
        }

        public bool CancelTrainerSession(int sessionId)
        {
            return _trainerManager.CancelSession(sessionId);
        }

        public List<TrainerSession> GetTrainerSessions()
        {
            return _trainerManager.GetAllSessions(this);
        }

        public List<TrainerSession> GetUpcomingTrainerSessions()
        {
            return _trainerManager.GetUpcomingSessions(this);
        }

        public int GetRemainingTrainerSessions()
        {
            return _trainerManager.GetRemainingSessionCount(this);
        }

        // Dietician Consultation Management

        public void BookDieticianConsultation(Dietician dietician, DateTime sessionTime)
        {
            _dieticianManager.BookSession(this, dietician, sessionTime);
        }

        public bool CancelDieticianConsultation(int consultationId)
        {
            return _dieticianManager.CancelSession(consultationId);
        }

        public List<DieticianConsultation> GetDieticianConsultations()
        {
            return _dieticianManager.GetAllSessions(this);
        }

        public List<DieticianConsultation> GetUpcomingDieticianConsultations()
        {
            return _dieticianManager.GetUpcomingSessions(this);
        }

        public int GetRemainingDieticianConsultations()
        {
            return _dieticianManager.GetRemainingSessionCount(this);
        }

        // Guest Pass Management

        public bool UseGuestPass()
        {
            if (GuestPasses <= 0)
                return false;
                
            GuestPasses--;
            return true;
        }

        public void ResetGuestPasses()
        {
            // Reset guest passes at the beginning of each month
            GuestPasses = 3;
        }
    }
}