using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEPremium : BECliente
    {
        public BEPremium() { precioExclusivo = true; }

        public bool precioExclusivo { get; }
    }
}
