using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineExam1.Entity
{
    public class AssignedTest
    {
        [Key]
        public int AssignmentID { get; set; }

        [Required]
        public int TestID { get; set; }

        [ForeignKey(nameof(TestID))]
        public TestStructure Test { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [Required]
        public User User { get; set; }

        [Required]
        public DateTime ScheduledDateTime { get; set; }

        
    }
}
