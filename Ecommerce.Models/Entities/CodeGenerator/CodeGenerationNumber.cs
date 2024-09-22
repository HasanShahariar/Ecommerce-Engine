using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models.Entities.CodeGenerator
{
    [Table("ECOMMERCE_CODE_GENERATION_NUMBER")]
    public class CodeGenerationNumber
    {
        public int Id { get; set; }
        public string Prefix { get; set; }
        public int Code { get; set; }
    }
}
