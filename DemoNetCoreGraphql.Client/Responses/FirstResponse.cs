using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNetCoreGraphql.Client.Responses.FirstResponse
{
    public class FirstResponse
    {
        public FirstDataResponse? First { get; set; }
    }
    public class FirstDataResponse
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
}
