using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SimPeg.Model;
using SimPeg.Repository.Api;
using Dapper;

namespace SimPeg.Repository.Service
{
    public class PegawaiRepository : IPegawaiRepository
    {
        private IDapperContext _context;
        private string _sql;

        public PegawaiRepository(IDapperContext context)
        {
            _context = context;
        }

        public Pegawai GetByID(string nik)
        {
            Pegawai pegawai = null;

            try
            {
                _sql = @"SELECT Nik, Nama, Alamat, Kota 
                         FROM Pegawai 
                         WHERE Nik = @nik";
                pegawai = _context.db.Query<Pegawai>(_sql, new { nik })
                                  .SingleOrDefault();
            }
            catch
            {
            }

            return pegawai;
        }

        public IList<Pegawai> GetAll()
        {
            IList<Pegawai> listOfPegawai = new List<Pegawai>();

            try
            {
                _sql = @"SELECT Nik, Nama, Alamat, Kota 
                         FROM Pegawai 
                         ORDER BY Nama";
                listOfPegawai = _context.db.Query<Pegawai>(_sql)
                                        .ToList();
            }
            catch
            {
            }

            return listOfPegawai;
        }

        public IList<Pegawai> GetByName(string name)
        {
            IList<Pegawai> listOfPegawai = new List<Pegawai>();

            try
            {
                _sql = @"SELECT Nik, Nama, Alamat, Kota 
                         FROM Pegawai 
                         WHERE Nama LIKE @name
                         ORDER BY Nama";

                name = string.Format("%{0}%", name);
                listOfPegawai = _context.db.Query<Pegawai>(_sql, new { name })
                                        .ToList();
            }
            catch
            {
            }

            return listOfPegawai;
        }

        public int Save(Pegawai obj)
        {
            var result = 0;

            try
            {
                _sql = @"INSERT INTO Pegawai (Nik, Nama, Alamat, Kota) 
                         VALUES (@Nik, @Nama, @Alamat, @Kota);";
                result = _context.db.Execute(_sql, obj);
            }
            catch
            {
            }

            return result;
        }

        public int Update(Pegawai obj)
        {
            var result = 0;

            try
            {
                _sql = @"UPDATE Pegawai SET Nama = @Nama, Alamat = @Alamat, Kota = @Kota
                         WHERE Nik = @Nik";
                result = _context.db.Execute(_sql, obj);
            }
            catch
            {
            }

            return result;
        }

        public int Delete(Pegawai obj)
        {
            var result = 0;

            try
            {
                _sql = @"DELETE FROM Pegawai
                         WHERE Nik = @Nik";
                result = _context.db.Execute(_sql, obj);
            }
            catch
            {
            }

            return result;
        }        
    }
}
