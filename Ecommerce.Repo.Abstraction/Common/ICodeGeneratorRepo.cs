using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Models.Common;
using Ecommerce.Models.Entities.CodeGenerator;

namespace Ecommerce.Repo.Abstraction.Common
{
    public interface ICodeGeneratorRepo : IDisposable
    {
        Task<string> GenerateCode( int operationTypeId);
        Task<IList<CodeGenerationOperationType>> GetAllOperationType();
        Task<Result> SaveOperationType(CodeGenerationOperationType operationType);
    }
}
