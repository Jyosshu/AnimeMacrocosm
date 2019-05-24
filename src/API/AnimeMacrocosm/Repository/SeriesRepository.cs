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

                    SqlCommand sqlCommand = new SqlCommand("SELECT SeriesId, Title FROM Series ORDER BY Title ASC", connection);

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

        public SeriesSummary GetSeriesById(int seriesId)
        {
            SeriesSummary seriesSummary = new SeriesSummary();

            string SERIES_BY_ID_SELECT = "SELECT se.SeriesId, se.Title FROM Series se WHERE se.SeriesId = @seriesId";

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
                                seriesSummary.SeriesId = Convert.ToInt32(reader["SeriesId"]);
                                seriesSummary.Title = Convert.ToString(reader["Title"]);
                            }
                        }
                    }
                    if (seriesSummary.SeriesId != 0 && seriesSummary.Title != null)
                    {
                        seriesSummary.SeriesItems = GetSeriesItemBySeriesId(seriesId);
                        seriesSummary.CreatorAuthors = GetCreatorAuthorsBySeriesId(seriesId);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was a general exception retrieving series: {seriesId}. {ex.Message}");
            }

            return seriesSummary;
        }

        public List<SeriesItem> GetSeriesItemBySeriesId(int seriesId)
        {
            List<SeriesItem> seriesItemCollection = new List<SeriesItem>();

            string SERIES_ITEM_BY_SERIES_ID_SELECT = @"SELECT si.SeriesItemId
, si.Title
, si.Description
, si.FormatId
, ca.CreatorAuthorId
, ca.FirstName
, ca.LastName
, ca.FirstName_English
, ca.LastName_English
, ca.CountryOfOrigin
, fo.FormatName
FROM SeriesItems si
INNER JOIN Series_Creators sc ON sc.SeriesId = si.SeriesId
INNER JOIN CreatorAuthors ca ON ca.CreatorAuthorId = sc.CreatorAuthorId
INNER JOIN Formats fo ON fo.FormatId = si.FormatId
WHERE si.SeriesId = @seriesId";

            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand(SERIES_ITEM_BY_SERIES_ID_SELECT, connection);
                    sqlCommand.Parameters.Add("@seriesId", SqlDbType.Int);
                    sqlCommand.Parameters["@seriesId"].Value = seriesId;

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                seriesItemCollection.Add(MapRowToSeriesItem(reader));
                            }
                        }
                    }

                    foreach (SeriesItem seriesItem in seriesItemCollection)
                    {
                        if (seriesItem != null)
                        {
                            seriesItem.ProductionStudios = GetProductionStudiosBySeriesItemId(seriesItem.SeriesItemId);
                            seriesItem.Distributors = GetDistributorsBySeriesItemId(seriesItem.SeriesItemId);                            
                        }
                    }                    
                }
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }
            return seriesItemCollection;
        }

        public SeriesItem GetSeriesItemById(int seriesItemId)
        {
            SeriesItem seriesItem = new SeriesItem();

            string SERIES_ITEM_SELECT = @"SELECT se.SeriesId 
, se.Title AS 'SeriesTitle'
, ca.CreatorAuthorId
, ca.FirstName
, ca.LastName
, si.SeriesItemId
, si.Title AS 'SeriesItemTitle'
, si.Description
, ps.ProductionStudioId
, ps.ProductionStudioName
, d.DistributorId
, d.DistributorName
, si.FormatId
, fo.FormatName
FROM Series se
INNER JOIN Series_SeriesItems ssi ON ssi.SeriesId = se.SeriesId
INNER JOIN SeriesItems si ON si.SeriesItemId = ssi.SeriesItemId
INNER JOIN Series_Creators sc ON sc.SeriesId = se.SeriesId
INNER JOIN CreatorAuthors ca ON ca.CreatorAuthorId = sc.CreatorId
INNER JOIN SeriesItem_Production sip ON sip.SeriesItemId = si.SeriesItemId
INNER JOIN ProductionStudios ps ON ps.ProductionStudioId = sip.ProductionStudioId
INNER JOIN SeriesItem_Distrobution sib ON sib.SeriesItemId = si.SeriesItemId
INNER JOIN Distributors d ON d.DistributorId = sib.DistributorId
INNER JOIN Formats fo ON fo.FormatId = si.FormatId
WHERE si.SeriesItemId = @ID";

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

        public List<CreatorAuthor> GetCreatorAuthorsBySeriesId(int seriesId)
        {
            List<CreatorAuthor> creatorAuthorsCollection = new List<CreatorAuthor>();

            string CREATOR_AUTHOR_BY_SERIES_ID = @"SELECT ca.CreatorAuthorId
, ca.FirstName_English
, ca.LastName_English
, ca.FirstName
, ca.LastName
, CountryOfOrigin
FROM CreatorAuthors ca
INNER JOIN Series_Creators sc ON sc.CreatorAuthorId = ca.CreatorAuthorId
WHERE sc.SeriesId = @seriesId";

            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand(CREATOR_AUTHOR_BY_SERIES_ID, connection);
                    sqlCommand.Parameters.Add("@seriesId", SqlDbType.Int);
                    sqlCommand.Parameters["@seriesId"].Value = seriesId;

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                creatorAuthorsCollection.Add(MapRowToCreatorAuthor(reader));
                            }
                        }
                    }
                }
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }

            return creatorAuthorsCollection;
        }

        // TODO: Do I want to refactor the Db for SeriesItems_Creators table again?
        public List<CreatorAuthor> GetCreatorAuthorsBySeriesItemId(int seriesItemId)
        {
            List<CreatorAuthor> creatorAuthorsCollection = new List<CreatorAuthor>();

            string CREATOR_AUTHOR_BY_SERIES_ID = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand(CREATOR_AUTHOR_BY_SERIES_ID, connection);
                    sqlCommand.Parameters.Add("@seriesItemId", SqlDbType.Int);
                    sqlCommand.Parameters["@seriesItemId"].Value = seriesItemId;

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                creatorAuthorsCollection.Add(MapRowToCreatorAuthor(reader));
                            }
                        }
                    }
                }
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }

            return creatorAuthorsCollection;
        }

        public List<ProductionStudio> GetProductionStudiosBySeriesItemId(int seriesItemId)
        {
            List<ProductionStudio> productionStudioCollection = new List<ProductionStudio>();
            string PRODUCTION_STUDIOS_BY_SERIESITEM_ID = @"SELECT ps.ProductionStudioId
, ps.ProductionStudioName
, ps.CountryOfOrigin
, ps.WebsiteURL
FROM ProductionStudios ps
INNER JOIN SeriesItem_Production sips ON sips.ProductionStudioId = ps.ProductionStudioId
WHERE sips.SeriesItemId = @seriesItemId";

            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand(PRODUCTION_STUDIOS_BY_SERIESITEM_ID, connection);
                    sqlCommand.Parameters.Add("@seriesItemId", SqlDbType.Int);
                    sqlCommand.Parameters["@seriesItemId"].Value = seriesItemId;

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                productionStudioCollection.Add(MapRowToProductionStudio(reader));
                            }
                        }
                    }
                }
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }

            return productionStudioCollection;
        }

        public List<Distributor> GetDistributorsBySeriesItemId(int seriesItemId)
        {
            List<Distributor> distributorCollection = new List<Distributor>();
            string DISTRIBUTORS_BY_SERIESITEM_ID = @"SELECT di.DistributorId
, di.DistributorName
, di.CountryOfOrigin
, di.WebsiteURL
FROM Distributors di
INNER JOIN SeriesItem_Distributor sidi ON sidi.DistributorId = di.DistributorId
WHERE sidi.SeriesItemId = @seriesItemId";

            try
            {
                using (SqlConnection connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection))
                {
                    connection.Open();

                    SqlCommand sqlCommand = new SqlCommand(DISTRIBUTORS_BY_SERIESITEM_ID, connection);
                    sqlCommand.Parameters.Add("@seriesItemId", SqlDbType.Int);
                    sqlCommand.Parameters["@seriesItemId"].Value = seriesItemId;

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                distributorCollection.Add(MapRowToDistributor(reader));
                            }
                        }
                    }
                }
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }

            return distributorCollection;
        }

        private Series MapRowToSeries(SqlDataReader reader)
        {
            Series tempSeries = new Series
            {
                SeriesId = Convert.ToInt32(reader["SeriesId"]),
                Title = Convert.ToString(reader["Title"])
            };

            return tempSeries;
        }

        private SeriesSummary MapRowToSeriesSummary(SqlDataReader reader)
        {
            SeriesSummary summary = new SeriesSummary
            {
                SeriesId = Convert.ToInt32(reader["SeriesId"]),
                Title = Convert.ToString(reader["Title"]),
                SeriesItems = new List<SeriesItem>()
            };

            //while (reader.HasRows)
            //{
            //    SeriesItem seriesItem = new SeriesItem
            //    {
            //        SeriesItemId = Convert.ToInt32(reader["SeriesItemId"]),

            //        Title = Convert.ToString(reader["SeriesItemTitle"]),
            //        Description = Convert.ToString(reader["Description"]),
            //        CreatorAuthor = new CreatorAuthor()
            //        {
            //            CreatorAuthorId = Convert.ToInt32(reader["CreatorAuthorId"]),
            //            FirstName = Convert.ToString(reader["FirstName"]),
            //            LastName = Convert.ToString(reader["LastName"]),
            //            FirstName_English = Convert.ToString(reader["FirstName_English"]),
            //            LastName_English = Convert.ToString(reader["LastName_English"])
            //        },
            //        // Length = Convert.ToString(reader["SeriesItemLength"]),
            //        Format = new Format
            //        {
            //            FormatId = Convert.ToInt32(reader["FormatId"]),
            //            FormatName = Convert.ToString(reader["FormatName"])
            //        },
            //        // ReleaseDate = DateTime.Parse(Convert.ToString(reader["ReleaseDate"]))
            //    };

            //    summary.SeriesItems.Add(seriesItem);
            //}


            return summary;
        }

        private SeriesItem MapRowToSeriesItem(SqlDataReader reader)
        {
            SeriesItem seriesItem = new SeriesItem
            {
                SeriesItemId = Convert.ToInt32(reader["SeriesItemId"]),

                Title = Convert.ToString(reader["Title"]),
                Description = Convert.ToString(reader["Description"]),

                CreatorAuthor = new CreatorAuthor()
                {
                    CreatorAuthorId = Convert.ToInt32(reader["CreatorAuthorId"]),
                    FirstName_English = Convert.ToString(reader["FirstName_English"]),
                    LastName_English = Convert.ToString(reader["LastName_English"]),
                    FirstName = Convert.ToString(reader["FirstName"]),
                    LastName = Convert.ToString(reader["LastName"]),
                    ContryOfOrigin = Convert.ToString(reader["CountryOfOrigin"])
                },
                // Length = Convert.ToString(reader["SeriesItemLength"]),
                Format = new Format
                {
                    FormatId = Convert.ToInt32(reader["FormatId"]),
                    FormatName = Convert.ToString(reader["FormatName"])
                },
                // ReleaseDate = DateTime.Parse(Convert.ToString(reader["ReleaseDate"]))
            };

            return seriesItem;
        }

        private CreatorAuthor MapRowToCreatorAuthor(SqlDataReader reader)
        {
            CreatorAuthor creatorAuthor = new CreatorAuthor
            {
                CreatorAuthorId = Convert.ToInt32(reader["CreatorAuthorId"]),
                FirstName_English = Convert.ToString(reader["FirstName_English"]),
                LastName_English = Convert.ToString(reader["LastName_English"]),
                FirstName = Convert.ToString(reader["FirstName"]),
                LastName = Convert.ToString(reader["LastName"]),
                ContryOfOrigin = Convert.ToString(reader["CountryOfOrigin"])
            };

            return creatorAuthor; 
        }

        private ProductionStudio MapRowToProductionStudio(SqlDataReader reader)
        {
            ProductionStudio productionStudio = new ProductionStudio
            {
                ProductionStudioId = Convert.ToInt32(reader["ProductionStudioId"]),
                ProductionStudioName = Convert.ToString(reader["ProductionStudioName"]),
                CountryOfOrigin = Convert.ToString(reader["CountryOfOrigin"]),
                WebsiteURL = Convert.ToString(reader["WebsiteURL"])
            };

            return productionStudio;
        }

        private Distributor MapRowToDistributor(SqlDataReader reader)
        {
            Distributor distributor = new Distributor
            {
                DistributorId = Convert.ToInt32(reader["DistributorId"]),
                DistributorName = Convert.ToString(reader["DistributorName"]),
                CountryOfOrigin = Convert.ToString(reader["CountryOfOrigin"]),
                WebsiteURL = Convert.ToString(reader["WebsiteURL"])
            };

            return distributor;
        }
    }
}
