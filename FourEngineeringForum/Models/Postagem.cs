using System;

namespace FourEngineeringForum.Models
{
    public class Postagem
    {
        public int ID { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataDePublicacao { get; set; }

        public int IdDoUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        public int IdDoTopico { get; set; }
        public virtual Topico Topico { get; set; }
    }
}
