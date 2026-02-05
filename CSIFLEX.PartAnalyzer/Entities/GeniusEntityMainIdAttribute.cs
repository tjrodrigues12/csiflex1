using System;
using System.Collections.Generic;
using System.Text;

namespace CSIFLEX.PartAnalyzer.Entities
{
    public class GeniusEntityMainIdAttribute : Attribute
    {
        public GeniusEntityMainIdAttribute(string idName)
        {
            IdName = idName;
        }
        public string IdName { get; set; }
    }
}
