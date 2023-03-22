using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Common.ErrorHandlers
{
    [Table("ErrorCode")]
    public class ErrorCodes
    {
        [Key]
        public int Id { get; set; }
        public int Code { get; set; }
        public string ErrorMessage { get; set; }
    }
}
