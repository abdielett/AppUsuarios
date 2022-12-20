
namespace WinAppUsuarios
{
    partial class Usuarios
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
            this.label1 = new System.Windows.Forms.Label();
            this.DataGridUsuarios = new System.Windows.Forms.DataGridView();
            this.btnAceptar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Usuarios";
            // 
            // DataGridUsuarios
            // 
            this.DataGridUsuarios.AllowUserToAddRows = false;
            this.DataGridUsuarios.BackgroundColor = System.Drawing.Color.Azure;
            this.DataGridUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridUsuarios.Location = new System.Drawing.Point(12, 66);
            this.DataGridUsuarios.Name = "DataGridUsuarios";
            this.DataGridUsuarios.RowHeadersVisible = false;
            this.DataGridUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridUsuarios.Size = new System.Drawing.Size(770, 315);
            this.DataGridUsuarios.TabIndex = 2;
            this.DataGridUsuarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridUsuarios_CellClick);
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.Lime;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.Black;
            this.btnAceptar.Location = new System.Drawing.Point(682, 27);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(100, 33);
            this.btnAceptar.TabIndex = 14;
            this.btnAceptar.Text = "Nuevo";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // Usuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(800, 394);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.DataGridUsuarios);
            this.Controls.Add(this.label1);
            this.Name = "Usuarios";
            this.Text = "Usuarios";
            this.Load += new System.EventHandler(this.Usuarios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DataGridUsuarios;
        private System.Windows.Forms.Button btnAceptar;
    }
}

