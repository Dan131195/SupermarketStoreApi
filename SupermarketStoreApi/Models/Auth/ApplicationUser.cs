using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SupermarketStoreApi.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        public Cliente? Cliente { get; set; }
        public ICollection<Ordine>? Ordini { get; set; }
        public ICollection<ProdottoCarrello>? ProdottiCarrello { get; set; }
    }

}
