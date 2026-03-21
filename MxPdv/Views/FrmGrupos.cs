using System;
using System.Linq;
using System.Windows.Forms;
using MxPdv.Entities;
using MxPdv.Interfaces;
using MxPdv.Services;

namespace MxPdv.Views
{
    public partial class FrmGrupos : Form
    {
        private int _grupoIdSelecionado = 0;
        private readonly IGrupoProdutoService _grupoService;

        public FrmGrupos()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            _grupoService = new GrupoProdutoService(); 
            
            InitializeComponent();
            CarregarGrid(); 
        }

        private void CarregarGrid()
        {
            try
            {
                var listaDeGrupo = _grupoService.ObterTodos()
                    .Select(x => new
                    {
                        Id = x.Id,
                        Nome = x.Nome,
                    }).ToList();
                
                dgvGrupos.DataSource = listaDeGrupo;
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
                var grupo = new GrupoProduto 
                { 
                    Id = _grupoIdSelecionado,
                    Nome = txtNome.Text 
                };

                _grupoService.Salvar(grupo);
                
                MessageBox.Show("Grupo salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                LimparCampos();
                CarregarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    if (this.ActiveControl is Button)
                        return base.ProcessCmdKey(ref msg, keyData);

                    if (this.ActiveControl == dgvGrupos)
                        SelecionarGrupoDaGrid();
                    
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
                    _grupoService.Excluir(_grupoIdSelecionado);
                    
                    MessageBox.Show("Grupo excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                    CarregarGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvGrupos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelecionarGrupoDaGrid();
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
        
        private void SelecionarGrupoDaGrid()
        {
            if (dgvGrupos.CurrentRow != null && dgvGrupos.CurrentRow.Index >= 0)
            {
                DataGridViewRow linha = dgvGrupos.CurrentRow;
                
                _grupoIdSelecionado = Convert.ToInt32(linha.Cells["Id"].Value);
                txtNome.Text = linha.Cells["Nome"].Value.ToString();
                
                txtNome.Focus(); 
            }
        }
        
    }
}