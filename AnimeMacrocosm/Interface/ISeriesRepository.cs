using System.Collections.Generic;
using AnimeMacrocosm.Models;

namespace AnimeMacrocosm.Interface
{
    public interface ISeriesRepository
    {
        List<Series> GetAllSeries();
        Series GetSeriesById(int seriesId);
        List<Series> GetSeriesByGenre(int genreId);
    }
}
