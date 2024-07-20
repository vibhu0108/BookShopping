﻿using Project_Ecomm_Offfical.Data;
using Project_Ecomm_Offical.DataAccess.Repository.IRepository;
using Project_Ecomm_Offical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Ecomm_Offical.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>,IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepository(ApplicationDbContext context): base (context) 
        {
            _context = context;    
        }
    }
}
