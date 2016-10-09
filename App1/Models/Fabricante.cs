using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models
{
    public class Fabricante
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Descricao}";
        }
    }
}
