using System.ComponentModel.DataAnnotations;

namespace Models.VM
{
    public class ProfileVM
    {
        [Required(ErrorMessage = "Name is required")]
        [Length(3, 15, ErrorMessage = "Name should be 3 to 15 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phone No. is required")]
        [Phone(ErrorMessage = "Please enter valid Phone No.")]
        public required string Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Length(5, 50, ErrorMessage = "Adddress should be 5 to 50 characters")]
        public required string Address { get; set; }
    }
}
