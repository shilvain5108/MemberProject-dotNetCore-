using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberManager.Models.DbModels;

namespace MemberManager.Context
{
    public class MemberContext : DbContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ISession session;
        public MemberContext(DbContextOptions<MemberContext> options, 
            IHttpContextAccessor _httpContextAccessor) : base(options)
        {
            httpContextAccessor = _httpContextAccessor;
            session = httpContextAccessor.HttpContext.Session;
        }

        public DbSet<Members> Members { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderDetailStatus> OrderDetailStatus { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductTypes> ProductTypes { get; set; }
        public DbSet<SendTypes> SendTypes { get; set; }

        public DbSet<SysRoles> SysRoles { get; set; }

        public DbSet<MemberRoles> MemberRoles { get; set; }

        public DbSet<SysFunctions> SysFunctions { get; set; }

        public DbSet<SysRolesFunctions> SysRolesFunctions { get; set; }
    }
}
