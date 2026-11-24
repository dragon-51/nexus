using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using NexusSimulation;

namespace ConsoleApp9
{
    public class PartidaSQL
    {
        private const string cadenaConexion =
            @"Server=.\SQLEXPRESS;Database=NEXUS;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }

        public static void ProbarConexion()
        {
            using SqlConnection conexion = ObtenerConexion();
            conexion.Open();
        }

        public static void GuardarPartida(
            Cadete cadete,
            string mundo,
            int semilla,
            float x,
            float y)
        {
            using SqlConnection conexion = ObtenerConexion();

            conexion.Open();

            string consulta = @"
                INSERT INTO dbo.Partidas
                (NombreJugador, Edad, Salud, SaludMaxima,
                 Energia, Estabilidad, Experiencia,
                 PosicionX, PosicionY, Inventario,
                 NombreMapa, Semilla, FechaGuardado)
                VALUES
                (@Nombre, @Edad, @Salud, @SaludMaxima,
                 @Energia, @Estabilidad, @Experiencia,
                 @X, @Y, @Inventario,
                 @Mundo, @Semilla, GETDATE())";

            using SqlCommand comando =
                new SqlCommand(consulta, conexion);

            comando.Parameters.AddWithValue(
                "@Nombre", cadete.Nombre);

            comando.Parameters.AddWithValue(
                "@Edad", cadete.Edad);

            comando.Parameters.AddWithValue(
                "@Salud", cadete.Salud);

            comando.Parameters.AddWithValue(
                "@SaludMaxima", cadete.SaludMaxima);

            comando.Parameters.AddWithValue(
                "@Energia", cadete.Energia);

            comando.Parameters.AddWithValue(
                "@Estabilidad", cadete.Estabilidad);

            comando.Parameters.AddWithValue(
                "@Experiencia", cadete.Experiencia);

            comando.Parameters.AddWithValue("@X", (int)x);
            comando.Parameters.AddWithValue("@Y", (int)y);

            comando.Parameters.AddWithValue(
                "@Inventario",
                JsonSerializer.Serialize(cadete.Inventario));

            comando.Parameters.AddWithValue("@Mundo", mundo);
            comando.Parameters.AddWithValue("@Semilla", semilla);

            comando.ExecuteNonQuery();
        }

        public static DataTable ObtenerPartidas()

        {
            public static DataTable CargarPartida(int id)
        {
            using SqlConnection conexion = ObtenerConexion();

            conexion.Open();

            string consulta = @"
        SELECT * FROM dbo.Partidas
        WHERE Id = @Id";

            using SqlCommand comando =
                new SqlCommand(consulta, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            using SqlDataAdapter adaptador =
                new SqlDataAdapter(comando);

            DataTable tabla = new DataTable();

            adaptador.Fill(tabla);

            return tabla;
        }
            using SqlConnection conexion = ObtenerConexion();

            conexion.Open();

            string consulta = @"
                SELECT Id, NombreJugador, Edad,
                       NombreMapa, Salud, Energia,
                       Estabilidad, Experiencia,
                       FechaGuardado
                FROM dbo.Partidas
                ORDER BY FechaGuardado DESC";

            using SqlDataAdapter adaptador =
                new SqlDataAdapter(consulta, conexion);

            DataTable tabla = new DataTable();

            adaptador.Fill(tabla);

            return tabla;
        }
    }
}