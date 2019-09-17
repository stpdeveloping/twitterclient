using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace TwitterApp.Common
{
    public class OAuthMessageHandler : DelegatingHandler
    {
        private static string _consumerKey = "qLT42Pa4Pm7FxY4v7fqtBw";
        private static string _consumerSecret = "s49oOJabVbS305j5yMVWcHOp3YX9XExl8pUHEv9g";
        private static string _token = "39523825-pCBfpmVcbyopUEXtwdOmMERq7VMtPk937YKO911tj";
        private static string _tokenSecret = "E2EpHYquZTRJ3NYLK9JYeGN0jGD5P8jH9bQHtdFb7JI";

        private OAuthBase _oAuthBase = new OAuthBase();
        public OAuthMessageHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string normalizedUri;
            string normalizedParameters;
            string authHeader;

            string signature = _oAuthBase.GenerateSignature(
             request.RequestUri,
             _consumerKey,
             _consumerSecret,           
             _token,
             _tokenSecret,
             request.Method.Method,
             _oAuthBase.GenerateTimeStamp(),
             _oAuthBase.GenerateNonce(),
             out normalizedUri,
             out normalizedParameters,
             out authHeader);
  
         request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", authHeader);
         return base.SendAsync(request, cancellationToken);

        }
    }
}