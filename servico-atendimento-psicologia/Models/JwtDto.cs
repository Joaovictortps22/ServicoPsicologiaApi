using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace servico_atendimento_psicologia.Models
{
    public class JwtDto
    {
        public string Token { get; set; }
        public DateTime Exp { get; set; }
    }
}
