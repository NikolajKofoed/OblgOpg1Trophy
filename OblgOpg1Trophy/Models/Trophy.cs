using OblgOpg1Trophy.Interfaces;

namespace OblgOpg1Trophy.Models
{
    /// <summary>
    /// Trophy class, meant to represent a trophy presented to the winner of a Competition
    /// </summary>
    public class Trophy : IValidate
    {

        public int Id { get; set; }
        /// <summary>
        /// The Competition that was held
        /// </summary>
        public string Competition { get; set; }
        /// <summary>
        /// The year the competition was held, and a winner was decided
        /// </summary>
        public int Year { get; set; }

        public Trophy(int id, string competition, int year)
        {
            Id = id;
            Competition = competition;
            Year = year;
        }

        public Trophy(string competition, int year)
        {
            Competition = competition;
            Year = year;
        }

        public Trophy()
        {
        }

        /// <summary>
        /// Validates the competition input
        /// </summary>
        /// <exception cref="ArgumentNullException">Throws this exception if competition is null or empty</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws this exception if string length is less than 3</exception>
        public void ValidateCompetition()
        {
            if (string.IsNullOrEmpty(Competition)) throw new ArgumentNullException("Konkurrence skal være udfyldt");
            if (Competition.Length < 3) throw new ArgumentOutOfRangeException("konkurrence Navn skal være min 3 tegn");
        }

        /// <summary>
        /// Validates the Year input
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Throws this exception if input of year is not between/or 1970-2024</exception>
        public void ValidateYear()
        {
            if (Year < 1970 || Year > 2024) throw new ArgumentOutOfRangeException("Året Konkurrence er vundet skal være mellem 1970-2024");
        }

        /// <summary>
        /// A method gathering all validate methods and calling them.
        /// </summary>
        public void Validate()
        {
            ValidateCompetition();
            ValidateYear();
        }

        public override string ToString()
        {
            return $"ID: {Id}, Competition: {Competition}, Year: {Year}";
        }
    }
}
