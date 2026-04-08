namespace CapaPresentacion
{
    partial class frmMovimiento
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelCliente = new System.Windows.Forms.Label();
            this.labelMotivo = new System.Windows.Forms.Label();
            this.cmbTipoMovimiento = new System.Windows.Forms.ComboBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.pnlUsuario = new System.Windows.Forms.Panel();
            this.lblUsernameUsuario = new System.Windows.Forms.Label();
            this.lblRolUsuario = new System.Windows.Forms.Label();
            this.lblNombreUsuario = new System.Windows.Forms.Label();
            this.cmbProveedor = new System.Windows.Forms.ComboBox();
            this.cmbCliente = new System.Windows.Forms.ComboBox();
            this.cmbMotivo = new System.Windows.Forms.ComboBox();
            this.lblNumMovimiento = new System.Windows.Forms.Label();
            this.cmbProducto = new System.Windows.Forms.ComboBox();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.txtCantidad = new System.Windows.Forms.NumericUpDown();
            this.btnAgregarProducto = new System.Windows.Forms.Button();
            this.btnEliminarProducto = new System.Windows.Forms.Button();
            this.dgvMovimientos = new System.Windows.Forms.DataGridView();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.pnlProductos = new System.Windows.Forms.Panel();
            this.pnlUsuario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).BeginInit();
            this.pnlEncabezado.SuspendLayout();
            this.pnlProductos.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo_Movimiento:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(450, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fecha:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(765, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Usuario:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 69);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Proveedor:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(450, 69);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "No.:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 23);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Producto:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(360, 23);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Categoría:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(615, 23);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 20);
            this.label8.TabIndex = 3;
            this.label8.Text = "Cantidad:";
            // 
            // labelCliente
            // 
            this.labelCliente.AutoSize = true;
            this.labelCliente.Location = new System.Drawing.Point(30, 69);
            this.labelCliente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCliente.Name = "labelCliente";
            this.labelCliente.Size = new System.Drawing.Size(62, 20);
            this.labelCliente.TabIndex = 6;
            this.labelCliente.Text = "Cliente:";
            this.labelCliente.Visible = false;
            // 
            // labelMotivo
            // 
            this.labelMotivo.AutoSize = true;
            this.labelMotivo.Location = new System.Drawing.Point(450, 69);
            this.labelMotivo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMotivo.Name = "labelMotivo";
            this.labelMotivo.Size = new System.Drawing.Size(59, 20);
            this.labelMotivo.TabIndex = 8;
            this.labelMotivo.Text = "Motivo:";
            this.labelMotivo.Visible = false;
            // 
            // cmbTipoMovimiento
            // 
            this.cmbTipoMovimiento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoMovimiento.FormattingEnabled = true;
            this.cmbTipoMovimiento.Items.AddRange(new object[] {
            "ENTRADA",
            "SALIDA"});
            this.cmbTipoMovimiento.Location = new System.Drawing.Point(195, 18);
            this.cmbTipoMovimiento.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbTipoMovimiento.Name = "cmbTipoMovimiento";
            this.cmbTipoMovimiento.Size = new System.Drawing.Size(223, 28);
            this.cmbTipoMovimiento.TabIndex = 1;
            this.cmbTipoMovimiento.SelectedIndexChanged += new System.EventHandler(this.cmbTipoMovimiento_SelectedIndexChanged);
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(518, 18);
            this.dtpFecha.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(223, 26);
            this.dtpFecha.TabIndex = 3;
            // 
            // pnlUsuario
            // 
            this.pnlUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUsuario.Controls.Add(this.lblUsernameUsuario);
            this.pnlUsuario.Controls.Add(this.lblRolUsuario);
            this.pnlUsuario.Controls.Add(this.lblNombreUsuario);
            this.pnlUsuario.Location = new System.Drawing.Point(840, 12);
            this.pnlUsuario.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlUsuario.Name = "pnlUsuario";
            this.pnlUsuario.Size = new System.Drawing.Size(299, 91);
            this.pnlUsuario.TabIndex = 5;
            // 
            // lblUsernameUsuario
            // 
            this.lblUsernameUsuario.AutoSize = true;
            this.lblUsernameUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lblUsernameUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblUsernameUsuario.Location = new System.Drawing.Point(12, 40);
            this.lblUsernameUsuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsernameUsuario.Name = "lblUsernameUsuario";
            this.lblUsernameUsuario.Size = new System.Drawing.Size(0, 20);
            this.lblUsernameUsuario.TabIndex = 1;
            // 
            // lblRolUsuario
            // 
            this.lblRolUsuario.AutoSize = true;
            this.lblRolUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblRolUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblRolUsuario.ForeColor = System.Drawing.Color.White;
            this.lblRolUsuario.Location = new System.Drawing.Point(12, 65);
            this.lblRolUsuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRolUsuario.Name = "lblRolUsuario";
            this.lblRolUsuario.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.lblRolUsuario.Size = new System.Drawing.Size(12, 24);
            this.lblRolUsuario.TabIndex = 2;
            // 
            // lblNombreUsuario
            // 
            this.lblNombreUsuario.AutoSize = true;
            this.lblNombreUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblNombreUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblNombreUsuario.Location = new System.Drawing.Point(12, 9);
            this.lblNombreUsuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombreUsuario.Name = "lblNombreUsuario";
            this.lblNombreUsuario.Size = new System.Drawing.Size(0, 25);
            this.lblNombreUsuario.TabIndex = 0;
            // 
            // cmbProveedor
            // 
            this.cmbProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProveedor.FormattingEnabled = true;
            this.cmbProveedor.Location = new System.Drawing.Point(195, 65);
            this.cmbProveedor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbProveedor.Name = "cmbProveedor";
            this.cmbProveedor.Size = new System.Drawing.Size(223, 28);
            this.cmbProveedor.TabIndex = 7;
            this.cmbProveedor.SelectedIndexChanged += new System.EventHandler(this.cmbProveedor_SelectedIndexChanged);
            // 
            // cmbCliente
            // 
            this.cmbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCliente.FormattingEnabled = true;
            this.cmbCliente.Location = new System.Drawing.Point(195, 65);
            this.cmbCliente.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbCliente.Name = "cmbCliente";
            this.cmbCliente.Size = new System.Drawing.Size(223, 28);
            this.cmbCliente.TabIndex = 7;
            this.cmbCliente.Visible = false;
            // 
            // cmbMotivo
            // 
            this.cmbMotivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMotivo.FormattingEnabled = true;
            this.cmbMotivo.Items.AddRange(new object[] {
            "Venta",
            "Daño",
            "Ajuste",
            "Uso Interno"});
            this.cmbMotivo.Location = new System.Drawing.Point(518, 65);
            this.cmbMotivo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbMotivo.Name = "cmbMotivo";
            this.cmbMotivo.Size = new System.Drawing.Size(223, 28);
            this.cmbMotivo.TabIndex = 8;
            this.cmbMotivo.Visible = false;
            // 
            // lblNumMovimiento
            // 
            this.lblNumMovimiento.AutoSize = true;
            this.lblNumMovimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblNumMovimiento.ForeColor = System.Drawing.Color.Blue;
            this.lblNumMovimiento.Location = new System.Drawing.Point(518, 69);
            this.lblNumMovimiento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNumMovimiento.Name = "lblNumMovimiento";
            this.lblNumMovimiento.Size = new System.Drawing.Size(36, 25);
            this.lblNumMovimiento.TabIndex = 9;
            this.lblNumMovimiento.Text = "---";
            // 
            // cmbProducto
            // 
            this.cmbProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProducto.FormattingEnabled = true;
            this.cmbProducto.Location = new System.Drawing.Point(120, 18);
            this.cmbProducto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbProducto.Name = "cmbProducto";
            this.cmbProducto.Size = new System.Drawing.Size(223, 28);
            this.cmbProducto.TabIndex = 1;
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Location = new System.Drawing.Point(465, 18);
            this.cmbCategoria.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(133, 28);
            this.cmbCategoria.TabIndex = 2;
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(705, 18);
            this.txtCantidad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCantidad.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(120, 26);
            this.txtCantidad.TabIndex = 4;
            // 
            // btnAgregarProducto
            // 
            this.btnAgregarProducto.BackColor = System.Drawing.Color.LimeGreen;
            this.btnAgregarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.btnAgregarProducto.ForeColor = System.Drawing.Color.White;
            this.btnAgregarProducto.Location = new System.Drawing.Point(874, 18);
            this.btnAgregarProducto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAgregarProducto.Name = "btnAgregarProducto";
            this.btnAgregarProducto.Size = new System.Drawing.Size(60, 38);
            this.btnAgregarProducto.TabIndex = 5;
            this.btnAgregarProducto.Text = "✓";
            this.btnAgregarProducto.UseVisualStyleBackColor = false;
            this.btnAgregarProducto.Click += new System.EventHandler(this.btnAgregarProducto_Click);
            // 
            // btnEliminarProducto
            // 
            this.btnEliminarProducto.BackColor = System.Drawing.Color.Red;
            this.btnEliminarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.btnEliminarProducto.ForeColor = System.Drawing.Color.White;
            this.btnEliminarProducto.Location = new System.Drawing.Point(942, 18);
            this.btnEliminarProducto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEliminarProducto.Name = "btnEliminarProducto";
            this.btnEliminarProducto.Size = new System.Drawing.Size(60, 38);
            this.btnEliminarProducto.TabIndex = 6;
            this.btnEliminarProducto.Text = "✕";
            this.btnEliminarProducto.UseVisualStyleBackColor = false;
            this.btnEliminarProducto.Click += new System.EventHandler(this.btnEliminarProducto_Click);
            // 
            // dgvMovimientos
            // 
            this.dgvMovimientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMovimientos.Location = new System.Drawing.Point(30, 77);
            this.dgvMovimientos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvMovimientos.Name = "dgvMovimientos";
            this.dgvMovimientos.RowHeadersWidth = 62;
            this.dgvMovimientos.Size = new System.Drawing.Size(750, 415);
            this.dgvMovimientos.TabIndex = 6;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(795, 92);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 62);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Store";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(795, 169);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 62);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(795, 246);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(120, 62);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Exit";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // pnlEncabezado
            // 
            this.pnlEncabezado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEncabezado.Controls.Add(this.label1);
            this.pnlEncabezado.Controls.Add(this.cmbTipoMovimiento);
            this.pnlEncabezado.Controls.Add(this.label2);
            this.pnlEncabezado.Controls.Add(this.dtpFecha);
            this.pnlEncabezado.Controls.Add(this.label3);
            this.pnlEncabezado.Controls.Add(this.pnlUsuario);
            this.pnlEncabezado.Controls.Add(this.label4);
            this.pnlEncabezado.Controls.Add(this.cmbProveedor);
            this.pnlEncabezado.Controls.Add(this.labelCliente);
            this.pnlEncabezado.Controls.Add(this.cmbCliente);
            this.pnlEncabezado.Controls.Add(this.labelMotivo);
            this.pnlEncabezado.Controls.Add(this.cmbMotivo);
            this.pnlEncabezado.Controls.Add(this.label5);
            this.pnlEncabezado.Controls.Add(this.lblNumMovimiento);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.pnlEncabezado.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Padding = new System.Windows.Forms.Padding(15, 15, 15, 15);
            this.pnlEncabezado.Size = new System.Drawing.Size(1176, 184);
            this.pnlEncabezado.TabIndex = 0;
            // 
            // pnlProductos
            // 
            this.pnlProductos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProductos.Controls.Add(this.label6);
            this.pnlProductos.Controls.Add(this.cmbProducto);
            this.pnlProductos.Controls.Add(this.label7);
            this.pnlProductos.Controls.Add(this.cmbCategoria);
            this.pnlProductos.Controls.Add(this.label8);
            this.pnlProductos.Controls.Add(this.txtCantidad);
            this.pnlProductos.Controls.Add(this.btnAgregarProducto);
            this.pnlProductos.Controls.Add(this.btnEliminarProducto);
            this.pnlProductos.Controls.Add(this.dgvMovimientos);
            this.pnlProductos.Controls.Add(this.btnGuardar);
            this.pnlProductos.Controls.Add(this.btnCancelar);
            this.pnlProductos.Controls.Add(this.btnSalir);
            this.pnlProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProductos.Location = new System.Drawing.Point(0, 184);
            this.pnlProductos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlProductos.Name = "pnlProductos";
            this.pnlProductos.Padding = new System.Windows.Forms.Padding(15, 15, 15, 15);
            this.pnlProductos.Size = new System.Drawing.Size(1176, 525);
            this.pnlProductos.TabIndex = 1;
            // 
            // frmMovimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 709);
            this.Controls.Add(this.pnlProductos);
            this.Controls.Add(this.pnlEncabezado);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmMovimiento";
            this.Text = "Form Movimiento";
            this.Load += new System.EventHandler(this.frmMovimiento_Load);
            this.pnlUsuario.ResumeLayout(false);
            this.pnlUsuario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).EndInit();
            this.pnlEncabezado.ResumeLayout(false);
            this.pnlEncabezado.PerformLayout();
            this.pnlProductos.ResumeLayout(false);
            this.pnlProductos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTipoMovimiento;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlUsuario;
        private System.Windows.Forms.Label lblNombreUsuario;
        private System.Windows.Forms.Label lblUsernameUsuario;
        private System.Windows.Forms.Label lblRolUsuario;
        private System.Windows.Forms.ComboBox cmbProveedor;
        private System.Windows.Forms.ComboBox cmbCliente;
        private System.Windows.Forms.ComboBox cmbMotivo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelCliente;
        private System.Windows.Forms.Label labelMotivo;
        private System.Windows.Forms.Label lblNumMovimiento;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbProducto;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown txtCantidad;
        private System.Windows.Forms.Button btnAgregarProducto;
        private System.Windows.Forms.Button btnEliminarProducto;
        private System.Windows.Forms.DataGridView dgvMovimientos;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Panel pnlEncabezado;
        private System.Windows.Forms.Panel pnlProductos;
    }
}
