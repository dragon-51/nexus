
using NexusSimulation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp9
{

    public partial class juego2d : Form
    {
        private readonly Cadete cadete;
        private readonly string nombreMundo;
        private readonly int semillaMundo;

        private readonly centinela robotEnemigo =
            new centinela();
        private readonly List<Enemigo2D> enemigos = new();
        private float jugadorX = 100;
        private float jugadorY = 400;
        private float velocidadY = 0;
        private const int anchoMundo = 5000;
        private float camaraX = 0;

        private const int anchoJugador = 30;
        private const int altoJugador = 40;
        private const float gravedad = 0.6f;
        private const float velocidad = 5f;
        private const float fuerzaSalto = -12f;

        private bool izquierda = false;
        private bool derecha = false;
        private bool enSuelo = false;

        private readonly System.Windows.Forms.Timer reloj =
            new System.Windows.Forms.Timer();

        private readonly List<Rectangle> plataformas =
            new List<Rectangle>();
        private readonly List<Rectangle> decoraciones =
    new List<Rectangle>();
        private Image? spriteJugador;
        
        private Image? texturaEnemigos;

        private float enemigoX = 500;
        private float enemigoY = 510;

        private const int anchoEnemigo = 40;
        private const int altoEnemigo = 40;

        private float velocidadEnemigo = 2f;
        private bool enemigoDerecha = true;
        private int vidaEnemigo
        {
            get => robotEnemigo.Salud;
            set => robotEnemigo.Salud = value;
        }

        private int vidaJugador
        {
            get => cadete.Salud;
            set => cadete.Salud = value;
        }

        private int vidaMaxima => cadete.SaludMaxima;
        private int danoEnemigo = 10;
        private int tiempoInvulnerable = 0;
        private int tiempoDisparo = 0;
        private int recargaAtaque = 0;
        private readonly Button btnGuardar = new Button();
        private readonly Button btnMenu = new Button();
        private Image? fondoJuego;
        private Image? texturaPlataformas;
        private int fotograma = 0;
        private int contadorAnimacion = 0;
        private bool mirandoDerecha = true;

        public juego2d(Cadete nuevoCadete, string mundo, int semilla)
        {
            InitializeComponent();

            cadete = nuevoCadete;
            nombreMundo = mundo;
            semillaMundo = semilla;

            Text = "NEXUS - " + nombreMundo;

            string rutaEnemigos = Path.Combine(
    AppContext.BaseDirectory,
    "sprites",
    "enemigos",
    "oga-swm-objectsandenemies-sheet-alpha.png"
);

            if (File.Exists(rutaEnemigos))
            {
                texturaEnemigos = Image.FromFile(rutaEnemigos);
            }
            else
            {
                MessageBox.Show("No se encontró la textura de enemigos.");
            }

            string rutaPlataformas = Path.Combine(
                AppContext.BaseDirectory,
                "sprites",
                "plataformas",
                "oga-swm-tiles-alpha.png"
            );

            if (File.Exists(rutaPlataformas))
            {
                texturaPlataformas = Image.FromFile(rutaPlataformas);
            }


            string rutaFondo = Path.Combine(
                AppContext.BaseDirectory,
                "sprites",
                "fondos",
                "oga-swm-bg-blobby.png"
            );

            if (File.Exists(rutaFondo))
            {
                fondoJuego = Image.FromFile(rutaFondo);
            }
            else
            {
                MessageBox.Show("No se encontró el fondo.");
            }

            string rutaJugador = Path.Combine(
    AppContext.BaseDirectory,
    "sprites",
    "personaje",
    "oga-swm-mainchar-sheet-alpha.png"
);

            if (File.Exists(rutaJugador))
            {
                spriteJugador = Image.FromFile(rutaJugador);
            }
            else
            {
                MessageBox.Show("No se encontró el sprite del personaje.");
            }

            ClientSize = new Size(1000, 600);
            Text = "NEXUS - Mundo 2D";
            BackColor = Color.FromArgb(15, 10, 30);
            DoubleBuffered = true;
            KeyPreview = true;
            this.Shown += (s, e) =>
            {
                this.Activate();
                this.Focus();
            };


            // Terreno con diferentes alturas
            plataformas.Add(new Rectangle(0, 550, 800, 50));
            plataformas.Add(new Rectangle(800, 500, 400, 100));
            plataformas.Add(new Rectangle(1200, 450, 400, 150));
            plataformas.Add(new Rectangle(1600, 550, 700, 50));
            plataformas.Add(new Rectangle(2300, 480, 500, 120));
            plataformas.Add(new Rectangle(2800, 400, 400, 200));
            plataformas.Add(new Rectangle(3200, 500, 600, 100));
            plataformas.Add(new Rectangle(3800, 550, 1200, 50));

            plataformas.Add(new Rectangle(150, 440, 180, 20));
            plataformas.Add(new Rectangle(400, 350, 170, 20));
            plataformas.Add(new Rectangle(680, 270, 180, 20));

            plataformas.Add(new Rectangle(1050, 440, 200, 20));
            plataformas.Add(new Rectangle(1300, 350, 180, 20));
            plataformas.Add(new Rectangle(1550, 270, 220, 20));
            plataformas.Add(new Rectangle(1900, 450, 250, 20));
            plataformas.Add(new Rectangle(2300, 360, 200, 20));
            plataformas.Add(new Rectangle(2600, 280, 180, 20));
            plataformas.Add(new Rectangle(3000, 440, 300, 20));
            plataformas.Add(new Rectangle(3500, 350, 220, 20));
            plataformas.Add(new Rectangle(3900, 270, 250, 20));
            plataformas.Add(new Rectangle(4400, 440, 300, 20));
           
            decoraciones.Add(new Rectangle(100, 250, 64, 300));
            decoraciones.Add(new Rectangle(450, 150, 64, 400));
            decoraciones.Add(new Rectangle(850, 200, 64, 350));
            decoraciones.Add(new Rectangle(1400, 100, 64, 450));
            decoraciones.Add(new Rectangle(1900, 250, 64, 300));
            decoraciones.Add(new Rectangle(2500, 150, 64, 400));


            KeyDown += JuegoForm_KeyDown;
            KeyUp += JuegoForm_KeyUp;

            reloj.Interval = 16;
            reloj.Tick += ActualizarJuego;
            GenerarEnemigos();
            ConfigurarBotonesJuego();
            reloj.Start();
        }

        private void JuegoForm_KeyDown(object? sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(
            "Tecla presionada: " + e.KeyCode);
            if (e.KeyCode == Keys.A) izquierda = true;
            if (e.KeyCode == Keys.D) derecha = true;
            if (e.KeyCode == Keys.J)
            {
                Atacar();
            }
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                if (enSuelo)
                {
                    velocidadY = fuerzaSalto;
                    enSuelo = false;
                }
            }

        }
        private void GenerarEnemigos()
        {
            enemigos.Clear();

            var superficies = plataformas.FindAll(p =>
                p.Width >= 160 && p.Left >= 300);

            if (superficies.Count == 0)
                return;

            int cantidad = Random.Shared.Next(8, 16);

            for (int i = 0; i < cantidad; i++)
            {
                Rectangle plataforma = superficies[
                    Random.Shared.Next(superficies.Count)];

                int minX = plataforma.Left;
                int maxX = plataforma.Right - anchoEnemigo;

                int x = Random.Shared.Next(minX, maxX + 1);
                int y = plataforma.Top - altoEnemigo;

                Personaje tipo;

                if (Random.Shared.Next(0, 2) == 0)
                    tipo = new centinela();
                else
                    tipo = new Enemigo();

                enemigos.Add(new Enemigo2D(
                    tipo,
                    x,
                    y,
                    minX,
                    maxX,
                    Random.Shared.Next(1, 4)));
            }
        }
        private void ConfigurarBotonesJuego()
        {
            btnGuardar.Text = "GUARDAR PARTIDA";
            btnGuardar.Size = new Size(160, 35);
            btnGuardar.Location = new Point(
                ClientSize.Width - 180, 15);

            btnGuardar.Anchor =
                AnchorStyles.Top | AnchorStyles.Right;

            btnGuardar.BackColor = Color.MediumPurple;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.TabStop = false;

            btnGuardar.Click += (s, e) =>
            {
                reloj.Stop();

                izquierda = false;
                derecha = false;

                try
                {
                    PartidaSQL.GuardarPartida(
                        cadete,
                        nombreMundo,
                        semillaMundo,
                        jugadorX,
                        jugadorY
                    );

                    MessageBox.Show(
                        "¡Partida guardada correctamente!",
                        "NEXUS",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error al guardar: " + ex.Message,
                        "NEXUS");
                }
                finally
                {
                    reloj.Start();
                    Focus();
                }
            };

            btnMenu.Text = "VOLVER AL MENÚ";
            btnMenu.Size = new Size(160, 35);
            btnMenu.Location = new Point(
                ClientSize.Width - 180, 60);

            btnMenu.Anchor =
                AnchorStyles.Top | AnchorStyles.Right;

            btnMenu.BackColor = Color.MediumPurple;
            btnMenu.ForeColor = Color.White;
            btnMenu.FlatStyle = FlatStyle.Flat;
            btnMenu.TabStop = false;

            btnMenu.Click += (s, e) =>
            {
                reloj.Stop();
                Close();
            };

            Controls.Add(btnGuardar);
            Controls.Add(btnMenu);

            btnGuardar.BringToFront();
            btnMenu.BringToFront();
        }
        private void Atacar()
        {
            if (recargaAtaque > 0 || cadete.Energia < 5)
                return;

            tiempoDisparo = 8;
            recargaAtaque = 25;
            cadete.Energia -= 5;

            foreach (Enemigo2D enemigo in enemigos)
            {
                if (enemigo.Datos.Salud <= 0)
                    continue;

                float distanciaX = enemigo.X - jugadorX;
                float distanciaY = Math.Abs(jugadorY - enemigo.Y);

                bool delante = mirandoDerecha
                    ? distanciaX >= 0
                    : distanciaX <= 0;

                if (delante &&
                    Math.Abs(distanciaX) <= 100 &&
                    distanciaY <= 30)
                {
                    int dano = Random.Shared.Next(15, 30);

                    enemigo.Datos.Salud = Math.Max(
                        0, enemigo.Datos.Salud - dano);

                    if (enemigo.Datos.Salud == 0)
                    {
                        cadete.Experiencia += 50;
                        cadete.Inventario.Add("Célula de energía");
                    }

                    break;
                }
            }
        }

        private void JuegoForm_KeyUp(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) izquierda = false;
            if (e.KeyCode == Keys.D) derecha = false;
        }

        private void ActualizarJuego(object? sender, EventArgs e)
        {

            float anteriorX = jugadorX;

            if (izquierda) jugadorX -= velocidad;
            if (derecha) jugadorX += velocidad;

            
            foreach (Rectangle plataforma in plataformas)
            {
                bool colision =
                    jugadorX + anchoJugador > plataforma.Left &&
                    jugadorX < plataforma.Right &&
                    jugadorY + altoJugador > plataforma.Top &&
                    jugadorY < plataforma.Bottom;

                if (colision)
                {
                    jugadorX = anteriorX;
                    break;
                }
            }

            if (derecha)
                mirandoDerecha = true;

            if (izquierda)
                mirandoDerecha = false;

            if (izquierda || derecha)
            {
                contadorAnimacion++;

                if (contadorAnimacion >= 8)
                {
                    fotograma = (fotograma + 1) % 8;
                    contadorAnimacion = 0;
                }
            }
            else
            {
                fotograma = 0;
                contadorAnimacion = 0;
            }

            jugadorX = Math.Clamp(
    jugadorX, 0, anchoMundo - anchoJugador);

            float anteriorY = jugadorY;

            velocidadY += gravedad;
            jugadorY += velocidadY;
            enSuelo = false;

            foreach (Rectangle plataforma in plataformas)
            {
                bool dentroHorizontal =
                    jugadorX + anchoJugador > plataforma.Left &&
                    jugadorX < plataforma.Right;

                bool caeSobrePlataforma =
                    velocidadY >= 0 &&
                    anteriorY + altoJugador <= plataforma.Top &&
                    jugadorY + altoJugador >= plataforma.Top;

                if (dentroHorizontal && caeSobrePlataforma)
                {
                    jugadorY = plataforma.Top - altoJugador;
                    velocidadY = 0;
                    enSuelo = true;
                    break;
                }
               
                if (jugadorY > 700)
                {
                    jugadorX = 100;
                    jugadorY = 400;
                    velocidadY = 0;
                    enSuelo = false;
                }

                camaraX = jugadorX - ClientSize.Width / 2f;

                camaraX = Math.Clamp(
                    camaraX, 0, Math.Max(0, anchoMundo - ClientSize.Width)
                );
            }
            foreach (Enemigo2D enemigo in enemigos)
            {
                enemigo.Actualizar();
            }

            Rectangle cuerpoJugador = new Rectangle(
                (int)jugadorX,
                (int)jugadorY,
                anchoJugador,
                altoJugador
            );

           

            if (tiempoInvulnerable > 0)
            {
                tiempoInvulnerable--;
            }
            foreach (Enemigo2D enemigo in enemigos)
            {
                if (enemigo.Datos.Salud <= 0)
                    continue;

                Rectangle cuerpoEnemigo = new Rectangle(
                    (int)enemigo.X,
                    (int)enemigo.Y,
                    anchoEnemigo,
                    altoEnemigo);

                if (cuerpoJugador.IntersectsWith(cuerpoEnemigo)
                    && tiempoInvulnerable == 0)
                {
                    vidaJugador = Math.Max(0, vidaJugador - danoEnemigo);

                    cadete.Estabilidad = Math.Max(
                        0, cadete.Estabilidad - 8);

                    tiempoInvulnerable = 60;

                    break;
                }
            }


            if (vidaJugador <= 0)
            {
                reloj.Stop();
                MessageBox.Show("Has muerto", "NEXUS");

                vidaJugador = vidaMaxima;
                jugadorX = 100;
                jugadorY = 400;
                velocidadY = 0;
                izquierda = false;
                derecha = false;
                tiempoInvulnerable = 60;

                reloj.Start();
            }
            if (tiempoDisparo > 0)
                tiempoDisparo--;

            if (recargaAtaque > 0)
                recargaAtaque--;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.Clear(Color.FromArgb(15, 10, 30));


            if (fondoJuego != null)
            {
                e.Graphics.InterpolationMode =
                    System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                e.Graphics.PixelOffsetMode =
                    System.Drawing.Drawing2D.PixelOffsetMode.Half;

                int anchoFondo = Math.Max(1, ClientSize.Width);
                int altoFondo = ClientSize.Height;

                int desplazamiento =
                    (int)(camaraX * 0.25f) % anchoFondo;

                for (int x = -desplazamiento;
                     x < ClientSize.Width;
                     x += anchoFondo)
                {
                    e.Graphics.DrawImage(
                        fondoJuego,
                        new Rectangle(
                            x, 0, anchoFondo, altoFondo)
                    );
                }
            }


            e.Graphics.TranslateTransform(-camaraX, 0);
            using Brush suelo =
                new SolidBrush(Color.FromArgb(55, 40, 85));

            using Brush borde =
                new SolidBrush(Color.MediumPurple);
            if (texturaPlataformas != null)
            {
                Rectangle origenTuberia =
                    new Rectangle(248, 0, 16, 16);

                foreach (Rectangle decoracion in decoraciones)
                {
                    for (int y = 0; y < decoracion.Height; y += 32)
                    {
                        Rectangle destinoTuberia = new Rectangle(
                            decoracion.X,
                            decoracion.Y + y,
                            decoracion.Width,
                            Math.Min(32, decoracion.Height - y)
                        );

                        e.Graphics.DrawImage(
                            texturaPlataformas,
                            destinoTuberia,
                            origenTuberia,
                            GraphicsUnit.Pixel
                        );
                    }
                }
            }



            foreach (Rectangle plataforma in plataformas)
            {
                if (texturaPlataformas != null)
                {
                    e.Graphics.InterpolationMode =
                        System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                    for (int y = 0; y < plataforma.Height; y += 32)
                    {
                        for (int x = 0; x < plataforma.Width; x += 32)
                        {
                            int ancho = Math.Min(32, plataforma.Width - x);
                            int alto = Math.Min(32, plataforma.Height - y);

                            Rectangle origenBloque = new Rectangle(
                                264, 0, ancho, alto);

                            Rectangle destinoBloque = new Rectangle(
                                plataforma.X + x,
                                plataforma.Y + y,
                                ancho,
                                alto
                            );

                            e.Graphics.DrawImage(
                                texturaPlataformas,
                                destinoBloque,
                                origenBloque,
                                GraphicsUnit.Pixel
                            );
                        }
                    }
                }
                else
                {
                    e.Graphics.FillRectangle(suelo, plataforma);
                }
            }
            if (texturaEnemigos != null && robotEnemigo.Salud > 0)
            {
                if (texturaEnemigos != null)
                {
                    e.Graphics.InterpolationMode =
                        System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                    Rectangle origenEnemigo =
                        new Rectangle(1, 31, 16, 14);

                    foreach (Enemigo2D enemigo in enemigos)
                    {
                        if (enemigo.Datos.Salud <= 0)
                            continue;

                        Rectangle destinoEnemigo = new Rectangle(
                            (int)enemigo.X,
                            (int)enemigo.Y,
                            anchoEnemigo,
                            altoEnemigo);

                        e.Graphics.DrawImage(
                            texturaEnemigos,
                            destinoEnemigo,
                            origenEnemigo,
                            GraphicsUnit.Pixel);

                        int relleno = (int)(40f *
                            enemigo.Datos.Salud /
                            enemigo.Datos.SaludMaxima);

                        e.Graphics.FillRectangle(
                            Brushes.Black,
                            enemigo.X, enemigo.Y - 12, 40, 6);

                        e.Graphics.FillRectangle(
                            Brushes.Red,
                            enemigo.X, enemigo.Y - 12, relleno, 6);

                        e.Graphics.DrawRectangle(
                            Pens.White,
                            enemigo.X, enemigo.Y - 12, 40, 6);
                    }
                }
            }


            if (spriteJugador != null)
            {
                e.Graphics.InterpolationMode =
                    System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                e.Graphics.PixelOffsetMode =
                    System.Drawing.Drawing2D.PixelOffsetMode.Half;


                Rectangle origen = new Rectangle(
                    1 + fotograma * 36,
                    53,
                    34,
                    24
                );





                Rectangle destino = new Rectangle(
                    (int)jugadorX,
                    (int)jugadorY,
                    anchoJugador,
                    altoJugador
                );


                var estado = e.Graphics.Save();

                if (!mirandoDerecha)
                {
                    e.Graphics.TranslateTransform(
                        destino.X + destino.Width,
                        destino.Y);

                    e.Graphics.ScaleTransform(-1, 1);

                    destino = new Rectangle(
                        0, 0, destino.Width, destino.Height);
                }

                e.Graphics.DrawImage(
                    spriteJugador,
                    destino,
                    origen,
                    GraphicsUnit.Pixel
                );

                e.Graphics.Restore(estado);

            }
            if (tiempoDisparo > 0)
            {
                float inicioX = jugadorX + anchoJugador / 2f;
                float inicioY = jugadorY + altoJugador / 2f;

                float finalX = inicioX +
                    (mirandoDerecha ? 100 : -100);

                using Pen brillo = new Pen(Color.DeepSkyBlue, 7);
                using Pen laser = new Pen(Color.White, 2);

                e.Graphics.DrawLine(brillo,
                    inicioX, inicioY, finalX, inicioY);

                e.Graphics.DrawLine(laser,
                    inicioX, inicioY, finalX, inicioY);
            }

            e.Graphics.ResetTransform();

            DibujarBarra(e.Graphics, "SALUD",
                cadete.Salud, cadete.SaludMaxima, 15, Color.Red);

            DibujarBarra(e.Graphics, "ENERGIA",
                cadete.Energia, 100, 60, Color.DodgerBlue);

            DibujarBarra(e.Graphics, "ESTABILIDAD",
                cadete.Estabilidad, 100, 105, Color.LimeGreen);

            e.Graphics.DrawString(
                "XP: " + cadete.Experiencia,
                Font, Brushes.Gold, 20, 155);
        }

        private void DibujarBarra(Graphics g, string nombre,
            int valor, int maximo, int y, Color color)
        {
            int x = 20;
            int ancho = 240;
            int segmentos = 20;

            using Font fuente = new Font(
                "Consolas", 10, FontStyle.Bold);

            using Brush fondo = new SolidBrush(
                Color.FromArgb(25, 30, 45));

            using Brush relleno = new SolidBrush(color);
            using Pen borde = new Pen(color, 1);

            g.DrawString(nombre, fuente,
                Brushes.White, x, y);

            g.DrawString(valor + "/" + maximo,
                fuente, relleno, x + 175, y);

            g.FillRectangle(fondo,
                x, y + 20, ancho, 20);

            float porcentaje = Math.Clamp(
                (float)valor / maximo, 0f, 1f);

            int activos = (int)Math.Ceiling(
                porcentaje * segmentos);

            for (int i = 0; i < segmentos; i++)
            {
                g.FillRectangle(
                    i < activos ? relleno : fondo,
                    x + 2 + i * 12,
                    y + 23, 9, 14);
            }

            g.DrawRectangle(borde,
                x, y + 20, ancho, 20);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            reloj.Stop();
            reloj.Dispose();
            base.OnFormClosed(e);
        }
    }

}
