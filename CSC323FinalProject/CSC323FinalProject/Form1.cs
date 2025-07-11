using CSC323FinalProject.Models;
using CSC323FinalProject.Enums;
using CSC323FinalProject.Services;
using System.Text;

namespace CSC323FinalProject
{
    public partial class Form1 : Form
    {
        // Create a class-level Gym instance to maintain state between button clicks
        private Gym gym;

        // Create instances of managers
        private TrainerSessionManager trainerSessionManager = new TrainerSessionManager();
        private List<DieticianConsultation> dieticianConsultations = new List<DieticianConsultation>();
        private int nextConsultationId = 1000;

        // Create service instances
        private MemberManagementService _memberManagementService;
        private StaffManagementService _staffManagementService;
        private SessionManagementService _sessionManagementService;

        public Form1()
        {
            InitializeComponent();
            // Initialize the gym when the form is created
            gym = new Gym("Fitness Central", "Downtown");
            
            // Initialize services
            _memberManagementService = new MemberManagementService(gym);
            _staffManagementService = new StaffManagementService(gym);
            _sessionManagementService = new SessionManagementService(gym, trainerSessionManager, dieticianConsultations, nextConsultationId);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Add new member
            _memberManagementService.AddRegularMember();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Remove member
            _memberManagementService.RemoveMember();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Add coach
            _staffManagementService.AddCoach();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Remove coach
            _staffManagementService.RemoveCoach();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Add dietician
            _staffManagementService.AddDietician();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Remove dietician
            _staffManagementService.RemoveDietician();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Book trainer session
            _sessionManagementService.BookTrainerSession();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Cancel trainer session
            _sessionManagementService.CancelTrainerSession();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Book dietician consultation
            _sessionManagementService.BookDieticianConsultation();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Cancel dietician consultation
            _sessionManagementService.CancelDieticianConsultation();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // Pay additional fees
            _memberManagementService.PayAdditionalFees();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // Check premium upgrade eligibility
            _memberManagementService.CheckPremiumUpgradeEligibility();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            gym.DisplayAllMembers();
        }
    }
}
