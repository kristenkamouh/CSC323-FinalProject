using CSC323FinalProject.Models;
using CSC323FinalProject.Enums;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CSC323FinalProject.Services
{
    public class StaffManagementService
    {
        private Gym _gym;

        public StaffManagementService(Gym gym)
        {
            _gym = gym ?? throw new ArgumentNullException(nameof(gym));
        }

        public void AddCoach()
        {
            try
            {
                // Get coach name
                string name = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter coach name:",
                    "Add New Coach",
                    ""
                );

                if (string.IsNullOrEmpty(name))
                    return; // User canceled

                // Get salary
                string salaryStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter salary (e.g., 50000.00):",
                    "Add New Coach",
                    "50000.00"
                );

                // Get hire date
                string yearStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter hire year:",
                    "Add New Coach",
                    DateTime.Now.Year.ToString()
                );

                string monthStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter hire month (1-12):",
                    "Add New Coach",
                    DateTime.Now.Month.ToString()
                );

                string dayStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter hire day (1-31):",
                    "Add New Coach",
                    DateTime.Now.Day.ToString()
                );

                // Get coach specialization
                string specialization = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter coach specialization (CrossFit, PowerLifter, PersonalTrainer, Lifestyle):",
                    "Add New Coach",
                    "PersonalTrainer"
                );

                // Parse values
                decimal salary = decimal.Parse(salaryStr);
                int year = int.Parse(yearStr);
                int month = int.Parse(monthStr);
                int day = int.Parse(dayStr);
                DateTime hireDate = new DateTime(year, month, day);

                // Parse specialization
                CoachType coachType;
                if (!Enum.TryParse(specialization, true, out coachType))
                {
                    MessageBox.Show(
                        "Invalid coach specialization. Using default: PersonalTrainer.",
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    coachType = CoachType.PersonalTrainer;
                }

                // Create and add coach
                GymCoach coach = new(name, salary, hireDate, coachType);
                _gym.AddCoach(coach);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error adding coach: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        public void RemoveCoach()
        {
            try
            {
                // Get coach name to remove
                string coachName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter the name of the coach to remove:",
                    "Remove Coach",
                    ""
                );

                if (string.IsNullOrEmpty(coachName))
                    return; // User canceled

                // Find the coach with the given name using GetCoaches method
                GymCoach coachToRemove = null;
                foreach (var coach in _gym.GetCoaches())
                {
                    if (coach.Name.Equals(coachName, StringComparison.OrdinalIgnoreCase))
                    {
                        coachToRemove = coach;
                        break;
                    }
                }

                // Remove the coach if found
                if (coachToRemove != null)
                {
                    _gym.RemoveCoach(coachToRemove);
                }
                else
                {
                    MessageBox.Show(
                        $"No coach found with name '{coachName}'.",
                        "Coach Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error removing coach: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        public void AddDietician()
        {
            try
            {
                // Get dietician name
                string name = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter dietician name:",
                    "Add Dietician",
                    ""
                );

                if (string.IsNullOrEmpty(name))
                    return; // User canceled

                // Get dietician salary
                string salaryStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter dietician salary:",
                    "Add Dietician",
                    "0"
                );

                // Get dietician consultation rate
                string rateStr = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter consultation rate per hour:",
                    "Add Dietician",
                    "0"
                );

                // Get dietician specialization
                string specialization = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter dietician specialization (WeightLoss, SportDietitian, SportNutritionist, CommunityDietitian):",
                    "Add Dietician",
                    "WeightLoss"
                );

                // Parse the salary
                decimal salary = 0;
                if (!decimal.TryParse(salaryStr, out salary) || salary < 0)
                {
                    MessageBox.Show(
                        "Invalid salary value. Please enter a non-negative number.",
                        "Input Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Parse the consultation rate
                decimal rate = 0;
                if (!decimal.TryParse(rateStr, out rate) || rate < 0)
                {
                    MessageBox.Show(
                        "Invalid consultation rate value. Please enter a non-negative number.",
                        "Input Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Parse the specialization
                DietitianType dieticianType = DietitianType.WeightLoss;
                if (!Enum.TryParse(specialization, true, out dieticianType))
                {
                    // Default to WeightLoss if invalid input
                    dieticianType = DietitianType.WeightLoss;
                }

                // Create the dietician
                Dietician dietician = new Dietician
                {
                    Name = name,
                    Salary = salary,
                    ConsultationRate = rate,
                    Specialization = dieticianType
                };

                // Add the dietician to the gym
                _gym.AddDietician(dietician);

                MessageBox.Show(
                    $"Dietician {name} added successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error adding dietician: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        public void RemoveDietician()
        {
            try
            {
                // Get dietician name to remove
                string dieticianName = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter the name of the dietician to remove:",
                    "Remove Dietician",
                    ""
                );

                if (string.IsNullOrEmpty(dieticianName))
                    return; // User canceled

                // Find the dietician with the given name using GetDieticians method
                Dietician dieticianToRemove = null;
                foreach (var dietician in _gym.GetDieticians())
                {
                    if (dietician.Name.Equals(dieticianName, StringComparison.OrdinalIgnoreCase))
                    {
                        dieticianToRemove = dietician;
                        break;
                    }
                }

                // Remove the dietician if found
                if (dieticianToRemove != null)
                {
                    _gym.RemoveDietician(dieticianToRemove);
                }
                else
                {
                    MessageBox.Show(
                        $"No dietician found with name '{dieticianName}'.",
                        "Dietician Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error removing dietician: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
