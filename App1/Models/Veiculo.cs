using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Ano { get; set; }
        public double Preco { get; set; }
        public int IdFabricante { get; set; }
        public bool SituacaoVenda { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Modelo} - {Ano} - {Preco} - {IdFabricante} - {SituacaoVenda}";
        }

    }
}
