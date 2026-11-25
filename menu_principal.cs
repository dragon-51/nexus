
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp9
{
    public partial class menu_principal : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private SoundPlayer? musica;
        private SoundPlayer? sonidoHover;
        private SoundPlayer? sonidoClick;

        public menu_principal()
        {
            InitializeComponent();

            ConfigurarMenu();
            ConfigurarSonidos();
        }

        private void ConfigurarMenu()
        {
            // Título NEXUS
            label1.Text = "";
            label1.Font = new Font(
                "Consolas", 28, FontStyle.Bold);
            label1.ForeColor = Color.Cyan;


            BUTTON1.Text = "";
            button2.Text = "";
            button4.Text = "";
            button3.Text = "";


            Button[] botones =
            {
                BUTTON1,
                button2,
                button4,
                button3
            };

            foreach (Button boton in botones)
            {
                boton.BackColor =
                    Color.FromArgb(25, 15, 55);

                boton.ForeColor = Color.White;
                boton.FlatStyle = FlatStyle.Flat;

                boton.FlatAppearance.BorderColor =
                    Color.MediumPurple;

                boton.FlatAppearance.BorderSize = 2;

                boton.Font = new Font(
                    "Consolas", 11, FontStyle.Bold);

                boton.Cursor = Cursors.Hand;
                boton.UseVisualStyleBackColor = false;

                boton.MouseEnter += (s, e) =>
                {
                    boton.BackColor =
                        Color.FromArgb(50, 30, 100);

                    Reproducir(sonidoHover);
                };

                boton.MouseLeave += (s, e) =>
                {
                    boton.BackColor =
                        Color.FromArgb(25, 15, 55);
                };
            }


            button3.Click += (s, e) =>
            {
                Reproducir(sonidoClick);

                DialogResult respuesta = MessageBox.Show(
                    "¿Estás seguro de que quieres salir de NEXUS?",
                    "NEXUS",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    musica?.Stop();
                    Application.Exit();
                }
                else
                {
                    musica?.PlayLooping();
                }
            };

        }

        private void ConfigurarSonidos()
        {
            string carpeta = Path.Combine(
                AppContext.BaseDirectory,
                "Sounds");

 


            musica = CargarSonido(
                Path.Combine(carpeta, "menu_ambiente.wav"));

            sonidoHover = CargarSonido(
                Path.Combine(carpeta, "hover.wav"));

            sonidoClick = CargarSonido(
                Path.Combine(carpeta, "click.wav"));

            try
            {
                musica?.PlayLooping();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    ex.Message);
            }
        }

        private SoundPlayer? CargarSonido(string ruta)
        {
            if (!File.Exists(ruta))
                return null;

            try
            {
                SoundPlayer reproductor =
                    new SoundPlayer(ruta);

                reproductor.Load();

                return reproductor;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    ex.Message);

                return null;
            }
        }

        private void Reproducir(SoundPlayer? sonido)
        {
            try
            {
                sonido?.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    ex.Message);
            }
        }

        // NUEVA PARTIDA

        // NUEVA PARTIDA
        private void button1_Click(
     object sender, EventArgs e)
        {
            Reproducir(sonidoClick);

            using (InicioCadete inicio = new InicioCadete())
            {
                if (inicio.ShowDialog(this) != DialogResult.OK ||
                    inicio.CadeteCreado == null)
                {
                    return;
                }

                this.Hide();

                try
                {
                    using (juego2d juego = new juego2d(
                        inicio.CadeteCreado,
                        inicio.NombreMundo,
                        inicio.SemillaMundo))
                    {
                        juego.ShowDialog();
                    }
                }
                finally
                {
                    this.Show();
                }
            }
        }

        private void button2_Click(
            object sender, EventArgs e)
        {
            Reproducir(sonidoClick);

            this.Hide();

            try
            {
                using (cargarpartidas ventana = new cargarpartidas())
                {
                    ventana.ShowDialog();
                }
            }
            finally
            {
                this.Show();
            }
        }



        // AJUSTES
        private void button4_Click(
            object sender, EventArgs e)
        {
            Reproducir(sonidoClick);

            MessageBox.Show(
                "Los ajustes de sonido estarán disponibles próximamente.",
                "NEXUS");
        }

        private void label1_Click(
            object sender, EventArgs e)
        {
        }

        private void label1_Click_1(
            object sender, EventArgs e)
        {
        }

        private void Form1_Load(
            object sender, EventArgs e)
        {
        }

        protected override void OnFormClosed(
            FormClosedEventArgs e)
        {
            musica?.Stop();
            musica?.Dispose();

            sonidoHover?.Dispose();
            sonidoClick?.Dispose();

            base.OnFormClosed(e);
        }
    }
}
