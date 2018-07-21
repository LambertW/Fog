using FogDemo.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FogDemo.WebApi.Db
{
    public class MyDbContext : DbContext
    {
        public DbSet<Organ> Organs { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var root = new Organ(new Guid("543A9FCF-4770-4FD9-865F-030E562BE238")) { Name = "集团总部" };
            root.InitPath();

            var develop = new Organ(new Guid("990CB229-CC18-41F3-8E2B-13F0F0110798")) { Name = "研发部" };
            develop.InitPath(root);

            var develop1 = new Organ(new Guid("08F41BF6-4388-4B1E-BD3E-2FF538B44B1B")) { Name = "研发一组" };
            develop1.InitPath(develop);

            var finanial = new Organ(new Guid("C36E43DF-3A99-45DA-80D9-3AC5D24F4014")) { Name = "财务部" };
            finanial.InitPath(root);

            modelBuilder.Entity<Organ>().HasData(
                root,
                develop,
                develop1,
                finanial
                );
        }
    }
}
