using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OblgOpg1Trophy.Models;

namespace OblgOpg1TrophyTests.Models
{
    /// <summary>
    /// TestClass for Trophy
    /// </summary>
    [TestClass()]
    public class TrophyTests
    {

        public Trophy trophy;
        public Trophy trophyCompetitionNull;
        public Trophy trophyCompetitionEmpty;
        public Trophy trophyCompetitionTooShort;
        public Trophy trophyYearLessThan1970;
        public Trophy trophyYearMoreThan2024;


        [TestInitialize]
        public void TestIni()
        {

            trophy = new Trophy(1, "Track", 1988);
            trophyCompetitionNull = new Trophy(2, null, 2001);
            trophyCompetitionEmpty = new Trophy(3, "", 2022);
            trophyCompetitionTooShort = new Trophy(4, "VB", 1998);
            trophyYearLessThan1970 = new Trophy(5, "Volley Ball", 1968);
            trophyYearMoreThan2024 = new Trophy(6, "Gymnastics", 3023);

        }


        /// <summary>
        /// Validates that the expected exceptions are being thrown when invalid input is given for Competition, when competition is null, empty or less than 3
        /// </summary>
        [TestMethod()]
        public void ValidateCompetitionTest()
        {
            trophy.Validate();
            Assert.ThrowsException<ArgumentNullException>(() => trophyCompetitionNull.ValidateCompetition());
            Assert.ThrowsException<ArgumentNullException>(() => trophyCompetitionEmpty.ValidateCompetition());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophyCompetitionTooShort.ValidateCompetition());
        }

        /// <summary>
        /// Validates that the expected exceptions are being thrown when invalid input is given for Year, when year is less than 1970 or more than 2024
        /// </summary>
        [TestMethod()]
        public void ValidateYearTest()
        {
            trophy.Validate();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophyYearLessThan1970.ValidateYear());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophyYearMoreThan2024.ValidateYear());
        }

        /// <summary>
        /// A method to test that all trophys throw the expected exception and ONLY the expected exception
        /// </summary>
        [TestMethod()]
        public void ValidateTest()
        {
            trophy.Validate();
            Assert.ThrowsException<ArgumentNullException>(() => trophyCompetitionNull.Validate());
            Assert.ThrowsException<ArgumentNullException>(() => trophyCompetitionEmpty.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophyCompetitionTooShort.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophyYearLessThan1970.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophyYearMoreThan2024.Validate());
        }

        [TestMethod()]
        public void ToStringTest()
        {
            // arrange
            var trophy1 = new Trophy(1, "trophy", 2000);
            var result = "ID: 1, Competition: trophy, Year: 2000";

            // assert
            Assert.AreEqual(trophy1.ToString(), result);
        }


        /// <summary>
        /// BoundaryValue test on Year, Legal values only
        /// </summary>
        /// <param name="year">Year takes the value of each datarow and tests if the values are legal</param>
        [TestMethod()]
        [DataRow(2024)]
        [DataRow(2023)]
        [DataRow(1970)]
        [DataRow(1971)]
        public void BoundaryValueTestForYearLegalValues(int year)
        {
            var trophy1 = new Trophy("trophy", year);

            trophy1.Validate();
        }

        /// <summary>
        /// BoundaryValue test on Year, Illegal values only
        /// </summary>
        /// <param name="year">Year takes the value of each datarow and tests if the values are Illegal</param>
        [TestMethod()]
        [DataRow(2025)]
        [DataRow(1969)]
        public void BoundaryValueTestForYearIllegalBoundaryValues(int year)
        {
            var trophy1 = new Trophy("trophy", year);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophy1.ValidateYear());
        }

    }
}