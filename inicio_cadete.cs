using System;
using System.Drawing;
using System.Windows.Forms;
using NexusSimulation;

namespace ConsoleApp9
{
    public class InicioCadete : Form
    {
        private readonly TextBox txtNombre = new();
        private readonly TextBox txtMundo = new();

        private readonly NumericUpDown numEdad = new();
        private readonly NumericUpDown numEnergia = new();
        private readonly NumericUpDown numEstabilidad = new();

        public Cadete? CadeteCreado { get; private set; }
        public string NombreMundo { get; private set; } = "";
        public int SemillaMundo { get; private set; }

        public InicioCadete()
        {
            Text = "NEXUS - Registro de Cadete";
            ClientSize = new Size(440, 430);
            StartPosition = FormStartPosition.CenterParent;

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            BackColor = Color.FromArgb(22, 18, 40);
            ForeColor = Color.White;

            Font = new Font("Consolas", 10);

            txtNombre.Text = "Nexus";
            txtMundo.Text = "Mundo desconocido";

            numEdad.Minimum = 16;
            numEdad.Maximum = 100;
            numEdad.Value = 18;

            numEnergia.Minimum = 0;
            numEnergia.Maximum = 100;
            numEnergia.Value = 100;

            numEstabilidad.Minimum = 0;
            numEstabilidad.Maximum = 100;
            numEstabilidad.Value = 100;

            AgregarCampo("NOMBRE DEL CADETE", txtNombre, 20);
            AgregarCampo("EDAD", numEdad, 85);
            AgregarCampo("NOMBRE DEL MUNDO", txtMundo, 150);
            AgregarCampo("ENERGÍA INICIAL", numEnergia, 215);
            AgregarCampo("ESTABILIDAD INICIAL", numEstabilidad, 280);

            Button btnIniciar = new Button
            {
                Text = "INICIAR AVENTURA",
                Left = 25,
                Top = 355,
                Width = 390,
                Height = 45,
                BackColor = Color.MediumPurple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            btnIniciar.Click += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Ingresa el nombre del cadete.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtMundo.Text))
                {
                    MessageBox.Show("Ingresa el nombre del mundo.");
                    return;
                }

                if (numEnergia.Value < 50 ||
                    numEstabilidad.Value < 20)
                {
                    MessageBox.Show(
                        "ACCESO RECHAZADO:\n" +
                        "Necesitas al menos 50 de energía " +
                        "y 20 de estabilidad.",
                        "Sistema NEXUS");

                    return;
                }

                CadeteCreado = new Cadete(
                    txtNombre.Text.Trim(),
                    (int)numEdad.Value,
                    (int)numEnergia.Value,
                    (int)numEstabilidad.Value
                );

                NombreMundo = txtMundo.Text.Trim();
                SemillaMundo = Random.Shared.Next(
                    1, int.MaxValue);

                DialogResult = DialogResult.OK;
                Close();
            };

            Controls.Add(btnIniciar);
            AcceptButton = btnIniciar;
        }

        private void AgregarCampo(
            string etiqueta, Control control, int y)
        {
            Label titulo = new Label
            {
                Text = etiqueta,
                Left = 25,
                Top = y,
                Width = 390,
                Height = 22
            };

            control.Left = 25;
            control.Top = y + 25;
            control.Width = 390;
            control.Height = 28;

            Controls.Add(titulo);
            Controls.Add(control);
        }
    }
}