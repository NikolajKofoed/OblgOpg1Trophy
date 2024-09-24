using Microsoft.VisualStudio.TestTools.UnitTesting;
using OblgOpg1Trophy.Interfaces;
using OblgOpg1Trophy.Models;
using OblgOpg1Trophy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OblgOpg1Trophy.Repositories.Tests
{
    /// <summary>
    /// TestClass for the Trophy Repository
    /// </summary>
    [TestClass()]
    public class TrophiesRepositoryTests
    {

        ITrophiesRepository _repo;

        [TestInitialize]
        public void TestIni()
        {
            _repo = new TrophiesRepository();

            _repo.Add(new Trophy("C Just Swim in", 2014));
            _repo.Add(new Trophy("B MMA World Championships", 1998));
            _repo.Add(new Trophy("A World Tour Swimming", 2003));
            _repo.Add(new Trophy("E Tour De France", 1988));
            _repo.Add(new Trophy("D Just Dance", 2023));


        }

        /// <summary>
        /// Tests the Get Method, and its filter and sort functions
        /// </summary>
        [TestMethod()]
        public void GetTest()
        {

            Assert.AreEqual(_repo.Get().First().Competition, "C Just Swim in");
            Assert.AreEqual(_repo.Get().Count(), 5);


            Assert.AreEqual(_repo.Get(yearLessThan: 2000).Count(), 2);
            Assert.AreEqual(_repo.Get(yearMoreThan: 2000).Count(), 3);
            Assert.AreEqual(_repo.Get(yearMoreThan: 2000, yearLessThan: 2004).Count(), 1);

            Assert.AreEqual(_repo.Get(sortBy: "YearAsc").First().Year, 1988);
            Assert.AreEqual(_repo.Get(sortBy: "YearDesc").First().Year, 2023);
            Assert.AreEqual(_repo.Get(sortBy: "CompAsc").First().Competition, "A World Tour Swimming");
            Assert.AreEqual(_repo.Get(sortBy: "CompDesc").First().Competition, "E Tour De France");

        }

        /// <summary>
        /// Tests that GetById retrieves an item
        /// </summary>
        [TestMethod()]
        public void GetByIdTest()
        {
            var item = _repo.GetById(1);
            var illegalItem = _repo.GetById(-123123); 

            Assert.IsNull(illegalItem);
            Assert.IsNotNull(item);
            Assert.AreEqual(item.Competition, "C Just Swim in");
            Assert.AreEqual(item.Year, 2014);
        }


        /// <summary>
        /// Tests that new items can be added to the repository list.
        /// </summary>
        [TestMethod()]
        public void AddTest()
        {
            // arrange 
            var trophy1 = _repo.Add(new Trophy("Diggin and stuff", 2024));

            Assert.AreEqual(_repo.Get().Count(), 6);
            Assert.AreEqual(trophy1.Competition, "Diggin and stuff");
            Assert.AreEqual(trophy1.Year, 2024);
        }

        /// <summary>
        /// Tests that items can be deleted from the repository list
        /// </summary>
        [TestMethod()]
        public void RemoveTest()
        {
            var initialCount = _repo.Get().Count();
            var removedTrophy = _repo.Remove(4);

            Assert.AreEqual(initialCount - 1, _repo.Get().Count());
            Assert.IsFalse(_repo.Get().Any(t => t.Id == removedTrophy.Id)); // Make sure the Trophy no longer exists in the list
            Assert.AreEqual(removedTrophy.Competition, "E Tour De France");
            Assert.AreEqual(removedTrophy.Year, 1988);

            var removedTrophyNull = _repo.Remove(-1);
            Assert.IsNull(removedTrophyNull); // make sure invalid input gives null
            Assert.AreEqual(initialCount - 1, _repo.Get().Count()); // Make sure count hasn't changed

        }


        /// <summary>
        /// Tests that items can be updated in the repository list
        /// </summary>
        [TestMethod()]
        public void UpdateTest()
        {
            var updatedTrophy = _repo.Update(1, new Trophy("Imaginary", 2005));

            Assert.AreEqual(_repo.GetById(1).Competition, updatedTrophy.Competition);
            Assert.AreEqual(_repo.GetById(1).Year, updatedTrophy.Year);
            Assert.AreEqual(_repo.GetById(1).Id, updatedTrophy.Id);

            var updatedTrophyNull = _repo.Update(-1, new Trophy("this is null", 2015));
            Assert.IsNull(updatedTrophyNull);
        }

    }
}