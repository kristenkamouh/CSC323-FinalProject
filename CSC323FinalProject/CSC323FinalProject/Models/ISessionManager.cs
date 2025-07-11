/*
 * Kristen Kamouh - 20241747, Christy Khalife - 20231256
 * Submission date: 9/7/2025
 * Instructor: Dr. Pierre Antoine Akiki
 * 
 * Final Project - CSC 323
 * 
 * File: ISessionManager.cs
 * Description: Interface for session management
 */

using System;
using System.Collections.Generic;

namespace CSC323FinalProject.Models
{
    public interface ISessionManager
    {
        // Basic operations
        int GenerateSessionId();
        bool ValidateSession(DateTime sessionTime);
    }

    public interface ITrainerSessionManager : ISessionManager
    {
        // Booking operations
        void BookSession(GymMember member, GymCoach coach, DateTime sessionTime);
        bool CancelSession(int sessionId);
        
        // Session retrieval
        List<TrainerSession> GetAllSessions(GymMember member);
        List<TrainerSession> GetUpcomingSessions(GymMember member);
        
        // Session count operations
        int GetRemainingSessionCount(GymMember member);
        int GetTotalAllowedSessions(GymMember member);
    }

    public interface IDieticianSessionManager : ISessionManager
    {
        // Booking operations
        void BookSession(GymMember member, Dietician dietician, DateTime sessionTime);
        bool CancelSession(int sessionId);
        
        // Session retrieval
        List<DieticianConsultation> GetAllSessions(GymMember member);
        List<DieticianConsultation> GetUpcomingSessions(GymMember member);
        
        // Session count operations
        int GetRemainingSessionCount(GymMember member);
        int GetTotalAllowedSessions(GymMember member);
    }
}
