using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AnimeMacrocosm.Interface;
using AnimeMacrocosm.Models;
using AnimeMacrocosm.Settings;
using Microsoft.Extensions.Options;
using Npgsql;
using Dapper;
using System.Runtime.InteropServices;

namespace AnimeMacrocosm.Repository
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly AppSettings _appSettings;
        private bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public SeriesRepository(IOptionsSnapshot<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public IDbConnection OpenConnection()
        {
            IDbConnection connection;

            if (isWindows == true)
            {
                connection = new SqlConnection(_appSettings.ConnectionStrings.DefaultConnection);
            } 
            else
            {
                connection = new NpgsqlConnection(_appSettings.ConnectionStrings.PostgresConnection);
            }

            connection.Open();

            return connection;
        }

        public List<Series> GetAllSeries()
        {
            List<Series> series = new List<Series>();
            
            try
            {
                using (var connection = OpenConnection())
                {
                    string getAllQuery = "SELECT SeriesId, Title FROM Series ORDER BY Title ASC";
                    series = connection.Query<Series>(getAllQuery).AsList();
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

            string SERIES_BY_ID_SELECT = "SELECT se.SeriesId, se.Title FROM Series se WHERE se.SeriesId = @SeriesId";

            try
            {
                using (var connection = OpenConnection())
                {
                    seriesSummary = connection.QueryFirst<SeriesSummary>(SERIES_BY_ID_SELECT, new { SeriesId = seriesId});

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
, fo.FormatName
FROM SeriesItems si
INNER JOIN Series_Creators sc ON sc.SeriesId = si.SeriesId
INNER JOIN CreatorAuthors ca ON ca.CreatorAuthorId = sc.CreatorAuthorId
INNER JOIN Formats fo ON fo.FormatId = si.FormatId
WHERE si.SeriesId = @SeriesId";

            try
            {
                using (var connection = OpenConnection())
                {
                    seriesItemCollection = connection.Query<SeriesItem, Format>(SERIES_ITEM_BY_SERIES_ID_SELECT, 
                        map: (seriesItem, format ) =>
                       {
                            seriesItem.Format = format;
                            return seriesItem;
                        },
                        param: new { SeriesId = seriesId }).ToList();

                    foreach (SeriesItem seriesItem in seriesItemCollection)
                    {
                        if (seriesItem != null)
                        {
                            seriesItem.CreatorAuthors = GetCreatorAuthorsBySeriesItemId(seriesItem.SeriesItemId);
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
                using (var connection = OpenConnection())
                {
                    seriesItem = connection.QueryFirstOrDefault<SeriesItem>(SERIES_ITEM_SELECT, new { ID = seriesItem });
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
WHERE sc.SeriesId = @SeriesId";

            try
            {
                using (var connection = OpenConnection())
                {
                    creatorAuthorsCollection =  connection.Query<CreatorAuthor>(CREATOR_AUTHOR_BY_SERIES_ID, new { SeriesId = seriesId }).AsList();
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

            string CREATOR_AUTHOR_BY_SERIES_ID = @"SELECT ca.CreatorAuthorId
, ca.FirstName
, ca.LastName
, ca.FirstName_English
, ca.LastName_English
, ca.CountryOfOrigin
FROM CreatorAuthors ca
INNER JOIN Series_Creators sc ON sc.CreatorAuthorId = ca.CreatorAuthorId
INNER JOIN SeriesItems si ON si.seriesId = sc.seriesId
WHERE si.SeriesItemId = @SeriesItemId";

            try
            {
                using (var connection = OpenConnection())
                {
                    creatorAuthorsCollection = connection.Query<CreatorAuthor>(CREATOR_AUTHOR_BY_SERIES_ID, new { SeriesItemId = seriesItemId }).AsList();
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
WHERE sips.SeriesItemId = @SeriesItemId";

            try
            {
                using (var connection = OpenConnection())
                {
                    productionStudioCollection = connection.Query<ProductionStudio>(PRODUCTION_STUDIOS_BY_SERIESITEM_ID, new { SeriesItemId = seriesItemId }).AsList();
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
WHERE sidi.SeriesItemId = @SeriesItemId";

            try
            {
                using (var connection = OpenConnection())
                {
                    distributorCollection = connection.Query<Distributor>(DISTRIBUTORS_BY_SERIESITEM_ID, new { SeriesItemId = seriesItemId }).AsList();
                }
            }
            catch (SqlException se)
            {
                Console.WriteLine(se.Message);
            }

            return distributorCollection;
        }
    }
}
