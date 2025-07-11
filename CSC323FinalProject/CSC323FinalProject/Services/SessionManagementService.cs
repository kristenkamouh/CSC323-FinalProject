using CSC323FinalProject.Models;
using CSC323FinalProject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSC323FinalProject.Services
{
    public class SessionManagementService
    {
        private readonly Gym _gym;
        private readonly TrainerSessionManager _trainerSessionManager;
        private readonly List<DieticianConsultation> _dieticianConsultations;
        private int _nextConsultationId;

        public SessionManagementService(Gym gym, TrainerSessionManager trainerSessionManager, List<DieticianConsultation> dieticianConsultations, int nextConsultationId = 1000)
        {
            _gym = gym ?? throw new ArgumentNullException(nameof(gym));
            _trainerSessionManager = trainerSessionManager ?? throw new ArgumentNullException(nameof(trainerSessionManager));
            _dieticianConsultations = dieticianConsultations ?? throw new ArgumentNullException(nameof(dieticianConsultations));
            _nextConsultationId = nextConsultationId;
        }

        public void BookTrainerSession()
        {
            try
            {
                // Book trainer session
                // Get member name
                string memberName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter member name:",
                    "Book Trainer Session",
                    ""
                );

                if (string.IsNullOrEmpty(memberName))
                    return; // User canceled

                // Get coach name
                string coachName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter coach name:",
                    "Book Trainer Session",
                    ""
                );

                if (string.IsNullOrEmpty(coachName))
                    return; // User canceled

                // Get session date and time
                string yearStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter session year:",
                    "Book Trainer Session",
                    DateTime.Now.Year.ToString()
                );

                string monthStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter session month (1-12):",
                    "Book Trainer Session",
                    DateTime.Now.Month.ToString()
                );

                string dayStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter session day (1-31):",
                    "Book Trainer Session",
                    DateTime.Now.Day.ToString()
                );

                string hourStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter session hour (0-23):",
                    "Book Trainer Session",
                    DateTime.Now.Hour.ToString()
                );

                string minuteStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter session minute (0-59):",
                    "Book Trainer Session",
                    "0"
                );

                // Find the member and coach
                GymMember member = null;
                foreach (var m in _gym.GetRegularMembers().Cast<GymMember>().Concat(_gym.GetPremiumMembers()))
                {
                    if (m.Name.Equals(memberName, StringComparison.OrdinalIgnoreCase))
                    {
                        member = m;
                        break;
                    }
                }

                GymCoach coach = null;
                foreach (var c in _gym.GetCoaches())
                {
                    if (c.Name.Equals(coachName, StringComparison.OrdinalIgnoreCase))
                    {
                        coach = c;
                        break;
                    }
                }

                // Validate member and coach
                if (member == null)
                {
                    MessageBox.Show(
                        $"No member found with name '{memberName}'.",
                        "Member Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (coach == null)
                {
                    MessageBox.Show(
                        $"No coach found with name '{coachName}'.",
                        "Coach Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Parse the date and time
                int year, month, day, hour, minute;
                if (!int.TryParse(yearStr, out year) ||
                    !int.TryParse(monthStr, out month) ||
                    !int.TryParse(dayStr, out day) ||
                    !int.TryParse(hourStr, out hour) ||
                    !int.TryParse(minuteStr, out minute))
                {
                    MessageBox.Show(
                        "Invalid date or time format. Please enter numeric values.",
                        "Input Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                DateTime sessionTime;
                try
                {
                    sessionTime = new DateTime(year, month, day, hour, minute, 0);
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show(
                        "Invalid date or time values. Please check your entries.",
                        "Input Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Book the session
                try
                {
                    bool bookingSuccess = _trainerSessionManager.BookSession(member, coach, sessionTime);
                    
                    if (bookingSuccess)
                    {
                        MessageBox.Show(
                            $"Trainer session booked for {member.Name} with {coach.Name} on {sessionTime.ToString("f")}",
                            "Session Booked",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Failed to book session: {ex.Message}",
                        "Booking Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error booking session: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        public void CancelTrainerSession()
        {
            try
            {
                // Cancel trainer session
                // Get member name
                string memberName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter member name:",
                    "Cancel Trainer Session",
                    ""
                );

                if (string.IsNullOrEmpty(memberName))
                    return; // User canceled

                // Find the member
                GymMember member = null;
                foreach (var m in _gym.GetRegularMembers().Cast<GymMember>().Concat(_gym.GetPremiumMembers()))
                {
                    if (m.Name.Equals(memberName, StringComparison.OrdinalIgnoreCase))
                    {
                        member = m;
                        break;
                    }
                }

                if (member == null)
                {
                    MessageBox.Show(
                        $"No member found with name '{memberName}'.",
                        "Member Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Get the upcoming sessions for this member
                var sessions = _trainerSessionManager.GetUpcomingSessions(member);

                if (sessions.Count == 0)
                {
                    MessageBox.Show(
                        $"No upcoming sessions found for {memberName}.",
                        "No Sessions",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Create a list of sessions to display
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Select a session to cancel:");

                for (int i = 0; i < sessions.Count; i++)
                {
                    sb.AppendLine($"{i + 1}. Coach: {sessions[i].Coach.Name}, Time: {sessions[i].SessionTime.ToString("f")}");
                }

                string selectionStr = Microsoft.VisualBasic.Interaction.InputBox(
                    sb.ToString(),
                    "Cancel Trainer Session",
                    "1"
                );

                if (string.IsNullOrEmpty(selectionStr))
                    return; // User canceled

                if (int.TryParse(selectionStr, out int selection) && selection > 0 && selection <= sessions.Count)
                {
                    var sessionToCancel = sessions[selection - 1];

                    // Cancel the session
                    if (_trainerSessionManager.CancelSession(sessionToCancel.Id))
                    {
                        MessageBox.Show(
                            $"Successfully canceled the session with {sessionToCancel.Coach.Name} on {sessionToCancel.SessionTime.ToString("f")}",
                            "Session Canceled",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    else
                    {
                        MessageBox.Show(
                            "Failed to cancel the session. It might be too late to cancel.",
                            "Cancellation Failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
                }
                else
                {
                    MessageBox.Show(
                        "Invalid selection.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error canceling session: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        public void BookDieticianConsultation()
        {
            try
            {
                // Book dietician consultation
                // Get member name
                string memberName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter member name:",
                    "Book Dietician Consultation",
                    ""
                );

                if (string.IsNullOrEmpty(memberName))
                    return; // User canceled

                // Get dietician name
                string dieticianName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter dietician name:",
                    "Book Dietician Consultation",
                    ""
                );

                if (string.IsNullOrEmpty(dieticianName))
                    return; // User canceled

                // Get consultation date and time
                string yearStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter consultation year:",
                    "Book Dietician Consultation",
                    DateTime.Now.Year.ToString()
                );

                string monthStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter consultation month (1-12):",
                    "Book Dietician Consultation",
                    DateTime.Now.Month.ToString()
                );

                string dayStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter consultation day (1-31):",
                    "Book Dietician Consultation",
                    DateTime.Now.Day.ToString()
                );

                string hourStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter consultation hour (0-23):",
                    "Book Dietician Consultation",
                    DateTime.Now.Hour.ToString()
                );

                string minuteStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter consultation minute (0-59):",
                    "Book Dietician Consultation",
                    "0"
                );

                // Find the member and dietician
                GymMember member = null;
                foreach (var m in _gym.GetRegularMembers().Cast<GymMember>().Concat(_gym.GetPremiumMembers()))
                {
                    if (m.Name.Equals(memberName, StringComparison.OrdinalIgnoreCase))
                    {
                        member = m;
                        break;
                    }
                }

                Dietician dietician = null;
                foreach (var d in _gym.GetDieticians())
                {
                    if (d.Name.Equals(dieticianName, StringComparison.OrdinalIgnoreCase))
                    {
                        dietician = d;
                        break;
                    }
                }

                // Validate member and dietician
                if (member == null)
                {
                    MessageBox.Show(
                        $"No member found with name '{memberName}'.",
                        "Member Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (dietician == null)
                {
                    MessageBox.Show(
                        $"No dietician found with name '{dieticianName}'.",
                        "Dietician Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Parse the date and time
                int year, month, day, hour, minute;
                if (!int.TryParse(yearStr, out year) ||
                    !int.TryParse(monthStr, out month) ||
                    !int.TryParse(dayStr, out day) ||
                    !int.TryParse(hourStr, out hour) ||
                    !int.TryParse(minuteStr, out minute))
                {
                    MessageBox.Show(
                        "Invalid date or time format. Please enter numeric values.",
                        "Input Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                DateTime consultationTime;
                try
                {
                    consultationTime = new DateTime(year, month, day, hour, minute, 0);
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show(
                        "Invalid date or time values. Please check your entries.",
                        "Input Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Validate consultation time
                if (consultationTime <= DateTime.Now.AddHours(2))
                {
                    MessageBox.Show(
                        "Consultation must be scheduled at least 2 hours in advance.",
                        "Invalid Time",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Check for double booking
                bool isDieticianBooked = _dieticianConsultations.Any(c =>
                    c.Dietician == dietician &&
                    c.SessionTime.Date == consultationTime.Date &&
                    Math.Abs((c.SessionTime - consultationTime).TotalHours) < 1 &&
                    c.Status == SessionStatus.Booked);

                if (isDieticianBooked)
                {
                    MessageBox.Show(
                        "Dietician is already booked for this time.",
                        "Dietician Unavailable",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                bool isMemberBooked = _dieticianConsultations.Any(c =>
                    c.Member == member &&
                    c.SessionTime.Date == consultationTime.Date &&
                    Math.Abs((c.SessionTime - consultationTime).TotalHours) < 1 &&
                    c.Status == SessionStatus.Booked);

                if (isMemberBooked)
                {
                    MessageBox.Show(
                        "Member already has a consultation scheduled for this time.",
                        "Member Unavailable",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Create the consultation
                DieticianConsultation consultation = new DieticianConsultation
                {
                    Id = _nextConsultationId++,
                    Member = member,
                    Dietician = dietician,
                    SessionTime = consultationTime,
                    Status = SessionStatus.Booked,
                    BookedDate = DateTime.Now
                };

                // Add to the list
                _dieticianConsultations.Add(consultation);

                MessageBox.Show(
                    $"Consultation booked for {member.Name} with {dietician.Name} on {consultationTime.ToString("f")}",
                    "Consultation Booked",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Schedule the consultation with the dietician
                dietician.ScheduleConsultation(member, consultationTime);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error booking consultation: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        public void CancelDieticianConsultation()
        {
            try
            {
                // Cancel dietician consultation
                // Get member name
                string memberName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter member name:",
                    "Cancel Dietician Consultation",
                    ""
                );

                if (string.IsNullOrEmpty(memberName))
                    return; // User canceled

                // Find the member
                GymMember member = null;
                foreach (var m in _gym.GetRegularMembers().Cast<GymMember>().Concat(_gym.GetPremiumMembers()))
                {
                    if (m.Name.Equals(memberName, StringComparison.OrdinalIgnoreCase))
                    {
                        member = m;
                        break;
                    }
                }

                if (member == null)
                {
                    MessageBox.Show(
                        $"No member found with name '{memberName}'.",
                        "Member Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Get the upcoming consultations for this member
                var consultations = _dieticianConsultations
                    .Where(c => c.Member == member && c.SessionTime > DateTime.Now && c.Status == SessionStatus.Booked)
                    .OrderBy(c => c.SessionTime)
                    .ToList();

                if (consultations.Count == 0)
                {
                    MessageBox.Show(
                        $"No upcoming consultations found for {memberName}.",
                        "No Consultations",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Create a list of consultations to display
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Select a consultation to cancel:");

                for (int i = 0; i < consultations.Count; i++)
                {
                    sb.AppendLine($"{i + 1}. Dietician: {consultations[i].Dietician.Name}, Time: {consultations[i].SessionTime.ToString("f")}");
                }

                string selectionStr = Microsoft.VisualBasic.Interaction.InputBox(
                    sb.ToString(),
                    "Cancel Dietician Consultation",
                    "1"
                );

                if (string.IsNullOrEmpty(selectionStr))
                    return; // User canceled

                if (int.TryParse(selectionStr, out int selection) && selection > 0 && selection <= consultations.Count)
                {
                    var consultationToCancel = consultations[selection - 1];

                    // Check if it's too late to cancel
                    if (consultationToCancel.SessionTime <= DateTime.Now.AddHours(24))
                    {
                        MessageBox.Show(
                            "Cannot cancel consultations less than 24 hours in advance.",
                            "Cancellation Failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    // Cancel the consultation
                    consultationToCancel.Status = SessionStatus.Cancelled;

                    MessageBox.Show(
                        $"Successfully canceled the consultation with {consultationToCancel.Dietician.Name} on {consultationToCancel.SessionTime.ToString("f")}",
                        "Consultation Canceled",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    MessageBox.Show(
                        "Invalid selection.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error canceling consultation: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
