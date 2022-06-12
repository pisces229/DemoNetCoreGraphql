using DemoNetCoreGraphql.Client.Responses.FirstResponse;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using System.Text.Json;

using var graphQLClient = new GraphQLHttpClient("https://localhost:9120/api/default", new SystemTextJsonSerializer());

var personAndFilmsRequest = new GraphQLRequest
{
    Query = @"
        query (
          $id: Int!
          $name: String!
          $display: Boolean!
        ) {
          first (
            input: { 
              id: $id
              name: $name
            }
          ) {
            id
            name @include(if: $display)
          }
        }
	",
    //   OperationName = "OperationName",
    Variables = new
    {
		id = 9527,
		name = "asdfg",
		display = true,
    },
};

var graphQLResponse = await graphQLClient.SendQueryAsync<FirstResponse>(personAndFilmsRequest);
Console.WriteLine("raw response:");
Console.WriteLine(JsonSerializer.Serialize(graphQLResponse, 
    new JsonSerializerOptions 
    { 
        WriteIndented = false 
    }));

Console.WriteLine($"Id: {graphQLResponse.Data.First!.Id}");
Console.WriteLine($"Name: {graphQLResponse.Data.First!.Name}");

Console.WriteLine();
Console.WriteLine("Press any key to quit...");
Console.ReadKey();

