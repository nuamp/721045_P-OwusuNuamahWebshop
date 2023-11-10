using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Customer key is auto-increment
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustomerEmail { get; set; }
        public string Address { get; set; }
        public string Username {  get; set; }
        public string Password { get; set; }
        public IEnumerable<Order> Orders { get; set; }

        public Customer(string firstname, string lastname, string email, string username, string pwd)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.CustomerEmail = email;
            this.Username = username;
            this.Password = pwd;
        }
    }
    
    

}
