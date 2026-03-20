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
            this.labelProduto = new System.Windows.Forms.Label();
            this.cbxProduto = new System.Windows.Forms.ComboBox();
            this.labelQuantidade = new System.Windows.Forms.Label();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.dgvCarrinho = new System.Windows.Forms.DataGridView();
            this.labelTotalTexto = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnFinalizar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrinho)).BeginInit();
            this.SuspendLayout();
            // 
            // labelProduto
            // 
            this.labelProduto.AutoSize = true;
            this.labelProduto.Location = new System.Drawing.Point(20, 23);
            this.labelProduto.Name = "labelProduto";
            this.labelProduto.Size = new System.Drawing.Size(47, 13);
            this.labelProduto.TabIndex = 8;
            this.labelProduto.Text = "Produto:";
            // 
            // cbxProduto
            // 
            this.cbxProduto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProduto.FormattingEnabled = true;
            this.cbxProduto.Location = new System.Drawing.Point(73, 20);
            this.cbxProduto.Name = "cbxProduto";
            this.cbxProduto.Size = new System.Drawing.Size(300, 21);
            this.cbxProduto.TabIndex = 7;
            // 
            // labelQuantidade
            // 
            this.labelQuantidade.AutoSize = true;
            this.labelQuantidade.Location = new System.Drawing.Point(390, 23);
            this.labelQuantidade.Name = "labelQuantidade";
            this.labelQuantidade.Size = new System.Drawing.Size(65, 13);
            this.labelQuantidade.TabIndex = 6;
            this.labelQuantidade.Text = "Quantidade:";
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.Location = new System.Drawing.Point(461, 20);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(60, 20);
            this.txtQuantidade.TabIndex = 5;
            this.txtQuantidade.Text = "1";
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(540, 18);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(150, 25);
            this.btnAdicionar.TabIndex = 4;
            this.btnAdicionar.Text = "Adicionar ao Carrinho";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // dgvCarrinho
            // 
            this.dgvCarrinho.AllowUserToAddRows = false;
            this.dgvCarrinho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCarrinho.Location = new System.Drawing.Point(23, 60);
            this.dgvCarrinho.Name = "dgvCarrinho";
            this.dgvCarrinho.ReadOnly = true;
            this.dgvCarrinho.Size = new System.Drawing.Size(780, 280);
            this.dgvCarrinho.TabIndex = 3;
            // 
            // labelTotalTexto
            // 
            this.labelTotalTexto.AutoSize = true;
            this.labelTotalTexto.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalTexto.Location = new System.Drawing.Point(20, 365);
            this.labelTotalTexto.Name = "labelTotalTexto";
            this.labelTotalTexto.Size = new System.Drawing.Size(108, 25);
            this.labelTotalTexto.TabIndex = 2;
            this.labelTotalTexto.Text = "Total: R$";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.Blue;
            this.lblTotal.Location = new System.Drawing.Point(125, 365);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(58, 25);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "0,00";
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizar.Location = new System.Drawing.Point(603, 355);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(200, 50);
            this.btnFinalizar.TabIndex = 0;
            this.btnFinalizar.Text = "FINALIZAR VENDA";
            this.btnFinalizar.UseVisualStyleBackColor = true;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            // 
            // FrmVendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 452);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.labelTotalTexto);
            this.Controls.Add(this.dgvCarrinho);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.txtQuantidade);
            this.Controls.Add(this.labelQuantidade);
            this.Controls.Add(this.cbxProduto);
            this.Controls.Add(this.labelProduto);
            this.Name = "FrmVendas";
            this.Text = "Ponto de Venda (PDV)";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarrinho)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label labelProduto;
        private System.Windows.Forms.ComboBox cbxProduto;
        private System.Windows.Forms.Label labelQuantidade;
        private System.Windows.Forms.TextBox txtQuantidade;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.DataGridView dgvCarrinho;
        private System.Windows.Forms.Label labelTotalTexto;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnFinalizar;
    }
}