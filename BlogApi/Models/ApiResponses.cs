using System.Net;

namespace BlogApi.Models
{
    public class ApiResponses
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages{ get; set; } = [];
        public object Result { get; set; }
    }
}
