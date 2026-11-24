namespace ConsoleApp9
{
    partial class menu_principal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(menu_principal));
            BUTTON1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // BUTTON1
            // 
            BUTTON1.BackColor = Color.Transparent;
            BUTTON1.BackgroundImage = (Image)resources.GetObject("BUTTON1.BackgroundImage");
            BUTTON1.BackgroundImageLayout = ImageLayout.Stretch;
            BUTTON1.FlatAppearance.BorderColor = Color.DarkSlateBlue;
            BUTTON1.FlatAppearance.BorderSize = 0;
            BUTTON1.FlatStyle = FlatStyle.Flat;
            BUTTON1.ForeColor = SystemColors.ButtonHighlight;
            BUTTON1.Location = new Point(575, 241);
            BUTTON1.Name = "BUTTON1";
            BUTTON1.Size = new Size(373, 122);
            BUTTON1.TabIndex = 0;
            BUTTON1.UseVisualStyleBackColor = false;
            BUTTON1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Transparent;
            button2.BackgroundImage = (Image)resources.GetObject("button2.BackgroundImage");
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button2.FlatAppearance.BorderColor = Color.DarkSlateBlue;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = Color.Transparent;
            button2.Location = new Point(575, 401);
            button2.Margin = new Padding(5);
            button2.Name = "button2";
            button2.Size = new Size(373, 111);
            button2.TabIndex = 1;
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.Transparent;
            button3.BackgroundImage = (Image)resources.GetObject("button3.BackgroundImage");
            button3.BackgroundImageLayout = ImageLayout.Stretch;
            button3.FlatAppearance.BorderColor = Color.DarkSlateBlue;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.ForeColor = SystemColors.ButtonHighlight;
            button3.Location = new Point(575, 663);
            button3.Name = "button3";
            button3.Size = new Size(373, 84);
            button3.TabIndex = 2;
            button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.BackColor = Color.Transparent;
            button4.BackgroundImage = (Image)resources.GetObject("button4.BackgroundImage");
            button4.BackgroundImageLayout = ImageLayout.Stretch;
            button4.FlatAppearance.BorderColor = Color.DarkSlateBlue;
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.ForeColor = SystemColors.ButtonHighlight;
            button4.Location = new Point(575, 544);
            button4.Name = "button4";
            button4.Size = new Size(373, 93);
            button4.TabIndex = 5;
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Bauhaus 93", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.HotTrack;
            label1.Image = (Image)resources.GetObject("label1.Image");
            label1.Location = new Point(285, -86);
            label1.Name = "label1";
            label1.Size = new Size(956, 324);
            label1.TabIndex = 3;
            label1.Visible = false;
            label1.Click += label1_Click_1;
            // 
            // menu_principal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1363, 827);
            Controls.Add(button4);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(BUTTON1);
            Name = "menu_principal";
            Text = "MENUPRINCIPAL";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button BUTTON1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Label label1;
    }
}