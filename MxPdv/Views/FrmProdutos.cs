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
            
            // É vital carregar o ComboBox ANTES da grade, 
            // para que a lista de grupos já esteja pronta quando a tela abrir.
            CarregarComboboxGrupos();
            CarregarGrid();
        }

        // --- A MÁGICA DO DESAFIO (COMBOBOX) ---
        private void CarregarComboboxGrupos()
        {
            try
            {
                using (var context = new MxPdvContext())
                {
                    // Busca todos os grupos no banco de dados
                    var listaDeGrupos = context.GruposProdutos.ToList();

                    // Preenche o ComboBox com a lista
                    cbxGrupo.DataSource = listaDeGrupos;
                    
                    // DisplayMember: O que o usuário VÊ na tela (O nome do grupo)
                    cbxGrupo.DisplayMember = "Nome"; 
                    
                    // ValueMember: O que o sistema GUARDA nos bastidores (O ID do grupo)
                    cbxGrupo.ValueMember = "Id";     

                    // SelectedIndex = -1 faz o ComboBox começar vazio, obrigando a escolha
                    cbxGrupo.SelectedIndex = -1;     
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os grupos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- LER E LISTAR ---
        private void CarregarGrid()
        {
            try
            {
                using (var context = new MxPdvContext())
                {
                    // O "Include" faz um JOIN no banco de dados para trazer o Nome do Grupo junto com o Produto
                    var produtos = context.Produtos.Include("Grupo").ToList();
                    
                    dgvProdutos.DataSource = produtos;
                    
                    // Esconde a coluna da propriedade de navegação para a grade ficar limpa
                    dgvProdutos.Columns["Grupo"].Visible = false; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- SALVAR (CREATE / UPDATE) ---
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // 1. Validações Inteligentes (Conta pontos no desafio!)
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O nome do produto é obrigatório!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Garante que o usuário selecionou um grupo (Regra de Negócio)
            if (cbxGrupo.SelectedValue == null)
            {
                MessageBox.Show("A seleção de um Grupo é obrigatória!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tenta converter o texto do preço para um número decimal
            if (!decimal.TryParse(txtPreco.Text, out decimal precoConvertido))
            {
                MessageBox.Show("Preço inválido. Digite apenas números e vírgula.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tenta converter o texto do estoque para um número inteiro
            if (!int.TryParse(txtEstoque.Text, out int estoqueConvertido))
            {
                MessageBox.Show("Estoque inválido. Digite um número inteiro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Gravação no Banco
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
                            // Pega o "Id" do grupo que está escondido no ValueMember do ComboBox
                            GrupoProdutoId = Convert.ToInt32(cbxGrupo.SelectedValue) 
                        };
                        context.Produtos.Add(novoProduto);
                    }
                    else // MODO ALTERAÇÃO
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

                    context.SaveChanges(); // Salva de verdade
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

        // --- EXCLUIR ---
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

        // --- SELECIONAR NA GRADE ---
        private void dgvProdutos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow linha = dgvProdutos.Rows[e.RowIndex];
                
                _produtoIdSelecionado = Convert.ToInt32(linha.Cells["Id"].Value);
                txtNome.Text = linha.Cells["Nome"].Value.ToString();
                txtPreco.Text = linha.Cells["Preco"].Value.ToString();
                txtEstoque.Text = linha.Cells["Estoque"].Value.ToString();
                
                // Faz o ComboBox pular automaticamente para o grupo que estava cadastrado no produto
                cbxGrupo.SelectedValue = linha.Cells["GrupoProdutoId"].Value;
            }
        }

        // --- LIMPAR ---
        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtPreco.Clear();
            txtEstoque.Clear();
            cbxGrupo.SelectedIndex = -1; // Volta a ficar vazio
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