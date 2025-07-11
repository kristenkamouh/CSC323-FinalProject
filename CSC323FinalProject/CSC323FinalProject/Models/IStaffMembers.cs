/*
 * Kristen Kamouh - 20241747, Christy khalife - 20231256
 * submission date: 9/7/2025
 * instructor: Dr. Pierre Antoine Akiki
 * 
 * Final Project - CSC 323
 * 
 * File: IStaffMembers.cs
 * Description: represents a staff member in the gym management system.
 */


namespace CSC323FinalProject.Models
{
    public interface IStaffMembers
    {
        public string Name { get; set; }

        public decimal Salary { get; set; }
        
        public void PerformDuties();
    }
}
