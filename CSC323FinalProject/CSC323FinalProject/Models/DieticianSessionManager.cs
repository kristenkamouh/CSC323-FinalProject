/*
 * Kristen Kamouh - 20241747, Christy Khalife - 20231256
 * Submission date: 9/7/2025
 * Instructor: Dr. Pierre Antoine Akiki
 * 
 * Final Project - CSC 323
 * 
 * File: DieticianSessionManager.cs
 * Description: Implementation of dietician consultation management
 */

using System;
using System.Collections.Generic;
using System.Linq;
using CSC323FinalProject.Enums;

namespace CSC323FinalProject.Models
{
    public class DieticianSessionManager : IDieticianSessionManager
    {
        // Constants
        private const int DEFAULT_NUTRITION_CONSULTATIONS = 1;
        private const int MIN_BOOKING_HOURS = 24;
        private const int MIN_CANCELLATION_HOURS = 24;
        
        // Collection to store consultations
        private readonly List<DieticianConsultation> _consultations;
        // Dictionary to track the last generated ID
        private static readonly Dictionary<string, HashSet<int>> _usedSessionIds = new Dictionary<string, HashSet<int>>();

        public DieticianSessionManager()
        {
            _consultations = new List<DieticianConsultation>();
        }

        public int GenerateSessionId()
        {
            var date = DateTime.Now.ToString("yyyyMMdd");
            if (!_usedSessionIds.ContainsKey(date))
            {
                _usedSessionIds[date] = new HashSet<int>();
            }
            
            // Generate a unique ID using date and random component
            int id;
            do
            {
                id = (int)(DateTime.Now.Ticks % 100000) + new Random().Next(1000, 9999);
            }
            while (_usedSessionIds[date].Contains(id));
            
            _usedSessionIds[date].Add(id);
            return id;
        }

        public bool ValidateSession(DateTime sessionTime)
        {
            // Session must be in the future
            if (sessionTime <= DateTime.Now)
                return false;

            // Dietician consultations must be booked at least 24 hours in advance
            if (sessionTime < DateTime.Now.AddHours(MIN_BOOKING_HOURS))
                return false;

            return true;
        }

        public void BookSession(GymMember member, Dietician dietician, DateTime sessionTime)
        {
            // Validate inputs
            if (member == null)
            {
                MessageBox.Show("Member cannot be null or empty.",
                    "Member Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
                
            if (dietician == null)
            {
                MessageBox.Show("Dietician cannot be null or empty.",
                    "Dietician Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            if (!ValidateSession(sessionTime))
            {
                MessageBox.Show("Invalid session time. Consultations must be at least {MIN_BOOKING_HOURS} hours in the future.",
                    "Invalid Session Time",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            
            // Check for double booking
            if (_consultations.Any(c => c.Dietician == dietician && 
                    c.SessionTime.Date == sessionTime.Date && 
                    Math.Abs((c.SessionTime - sessionTime).TotalHours) < 1 &&
                    c.Status == SessionStatus.Booked))
            {
                MessageBox.Show("Dietician already booked.",
                    "Dietician Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            if (_consultations.Any(c => c.Member == member && 
                    c.SessionTime.Date == sessionTime.Date && 
                    Math.Abs((c.SessionTime - sessionTime).TotalHours) < 1 &&
                    c.Status == SessionStatus.Booked))
            {
                MessageBox.Show("Member already has consultation booked at this time.",
                    "Member Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            // Check remaining sessions
            if (GetRemainingSessionCount(member) <= 0)
            {
                MessageBox.Show("No remaining consultation available",
                    "Consultation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            // Create new consultation
            var consultation = new DieticianConsultation
            {
                Id = GenerateSessionId(),
                Member = member,
                Dietician = dietician,
                SessionTime = sessionTime,
                Status = SessionStatus.Booked,
                BookedDate = DateTime.Now
            };

            // Add to collection
            _consultations.Add(consultation);
        }

        public bool CancelSession(int sessionId)
        {
            var consultation = _consultations.FirstOrDefault(c => c.Id == sessionId);
            if (consultation == null)
                return false;

            // Can only cancel sessions that are at least 24 hours away
            if (consultation.SessionTime <= DateTime.Now.AddHours(MIN_CANCELLATION_HOURS))
            {
                MessageBox.Show("Cannot cancel consultations less than {MIN_CANCELLATION_HOURS} hours in advance.",
                    "Cancelation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            consultation.Status = SessionStatus.Cancelled;
            return true;
        }

        public List<DieticianConsultation> GetAllSessions(GymMember member)
        {
            return _consultations
                .Where(c => c.Member == member)
                .ToList();
        }

        public List<DieticianConsultation> GetUpcomingSessions(GymMember member)
        {
            return _consultations
                .Where(c => c.Member == member && 
                    c.SessionTime > DateTime.Now && 
                    c.Status == SessionStatus.Booked)
                .OrderBy(c => c.SessionTime)
                .ToList();
        }

        public int GetRemainingSessionCount(GymMember member)
        {
            // Get total allowed
            int totalConsultations = GetTotalAllowedSessions(member);

            // Count used consultations for current month
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            int usedConsultations = _consultations
                .Where(c => c.Member == member &&
                    c.SessionTime.Month == currentMonth &&
                    c.SessionTime.Year == currentYear &&
                    (c.Status == SessionStatus.Booked || c.Status == SessionStatus.Completed))
                .Count();

            return Math.Max(0, totalConsultations - usedConsultations);
        }

        public int GetTotalAllowedSessions(GymMember member)
        {
            // Get from membership plan using interface property
            if (member?.MembershipPlan != null)
            {
                return member.MembershipPlan.NutritionConsultations;
            }
            
            // Default for premium members if not specified
            return DEFAULT_NUTRITION_CONSULTATIONS;
        }
    }
}
