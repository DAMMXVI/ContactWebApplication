using ContactWebApplication.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactWebApplication.Models
{
    [Table("ListContact")]
    public class Contact
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Display(Name = "fullName", ResourceType = typeof(Resource))]
        [StringLength(20, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "fullNameStrLen"), Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "fullNameRequired")]
        public string fullName { get; set; }

        [StringLength(11)]
        [RegularExpression(@"^(05)[0-9][0-9][1-9]([0-9]){6}$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "phoneNumberInvalid")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "phoneNumberRequired")]
        [Display(Name = "phoneNumber", ResourceType = typeof(Resource))]
        public string phoneNumber { get; set; }

        [StringLength(50, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "addressStrLen")]
        [Display(Name = "address", ResourceType = typeof(Resource))]
        public string address { get; set; }

        [StringLength(120, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "noteStrLen")]
        [Display(Name = "note", ResourceType = typeof(Resource))]
        public string note { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "dateAdded", ResourceType = typeof(Resource))]
        public DateTime? dateAdded { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "dateEdited", ResourceType = typeof(Resource))]
        public DateTime? dateEdited { get; set; }


    }
}