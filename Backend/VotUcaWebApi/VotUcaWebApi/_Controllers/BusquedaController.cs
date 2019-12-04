using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VotUcaWebApi
{
    [Route("api/[controller]")]
    public class BusquedaController : Controller
    {
        // GET: api/<controller>
        [HttpGet("{usuarioId}")]
        public string Busqueda(int usuarioId)
        {
            Usuario InformacionUsuario = new Usuario();
            DataTable _consulta = new DataTable();
            try
            {
                _consulta = DBConn.ConsultaSQL($"select * from Usuarios where idusuario = {usuarioId}");
                if (_consulta.Rows.Count > 0)
                {
                    InformacionUsuario.IdUsuario = Convert.ToInt32(_consulta.Rows[0].ItemArray[0]);
                    InformacionUsuario.DNI = _consulta.Rows[0].ItemArray[1].ToString();
                }
            }
            catch (Exception)
            {
                InformacionUsuario.IdUsuario = 0;
                InformacionUsuario.DNI = "";
            }
            var jsonConver = JsonConvert.SerializeObject(InformacionUsuario);
            return jsonConver.ToString();
        }



        /*
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
