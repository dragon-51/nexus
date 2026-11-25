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
            float y,
            List<Enemigo2D>? enemigos = null,
            bool misionCompletada = false)


        {
            using SqlConnection conexion = ObtenerConexion();
            conexion.Open();

            using SqlTransaction transaccion =
                conexion.BeginTransaction();

            try
            {
                string consulta = @"
            INSERT INTO dbo.Partidas
            (NombreJugador, Edad, Salud, SaludMaxima,
             Energia, Estabilidad, Experiencia,
             PosicionX, PosicionY, Inventario,
             NombreMapa, Semilla, FechaGuardado)
            OUTPUT INSERTED.Id
            VALUES
            (@Nombre, @Edad, @Salud, @SaludMaxima,
             @Energia, @Estabilidad, @Experiencia,
             @X, @Y, @Inventario,
             @Mundo, @Semilla, GETDATE())";

                using SqlCommand comando =
                    new SqlCommand(consulta, conexion, transaccion);

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

                int idPartida = Convert.ToInt32(
                    comando.ExecuteScalar());

                // Agrupar los objetos del inventario
                Dictionary<string, int> objetos = new();

                foreach (string objeto in cadete.Inventario)
                {
                    if (objetos.ContainsKey(objeto))
                        objetos[objeto]++;
                    else
                        objetos[objeto] = 1;
                }

                // Guardar inventario relacionado con la partida
                foreach (var objeto in objetos)
                {
                    string sqlInventario = @"
                INSERT INTO dbo.Inventario
                (IdPartida, NombreObjeto, Cantidad)
                VALUES
                (@IdPartida, @NombreObjeto, @Cantidad)";

                    using SqlCommand cmdInventario =
                        new SqlCommand(
                            sqlInventario, conexion, transaccion);

                    cmdInventario.Parameters.AddWithValue(
                        "@IdPartida", idPartida);

                    cmdInventario.Parameters.AddWithValue(
                        "@NombreObjeto", objeto.Key);

                    cmdInventario.Parameters.AddWithValue(
                        "@Cantidad", objeto.Value);

                    cmdInventario.ExecuteNonQuery();
                }

                // Guardar enemigos relacionados con la partida
                if (enemigos != null)
                {
                    foreach (Enemigo2D enemigo in enemigos)
                    {
                        string sqlEnemigos = @"
            INSERT INTO dbo.Enemigos
            (IdPartida, Nombre, Salud, PosicionX, PosicionY)
            VALUES
            (@IdPartida, @Nombre, @Salud, @X, @Y)";

                        using SqlCommand cmdEnemigo =
                            new SqlCommand(
                                sqlEnemigos, conexion, transaccion);

                        cmdEnemigo.Parameters.AddWithValue(
                            "@IdPartida", idPartida);

                        cmdEnemigo.Parameters.AddWithValue(
                            "@Nombre", enemigo.Datos.Nombre);

                        cmdEnemigo.Parameters.AddWithValue(
                            "@Salud", enemigo.Datos.Salud);

                        cmdEnemigo.Parameters.AddWithValue(
                            "@X", enemigo.X);

                        cmdEnemigo.Parameters.AddWithValue(
                            "@Y", enemigo.Y);

                        cmdEnemigo.ExecuteNonQuery();
                    }
                }

                // Guardar el estado de la misión
                string sqlMision = @"
    INSERT INTO dbo.Misiones
    (IdPartida, NombreMision, Estado)
    VALUES
    (@IdPartida, @NombreMision, @Estado)";

                using SqlCommand cmdMision =
                    new SqlCommand(sqlMision, conexion, transaccion);

                cmdMision.Parameters.AddWithValue(
                    "@IdPartida", idPartida);

                cmdMision.Parameters.AddWithValue(
                    "@NombreMision", "La anomalía perdida");

                cmdMision.Parameters.AddWithValue(
                    "@Estado",
                    misionCompletada ? "Completada" : "En progreso");

                cmdMision.ExecuteNonQuery();

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }

        public static DataTable ObtenerPartidas()
        {
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

        public static bool CargarMision(int idPartida)
        {
            using SqlConnection conexion = ObtenerConexion();
            conexion.Open();

            string consulta = @"
        SELECT Estado
        FROM dbo.Misiones
        WHERE IdPartida = @IdPartida
        AND NombreMision = @NombreMision";

            using SqlCommand comando =
                new SqlCommand(consulta, conexion);

            comando.Parameters.AddWithValue(
                "@IdPartida", idPartida);

            comando.Parameters.AddWithValue(
                "@NombreMision", "La anomalía perdida");

            object? resultado = comando.ExecuteScalar();

            return resultado?.ToString() == "Completada";
        }

        public static DataTable CargarEnemigos(int idPartida)
        {

            using SqlConnection conexion = ObtenerConexion();

            conexion.Open();

            string consulta = @"
        SELECT Nombre, Salud, PosicionX, PosicionY
        FROM dbo.Enemigos
        WHERE IdPartida = @IdPartida
        ORDER BY IdEnemigo";

            using SqlCommand comando =
                new SqlCommand(consulta, conexion);

            comando.Parameters.AddWithValue(
                "@IdPartida", idPartida);

            using SqlDataAdapter adaptador =
                new SqlDataAdapter(comando);

            DataTable tabla = new DataTable();

            adaptador.Fill(tabla);

            return tabla;
        }

    }
}
       
