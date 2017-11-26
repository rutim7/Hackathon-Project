using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Core.Enums
{
    public enum Category
    {
        [Display(Name = "Спорт")]
        Sport = 0,

        [Display(Name = "Технології")]
        Technology = 1,

        [Display(Name = "Політика")]
        Politic = 2,

        [Display(Name = "Розваги")]
        Entertainment = 3,

        [Display(Name = "Відпочинок")]
        Rest = 4,

        [Display(Name = "Суспільство")]
        Society = 5,

        [Display(Name = "Авто")]
        Auto = 6,

        [Display(Name = "Медицина")]
        Medicine = 7
    }
}