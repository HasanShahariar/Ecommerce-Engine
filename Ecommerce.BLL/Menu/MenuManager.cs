using Ecommerce.BLL.Abstraction.Menu;
using Ecommerce.BLL.Base;
using Ecommerce.Repo.Abstraction.Menu;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Models.Entities.Menues;

namespace Ecommerce.BLL.Menu
{
    public class MenuManager: Manager<Ecommerce.Models.Entities.Menues.Menu>, IMenuManager
    {
        IMenuRepository _repository;

        public MenuManager(IMenuRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IList<Ecommerce.Models.Entities.Menues.Menu>> GetMenuList(string role)
        {
            var results = await _repository.GetAllMenu(role);

            var mainMenu = new List<Ecommerce.Models.Entities.Menues.Menu>();

            foreach (var result in results)
            {
                if (result.MenuId == null)
                {
                    mainMenu.Add(result);
                }
            }
            return mainMenu;
        }

        public async Task<IList<Models.Entities.Menues.Menu>> GetPermitedMenuByRoles(IList<string> roles)
        {
            var results = await _repository.GetPermitedMenuByRoles(roles);

            var mainMenu = new List<Ecommerce.Models.Entities.Menues.Menu>();

            foreach (var result in results)
            {
                if (result.MenuId == null)
                {
                    mainMenu.Add(result);
                }
            }
            return mainMenu;
        }

        public Task<IList<Models.Entities.Menues.Menu>> GetTopMenu()
        {
            return _repository.GetTopMenu();
        }
    }
}
