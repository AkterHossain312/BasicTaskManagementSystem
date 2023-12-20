using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class SeedRolesModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Permissions { get; set; }
    }
}