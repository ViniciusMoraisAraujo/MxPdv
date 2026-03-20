using System;
using System.Linq;
using System.Windows.Forms;
using MxPdv.Data;

namespace MxPdv.Views
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        // Este evento roda toda vez que a tela principal ganha foco novamente
        // (exemplo: quando você fecha a tela de vendas e volta pro menu).
        // Isso garante que os números do Dashboard estejam SEMPRE atualizados em tempo real!
        private void FrmPrincipal_Activated(object sender, EventArgs e)
        {
            CarregarDashboard();
        }

        private void CarregarDashboard()
        {
            try
            {
                using (var context = new MxPdvContext())
                {
                    // 1. LÓGICA: SOMA DAS VENDAS DE HOJE
                    var inicioDoDia = DateTime.Today; // Pega a data de hoje, ex: 20/03/2026 00:00:00
                    
                    // Vai no banco e busca só as vendas que aconteceram hoje
                    var vendasDeHoje = context.Vendas.Where(v => v.DataVenda >= inicioDoDia).ToList();
                    
                    // Soma o ValorTotal de todas essas vendas
                    decimal totalHoje = vendasDeHoje.Sum(v => v.ValorTotal);
                    lblTotalHoje.Text = $"R$ {totalHoje:N2}";

                    // 2. LÓGICA: QUANTIDADE TOTAL DE VENDAS
                    int quantidadeVendas = context.Vendas.Count();
                    lblQtdVendas.Text = quantidadeVendas.ToString();

                    // 3. LÓGICA: PRODUTO COM ESTOQUE MAIS CRÍTICO
                    // Ordena o estoque do menor para o maior (Ascending) e pega o primeiro da lista
                    var piorEstoque = context.Produtos.OrderBy(p => p.Estoque).FirstOrDefault();
                    
                    if (piorEstoque != null)
                    {
                        lblAlertaEstoque.Text = $"{piorEstoque.Nome}\n({piorEstoque.Estoque} un. restantes)";
                    }
                    else
                    {
                        lblAlertaEstoque.Text = "Nenhum produto cadastrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Como é só um painel informativo, não damos "crash" se der erro de conexão, só exibimos no console.
                Console.WriteLine("Erro ao atualizar dashboard: " + ex.Message);
            }
        }

        // --- NAVEGAÇÃO DO MENU ---
        
        private void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // O ShowDialog "trava" a tela principal atrás até o usuário fechar a tela de Grupos.
            var frmGrupos = new FrmGrupos();
            frmGrupos.ShowDialog(); 
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmProdutos = new FrmProdutos();
            frmProdutos.ShowDialog();
        }

        private void abrirPDVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmVendas = new FrmVendas();
            frmVendas.ShowDialog();
        }
    }
}