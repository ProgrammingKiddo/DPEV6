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
    public class InsercionController : Controller
    {
        // GET: api/<controller>
        [HttpGet("{usuDNI}")]
        public void Insercion(String usuDNI)
        {
            DataTable _consulta = new DataTable();
            try
            {
                _consulta = DBConn.ConsultaSQL("insert into Usuarios (DNI) values ('"+usuDNI+"')");
            }
            catch (Exception)
            {
            }
        }
    }
}