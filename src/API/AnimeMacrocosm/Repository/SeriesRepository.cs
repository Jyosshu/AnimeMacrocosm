using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AnimeMacrocosm.Interface;
using AnimeMacrocosm.Models;
using AnimeMacrocosm.Settings;
using Microsoft.Extensions.Options;

namespace AnimeMacrocosm.Repository
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly AppSettings _appSettings;

        public SeriesRepository(IOptionsSnapshot<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public List<Series> GetAllSeries()
        {
            List<Series> series = new List<Series>();
            const string GET_ALL_QUERY = @"SELECT se.SeriesId 
, se.Title
, ca.Id 'CreatorAuthorId'
, ca.FirstName
, ca.LastName
, si.Id 'SeriesItemId'
, si.SeriesId
, si.Title
, si.Description
, si.ProductionId
, si.DistributorId
, si.CreatorAuthorId
, si.Length
, si.FormatId
, si.ReleaseDate
FROM Series se
INNER JOIN SeriesCreators sc ON sc.SeriesId = se.SeriesId
INNER JOIN CreatorAuthors ca ON ca.Id = sc.CreatorId
INNER JOIN SeriesItems si ON si.SeriesId = se.SeriesId
ORDER BY se.Title";

            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand(GET_ALL_QUERY, connection);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        series.Add(MapRowToSeries(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was a general exception retrieving all Series.  {ex.Message}");
            }

            return series;
        }

        public Series GetSeriesById(int seriesId)
        {
            Series series = new Series();

            string SERIES_BY_ID_SELECT = @"SELECT *
FROM Series se
INNER JOIN SeriesCreators sc ON sc.SeriesId = se.SeriesId
INNER JOIN CreatorAuthors ca ON ca.Id = sc.CreatorId
INNER JOIN SeriesItems si ON si.SeriesId = se.SeriesId
WHERE se.SeriesId = @seriesId";

            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand(SERIES_BY_ID_SELECT, connection);
                    sqlCommand.Parameters.AddWithValue("@seriesId", seriesId);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        series = MapRowToSeries(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was a general exception retrieving series: {seriesId}. {ex.Message}");
            }

            return series;
        }

        public SeriesItem GetSeriesItemById(int seriesItemId)
        {
            SeriesItem seriesItem = new SeriesItem();

            string SERIES_ITEM_SELECT = @"SELECT se.SeriesId 
, se.Title
, ca.Id 'CreatorAuthorId'
, ca.FirstName
, ca.LastName
, si.Id 'SeriesItemId'
, si.SeriesId
, si.Title
, si.Description
, si.ProductionId
, ps.ProductionStudioName
, si.DistributorId
, di.DistributorName
, si.CreatorAuthorId
, si.FormatId
, fo.FormatName
FROM Series se
INNER JOIN SeriesCreators sc ON sc.SeriesId = se.SeriesId
INNER JOIN CreatorAuthors ca ON ca.Id = sc.CreatorId
INNER JOIN SeriesItems si ON si.SeriesId = se.SeriesId
INNER JOIN ProductionStudios ps ON ps.Id = si.ProductionId
INNER JOIN Distributors di ON di.Id = si.DistributorId
INNER JOIN Formats fo ON fo.FormatId = si.FormatId
WHERE si.id = @seriesItemId";

            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand(SERIES_ITEM_SELECT, connection);
                    sqlCommand.Parameters.AddWithValue("@seriesItemId", seriesItemId);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        seriesItem = MapRowToSeriesItem(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was a general exception retrieving series: {seriesItemId}. {ex.Message}");
            }

            return seriesItem;
        }


        //public List<Series> GetSeriesByGenre(int genreId)
        //{
        //    List<Series> seriesByGenre = new List<Series>();

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
        //        {
        //            connection.Open();

        //            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Series WHERE GenreId = @genreId");
        //            sqlCommand.Parameters.AddWithValue("seriesId", genreId);
        //            SqlDataReader reader = sqlCommand.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                seriesByGenre.Add(MapRowToSeries(reader));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"There was a general exception retrieving series: {genreId}. {ex.Message}");
        //    }

        //    return seriesByGenre;
        //}

        private Series MapRowToSeries(SqlDataReader reader)
        {
            Series tempSeries = new Series
            {
                SeriesId = Convert.ToInt32(reader["SeriesId"]),
                Title = Convert.ToString(reader["Title"]),
                SeriesCreators = new List<SeriesCreator>(),
                SeriesItems = new List<SeriesItem>()
            };

            SeriesCreator seriesCreator = new SeriesCreator
            {                 
                CreatorAuthor = new CreatorAuthor
                {
                    Id = Convert.ToInt32(reader["CreatorAuthorId"]),
                    FirstName = Convert.ToString(reader["FirstName"]),
                    LastName = Convert.ToString(reader["LastName"]),
                }
            };

            tempSeries.SeriesCreators.Add(seriesCreator);

            // TODO: Add the objects to seriesItem for the associated IDs?
            //SeriesItem seriesItem = new SeriesItem
            //{
            //    Id = Convert.ToInt32(reader["Id"]), // TODO: fill in this collumn
            //    SeriesId = Convert.ToInt32("SeriesId"),
            //    Title = Convert.ToString(reader["Title"]),
            //    Description = Convert.ToString(reader["Description"]),
            //    ProductionId = Convert.ToInt32(reader["ProductionId"]),
            //    DistributorId = Convert.ToInt32(reader["DistrobutionId"]),
            //    CreatorAuthorId = Convert.ToInt32(reader["CreatorAuthorId"]),
            //    Length = Convert.ToString(reader["Length"]),
            //    FormatId = Convert.ToInt32(reader["FormatId"]),
            //    ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"])
            //};
            SeriesItem seriesItem = MapRowToSeriesItem(reader);

            tempSeries.SeriesItems.Add(seriesItem);

            return tempSeries;
        }

        private SeriesItem MapRowToSeriesItem(SqlDataReader reader)
        {
            SeriesItem seriesItem = new SeriesItem
            {
                Id = Convert.ToInt32(reader["SeriesItemId"]),
                SeriesId = Convert.ToInt32(reader["SeriesId"]),
                Title = Convert.ToString(reader["Title"]),
                Description = Convert.ToString(reader["Description"]),
                ProductionStudio = new ProductionStudio
                {
                    Id = Convert.ToInt32(reader["ProductionStudioId"]),
                    ProductionStudioName = Convert.ToString(reader["ProductionStudioName"]),
                    // Country = Convert.ToString(reader["ProductionStudioCountry"])
                },
                Distributor = new Distributor
                {
                    Id = Convert.ToInt32(reader["DistributorId"]),
                    DistributorName = Convert.ToString(reader["DistributorName"]),
                    // Country = Convert.ToString(reader["DistributorCountry"])
                },
                // Length = Convert.ToString(reader["SeriesItemLength"]),
                //CreatorAuthors = new List<CreatorAuthor>()
                //{
                    
                //},
                Format = new Format
                {
                    FormatId = Convert.ToInt32(reader["FormatId"]),
                    FormatName = Convert.ToString(reader["FormatName"])
                },
                // ReleaseDate = DateTime.Parse(Convert.ToString(reader["ReleaseDate"]))
            };
            return seriesItem;
        }
    }
}
