using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class PickWinnerModel
    {
        [Required(ErrorMessage = "Contest Unique Code is required field!")]
        public string ContestUniqueCode { get; set; }

        [Required(ErrorMessage = "Group Name is required field!")]
        public string GroupName { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Number Of Winners To Pick must be greater than 0")]
        public int NumberOfWinnersToPick { get; set; }

        [Required(ErrorMessage = "Exclude Past Winner Mobile is required field!")]
        public bool ExcludePastWinnerMobile { get; set; } = true;

        [Required(ErrorMessage = "With Receipt Uploaded is required field!")]
        public bool WithReceiptUploaded { get; set; } = false;

        [Required(ErrorMessage = "Start Date is required field!")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required field!")]
        public DateTime EndDate { get; set; }
    }
}
