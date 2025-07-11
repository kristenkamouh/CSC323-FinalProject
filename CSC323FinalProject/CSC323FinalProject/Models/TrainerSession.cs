/*
 * Kristen Kamouh - 20241747, Christy Khalife - 20231256
 * Submission date: 9/7/2025
 * Instructor: Dr. Pierre Antoine Akiki
 * 
 * Final Project - CSC 323
 * 
 * File: TrainerSession.cs
 * Description: Represents a session between a gym member and a coach.
 */

using System;
using CSC323FinalProject.Enums;

namespace CSC323FinalProject.Models
{
    public class TrainerSession
    {
        public int Id { get; set; }
        public GymMember Member { get; set; }
        public GymCoach Coach { get; set; }
        public DateTime SessionTime { get; set; }
        public SessionStatus Status { get; set; }
        public DateTime BookedDate { get; set; }
    }
}
