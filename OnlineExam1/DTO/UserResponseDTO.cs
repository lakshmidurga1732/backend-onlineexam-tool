using System.ComponentModel.DataAnnotations;

namespace OnlineExam1.DTO
{
    public class UserResponseDTO
    {
        [Required]
        public int ResponseID { get; set; }
        public int UserId { get; set; }

        [Required]
        public int TestID { get; set; }

        [Required]
        public int QuestionID { get; set; }

        [Required]
        [StringLength(255)]
        
        public string UserAnswer { get; set; }
    }
}
