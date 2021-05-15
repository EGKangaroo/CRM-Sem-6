using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CRM_Contact_Service.Data
{
    public class ContactServiceContext : DbContext
    {
        public ContactServiceContext (DbContextOptions<ContactServiceContext> options)
            : base(options)
        {
        }

        public DbSet<CRM_Contact_Service.Data.ContactInfoDAO> ContactInfoDAO { get; set; }
    }
}
