using System.ComponentModel.DataAnnotations;

namespace JobCandidateHub.Domain.Entities
{
    public class JobCandidate
    {

        [Required(ErrorMessage = "Please enter First name")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Last name")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string PhoneNumber { get; set; }

        
        [Required(ErrorMessage = "Please enter Email Address")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        public string BestCallTime { get; set; } 

        [Url(ErrorMessage = "Invalid LinkedIn profile URL")]
        [StringLength(200, ErrorMessage = "LinkedIn profile URL cannot exceed 200 characters")]
        public string LinkedInProfileUrl { get; set; }

        [Url(ErrorMessage = "Invalid GitHub profile URL")]
        [StringLength(200, ErrorMessage = "GitHub profile URL cannot exceed 200 characters")]
        public string GitHubProfileUrl { get; set; }

        public string Comment { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
