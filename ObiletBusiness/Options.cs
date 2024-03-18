using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObiletBusiness
{
    public class Options
    {
        public required string Authorization { get; set; }
        public required string Port { get; set; }
        public required string ApiUri { get; set; }
        public required string GetLocations { get; set; }
        public required string GetJourneys { get; set; }
        public required string GetSession { get; set; }
        public required string Token { get; set; }
        public required int SessionType { get; set; }
    }
}
