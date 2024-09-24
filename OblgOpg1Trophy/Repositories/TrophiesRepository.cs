using OblgOpg1Trophy.Interfaces;
using OblgOpg1Trophy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OblgOpg1Trophy.Repositories
{
    /// <summary>
    /// TrophiesRepository, used for storing/altering/Getting trophies, contains CRUD
    /// </summary>
    public class TrophiesRepository : ITrophiesRepository
    {

        private List<Trophy> _trophies;
        private int nextId = 1;

        public TrophiesRepository()
        {
            _trophies = new List<Trophy>();
        }

        /// <summary>
        /// Get method used to return a copy of the list of trophies
        /// </summary>
        /// <param name="sortBy">Sorts the list based on properties, can sort by ascending or descending</param>
        /// <param name="yearLessThan">Filters out all items in the list less than this value</param>
        /// <param name="yearMoreThan">Filters out all items in the list more than this value</param>
        /// <returns></returns>
        public IEnumerable<Trophy> Get(string? sortBy = null, int? yearLessThan = null, int? yearMoreThan = null)
        {
            IEnumerable<Trophy> trophies = new List<Trophy>(_trophies);

            if (yearLessThan != null)
            {
                trophies = trophies.Where(a => a.Year < yearLessThan);
            }
            if (yearMoreThan != null)
            {
                trophies = trophies.Where(a => a.Year > yearMoreThan);
            }

            if (sortBy != null)
            {
                switch (sortBy)
                {
                    case "YearDesc":
                        trophies = trophies.OrderByDescending(a => a.Year);
                        break;
                    case "YearAsc":
                        trophies = trophies.OrderBy(a => a.Year);
                        break;
                    case "CompDesc":
                        trophies = trophies.OrderByDescending(a => a.Competition);
                        break;
                    case "CompAsc":
                        trophies = trophies.OrderBy(a => a.Competition);
                        break;
                    default:
                        break;
                }
            }

            return trophies;
        }

        /// <summary>
        /// Retrives a item from the list of trophies with the given Id
        /// </summary>
        /// <param name="id">the id given to retrieve an item from the list</param>
        /// <returns></returns>
        public Trophy? GetById(int id)
        {
            return _trophies.FirstOrDefault(a => a.Id == id);
        }

        /// <summary>
        /// Adds an item to the list of trophies
        /// </summary>
        /// <param name="trophy">The trophy to get added</param>
        /// <returns></returns>
        public Trophy? Add(Trophy trophy)
        {
            trophy.Validate();
            if (trophy != null)
            {
                trophy.Id = nextId++;
                _trophies.Add(trophy);
            }

            return trophy;
        }

        /// <summary>
        /// Removes an item from the list of trophies
        /// </summary>
        /// <param name="id">the id given as argument to find the item to be deleted</param>
        /// <returns></returns>
        public Trophy? Remove(int id)
        {
            var trophyToBeDeleted = _trophies.FirstOrDefault(a => a.Id == id);

            if (trophyToBeDeleted != null) _trophies.Remove(trophyToBeDeleted);
            return trophyToBeDeleted;
        }

        /// <summary>
        /// Updates an item from the list of trophies
        /// </summary>
        /// <param name="id">The id given as an argument to find the item to be updated</param>
        /// <param name="values">The trophy containing the new values that you want to update with</param>
        /// <returns></returns>
        public Trophy? Update(int id, Trophy values)
        {
            values.Validate();

            var trophyToBeUpdated = _trophies.FirstOrDefault(a => a.Id == id);

            if (values != null && trophyToBeUpdated != null)
            {
                trophyToBeUpdated.Competition = values.Competition;
                trophyToBeUpdated.Year = values.Year;
            }

            return trophyToBeUpdated;
        }
    }
}
