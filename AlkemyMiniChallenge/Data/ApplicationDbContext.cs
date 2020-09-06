using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AlkemyMiniChallenge.Models;

namespace AlkemyMiniChallenge.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AlkemyMiniChallenge.Models.Category> Category { get; set; }
        public DbSet<AlkemyMiniChallenge.Models.Operation> Operation { get; set; }
    }
}
