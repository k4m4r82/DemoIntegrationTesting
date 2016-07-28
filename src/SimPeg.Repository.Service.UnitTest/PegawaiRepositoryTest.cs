using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using SimPeg.Model;
using SimPeg.Repository.Api;
using SimPeg.Repository.Service;

namespace SimPeg.Repository.Service.UnitTest
{
    [TestFixture]
    public class PegawaiRepositoryTest
    {
        private IDapperContext _context;
        private IPegawaiRepository _pegawaiRepo;

        [SetUp]
        public void Init()
        {
            _context = new DapperContext();
            _pegawaiRepo = new PegawaiRepository(_context);
        }

        [Test]
        public void GetByIDTest()
        {
            var nik = "12345";
            var pegawai = _pegawaiRepo.GetByID(nik);

            Assert.IsNotNull(pegawai);
            Assert.AreEqual("12345", pegawai.Nik);
            Assert.AreEqual("Janoe Hendarto, M.Kom", pegawai.Nama);
            Assert.AreEqual("Seturan Yogyakarta", pegawai.Alamat);
            Assert.AreEqual("Yogyakarta", pegawai.Kota);
        }

        [Test]
        public void GetAllTest()
        {
            var listOfPegawai = _pegawaiRepo.GetAll();
            Assert.AreEqual(3, listOfPegawai.Count);

            var index = 2;
            var pegawai = listOfPegawai[index];

            Assert.IsNotNull(pegawai);
            Assert.AreEqual("12346", pegawai.Nik);
            Assert.AreEqual("Vina Zahrotun Kamila, M.Kom", pegawai.Nama);
            Assert.AreEqual("Jl. Sunan Kudus", pegawai.Alamat);
            Assert.AreEqual("Semarang", pegawai.Kota);
        }

        [Test]
        public void SaveTest()
        {
            var pegawai = new Pegawai
            {
                Nik = "12348",
                Nama = "Kamarudin",
                Alamat = "Piyungan, Bantul",
                Kota = "Yogyakarta"
            };

            var result = _pegawaiRepo.Save(pegawai);
            Assert.IsTrue(result != 0);

            var pegawaiBaru = _pegawaiRepo.GetByID(pegawai.Nik);
            Assert.IsNotNull(pegawaiBaru);
            Assert.AreEqual(pegawai.Nik, pegawaiBaru.Nik);
            Assert.AreEqual(pegawai.Nama, pegawaiBaru.Nama);
            Assert.AreEqual(pegawai.Alamat, pegawaiBaru.Alamat);
            Assert.AreEqual(pegawai.Kota, pegawaiBaru.Kota);
        }

        [Test]
        public void UpdateTest()
        {
            var pegawai = new Pegawai
            {
                Nik = "12348",
                Nama = "Kamarudin, S.Kom",
                Alamat = "Jl. Wonosari KM 10, Piyungan, Bantul",
                Kota = "Yogyakarta"
            };

            var result = _pegawaiRepo.Update(pegawai);
            Assert.IsTrue(result != 0);

            var pegawaiUpdated = _pegawaiRepo.GetByID(pegawai.Nik);
            Assert.IsNotNull(pegawaiUpdated);
            Assert.AreEqual(pegawai.Nik, pegawaiUpdated.Nik);
            Assert.AreEqual(pegawai.Nama, pegawaiUpdated.Nama);
            Assert.AreEqual(pegawai.Alamat, pegawaiUpdated.Alamat);
            Assert.AreEqual(pegawai.Kota, pegawaiUpdated.Kota);
        }

        [Test]
        public void DeleteTest()
        {
            var nik = "12345";
            var pegawai = _pegawaiRepo.GetByID(nik);

            var result = _pegawaiRepo.Delete(pegawai);
            Assert.IsTrue(result != 0);

            var pegawaiDeleted = _pegawaiRepo.GetByID(nik);
            Assert.IsNull(pegawaiDeleted);
        }

        [TearDown]
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
