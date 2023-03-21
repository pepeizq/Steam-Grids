using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Herramientas
{
    public static class Decompiladores
    {
        public static async Task<string> CogerHtml(string enlace, string autorizacion = null)
        {
            string html = String.Empty;

            HttpClient cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:15.0) Gecko/20100101 Firefox/15.0.1");
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", autorizacion);

            try
            {
                HttpResponseMessage respuesta = new HttpResponseMessage();
                respuesta = await cliente.GetAsync(new Uri(enlace));
                cliente.Dispose();
                respuesta.EnsureSuccessStatusCode();
        
                html = await respuesta.Content.ReadAsStringAsync() as string;
                respuesta.Dispose();
            }
            catch (Exception)
            {

            };

            cliente.Dispose();
            return html;
        }
    }
}
