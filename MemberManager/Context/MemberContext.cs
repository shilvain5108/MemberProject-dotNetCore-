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
        public MemberContext(DbContextOptions<MemberContext> options, IHttpContextAccessor _httpContextAccessor) : base(options)
        {
            httpContextAccessor = _httpContextAccessor;
            //session = httpContextAccessor.HttpContext.Session;
        }


        public DbSet<MemberDatas> MemberDatas { get; set; }
    }
}
