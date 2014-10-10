using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cobaapidatabase.Models
{
    public class mahasiswaRepository : iMahasiswa
    {
        private DbAnggaEntities db = new DbAnggaEntities();
        private List<mahasiswa> siswas = new List<mahasiswa>();
        //private int _nextId = 1;
        public IEnumerable<mahasiswa> GetAll()
        {
            siswas = db.mahasiswas.ToList();
            return siswas;
        }
        public mahasiswa Get(string id)
        {
            //siswas = db.mahasiswas.ToList();
            return siswas.Find(s => s.MhsId == id);
        }
        public mahasiswa Add(mahasiswa item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            siswas.Add(item);
            return item;
        }
        public void Remove(string id)
        {
            siswas.RemoveAll(s => s.MhsId == id);
        }
        public bool Update(mahasiswa item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = siswas.FindIndex(s => s.MhsId == item.MhsId);
            if (index == -1)
            {
                return false;
            }
            siswas.RemoveAt(index);
            siswas.Add(item);
            return true;
        }
    }
}