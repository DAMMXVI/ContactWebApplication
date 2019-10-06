using System;
using System.Data.Entity;

namespace ContactWebApplication.Models.dbManager
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DatabaseContext()
        {
            Database.SetInitializer<DatabaseContext>(new Creator());
        }
    }
    public class Creator : CreateDatabaseIfNotExists<DatabaseContext>  //Database mevcut değilse
    {
        protected override void Seed(DatabaseContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                Contact contact = new Contact();
                contact.fullName = FakeData.NameData.GetFullName();
                contact.phoneNumber = "0534854754" + i.ToString();
                contact.address = FakeData.PlaceData.GetAddress();
                contact.note = "Note" + i.ToString();
                contact.dateAdded = DateTime.Now;
                context.Contacts.Add(contact);
            }
            context.SaveChanges();
        }
    }
}
