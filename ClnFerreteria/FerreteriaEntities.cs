using System;
using System.Collections.Generic;

namespace ClnFerreteria
{
    internal class FerreteriaEntities : IDisposable
    {
        public IEnumerable<object> Producto { get; internal set; }
        public IEnumerable<object> Usuario { get; internal set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        internal object paProductoListar(string parametro)
        {
            throw new NotImplementedException();
        }

        internal int SaveChanges()
        {
            throw new NotImplementedException();
        }

        internal void SavestChanges()
        {
            throw new NotImplementedException();
        }
    }
}