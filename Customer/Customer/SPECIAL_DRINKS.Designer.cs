namespace Customer
{
    partial class SPECIAL_DRINKS
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnFruitTea = new System.Windows.Forms.Button();
            this.btnMilkTea = new System.Windows.Forms.Button();
            this.btnSpecialDrinks = new System.Windows.Forms.Button();
            this.pbCart = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCart)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.BackgroundImage = global::Customer.Properties.Resources._1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(21, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(174, 123);
            this.pictureBox1.TabIndex = 98;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(170, 180);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1107, 554);
            this.flowLayoutPanel1.TabIndex = 97;
            // 
            // btnFruitTea
            // 
            this.btnFruitTea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnFruitTea.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFruitTea.Location = new System.Drawing.Point(958, 71);
            this.btnFruitTea.Name = "btnFruitTea";
            this.btnFruitTea.Size = new System.Drawing.Size(253, 85);
            this.btnFruitTea.TabIndex = 96;
            this.btnFruitTea.Text = "FRUIT TEA";
            this.btnFruitTea.UseVisualStyleBackColor = false;
            this.btnFruitTea.Click += new System.EventHandler(this.btnFruitTea_Click);
            // 
            // btnMilkTea
            // 
            this.btnMilkTea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnMilkTea.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMilkTea.Location = new System.Drawing.Point(632, 71);
            this.btnMilkTea.Name = "btnMilkTea";
            this.btnMilkTea.Size = new System.Drawing.Size(253, 85);
            this.btnMilkTea.TabIndex = 95;
            this.btnMilkTea.Text = "MILK TEA";
            this.btnMilkTea.UseVisualStyleBackColor = false;
            this.btnMilkTea.Click += new System.EventHandler(this.btnMilkTea_Click);
            // 
            // btnSpecialDrinks
            // 
            this.btnSpecialDrinks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSpecialDrinks.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpecialDrinks.Location = new System.Drawing.Point(286, 71);
            this.btnSpecialDrinks.Name = "btnSpecialDrinks";
            this.btnSpecialDrinks.Size = new System.Drawing.Size(253, 85);
            this.btnSpecialDrinks.TabIndex = 94;
            this.btnSpecialDrinks.Text = "SPECIAL DRINKS";
            this.btnSpecialDrinks.UseVisualStyleBackColor = false;
            // 
            // pbCart
            // 
            this.pbCart.BackColor = System.Drawing.Color.Transparent;
            this.pbCart.BackgroundImage = global::Customer.Properties.Resources.cart_removebg_preview;
            this.pbCart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCart.Location = new System.Drawing.Point(1237, 71);
            this.pbCart.Name = "pbCart";
            this.pbCart.Size = new System.Drawing.Size(107, 85);
            this.pbCart.TabIndex = 99;
            this.pbCart.TabStop = false;
            this.pbCart.Click += new System.EventHandler(this.pbCart_Click);
            // 
            // SPECIAL_DRINKS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Customer.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1378, 746);
            this.Controls.Add(this.pbCart);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btnFruitTea);
            this.Controls.Add(this.btnMilkTea);
            this.Controls.Add(this.btnSpecialDrinks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SPECIAL_DRINKS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Start_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnFruitTea;
        private System.Windows.Forms.Button btnMilkTea;
        private System.Windows.Forms.Button btnSpecialDrinks;
        private System.Windows.Forms.PictureBox pbCart;
    }
}