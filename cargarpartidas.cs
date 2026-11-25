
using System;
using NexusSimulation;
using System.Collections.Generic;
using System.Text.Json;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ConsoleApp9
{
    public partial class cargarpartidas : Form
    {
        private DataGridView tablaPartidas = new DataGridView();

        public cargarpartidas()
        {
            InitializeComponent();

            ConfigurarTabla();
            MostrarPartidas();
        }

        private void ConfigurarTabla()
        {
            Text = "NEXUS - Partidas guardadas";
            Size = new Size(1000, 500);
            BackColor = Color.FromArgb(15, 10, 30);

            tablaPartidas.Dock = DockStyle.Fill;
            tablaPartidas.ReadOnly = true;
            tablaPartidas.AllowUserToAddRows = false;
            tablaPartidas.MultiSelect = false;

            tablaPartidas.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            tablaPartidas.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            Controls.Add(tablaPartidas);
            tablaPartidas.BringToFront();
            tablaPartidas.CellDoubleClick += TablaPartidas_CellDoubleClick;
        }

        private void MostrarPartidas()
        {
            try
            {
                tablaPartidas.DataSource =
                    PartidaSQL.ObtenerPartidas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar partidas: " + ex.Message,
                    "NEXUS");
            }
        }
        private void TablaPartidas_CellDoubleClick(
            object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                int id = Convert.ToInt32(
                    tablaPartidas.Rows[e.RowIndex].Cells["Id"].Value);

                DataTable datos = PartidaSQL.CargarPartida(id);

                if (datos.Rows.Count == 0)
                    return;

                DataRow fila = datos.Rows[0];

                Cadete cadete = new Cadete(
                    fila["NombreJugador"].ToString()!,
                    Convert.ToInt32(fila["Edad"]),
                    Convert.ToInt32(fila["Energia"]),
                    Convert.ToInt32(fila["Estabilidad"]));

                cadete.Salud = Convert.ToInt32(fila["Salud"]);
                cadete.Experiencia = Convert.ToInt32(fila["Experiencia"]);

                var inventarioGuardado =
                    JsonSerializer.Deserialize<List<string>>(
                        fila["Inventario"].ToString() ?? "[]");

                if (inventarioGuardado != null)
                {
                    cadete.Inventario.Clear();
                    cadete.Inventario.AddRange(inventarioGuardado);
                }


                string mundo = fila["NombreMapa"].ToString()!;
                int semilla = Convert.ToInt32(fila["Semilla"]);

                float x = Convert.ToSingle(fila["PosicionX"]);
                float y = Convert.ToSingle(fila["PosicionY"]);

                this.Hide();

                try
                {

                    using (juego2d juego = new juego2d(
                        cadete, mundo, semilla, x, y, id))

                    {
                        juego.ShowDialog();
                    }
                }
                finally
                {
                    MostrarPartidas();
                    this.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al recuperar partida: " + ex.Message,
                    "NEXUS");
            }
        }

    }
}
