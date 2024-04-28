using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.Models
{
    namespace Bot.Models
    {
        public class User
        {
            public string Avatar_url { get; set; }
            public List<Repository> Repositories { get; set; }
    }
}

public class Repository
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Full_Name { get; set; }
        public string Html_Url { get; set; }
        public string Description { get; set; }
        public string Created_at{ get; set; }
        public string Language { get; set; }
      
    }
}
