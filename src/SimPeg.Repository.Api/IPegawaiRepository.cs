using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SimPeg.Model;

namespace SimPeg.Repository.Api
{
    public interface IPegawaiRepository : IBaseRepository<Pegawai>
    {
        Pegawai GetByID(string nik);
        IList<Pegawai> GetByName(string name);
    }
}
