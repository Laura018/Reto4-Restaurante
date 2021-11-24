using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Reto_4_Restaurante.Models;

namespace Reto_4_Restaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ServiciosController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        //OBTENER
         [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select id_servicios, titulo, descripcion, imagen_servicios
                        from 
                        servicios
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

        //ELIMINACION
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                        delete from servicios 
                        where id_servicios=@id;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        //ACTUALIZAR
        [HttpPut("{id}")]
        public JsonResult Put(int id, Servicios servicios)
        {

            string query = @"
                        update servicios set 
                        titulo =@tituloServicios,
                        descripcion =@descripcionServicios,  
                        imagen_servicios =@ImagenServicios
                        where id_servicios =@id;
                        
            ";

            if (id == servicios.id_servicios)
            {
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
                MySqlDataReader myReader;
                using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
                {
                    mycon.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                    {
                        myCommand.Parameters.AddWithValue("@id", servicios.id_servicios);
                        myCommand.Parameters.AddWithValue("@tituloServicios", servicios.titulo);
                        myCommand.Parameters.AddWithValue("@descripcionServicios", servicios.descripcion);
                        myCommand.Parameters.AddWithValue("@ImagenServicios", servicios.imagen_servicios);


                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                    }
                }
            }

            return new JsonResult("Updated Successfully");
        }

        //CREACIÓN
        [HttpPost]
        public JsonResult Post(Models.Servicios servicios)
        {
            string query = @"
                        insert into servicios 
                        (titulo,descripcion,imagen_servicios) 
                        values
                         (@tituloServicios,@descripcionServicios,@ImagenServicios) ;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@tituloServicios", servicios.titulo);
                    myCommand.Parameters.AddWithValue("@descripcionServicios", servicios.descripcion);
                    myCommand.Parameters.AddWithValue("@ImagenServicios", servicios.imagen_servicios);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }



    }
}
