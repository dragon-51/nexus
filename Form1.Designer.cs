namespace ConsoleApp9
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            BUTTON1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label1 = new Label();
            label2 = new Label();
            button4 = new Button();
            SuspendLayout();
            // 
            // BUTTON1
            // 
            BUTTON1.BackColor = SystemColors.ActiveCaptionText;
            BUTTON1.FlatAppearance.BorderColor = Color.DarkSlateBlue;
            BUTTON1.FlatAppearance.BorderSize = 3;
            BUTTON1.FlatStyle = FlatStyle.Flat;
            BUTTON1.ForeColor = SystemColors.ButtonHighlight;
            BUTTON1.Location = new Point(324, 200);
            BUTTON1.Name = "BUTTON1";
            BUTTON1.Size = new Size(310, 59);
            BUTTON1.TabIndex = 0;
            BUTTON1.Text = "INICIAR";
            BUTTON1.UseVisualStyleBackColor = false;
            BUTTON1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ActiveCaptionText;
            button2.FlatAppearance.BorderColor = Color.DarkSlateBlue;
            button2.FlatAppearance.BorderSize = 3;
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.Location = new Point(324, 285);
            button2.Margin = new Padding(5);
            button2.Name = "button2";
            button2.Size = new Size(310, 59);
            button2.TabIndex = 1;
            button2.Text = "CARGAR PARTIDA";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.ActiveCaptionText;
            button3.FlatAppearance.BorderColor = Color.DarkSlateBlue;
            button3.FlatAppearance.BorderSize = 3;
            button3.FlatStyle = FlatStyle.Flat;
            button3.ForeColor = SystemColors.ButtonHighlight;
            button3.Location = new Point(324, 454);
            button3.Name = "button3";
            button3.Size = new Size(310, 59);
            button3.TabIndex = 2;
            button3.Text = "SALIR";
            button3.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Bauhaus 93", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.HotTrack;
            label1.Location = new Point(355, 26);
            label1.Name = "label1";
            label1.Size = new Size(245, 91);
            label1.TabIndex = 3;
            label1.Text = "NEXUS";
            label1.Click += label1_Click_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Showcard Gothic", 24F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ActiveCaption;
            label2.Location = new Point(208, 130);
            label2.Name = "label2";
            label2.Size = new Size(591, 50);
            label2.TabIndex = 4;
            label2.Text = "bienvenido a nexus cadete!";
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ActiveCaptionText;
            button4.FlatAppearance.BorderColor = Color.DarkSlateBlue;
            button4.FlatAppearance.BorderSize = 3;
            button4.FlatStyle = FlatStyle.Flat;
            button4.ForeColor = SystemColors.ButtonHighlight;
            button4.Location = new Point(324, 364);
            button4.Name = "button4";
            button4.Size = new Size(310, 59);
            button4.TabIndex = 5;
            button4.Text = "AJUSTES";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Captura_de_pantalla_2026_11_09_114647;
            ClientSize = new Size(938, 644);
            Controls.Add(button4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(BUTTON1);
            Name = "Form1";
            Text = "MENUPRINCIPAL";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BUTTON1;
        private Button button2;
        private Button button3;
        private Label label1;
        private Label label2;
        private Button button4;
    }
}