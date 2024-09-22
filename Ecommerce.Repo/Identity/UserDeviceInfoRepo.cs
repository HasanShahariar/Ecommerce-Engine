using Ecommerce.Database.Database;
using Ecommerce.Models.Entities.Identity;
using Ecommerce.Repo.Abstraction.Identity;
using Ecommerce.Repo.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Repo.Identity
{
    public class UserDeviceInfoRepo: Repository<UserDeviceInfo>, IUserDeviceInfoRepo
    {
        private readonly ApplicationDbContext _db;
        public UserDeviceInfoRepo(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
    }
}
