using System;
using System.Collections.Generic;

namespace FourEngineeringForum.Models
{
    public class Topico
    {
        public Topico()
        {
            this.Postagem = new List<Postagem>();
        }

        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public virtual IList<Postagem> Postagem { get; set; }
    }
}
