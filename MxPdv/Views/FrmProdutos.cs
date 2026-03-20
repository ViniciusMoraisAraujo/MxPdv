using System;
using System.Linq;
using System.Windows.Forms;
using MxPdv.Data;
using MxPdv.Entities;

namespace MxPdv.Views
{
    public partial class FrmProdutos : Form
    {
        private int _produtoIdSelecionado = 0;

        public FrmProdutos()
        {
            InitializeComponent();

            CarregarComboboxGrupos();
            CarregarGrid();
        }
        private void CarregarComboboxGrupos()
        {
            try
            {
                using (var context = new MxPdvContext())
                {
                    var listaDeGrupos = context.GruposProdutos.ToList();

                    cbxGrupo.DataSource = listaDeGrupos;
                    
                    cbxGrupo.DisplayMember = "Nome"; 
                    
                    cbxGrupo.ValueMember = "Id";     

                    cbxGrupo.SelectedIndex = -1;     
                }
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
                using (var context = new MxPdvContext())
                {
                    var listaDeProdutos = context.Produtos
                        .Select(p => new 
                        { 
                            Id = p.Id, 
                            Nome = p.Nome,
                            Preco = p.Preco,
                            Estoque = p.Estoque,
                            GrupoProdutoId = p.GrupoProdutoId, 
                            Grupo = p.GrupoProduto.Nome 
                        })
                        .ToList();     
                    
                    dgvProdutos.DataSource = listaDeProdutos;
                    
                    dgvProdutos.Columns["Grupo"].Visible = false; 
                    
                    if (dgvProdutos.Columns["GrupoProduto"] != null)
                    {
                        dgvProdutos.Columns["GrupoProduto"].Visible = false; 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                using (var context = new MxPdvContext())
                {
                    if (_produtoIdSelecionado == 0) // MODO INCLUSÃO
                    {
                        var novoProduto = new Produto 
                        { 
                            Nome = txtNome.Text,
                            Preco = precoConvertido,
                            Estoque = estoqueConvertido,
                            GrupoProdutoId = Convert.ToInt32(cbxGrupo.SelectedValue) 
                        };
                        context.Produtos.Add(novoProduto);
                    }
                    else 
                    {
                        var produtoExistente = context.Produtos.Find(_produtoIdSelecionado);
                        if (produtoExistente != null)
                        {
                            produtoExistente.Nome = txtNome.Text;
                            produtoExistente.Preco = precoConvertido;
                            produtoExistente.Estoque = estoqueConvertido;
                            produtoExistente.GrupoProdutoId = Convert.ToInt32(cbxGrupo.SelectedValue);
                        }
                    }

                    context.SaveChanges(); 
                    MessageBox.Show("Produto salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LimparCampos();
                    CarregarGrid();
                }
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
                    using (var context = new MxPdvContext())
                    {
                        var produto = context.Produtos.Find(_produtoIdSelecionado);
                        if (produto != null)
                        {
                            context.Produtos.Remove(produto);
                            context.SaveChanges();
                            MessageBox.Show("Produto excluído!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimparCampos();
                            CarregarGrid();
                        }
                    }
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

        private void dvgProdutos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}