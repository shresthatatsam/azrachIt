using Microsoft.EntityFrameworkCore;
using System;

namespace TaskManagement.TaskManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

    }
}
