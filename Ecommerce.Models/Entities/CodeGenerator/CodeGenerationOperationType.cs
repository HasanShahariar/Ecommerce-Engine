using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models.Entities.CodeGenerator
{
    [Table("ECOMMERCE_CODE_GENERATION_OPERATION_TYPE")]
    public class CodeGenerationOperationType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? OperationNature { get; set; }
    }
}
