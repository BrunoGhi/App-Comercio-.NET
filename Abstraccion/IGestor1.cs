using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraccion
{
    public interface IGestor1<T> where T : IEntidad
    {
        bool Guardar(T objeto);

        bool Baja(T objeto);

        List<T> ListarTodo();
    }
}
