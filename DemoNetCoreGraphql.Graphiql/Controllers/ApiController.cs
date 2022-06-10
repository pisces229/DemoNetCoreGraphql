using GraphQL;
using GraphQL.Instrumentation;
using GraphQL.Transport;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DemoNetCoreGraphql.Graphiql.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;
        private readonly IGraphQLSerializer _graphQLSerializer;
        public ApiController(IDocumentExecuter documentExecuter, 
            ISchema schema,
            IGraphQLSerializer graphQLSerializer)
        {
            _documentExecuter = documentExecuter;
            _schema = schema;
            _graphQLSerializer = graphQLSerializer;
        }

        [HttpPost("graphql")]
        public async Task GraphQL([FromBody] GraphQLRequest request)
        {
            var executionResult = await _documentExecuter.ExecuteAsync(options =>
            {
                options.Schema = _schema;
                options.Query = request.Query;
                options.OperationName = request.OperationName;
                options.Variables = request.Variables;
                //options.Extensions = request.Extensions;
                options.RequestServices = HttpContext.RequestServices;
                options.UserContext = new GraphQLUserContext
                {
                    User = HttpContext.User,
                };
                options.CancellationToken = HttpContext.RequestAborted;
            });
            executionResult.EnrichWithApolloTracing(DateTime.UtcNow);
            HttpContext.Response.ContentType = "application/json";
            HttpContext.Response.StatusCode = executionResult.Executed ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest;
            await _graphQLSerializer.WriteAsync(HttpContext.Response.Body, executionResult, HttpContext.RequestAborted);
        }
    }
}
