using System;
using System.Collections.Generic;
using System.Data;
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
            
            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand(GET_ALL_SERIES_SELECT_QUERY, connection);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                series.Add(MapRowToSeries(reader));
                            }
                        }
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
                    sqlCommand.Parameters.Add("@seriesId", SqlDbType.Int);
                    sqlCommand.Parameters["@seriesId"].Value = seriesId;

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                series = MapRowToSeries(reader);
                            }
                        }
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
, se.Title AS 'SeriesTitle'
, ca.Id AS 'CreatorAuthorId'
, ca.FirstName
, ca.LastName
, si.Id AS 'SeriesItemId'
, si.Title AS 'SeriesItemTitle'
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
WHERE si.id = @ID";

            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand(SERIES_ITEM_SELECT, connection);

                    // http://www.dbdelta.com/addwithvalue-is-evil/
                    sqlCommand.Parameters.Add("@ID", SqlDbType.Int);
                    sqlCommand.Parameters["@ID"].Value = seriesItemId;

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                seriesItem = MapRowToSeriesItem(reader);
                            }
                        }
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
                Title = Convert.ToString(reader["Title"])
            };

            return tempSeries;
        }

        private SeriesItem MapRowToSeriesItem(SqlDataReader reader)
        {
            SeriesItem seriesItem = new SeriesItem
            {
                SeriesItemId = Convert.ToInt32(reader["SeriesItemId"]),

                Title = Convert.ToString(reader["SeriesItemTitle"]),
                Description = Convert.ToString(reader["Description"]),
                ProductionStudio = new ProductionStudio
                {
                    ProductionStudioId = Convert.ToInt32(reader["ProductionId"]),
                    ProductionStudioName = Convert.ToString(reader["ProductionStudioName"]),
                    // Country = Convert.ToString(reader["ProductionStudioCountry"])
                },
                Distributor = new Distributor
                {
                    DistributorId = Convert.ToInt32(reader["DistributorId"]),
                    DistributorName = Convert.ToString(reader["DistributorName"]),
                    // Country = Convert.ToString(reader["DistributorCountry"])
                },
                // Length = Convert.ToString(reader["SeriesItemLength"]),
                Format = new Format
                {
                    FormatId = Convert.ToInt32(reader["FormatId"]),
                    FormatName = Convert.ToString(reader["FormatName"])
                },
                // ReleaseDate = DateTime.Parse(Convert.ToString(reader["ReleaseDate"]))
            };

            // TODO: build List<CreatorAuthors>()
            //CreatorAuthors = new CreatorAuthor()
            //{
            //    CreatorAuthorId = Convert.ToInt32(reader["CreatorAuthorId"]),
            //    FirstName = Convert.ToString(reader["FirstName"]),
            //    LastName = Convert.ToString(reader["LastName"])
            //},

            return seriesItem;
        }

        private readonly string GET_ALL_SERIES_SELECT_QUERY = @"SELECT se.SeriesId 
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
    }
}
