using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using MxPdv.Data;
using MxPdv.Entities; 

namespace MxPdv.Views
{
    public partial class FrmVendas : Form
    {
        public class ItemCarrinho
        {
            public int ProdutoId { get; set; }
            public string NomeProduto { get; set; }
            public int Quantidade { get; set; }
            public decimal ValorUnitario { get; set; }
            public decimal Subtotal { get; set; }
        }
        
        private BindingList<ItemCarrinho> _carrinho = new BindingList<ItemCarrinho>();
        private decimal _valorTotalVenda = 0;
        
        public FrmVendas()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            ConfigurarCarrinho();
            CarregarComboboxProdutos();
        }

        private void ConfigurarCarrinho()
        {
            dgvCarrinho.DataSource = _carrinho;
        }

        private void CarregarComboboxProdutos()
        {
            try
            {
                using (var context = new MxPdvContext())
                {
                    var produtosDisponiveis = context.Produtos.Where(p => p.Estoque > 0).ToList();
                    
                    cbxProduto.DataSource = produtosDisponiveis;
                    cbxProduto.DisplayMember = "Nome"; 
                    cbxProduto.ValueMember = "Id";     
                    cbxProduto.SelectedIndex = -1;     
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {

                case Keys.F3:
                    btnFinalizar.PerformClick();
                    return true;

                case Keys.F5:
                    if (MessageBox.Show("Deseja realmente cancelar esta venda e limpar o carrinho?", "Cancelar Venda", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        LimparTelaVenda();
                    }
                    return true;

                case Keys.F8:
                    cbxProduto.Focus();
                    cbxProduto.DroppedDown = true; 
                    return true;

                case Keys.Enter:
                    if (txtQuantidade.Focused || cbxProduto.Focused)
                    {
                        btnAdicionar.PerformClick();
                        return true;
                    }
                    break; 
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (cbxProduto.SelectedValue == null)
            {
                MessageBox.Show("Selecione um produto para vender.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantidade.Text, out int quantidadeDesejada) || quantidadeDesejada <= 0)
            {
                MessageBox.Show("Digite uma quantidade válida e maior que zero.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var context = new MxPdvContext())
                {
                    int produtoId = Convert.ToInt32(cbxProduto.SelectedValue);
                    var produto = context.Produtos.Find(produtoId);

                    if (produto.Estoque < quantidadeDesejada)
                    {
                        MessageBox.Show($"Estoque insuficiente! Temos apenas {produto.Estoque} unidades de {produto.Nome}.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    decimal subtotal = produto.Preco * quantidadeDesejada;

                    _carrinho.Add(new ItemCarrinho
                    {
                        ProdutoId = produto.Id,
                        NomeProduto = produto.Nome,
                        Quantidade = quantidadeDesejada,
                        ValorUnitario = produto.Preco,
                        Subtotal = subtotal
                    });

                    _valorTotalVenda += subtotal;
                    lblTotal.Text = _valorTotalVenda.ToString("N2"); 

                    txtQuantidade.Text = "1";
                    cbxProduto.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar item: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (_carrinho.Count == 0)
            {
                MessageBox.Show("O carrinho está vazio. Adicione produtos antes de finalizar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Confirma o fechamento da venda no valor de R$ {_valorTotalVenda:N2}?", "Finalizar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (var context = new MxPdvContext())
                    {
                        var novaVenda = new Venda
                        {
                            DataVenda = DateTime.Now,
                            ValorTotal = _valorTotalVenda
                        };

                        foreach (var itemCarrinho in _carrinho)
                        {
                            var itemVenda = new ItemVenda
                            {
                                ProdutoId = itemCarrinho.ProdutoId,
                                Quantidade = itemCarrinho.Quantidade,
                                ValorUnitario = itemCarrinho.ValorUnitario
                            };
                            
                            novaVenda.Itens.Add(itemVenda);

                            var produtoNoBanco = context.Produtos.Find(itemCarrinho.ProdutoId);
                            if (produtoNoBanco != null)
                            {
                                produtoNoBanco.Estoque -= itemCarrinho.Quantidade;
                            }
                        }

                        context.Vendas.Add(novaVenda);
                        context.SaveChanges(); 

                        MessageBox.Show("Venda finalizada com sucesso e estoque atualizado!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LimparTelaVenda();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao finalizar a venda: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LimparTelaVenda()
        {
            _carrinho.Clear(); 
            _valorTotalVenda = 0;
            lblTotal.Text = "0,00";
            cbxProduto.SelectedIndex = -1;
            txtQuantidade.Text = "1";
            
            CarregarComboboxProdutos(); 
        }


    }



}