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
            const string GET_ALL_QUERY = @"SELECT s.SeriesId 
, s.Title
, ca.Id 'CreatorAuthorId'
, ca.FirstName
, ca.LastName 
FROM Series s
Inner Join SeriesCreators sc ON sc.SeriesId = s.SeriesId
Inner Join CreatorAuthors ca ON ca.Id = sc.CreatorId
ORDER BY s.Title";

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

            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Series WHERE SeriesId = @seriesId", connection);
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
            Series tempSeries = new Series()
            {
                SeriesId = Convert.ToInt32(reader["SeriesId"]),
                Title = Convert.ToString(reader["Title"]),
                SeriesCreators = new List<SeriesCreator>()
            };

            SeriesCreator seriesCreator = new SeriesCreator()
            {                 
                CreatorAuthor = new CreatorAuthor()
                {
                    Id = Convert.ToInt32(reader["CreatorAuthorId"]),
                    FirstName = Convert.ToString(reader["FirstName"]),
                    LastName = Convert.ToString(reader["LastName"]),
                }
            };

            tempSeries.SeriesCreators.Add(seriesCreator);
            return tempSeries;
        }
    }
}
