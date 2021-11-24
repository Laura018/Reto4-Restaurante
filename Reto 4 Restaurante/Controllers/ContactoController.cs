using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Reto_4_Restaurante.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Reto_4_Restaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ContactoController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //CONSULTA

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select id_contacto,nombre,email,telefono,asunto,mensaje
                        from 
                        contacto
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }



        //ACTUALIZACION
        [HttpPut]
        public JsonResult Put(Contacto emp)
        {
            if (emp is null)
            {
                throw new ArgumentNullException(nameof(emp));
            }

            string query = @"
                        update contacto set 
                        nombre =@nombre,
                        email =@email,
                        telefono =@telefono ,
                        asunto =@asunto,
                        mensaje =@mensaje
                         where id_contacto =@ContactoId;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ContactoId", emp.id_contacto);
                    myCommand.Parameters.AddWithValue("@nombre", emp.nombre);
                    myCommand.Parameters.AddWithValue("@email", emp.email);
                    myCommand.Parameters.AddWithValue("@telefono", emp.telefono);
                    myCommand.Parameters.AddWithValue("@asunto", emp.asunto);
                    myCommand.Parameters.AddWithValue("@mensaje", emp.mensaje);


                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }
        //AGREGAR
        [HttpPost]
        public JsonResult Post(Models.Contacto emp)
        {
            string query = @"
                        insert into contacto 
                        (nombre,email,telefono,asunto,mensaje) 
                        values
                         (@ContactoNombre,@ContactoEmail,@ContactoTelefono,@ContactoAsunto,@ContactoMensaje);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {

                    myCommand.Parameters.AddWithValue("@ContactoNombre", emp.nombre);
                    myCommand.Parameters.AddWithValue("@ContactoEmail", emp.email);
                    myCommand.Parameters.AddWithValue("@ContactoTelefono", emp.telefono);
                    myCommand.Parameters.AddWithValue("@ContactoAsunto", emp.asunto);
                    myCommand.Parameters.AddWithValue("@ContactoMensaje", emp.mensaje);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }
        //ELIMINAR
        [HttpDelete("{id_contacto}")]
        public JsonResult Delete(int id_contacto)
        {
            string query = @"
                        delete from contacto 
                        where id_contacto=@ContactoId;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ContactoId", id_contacto);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

    }
}
