using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGuide.Data.Repositories
{
    public class BaseRepository
    {
        protected readonly DatabaseContext _context;
        public BaseRepository(DatabaseContext context)
        {
            _context = context;
        }

    }
}
