/*
 * Kristen Kamouh - 20241747, Christy khalife - 20231256
 * submission date: 9/7/2025
 * instructor: Dr. Pierre Antoine Akiki
 * 
 * Final Project - CSC 323
 * 
 * File: IGym.cs
 * Description: represents a gym interface that defines the properties and 
                methods for managing gym members and membership plans.
 */

using System.Collections.Generic;

namespace CSC323FinalProject.Models
{
    public interface IGym
    {
        public string GymName { get; set; }

        public string GymLocation { get; set; }

        public IMembershipPlan GetMembershipPlan { get; }
        
        public List<IMembershipPlan> GetAllMembershipPlans();

        public void AddMember(GymMember member);

        public void RemoveMember(GymMember member);
        
        public void AddPremiumMember(PremiumGymMembers member);
        
        public void AddRegularMember(RegularGymMembers member);
        
        public void AddCoach(GymCoach coach);
        
        public void RemoveCoach(GymCoach coach);
        
        public void AddDietician(Dietician dietician);
        
        public void RemoveDietician(Dietician dietician);
        
        public void AddMembershipPlan(MembershipPlan plan);
        
        public void RemoveMembershipPlan(MembershipPlan plan);
        
        public List<PremiumGymMembers> GetPremiumMembers();
        
        public List<RegularGymMembers> GetRegularMembers();
        
        public void DisplayAllMembers();
        
        public void DisplayAllCoaches();
        
        public void DisplayAllDieticians();
        
        public void DisplayAllPlans();
        
        public void DisplayMembersByType(string memberType);
    }
}
