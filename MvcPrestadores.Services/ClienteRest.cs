using System.Net.Http;

namespace MvcPrestadores.Services
{
    public static class ClienteRest
    {

        private static HttpClient _cliente = null;

        public static HttpClient ObtenerHttpClient(string Uri)
        {

            try
            {

                if (_cliente == null)
                {
                    _cliente = new HttpClient();
                    return _cliente;
                }
                else
                {
                    return _cliente;
                }
            }
            catch
            {
                _cliente = new HttpClient();
                return _cliente;
            }

        }
    }
}
