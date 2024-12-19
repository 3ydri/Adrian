namespace Customer
{
    partial class MILK_TEA
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
            this.btnFruitTea = new System.Windows.Forms.Button();
            this.btnMilkTea = new System.Windows.Forms.Button();
            this.btnSpecialDrinks = new System.Windows.Forms.Button();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
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
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(214, 147);
            this.pictureBox1.TabIndex = 103;
            this.pictureBox1.TabStop = false;
            // 
            // btnFruitTea
            // 
            this.btnFruitTea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnFruitTea.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFruitTea.Location = new System.Drawing.Point(958, 71);
            this.btnFruitTea.Name = "btnFruitTea";
            this.btnFruitTea.Size = new System.Drawing.Size(253, 85);
            this.btnFruitTea.TabIndex = 101;
            this.btnFruitTea.Text = "FRUIT TEA";
            this.btnFruitTea.UseVisualStyleBackColor = false;
            this.btnFruitTea.Click += new System.EventHandler(this.btnFruitTea_Click);
            // 
            // btnMilkTea
            // 
            this.btnMilkTea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnMilkTea.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMilkTea.Location = new System.Drawing.Point(632, 71);
            this.btnMilkTea.Name = "btnMilkTea";
            this.btnMilkTea.Size = new System.Drawing.Size(253, 85);
            this.btnMilkTea.TabIndex = 100;
            this.btnMilkTea.Text = "MILK TEA";
            this.btnMilkTea.UseVisualStyleBackColor = false;
            // 
            // btnSpecialDrinks
            // 
            this.btnSpecialDrinks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnSpecialDrinks.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpecialDrinks.Location = new System.Drawing.Point(286, 71);
            this.btnSpecialDrinks.Name = "btnSpecialDrinks";
            this.btnSpecialDrinks.Size = new System.Drawing.Size(253, 85);
            this.btnSpecialDrinks.TabIndex = 99;
            this.btnSpecialDrinks.Text = "SPECIAL DRINKS";
            this.btnSpecialDrinks.UseVisualStyleBackColor = false;
            this.btnSpecialDrinks.Click += new System.EventHandler(this.btnSpecialDrinks_Click);
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flowLayoutPanel.Location = new System.Drawing.Point(157, 182);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(1127, 558);
            this.flowLayoutPanel.TabIndex = 102;
            // 
            // pbCart
            // 
            this.pbCart.BackColor = System.Drawing.Color.Transparent;
            this.pbCart.BackgroundImage = global::Customer.Properties.Resources.cart_removebg_preview;
            this.pbCart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCart.Location = new System.Drawing.Point(1233, 71);
            this.pbCart.Name = "pbCart";
            this.pbCart.Size = new System.Drawing.Size(107, 85);
            this.pbCart.TabIndex = 104;
            this.pbCart.TabStop = false;
            this.pbCart.Click += new System.EventHandler(this.pbCart_Click);
            // 
            // MILK_TEA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Customer.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1378, 746);
            this.Controls.Add(this.pbCart);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.btnFruitTea);
            this.Controls.Add(this.btnMilkTea);
            this.Controls.Add(this.btnSpecialDrinks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MILK_TEA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MILK_TEA";
            this.Load += new System.EventHandler(this.MILK_TEA_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnFruitTea;
        private System.Windows.Forms.Button btnMilkTea;
        private System.Windows.Forms.Button btnSpecialDrinks;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.PictureBox pbCart;
    }
}