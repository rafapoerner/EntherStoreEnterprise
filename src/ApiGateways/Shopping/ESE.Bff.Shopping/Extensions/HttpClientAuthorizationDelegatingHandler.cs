using ESE.WebApi.Core.User;
using System.Net.Http.Headers;


namespace ESE.Bff.Shopping.Extensions
{
    public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IAspNetUser _aspNetuser;

        public HttpClientAuthorizationDelegatingHandler(IAspNetUser aspNetuser)
        {
            _aspNetuser = aspNetuser;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _aspNetuser.ObterHttpContext().Request.Headers["Authorization"];

            if(!string.IsNullOrEmpty(authorizationHeader) )
            {
                request.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }

            var token = _aspNetuser.GetUserToken();

            if(token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
