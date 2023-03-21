using Newtonsoft.Json;
using System.Collections.Generic;

namespace Modulos
{
    public class SteamGridJuego
    {
        [JsonProperty("data")]
        public SteamGridJuegoDatos Datos { get; set; }
    }

    public class SteamGridJuegoDatos
    {
        [JsonProperty("id")]
        public string ID { get; set; }
    }

    //--------------------------------------------------------------

    public class SteamGridImagenes
    {
        [JsonProperty("data")]
        public List<SteamGridImagen> Grids { get; set; }
    }

    public class SteamGridImagen
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("url")]
        public string Enlace { get; set; }

        [JsonProperty("thumb")]
        public string Previo { get; set; }
    }
}
