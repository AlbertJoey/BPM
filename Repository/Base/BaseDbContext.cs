using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eage.BPM.Repository
{
    using System.Data.Entity;
    public class BaseDbContext : DbContext
    {
        public BaseDbContext() : base("name=BPMEntities") { }
    }
}
