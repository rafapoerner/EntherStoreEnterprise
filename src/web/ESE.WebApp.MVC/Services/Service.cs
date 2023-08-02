using ESE.WebApp.MVC.Extensions;

namespace ESE.WebApp.MVC.Services
{
    public abstract class Service
    {
        protected bool HandleResponseErrors(HttpResponseMessage responseMessage) 
        {
            switch ((int)responseMessage.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(responseMessage.StatusCode);

                case 400:
                    return false;
            }

            responseMessage.EnsureSuccessStatusCode();
            return true;
        }
    }
}
