using ApplicationCore.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class TipoAnexo : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Nome { get; set; }
    }
}
