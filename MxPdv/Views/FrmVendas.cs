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
        // 1. O "Carrinho" na memória. 
        // Usamos BindingList porque ela avisa o DataGridView automaticamente quando ganha um novo item.
        private BindingList<ItemCarrinho> _carrinho = new BindingList<ItemCarrinho>();
        private decimal _valorTotalVenda = 0;

        public FrmVendas()
        {
            InitializeComponent();
            ConfigurarCarrinho();
            CarregarComboboxProdutos();
        }

        private void ConfigurarCarrinho()
        {
            // Liga a nossa lista temporária à tabela da tela
            dgvCarrinho.DataSource = _carrinho;
        }

        private void CarregarComboboxProdutos()
        {
            try
            {
                using (var context = new MxPdvContext())
                {
                    // VALIDAÇÃO INTELIGENTE: Traz apenas produtos que tenham estoque maior que zero!
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

        // --- ADICIONAR AO CARRINHO ---
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

                    // VALIDAÇÃO INTELIGENTE: Impede a venda se o cliente pedir mais do que temos
                    if (produto.Estoque < quantidadeDesejada)
                    {
                        MessageBox.Show($"Estoque insuficiente! Temos apenas {produto.Estoque} unidades de {produto.Nome}.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Calcula o subtotal e adiciona na lista temporária (Carrinho)
                    decimal subtotal = produto.Preco * quantidadeDesejada;

                    _carrinho.Add(new ItemCarrinho
                    {
                        ProdutoId = produto.Id,
                        NomeProduto = produto.Nome,
                        Quantidade = quantidadeDesejada,
                        ValorUnitario = produto.Preco,
                        Subtotal = subtotal
                    });

                    // Atualiza o Total Grande na Tela
                    _valorTotalVenda += subtotal;
                    lblTotal.Text = _valorTotalVenda.ToString("N2"); // "N2" formata para ficar 0,00

                    // Prepara a tela para o próximo produto
                    txtQuantidade.Text = "1";
                    cbxProduto.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar item: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- FINALIZAR A VENDA E BAIXAR ESTOQUE ---
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
                        // 1. Cria a Venda Principal com a Data e o Total
                        var novaVenda = new Venda
                        {
                            DataVenda = DateTime.Now,
                            ValorTotal = _valorTotalVenda
                        };

                        // 2. Transfere os itens do Carrinho para o Banco de Dados
                        foreach (var itemCarrinho in _carrinho)
                        {
                            var itemVenda = new ItemVenda
                            {
                                ProdutoId = itemCarrinho.ProdutoId,
                                Quantidade = itemCarrinho.Quantidade,
                                ValorUnitario = itemCarrinho.ValorUnitario
                            };
                            
                            novaVenda.Itens.Add(itemVenda);

                            // 3. A Mágica da Baixa de Estoque!
                            var produtoNoBanco = context.Produtos.Find(itemCarrinho.ProdutoId);
                            if (produtoNoBanco != null)
                            {
                                // Subtrai o estoque atual pela quantidade vendida
                                produtoNoBanco.Estoque -= itemCarrinho.Quantidade;
                            }
                        }

                        // 4. Salva a Venda, os Itens e o Estoque tudo de uma vez
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
            _carrinho.Clear(); // Limpa a grade
            _valorTotalVenda = 0;
            lblTotal.Text = "0,00";
            cbxProduto.SelectedIndex = -1;
            txtQuantidade.Text = "1";
            
            // Recarrega os produtos para esconder os que o estoque acabou de zerar!
            CarregarComboboxProdutos(); 
        }
    }

    // --- CLASSE AUXILIAR ---
    // Criamos esta classe extra no final do arquivo apenas para o DataGridView ficar bonito.
    // Ela não vai para o banco de dados.
    public class ItemCarrinho
    {
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}