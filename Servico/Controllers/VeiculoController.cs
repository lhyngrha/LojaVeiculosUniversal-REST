using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servico.Controllers
{
    public class VeiculoController : ApiController
    {
        public IEnumerable<Models.Veiculo> Get()
        {
            Models.LojaVeiculoDataContext dc = new Models.LojaVeiculoDataContext();
            var veiculos = from f in dc.Veiculos select f;
            return veiculos.ToList();
        }

        // POST api/values
        public void Post([FromBody]Models.Veiculo veiculo)
        {
            Models.LojaVeiculoDataContext dc = new Models.LojaVeiculoDataContext();
            dc.Veiculos.InsertOnSubmit(new Models.Veiculo() {Modelo = veiculo.Modelo, Ano = veiculo.Ano, Preco=veiculo.Preco, IdFabricante=veiculo.IdFabricante, SituacaoVenda=veiculo.SituacaoVenda });
            dc.SubmitChanges();
        }

        public void Put(int id, [FromBody]Models.Veiculo veiculo)
        {
            Models.LojaVeiculoDataContext dc = new Models.LojaVeiculoDataContext();
            var ve = (from f in dc.Veiculos where f.Id == id select f).Single();
            ve.SituacaoVenda = veiculo.SituacaoVenda;
            dc.SubmitChanges();
        }
    }
}
