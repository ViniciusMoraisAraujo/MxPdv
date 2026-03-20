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
                    var inicioDoDia = DateTime.Today; 
                    
                    var vendasDeHoje = context.Vendas.Where(v => v.DataVenda >= inicioDoDia).ToList();
                    
                    decimal totalHoje = vendasDeHoje.Sum(v => v.ValorTotal);
                    lblTotalHoje.Text = $"R$ {totalHoje:N2}";

                    int quantidadeVendas = context.Vendas.Count();
                    lblQtdVendas.Text = quantidadeVendas.ToString();
                    
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
                Console.WriteLine("Erro ao atualizar dashboard: " + ex.Message);
            }
        }

        
        private void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmGrupos = new FrmGrupos();
            frmGrupos.ShowDialog(); 
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmProdutos = new FrmProdutos();
            frmProdutos.ShowDialog();
        }
        
        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmUsuarios = new FrmUsuarios();
            frmUsuarios.ShowDialog();
        }

        private void abrirPDVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmVendas = new FrmVendas();
            frmVendas.ShowDialog();
        }
    }
}