using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace servico_atendimento_psicologia.Models
{
    public class UsuarioContext
    {
        public class TodoContext : DbContext
        {
            public TodoContext(DbContextOptions<TodoContext> options)
                : base(options)
            {
            }

            public DbSet<UsuarioDto> TodoItems { get; set; } = null!;
        }
    }
}
