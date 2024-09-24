using OblgOpg1Trophy.Models;

namespace OblgOpg1Trophy.Interfaces
{
    public interface ITrophiesRepository
    {
        Trophy? Add(Trophy trophy);
        IEnumerable<Trophy> Get(string? sortBy = null, int? yearLessThan = null, int? yearMoreThan = null);
        Trophy? GetById(int id);
        Trophy? Remove(int id);
        Trophy? Update(int id, Trophy values);
    }
}