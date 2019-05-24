using System.Collections.Generic;
using AnimeMacrocosm.Models;

namespace AnimeMacrocosm.Interface
{
    public interface ISeriesRepository
    {
        List<Series> GetAllSeries();
        SeriesSummary GetSeriesById(int seriesId);
        // List<Series> GetSeriesByGenre(int genreId);
        SeriesItem GetSeriesItemById(int seriesItemId);
    }
}
