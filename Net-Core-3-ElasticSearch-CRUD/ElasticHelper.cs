using Nest;
using System;
using System.Threading.Tasks;

namespace Net_Core_3_ElasticSearch_CRUD
{
    public static class ElasticHelper
    {
        #region ClientSection
        private static ElasticClient elasticClient;
        public static ElasticClient EsClient { get { return elasticClient ?? GetClient(); } set { elasticClient = value; } }

        public static ElasticClient GetClient()
        {
            Uri EsNode = new Uri("http://localhost:9200/");
            elasticClient = new ElasticClient(new ConnectionSettings(EsNode));
            return elasticClient;
        }
        #endregion

        #region IndexSection
        public static IGetIndexResponse GetAllIndices()
        {
            return EsClient.GetIndex(null, c => c.AllIndices());
        }

        public static Task<IGetIndexResponse> GetAllIndicesAsync()
        {
            return EsClient.GetIndexAsync(null, c => c.AllIndices());
        }

        public static IGetIndexResponse GetIndex(string indexName)
        {
            return EsClient.GetIndex(null, c => c.Index(indexName));
        }

        public static Task<IGetIndexResponse> GetIndexAsync(string indexName)
        {
            return EsClient.GetIndexAsync(null, c => c.Index(indexName));
        }

        /// <summary>
        /// Creates Index ps:if index doesnt exist
        /// </summary>
        /// <typeparam name="TClass">Class type</typeparam>
        /// <param name="indexName">index name</param>
        /// <param name="numberOfReplicas">replica count</param>
        /// <param name="numberOfShards">shard count </param>
        public static ICreateIndexResponse CreateIndex<TClass>(string indexName, int numberOfReplicas, int numberOfShards) where TClass : class
        {
            var settings = new IndexSettings { NumberOfReplicas = numberOfReplicas, NumberOfShards = numberOfShards };

            var indexConfig = new IndexState
            {
                Settings = settings
            };

            if (!EsClient.IndexExists(indexName).Exists)
            {
                //EsClient.DeleteIndex("testindex");
                return EsClient.CreateIndex(indexName, c => c
                    .Mappings(ms => ms
                        .Map<TClass>(m => m
                            .AutoMap(typeof(TClass))
                        )
                    )
                );
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Creates Index Async ps:if index doesnt exist
        /// </summary>
        /// <typeparam name="TClass">Class type</typeparam>
        /// <param name="indexName">index name</param>
        /// <param name="numberOfReplicas">replica count</param>
        /// <param name="numberOfShards">shard count </param>
        public static Task<ICreateIndexResponse> CreateIndexAsync<TClass>(string indexName, int numberOfReplicas, int numberOfShards) where TClass : class
        {
            var settings = new IndexSettings { NumberOfReplicas = numberOfReplicas, NumberOfShards = numberOfShards };

            var indexConfig = new IndexState
            {
                Settings = settings
            };

            if (!EsClient.IndexExists(indexName).Exists)
            {
                //EsClient.DeleteIndex("testindex");
                return EsClient.CreateIndexAsync(indexName, c => c
                    .Mappings(ms => ms
                        .Map<TClass>(m => m
                            .AutoMap(typeof(TClass))
                        )
                    )
                );
            }
            else
            {
                return null;
            }
        }

        public static IDeleteIndexResponse DeleteIndex(string indexName)
        {
            return EsClient.DeleteIndex(indexName);
        }

        public static Task<IDeleteIndexResponse> DeleteIndexAsync(string indexName)
        {
            return EsClient.DeleteIndexAsync(indexName);
        }
        #endregion

        #region DocumentSection

        public static ISearchResponse<TClass> GetAllDocumentByIndex<TClass>(string indexName, int from = 0, int size = 20) where TClass : class
        {
            return EsClient.Search<TClass>(s => s
                .Index(indexName)
                .Type(typeof(TClass))
                .From(from)
                .Size(size)
                .Query(q => q.MatchAll())
            );
        }

        public static Task<ISearchResponse<TClass>> GetAllDocumentByIndexAsync<TClass>(string indexName, int from = 0, int size = 20) where TClass : class
        {
            return EsClient.SearchAsync<TClass>(s => s
                .Index(indexName)
                .Type(typeof(TClass))
                .From(from)
                .Size(size)
                .Query(q => q.MatchAll())
            );
        }

        public static IGetResponse<TClass> GetDocumentById<TClass>(string indexName, string docId) where TClass : class
        {
            return EsClient.Get<TClass>(docId, s => s
                .Index(indexName)
                .Type(typeof(TClass))
            );
        }

        public static Task<IGetResponse<TClass>> GetDocumentByIdAsync<TClass>(string indexName, string docId) where TClass : class
        {
            return EsClient.GetAsync<TClass>(docId, s => s
                .Index(indexName)
                .Type(typeof(TClass))
            );
        }

        public static ISearchResponse<TClass> GetDocumentWithQuery<TClass>(string indexName, Func<WildcardQueryDescriptor<TClass>, IWildcardQuery> query) where TClass : class
        {
            return EsClient.Search<TClass>(s => s
                .Index(indexName)
                .Query(c => c.Wildcard(query))
                .Type(typeof(TClass)));
        }

        public static Task<ISearchResponse<TClass>> GetDocumentWithQueryAsync<TClass>(string indexName, Func<WildcardQueryDescriptor<TClass>, IWildcardQuery> query) where TClass : class
        {
            return EsClient.SearchAsync<TClass>(s => s
                .Index(indexName)
                .Query(c => c.Wildcard(query))
                .Type(typeof(TClass)));
        }

        public static IIndexResponse CreateDocument<TClass>(string indexName, TClass obj, string docId) where TClass : class
        {
            return EsClient.Index(obj, i => i
             .Index(indexName)
             .Type(typeof(TClass))
             .Id(docId ?? Guid.NewGuid().ToString())
             .Refresh(Elasticsearch.Net.Refresh.True));
        }

        public static Task<IIndexResponse> CreateDocumentAsync<TClass>(string indexName, TClass obj, string docId) where TClass : class
        {
            return EsClient.IndexAsync(obj, i => i
             .Index(indexName)
             .Type(typeof(TClass))
             .Id(docId ?? Guid.NewGuid().ToString())
             .Refresh(Elasticsearch.Net.Refresh.True));
        }

        public static IUpdateResponse<TClass> UpdateDocument<TClass>(string docId, string indexName, TClass obj) where TClass : class
        {
            return EsClient.Update<TClass>(docId, u => u
                .Index(indexName)
                .Type(typeof(TClass))
                .Doc(obj)
                .Refresh(Elasticsearch.Net.Refresh.True));
        }

        public static Task<IUpdateResponse<TClass>> UpdateDocumentAsync<TClass>(string docId, string indexName, TClass obj) where TClass : class
        {
            return EsClient.UpdateAsync<TClass>(docId, u => u
                .Index(indexName)
                .Type(typeof(TClass))
                .Doc(obj)
                .Refresh(Elasticsearch.Net.Refresh.True));
        }

        public static IDeleteResponse DeleteDocument<TClass>(string docId, string indexName) where TClass : class
        {
            return EsClient.Delete<TClass>(docId, u => u
                .Index(indexName)
                .Type(typeof(TClass))
                .Refresh(Elasticsearch.Net.Refresh.True));
        }

        public static Task<IDeleteResponse> DeleteDocumentAsync<TClass>(string docId, string indexName) where TClass : class
        {
            return EsClient.DeleteAsync<TClass>(docId, u => u
                .Index(indexName)
                .Type(typeof(TClass))
                .Refresh(Elasticsearch.Net.Refresh.True));
        }

        #endregion
    }
}

