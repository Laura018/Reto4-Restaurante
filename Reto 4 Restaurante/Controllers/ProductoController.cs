using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Reto_4_Restaurante.Models;

namespace Reto_4_Restaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ProductoController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        //CONSULTA
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select id_producto,nombre,descripcion,precio,imagen_producto
                        from 
                        producto
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
        [HttpDelete("{id_producto}")]
        public JsonResult Delete(int id_producto)
        {
            string query = @"
                        delete from producto 
                        where id_producto=@ProductoId_producto;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ProductoId_producto", id_producto);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }


        //ACTUALIZACIÓN


        [HttpPut]
        public JsonResult Put(Producto pro)
        {
            string query = @"
                        update producto set 
                        nombre =@ProductoNombre,
                        descripcion =@ProductoDescripcion,
                        precio =@ProductoPrecio,
                        imagen_producto =@ProductoImagen_producto
                        where id_producto =@ProductoId_producto;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ProductoId_producto", pro.id_producto);
                    myCommand.Parameters.AddWithValue("@ProductoNombre", pro.nombre);
                    myCommand.Parameters.AddWithValue("@ProductoDescripcion", pro.descripcion);
                    myCommand.Parameters.AddWithValue("@ProductoPrecio", pro.precio);
                    myCommand.Parameters.AddWithValue("@ProductoImagen_producto", pro.imagen_producto);


                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        //CREACIÓN

        [HttpPost]
        public JsonResult Post(Models.Producto pro)
        {
            string query = @"
                        insert into producto 
                        (nombre,descripcion,precio,imagen_producto) 
                        values
                         (@ProductoNombre,@ProductoDescripcion,@ProductoPrecio,@ProductoImagen_producto);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TestAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ProductoNombre", pro.nombre);
                    myCommand.Parameters.AddWithValue("@ProductoDescripcion", pro.descripcion);
                    myCommand.Parameters.AddWithValue("@ProductoPrecio", pro.precio);
                    myCommand.Parameters.AddWithValue("@ProductoImagen_producto", pro.imagen_producto);

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

