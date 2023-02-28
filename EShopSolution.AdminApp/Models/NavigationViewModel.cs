using EShopSolution.ViewModels.System.Languages;
using System.Collections.Generic;

namespace EShopSolution.AdminApp.Models
{
    public class NavigationViewModel
    {
        public List<LanguageViewModel> Languages{ get; set; }
        public string CurrentLanguageId { get; set; }
    }
}
