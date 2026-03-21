using System.ComponentModel;

namespace MxPdv.Views
{
    partial class FrmVendas
    {
        private IContainer components = null;

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
            this.pnlTopo = new System.Windows.Forms.Panel();
            this.pnlInputs = new System.Windows.Forms.Panel();
            this.labelQuantidade = new System.Windows.Forms.Label();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.labelX = new System.Windows.Forms.Label();
            this.labelProduto = new System.Windows.Forms.Label();
            this.cbxProduto = new System.Windows.Forms.ComboBox();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.lblAtalhos = new System.Windows.Forms.Label();
            this.pnlDireita = new System.Windows.Forms.Panel();
            this.dgvCarrinho = new System.Windows.Forms.DataGridView();
            this.lblTituloGrid = new System.Windows.Forms.Label();
            this.pnlTotais = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.labelTotalTexto = new System.Windows.Forms.Label();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.pnlEsquerda = new System.Windows.Forms.Panel();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.pnlTopo.SuspendLayout();
            this.pnlInputs.SuspendLayout();
            this.pnlDireita.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrinho)).BeginInit();
            this.pnlTotais.SuspendLayout();
            this.pnlEsquerda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTopo
            // 
            this.pnlTopo.Controls.Add(this.pnlInputs);
            this.pnlTopo.Controls.Add(this.lblAtalhos);
            this.pnlTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopo.Location = new System.Drawing.Point(0, 0);
            this.pnlTopo.Name = "pnlTopo";
            this.pnlTopo.Size = new System.Drawing.Size(1008, 100);
            this.pnlTopo.TabIndex = 0;
            // 
            // pnlInputs
            // 
            this.pnlInputs.BackColor = System.Drawing.Color.DimGray;
            this.pnlInputs.Controls.Add(this.labelQuantidade);
            this.pnlInputs.Controls.Add(this.txtQuantidade);
            this.pnlInputs.Controls.Add(this.labelX);
            this.pnlInputs.Controls.Add(this.labelProduto);
            this.pnlInputs.Controls.Add(this.cbxProduto);
            this.pnlInputs.Controls.Add(this.btnAdicionar);
            this.pnlInputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInputs.Location = new System.Drawing.Point(0, 30);
            this.pnlInputs.Name = "pnlInputs";
            this.pnlInputs.Size = new System.Drawing.Size(1008, 70);
            this.pnlInputs.TabIndex = 1;
            // 
            // labelQuantidade
            // 
            this.labelQuantidade.AutoSize = true;
            this.labelQuantidade.ForeColor = System.Drawing.Color.White;
            this.labelQuantidade.Location = new System.Drawing.Point(12, 10);
            this.labelQuantidade.Name = "labelQuantidade";
            this.labelQuantidade.Size = new System.Drawing.Size(78, 13);
            this.labelQuantidade.TabIndex = 6;
            this.labelQuantidade.Text = "QUANTIDADE";
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtQuantidade.Location = new System.Drawing.Point(15, 26);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(80, 32);
            this.txtQuantidade.TabIndex = 1;
            this.txtQuantidade.Text = "1";
            this.txtQuantidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelX.ForeColor = System.Drawing.Color.White;
            this.labelX.Location = new System.Drawing.Point(101, 29);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(24, 25);
            this.labelX.TabIndex = 7;
            this.labelX.Text = "X";
            // 
            // labelProduto
            // 
            this.labelProduto.AutoSize = true;
            this.labelProduto.ForeColor = System.Drawing.Color.White;
            this.labelProduto.Location = new System.Drawing.Point(131, 10);
            this.labelProduto.Name = "labelProduto";
            this.labelProduto.Size = new System.Drawing.Size(61, 13);
            this.labelProduto.TabIndex = 8;
            this.labelProduto.Text = "PRODUTO";
            // 
            // cbxProduto
            // 
            this.cbxProduto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProduto.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.cbxProduto.FormattingEnabled = true;
            this.cbxProduto.Location = new System.Drawing.Point(131, 25);
            this.cbxProduto.Name = "cbxProduto";
            this.cbxProduto.Size = new System.Drawing.Size(400, 33);
            this.cbxProduto.TabIndex = 2;
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnAdicionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAdicionar.ForeColor = System.Drawing.Color.White;
            this.btnAdicionar.Location = new System.Drawing.Point(540, 24);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(120, 35);
            this.btnAdicionar.TabIndex = 3;
            this.btnAdicionar.Text = "Lançar";
            this.btnAdicionar.UseVisualStyleBackColor = false;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // lblAtalhos
            // 
            this.lblAtalhos.BackColor = System.Drawing.Color.White;
            this.lblAtalhos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAtalhos.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAtalhos.Location = new System.Drawing.Point(0, 0);
            this.lblAtalhos.Name = "lblAtalhos";
            this.lblAtalhos.Size = new System.Drawing.Size(1008, 30);
            this.lblAtalhos.TabIndex = 0;
            this.lblAtalhos.Text = "F3 Finalizar Venda  |  F5 Cancelar Venda  |  F8 Consultar Produto";
            this.lblAtalhos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlDireita
            // 
            this.pnlDireita.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlDireita.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDireita.Controls.Add(this.dgvCarrinho);
            this.pnlDireita.Controls.Add(this.lblTituloGrid);
            this.pnlDireita.Controls.Add(this.pnlTotais);
            this.pnlDireita.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDireita.Location = new System.Drawing.Point(608, 100);
            this.pnlDireita.Name = "pnlDireita";
            this.pnlDireita.Size = new System.Drawing.Size(400, 629);
            this.pnlDireita.TabIndex = 1;
            // 
            // dgvCarrinho
            // 
            this.dgvCarrinho.AllowUserToAddRows = false;
            this.dgvCarrinho.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCarrinho.BackgroundColor = System.Drawing.Color.White;
            this.dgvCarrinho.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCarrinho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCarrinho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCarrinho.Location = new System.Drawing.Point(0, 30);
            this.dgvCarrinho.Name = "dgvCarrinho";
            this.dgvCarrinho.ReadOnly = true;
            this.dgvCarrinho.RowTemplate.Height = 35;
            this.dgvCarrinho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCarrinho.Size = new System.Drawing.Size(398, 497);
            this.dgvCarrinho.TabIndex = 1;
            // 
            // lblTituloGrid
            // 
            this.lblTituloGrid.BackColor = System.Drawing.Color.DimGray;
            this.lblTituloGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloGrid.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloGrid.ForeColor = System.Drawing.Color.White;
            this.lblTituloGrid.Location = new System.Drawing.Point(0, 0);
            this.lblTituloGrid.Name = "lblTituloGrid";
            this.lblTituloGrid.Size = new System.Drawing.Size(398, 30);
            this.lblTituloGrid.TabIndex = 0;
            this.lblTituloGrid.Text = "LISTA DE PRODUTOS (ITENS)";
            this.lblTituloGrid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTotais
            // 
            this.pnlTotais.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlTotais.Controls.Add(this.lblTotal);
            this.pnlTotais.Controls.Add(this.labelTotalTexto);
            this.pnlTotais.Controls.Add(this.btnFinalizar);
            this.pnlTotais.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTotais.Location = new System.Drawing.Point(0, 527);
            this.pnlTotais.Name = "pnlTotais";
            this.pnlTotais.Size = new System.Drawing.Size(398, 100);
            this.pnlTotais.TabIndex = 2;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.Navy;
            this.lblTotal.Location = new System.Drawing.Point(10, 35);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(200, 45);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "R$ 0,00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTotalTexto
            // 
            this.labelTotalTexto.AutoSize = true;
            this.labelTotalTexto.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelTotalTexto.Location = new System.Drawing.Point(10, 10);
            this.labelTotalTexto.Name = "labelTotalTexto";
            this.labelTotalTexto.Size = new System.Drawing.Size(93, 21);
            this.labelTotalTexto.TabIndex = 2;
            this.labelTotalTexto.Text = "SUB-TOTAL";
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinalizar.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnFinalizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalizar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnFinalizar.ForeColor = System.Drawing.Color.White;
            this.btnFinalizar.Location = new System.Drawing.Point(218, 20);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(170, 60);
            this.btnFinalizar.TabIndex = 0;
            this.btnFinalizar.Text = "[F3] FINALIZAR";
            this.btnFinalizar.UseVisualStyleBackColor = false;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // pnlEsquerda
            // 
            this.pnlEsquerda.BackColor = System.Drawing.Color.White;
            this.pnlEsquerda.Controls.Add(this.pbLogo);
            this.pnlEsquerda.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEsquerda.Location = new System.Drawing.Point(0, 100);
            this.pnlEsquerda.Name = "pnlEsquerda";
            this.pnlEsquerda.Size = new System.Drawing.Size(608, 629);
            this.pnlEsquerda.TabIndex = 2;
            // 
            // pbLogo
            // 
            this.pbLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbLogo.Location = new System.Drawing.Point(0, 0);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(608, 629);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // FrmVendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.pnlEsquerda);
            this.Controls.Add(this.pnlDireita);
            this.Controls.Add(this.pnlTopo);
            this.Name = "FrmVendas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ponto de Venda (PDV)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlTopo.ResumeLayout(false);
            this.pnlInputs.ResumeLayout(false);
            this.pnlInputs.PerformLayout();
            this.pnlDireita.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrinho)).EndInit();
            this.pnlTotais.ResumeLayout(false);
            this.pnlTotais.PerformLayout();
            this.pnlEsquerda.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTopo;
        private System.Windows.Forms.Panel pnlInputs;
        private System.Windows.Forms.Label lblAtalhos;
        private System.Windows.Forms.Label labelQuantidade;
        private System.Windows.Forms.TextBox txtQuantidade;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelProduto;
        private System.Windows.Forms.ComboBox cbxProduto;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Panel pnlDireita;
        private System.Windows.Forms.Label lblTituloGrid;
        private System.Windows.Forms.DataGridView dgvCarrinho;
        private System.Windows.Forms.Panel pnlTotais;
        private System.Windows.Forms.Label labelTotalTexto;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnFinalizar;
        private System.Windows.Forms.Panel pnlEsquerda;
        private System.Windows.Forms.PictureBox pbLogo;
    }
}