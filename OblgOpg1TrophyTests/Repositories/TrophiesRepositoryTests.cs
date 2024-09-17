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
            var illegalItem = _repo.GetById(123123); // Assume Id: 123123 is invalid

            Assert.IsNull(illegalItem);
            Assert.IsNotNull(item);
        }


        /// <summary>
        /// Tests that new items can be added to the repository list.
        /// </summary>
        [TestMethod()]
        public void AddTest()
        {
            // arrange 
            var trophy1 = new Trophy("Diggin and stuff", 2024);

            Assert.AreEqual(_repo.Get().Count(), 5);

            _repo.Add(trophy1);

            Assert.AreEqual(_repo.Get().Count(), 6);
        }

        /// <summary>
        /// Tests that items can be deleted from the repository list
        /// </summary>
        [TestMethod()]
        public void RemoveTest()
        {
            var initialCount = _repo.Get().Count();
            var removedItem = _repo.Remove(4); // Assume ID 4 exists

            Assert.AreEqual(initialCount - 1, _repo.Get().Count());
            Assert.IsFalse(_repo.Get().Any(t => t.Id == removedItem.Id)); // Make sure the Trophy no longer exists in the list
        }


        /// <summary>
        /// Tests that items can be updated in the repository list
        /// </summary>
        [TestMethod()]
        public void UpdateTest()
        {
            var originalValues = _repo.GetById(1);
            var updatedValues = new Trophy("pølse spisning", 2004);

            _repo.Update(1, updatedValues);

            var updatedTrophy = _repo.GetById(1);
            Assert.AreEqual(updatedValues.Competition, updatedTrophy.Competition);
            Assert.AreEqual(updatedValues.Year, updatedTrophy.Year);

            // Ensure other items are unchanged
            var otherTrophy = _repo.GetById(2); // Assuming ID 2 exists
            Assert.AreNotEqual(updatedValues.Competition, otherTrophy.Competition);
            Assert.AreNotEqual(updatedValues.Year, otherTrophy.Year);
        }

    }
}