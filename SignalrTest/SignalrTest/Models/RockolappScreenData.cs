using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalrTest.Models
{
    public class RockolappScreenData
    {
        public string Artist { get; set; }
        public string ImageUrl { get; set; }
        public string TrackName { get; set; }
        public int Duration { get; set; }
        public int LapsedTime { get; set; }
    }
}