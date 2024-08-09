using BlazorBasicsServer.Models;

namespace BlazorBasicsServer.Pages.PersonInfo
{
    public partial class PersonsDirectory
    {
        public List<Person> Persons { get; set; }

        public PersonsDirectory()
        {
            Persons = new List<Person>
            {
                new Person()
                {
                    Name = "John",
                    LastName = "Doe",
                    Addresses = new List<Address>
                    {
                        new Address()
                        {
                            AddressType = "House",
                            City = "Test City",
                            PostalCode = "TC1",
                            Street = "Fake Street 123"
                        },
                        new Address()
                        {
                            AddressType = "Apartment",
                            City = "Test City",
                            PostalCode = "TC1",
                            Street = "Fake Street 1234"
                        }
                    }
                },
                new Person()
                {
                    Name = "Jack",
                    LastName = "Who",
                    Addresses = new List<Address>
                    {
                        new Address()
                        {
                            AddressType = "Hotel",
                            City = "Test City",
                            PostalCode = "TC1",
                            Street = "Fake Street 12345"
                        },
                    }
                },
                new Person()
                {
                    Name = "Jason",
                    LastName = "Fake",
                    Addresses = new List<Address>()
                }
            };
        }

        
    }
}
