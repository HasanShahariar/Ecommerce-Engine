using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Database.Database;
using Ecommerce.Models.Common;
using Ecommerce.Models.Entities.CodeGenerator;
using Ecommerce.Repo.Abstraction.Common;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repo.Common
{
    public class CodeGeneratorRepo : ICodeGeneratorRepo
    {
        private readonly ApplicationDbContext _context;

        public CodeGeneratorRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<string> GenerateCode(int operationTypeId)
        {


            var operationType = await _context.CodeGenerationOperationTypes
                .FirstOrDefaultAsync(c => c.Id == operationTypeId);

            if (operationType == null)
            {
                throw new Exception("operation type missing");
            }

            string code;
            string seperator = "";



            string prefix = seperator + operationType.Code + seperator + DateTime.Now.Year.ToString().Remove(0, 2) + DateTime.Now.Month + DateTime.Now.Second;

            var codeGenerationNumber = await _context.CodeGenerationNumbers.Where(c => c.Prefix == prefix).OrderByDescending(o => o.Code).FirstOrDefaultAsync() ??
                                       new CodeGenerationNumber { Code = 0, Prefix = prefix };

            var maxCode = new CodeGenerationNumber { Code = codeGenerationNumber.Code + 1, Prefix = prefix };

            code = prefix + maxCode.Code;

            _context.CodeGenerationNumbers.Add(maxCode);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return code;


            throw new Exception("internal code generation problem");
        }

        public async Task<IList<CodeGenerationOperationType>> GetAllOperationType()
        {
            var data = await _context.CodeGenerationOperationTypes.ToListAsync();

            return data.OrderBy(c => c.Name).ToList();

        }

        public async Task<Result> SaveOperationType(CodeGenerationOperationType operationType)
        {
            if (operationType.Id > 0)
            {
                var data = await _context.CodeGenerationOperationTypes.FirstOrDefaultAsync(c => c.Id == operationType.Id);

                if (data == null)
                    return Result.Failure(new List<string> { "OperationTypes not found" });

                data.Code = operationType.Code;
                data.Name = operationType.Name;
                var isUpdate = _context.SaveChanges();
                if (isUpdate > 0)
                {
                    return Result.Success();
                }
                return Result.Failure(new List<string> { "failed to update OperationTypes" });
            }
            _context.CodeGenerationOperationTypes.Add(operationType);
            var result = _context.SaveChanges();

            if (result > 0)
            {
                return Result.Success();
            }
            return Result.Failure(new List<string> { "failed to save OperationTypes" });
        }
    }

}
