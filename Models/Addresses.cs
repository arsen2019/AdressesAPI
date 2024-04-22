using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
namespace AdressesAPI.Models
{
    public class Address 
    {
        public Address() 
        {
            Count();
        }

        /*        public Address(SqlDataReader reader) : base(reader)
                {
                    DataReader = reader;
                    AddressId = Convert.ToInt32(reader["AdressId"].ToString());
                    StudentId = Convert.ToInt32(reader["StudentId"].ToString());
                    Name = reader["Name"].ToString();
                    StreetNumber = reader["StreetNumber"].ToString();
                    StreetName = reader["StreetName"].ToString();
                    City = reader["City"].ToString();
                    ProvinceState = reader["ProvinceState"].ToString();
                    Country = reader["Country"].ToString();
                    PostalCode = reader["PostalCode"].ToString();

                }
        */
        [Key]
        private int AddressId { get; set; } = 0;
        [Required]
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string City { get; set; }
        public string ProvinceState { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }

        private void Count() => AddressId++;

        public int getId() => AddressId;
        
    }

}
