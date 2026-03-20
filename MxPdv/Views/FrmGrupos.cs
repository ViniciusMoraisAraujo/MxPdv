using System;
using System.Linq;
using System.Windows.Forms;
using MxPdv.Data;
using MxPdv.Entities;

namespace MxPdv.Views
{
    public partial class FrmGrupos : Form
    {
        private int _grupoIdSelecionado = 0;

        public FrmGrupos()
        {
            InitializeComponent();
            CarregarGrid(); 
        }
        private void CarregarGrid()
        {
            try
            {
                using (var context = new MxPdvContext())
                {
                    var listaDeGrupo = context.GruposProdutos
                        .Select(x => new
                        {
                            Id = x.Id,
                            Nome = x.Nome,
                        }).ToList();
                    
                    dgvGrupos.DataSource = listaDeGrupo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os grupos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O nome do grupo é obrigatório!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var context = new MxPdvContext())
                {
                    if (_grupoIdSelecionado == 0)
                    {
                        var novoGrupo = new GrupoProduto { Nome = txtNome.Text };
                        context.GruposProdutos.Add(novoGrupo);
                    }
                    else
                    {
                        var grupoExistente = context.GruposProdutos.Find(_grupoIdSelecionado);
                        if (grupoExistente != null)
                        {
                            grupoExistente.Nome = txtNome.Text;
                        }
                    }

                    context.SaveChanges();
                    
                    MessageBox.Show("Grupo salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LimparCampos();
                    CarregarGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (_grupoIdSelecionado == 0)
            {
                MessageBox.Show("Selecione um grupo na lista para excluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este grupo?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    using (var context = new MxPdvContext())
                    {
                        var grupo = context.GruposProdutos.Find(_grupoIdSelecionado);
                        if (grupo != null)
                        {
                            context.GruposProdutos.Remove(grupo); 
                            context.SaveChanges();                
                            MessageBox.Show("Grupo excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimparCampos();
                            CarregarGrid();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não é possível excluir este grupo, pois podem existir produtos vinculados a ele.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvGrupos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow linha = dgvGrupos.Rows[e.RowIndex];
                
                _grupoIdSelecionado = Convert.ToInt32(linha.Cells["Id"].Value);
                txtNome.Text = linha.Cells["Nome"].Value.ToString();
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            _grupoIdSelecionado = 0; 
            txtNome.Focus();
        }

        private void FrmGrupos_Load(object sender, EventArgs e)
        {
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
        }

        private void Excluir_Click(object sender, EventArgs e)
        {
        }


    }
}