using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoInde.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Usuariosxrol = new HashSet<Usuariosxrol>();
            Usuarioxtransformador = new HashSet<Usuarioxtransformador>();
            UsuarioxunidadGeneradora = new HashSet<UsuarioxunidadGeneradora>();
        }

        public int CodUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public byte[] Contrasenia { get; set; }
        [DataType(DataType.Date)]
        public DateTime FecNacimiento { get; set; }
        public string Email { get; set; }
        public int CodGenero { get; set; }

        public virtual Genero CodGeneroNavigation { get; set; }
        public virtual ICollection<Usuariosxrol> Usuariosxrol { get; set; }
        public virtual ICollection<Usuarioxtransformador> Usuarioxtransformador { get; set; }
        public virtual ICollection<UsuarioxunidadGeneradora> UsuarioxunidadGeneradora { get; set; }
    }
}
