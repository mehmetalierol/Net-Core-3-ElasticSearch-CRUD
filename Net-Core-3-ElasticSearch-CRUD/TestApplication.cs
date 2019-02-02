using System;
using System.Collections.Generic;
using System.Text;

namespace Net_Core_3_ElasticSearch_CRUD
{
    public class TestApplication
    {
        const string IndexName = "TestIndex01";
        static void Main(string[] args)
        {
            Console.WriteLine("App Started!");
            //Index Operations
            //var createIndexResponse = ElasticHelper.CreateIndex<Vehicle>(IndexName, 0, 1);
            //var getAllIndexResponse = ElasticHelper.GetAllIndices();
            //var getIndexByNameResponse = ElasticHelper.GetIndex(IndexName);
            //var deleteIndexREsponse = ElasticHelper.DeleteIndex(IndexName);

            //Document Operation
            //var vehicle = new Vehicle
            //{
            //    AvgGasUsage = 10,
            //    BrandName = "Toyota",
            //    ModelName = "Auris",
            //    Description = "Az yakar çok kaçar",
            //    TopSpeed = 220
            //};
            //var res = ElasticHelper.CreateDocument(IndexName, vehicle, null);
            //var getDocumentByIdResponse = ElasticHelper.GetDocumentById<Vehicle>(IndexName, "some_document_id");
            //var getDocumentWithQueryResponse = ElasticHelper.GetDocumentWithQuery<Vehicle>(IndexName, s => s.Field(x => x.ModelName).Value("*search_Text_Here*"));
            //var getAllDocumentByIndexResponse = ElasticHelper.GetAllDocumentByIndex<Vehicle>(IndexName);
            //foreach (var item in getAllDocumentByIndexResponse.Hits)
            //{
            //    item.Source.Description = "changed description";
            //    var updateDocumentResponse = ElasticHelper.UpdateDocument<Vehicle>(item.Id, IndexName, item.Source);
            //    var deleteDocumentResponse = ElasticHelper.DeleteDocument<Vehicle>(item.Id, IndexName);
            //}

            Console.ReadKey();
        }
    }
}
