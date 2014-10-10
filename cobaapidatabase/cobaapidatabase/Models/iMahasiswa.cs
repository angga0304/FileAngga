using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cobaapidatabase.Models
{
    interface iMahasiswa
    {
        IEnumerable<mahasiswa> GetAll();
        mahasiswa Get(string id);
        mahasiswa Add(mahasiswa item);
        void Remove(string id);
        bool Update(mahasiswa item);
    }
}
