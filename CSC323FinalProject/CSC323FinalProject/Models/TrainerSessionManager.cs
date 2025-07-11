/*
 * Kristen Kamouh - 20241747, Christy Khalife - 20231256
 * Submission date: 9/7/2025
 * Instructor: Dr. Pierre Antoine Akiki
 * 
 * Final Project - CSC 323
 * 
 * File: TrainerSessionManager.cs
 * Description: Implementation of trainer session management
 */

using System;
using System.Collections.Generic;
using System.Linq;
using CSC323FinalProject.Enums;

namespace CSC323FinalProject.Models
{
    public class TrainerSessionManager : ITrainerSessionManager
    {
        // Constants
        private const int DEFAULT_TRAINER_SESSIONS = 4;
        private const int MIN_BOOKING_HOURS = 2;
        private const int MIN_CANCELLATION_HOURS = 24;
        
        // Collection to store sessions
        private readonly List<TrainerSession> _sessions;
        // Dictionary to track the last generated ID
        private static readonly Dictionary<string, HashSet<int>> _usedSessionIds = new Dictionary<string, HashSet<int>>();

        public TrainerSessionManager()
        {
            _sessions = new List<TrainerSession>();
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

            // Session must be booked at least 2 hours in advance
            if (sessionTime < DateTime.Now.AddHours(MIN_BOOKING_HOURS))
                return false;

            return true;
        }

        public bool BookSession(GymMember member, GymCoach coach, DateTime sessionTime)
        {
            // Validate inputs
            if (member == null)
            {
                MessageBox.Show("Member cannot be null or empty.",
                    "Member Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }

            if (coach == null)
            {
                MessageBox.Show("Coach cannot be null or empty.",
                    "Coach Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }

            if (!ValidateSession(sessionTime))
            {
                MessageBox.Show($"Invalid session time. Sessions must be at least {MIN_BOOKING_HOURS} hours in the future.",
                    "Session Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }
            
            // Check for double booking
            if (_sessions.Any(s => s.Coach == coach && 
                                  s.SessionTime.Date == sessionTime.Date && 
                                  Math.Abs((s.SessionTime - sessionTime).TotalHours) < 1 &&
                                  s.Status == SessionStatus.Booked))
            {
                MessageBox.Show("Coach already booked",
                    "Coach Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }

            if (_sessions.Any(s => s.Member == member && 
                                  s.SessionTime.Date == sessionTime.Date && 
                                  Math.Abs((s.SessionTime - sessionTime).TotalHours) < 1 &&
                                  s.Status == SessionStatus.Booked))
            {
                MessageBox.Show("Member already has a session",
                    "Member Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }

            // Check remaining sessions
            if (GetRemainingSessionCount(member) <= 0)
            {
                MessageBox.Show("Member has no remaining sessions.",
                    "Member Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }

            // Create new session
            var session = new TrainerSession
            {
                Id = GenerateSessionId(),
                Member = member,
                Coach = coach,
                SessionTime = sessionTime,
                Status = SessionStatus.Booked,
                BookedDate = DateTime.Now
            };

            // Add to collection
            _sessions.Add(session);
            return true;
        }

        public bool CancelSession(int sessionId)
        {
            var session = _sessions.FirstOrDefault(s => s.Id == sessionId);
            if (session == null)
                return false;

            // Can only cancel sessions that are at least 24 hours away
            if (session.SessionTime <= DateTime.Now.AddHours(MIN_CANCELLATION_HOURS))
            {
                MessageBox.Show("Cannot cancel sessions less than {MIN_CANCELLATION_HOURS} hours in advance.",
                    "Session Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            session.Status = SessionStatus.Cancelled;
            return true;
        }

        public List<TrainerSession> GetAllSessions(GymMember member)
        {
            return _sessions
                .Where(s => s.Member == member)
                .ToList();
        }

        public List<TrainerSession> GetUpcomingSessions(GymMember member)
        {
            return _sessions
                .Where(s => s.Member == member && 
                       s.SessionTime > DateTime.Now && 
                       s.Status == SessionStatus.Booked)
                .OrderBy(s => s.SessionTime)
                .ToList();
        }

        public int GetRemainingSessionCount(GymMember member)
        {
            // Get total allowed
            int totalSessions = GetTotalAllowedSessions(member);

            // Count used sessions for current month
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            int usedSessions = _sessions
                .Where(s => s.Member == member &&
                       s.SessionTime.Month == currentMonth &&
                       s.SessionTime.Year == currentYear &&
                       (s.Status == SessionStatus.Booked || s.Status == SessionStatus.Completed))
                .Count();

            return Math.Max(0, totalSessions - usedSessions);
        }

        public int GetTotalAllowedSessions(GymMember member)
        {
            // Get from membership plan using the interface property
            if (member?.MembershipPlan != null)
            {
                return member.MembershipPlan.PersonalTrainerSessions;
            }
            
            // Default for premium members if not specified
            return DEFAULT_TRAINER_SESSIONS;
        }

        void ITrainerSessionManager.BookSession(GymMember member, GymCoach coach, DateTime sessionTime)
        {
            throw new NotImplementedException();
        }
    }
}
