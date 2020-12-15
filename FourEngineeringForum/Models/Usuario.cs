using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FourEngineeringForum.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        [DisplayName("Perfil")]
        public int PerfilDoUsuario { get; set; }
    }
}
