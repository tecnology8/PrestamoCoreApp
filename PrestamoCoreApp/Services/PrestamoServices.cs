using Microsoft.Extensions.Configuration;
using PrestamoCoreApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamoCoreApp.Services
{
    public class PrestamoServices
    {
        private readonly string _connectionString;
        public PrestamoServices(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("EmployeeDb");
        }
        public IEnumerable<PrestamoModel> GetPrestamos()
        {
            var lista = new List<PrestamoModel>();

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                conn.Open();
                var cmd = new SqlCommand("SP_Prestamos_Get", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var prestamo = new PrestamoModel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Tipo = reader["Tipo"].ToString(),
                        Tasa = reader["Tasa"].ToString()
                    };

                    lista.Add(prestamo);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }
        public void AddPrestamo(PrestamoModel model)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                conn.Open();
                var cmd = new SqlCommand("SP_Prestamo_Add", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Tipo", model.Tipo);
                cmd.Parameters.AddWithValue("@Tasa", model.Tasa);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdatePrestamo(PrestamoModel model)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                conn.Open();
                var cmd = new SqlCommand("SP_Prestamo_Update", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@Tipo", model.Tipo);
                cmd.Parameters.AddWithValue("@Tasa", model.Tasa);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PrestamoModel GetById(int? id)
        {
            try
            {
                var prestamo = new PrestamoModel();

                using SqlConnection conn = new SqlConnection(_connectionString);
                conn.Open();

                string query = $"{"SELECT * FROM Prestamos WHERE Id= " + id}";
                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    prestamo.Id = Convert.ToInt32(reader.GetInt32(reader.GetOrdinal("Id")));
                    prestamo.Tipo = reader.GetString(reader.GetOrdinal("Tipo"));
                    prestamo.Tasa = reader.GetString(reader.GetOrdinal("Tasa"));
                }
                return prestamo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeletePrestamo(int? id)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                conn.Open();
                var cmd = new SqlCommand("SP_Prestamo_Delete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }
    }
}