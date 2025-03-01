using CMS.Domain.Admin.Repository;
using CMS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Class1
    {
        private readonly DataContext _context;
        private readonly IProductRepository _repository;

        public Class1(DataContext context, IProductRepository repository)
        {
            _context = context;
            _repository = repository;
        }


    }
}
