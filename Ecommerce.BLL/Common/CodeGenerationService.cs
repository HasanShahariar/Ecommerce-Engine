using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.BLL.Abstraction.Common;
using Ecommerce.Models.Common;
using Ecommerce.Models.Entities.CodeGenerator;
using Ecommerce.Repo.Abstraction.Common;


namespace Ecommerce.BLL.Common
{
    public class CodeGenerationService : ICodeGenerationService
    {
        private readonly ICodeGeneratorRepo _repo;

        public CodeGenerationService(ICodeGeneratorRepo repo)
        {
            _repo = repo;
        }
        public void Dispose()
        {
            _repo.Dispose();
        }

        public async Task<string> GenerateCode(int operationTypeId)
        {
            return await _repo.GenerateCode(operationTypeId);
        }

        public async Task<IList<CodeGenerationOperationType>> GetAllOperationType()
        {
            return await _repo.GetAllOperationType();
        }

        public async Task<Result> SaveOperationType(CodeGenerationOperationType operationType)
        {
            return await _repo.SaveOperationType(operationType);
        }
    }
}
