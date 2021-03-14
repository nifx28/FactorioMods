using System;
using System.Collections.Generic;

namespace FactorioMods.Models
{
    public class Factorio
    {
        public int NameLength;
        public Dictionary<string, Tuple<ModDesc, string, string>> ModsDict = new Dictionary<string, Tuple<ModDesc, string, string>>();
    }
}
