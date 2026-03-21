using System;
using System.Linq;
using System.Windows.Forms;
using MxPdv.Entities;
using MxPdv.Interfaces;
using MxPdv.Services;

namespace MxPdv.Views
{
    public partial class FrmProdutos : Form
    {
        private int _produtoIdSelecionado = 0;
        private readonly IProdutoService _produtoService;
        private readonly IGrupoProdutoService _grupoService;

        public FrmProdutos()
        {
            _produtoService = new ProdutoService();
            _grupoService = new GrupoProdutoService(); 
            
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            CarregarComboboxGrupos();
            CarregarGrid();
        }

        private void CarregarComboboxGrupos()
        {
            try
            {
                var listaDeGrupos = _grupoService.ObterTodos();

                cbxGrupo.DataSource = listaDeGrupos;
                cbxGrupo.DisplayMember = "Nome"; 
                cbxGrupo.ValueMember = "Id";     
                cbxGrupo.SelectedIndex = -1;     
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os grupos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarGrid()
        {
            try
            {
                // Chamamos o serviço e mapeamos o resultado.
                var listaDeProdutos = _produtoService.ObterTodos()
                    .Select(p => new 
                    { 
                        Id = p.Id, 
                        Nome = p.Nome,
                        Preco = p.Preco,
                        Estoque = p.Estoque,
                        GrupoProdutoId = p.GrupoProdutoId, 
                        Grupo = p.GrupoProduto?.Nome 
                    })
                    .ToList();     
                
                dgvProdutos.DataSource = listaDeProdutos;
                
                if (dgvProdutos.Columns["Grupo"] != null)
                    dgvProdutos.Columns["Grupo"].Visible = false; 
                
                if (dgvProdutos.Columns["GrupoProduto"] != null)
                    dgvProdutos.Columns["GrupoProduto"].Visible = false; 
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
                case Keys.Enter:
                    SendKeys.Send("{TAB}");
                    return true; 
                case Keys.F2:
                    btnNovo.PerformClick(); 
                    return true;
                case Keys.F3:
                    btnSalvar.PerformClick(); 
                    return true;
                case Keys.F4:
                    btnExcluir.PerformClick(); 
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O nome do produto é obrigatório!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cbxGrupo.SelectedValue == null)
            {
                MessageBox.Show("A seleção de um Grupo é obrigatória!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtPreco.Text, out decimal precoConvertido))
            {
                MessageBox.Show("Preço inválido. Digite apenas números e vírgula.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtEstoque.Text, out int estoqueConvertido))
            {
                MessageBox.Show("Estoque inválido. Digite um número inteiro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var produto = new Produto 
                { 
                    Id = _produtoIdSelecionado, 
                    Nome = txtNome.Text,
                    Preco = precoConvertido,
                    Estoque = estoqueConvertido,
                    GrupoProdutoId = Convert.ToInt32(cbxGrupo.SelectedValue) 
                };

                _produtoService.Salvar(produto);

                MessageBox.Show("Produto salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                LimparCampos();
                CarregarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar o produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (_produtoIdSelecionado == 0)
            {
                MessageBox.Show("Selecione um produto na lista para excluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Tem certeza que deseja excluir este produto?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _produtoService.Excluir(_produtoIdSelecionado);
                    
                    MessageBox.Show("Produto excluído!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                    CarregarGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvProdutos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow linha = dgvProdutos.Rows[e.RowIndex];
                
                _produtoIdSelecionado = Convert.ToInt32(linha.Cells["Id"].Value);
                txtNome.Text = linha.Cells["Nome"].Value.ToString();
                txtPreco.Text = linha.Cells["Preco"].Value.ToString();
                txtEstoque.Text = linha.Cells["Estoque"].Value.ToString();
                
                cbxGrupo.SelectedValue = linha.Cells["GrupoProdutoId"].Value;
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtPreco.Clear();
            txtEstoque.Clear();
            cbxGrupo.SelectedIndex = -1; 
            _produtoIdSelecionado = 0;
            txtNome.Focus();
        }

    }
}