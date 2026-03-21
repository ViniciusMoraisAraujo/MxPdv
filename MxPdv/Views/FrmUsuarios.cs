using System;
using System.Linq;
using System.Windows.Forms;
using MxPdv.Entities;
using MxPdv.Interfaces;
using MxPdv.Services;

namespace MxPdv.Views
{
    public partial class FrmUsuarios : Form
    {
        private int _usuarioIdSelecionado = 0;
        private readonly IUsuarioService _usuarioService;

        public FrmUsuarios()
        {
            _usuarioService = new UsuarioService();
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            CarregarGrid();
        }

        private void CarregarGrid()
        {
            try
            {
                var listaDeUsuarios = _usuarioService.ObterTodos()
                    .Select(u => new 
                    { 
                        Id = u.Id, 
                        Login = u.Login 
                    })
                    .ToList();

                dgvUsuarios.DataSource = listaDeUsuarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os utilizadores: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("O Login e a Senha são obrigatórios!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var usuario = new Usuario 
                { 
                    Id = _usuarioIdSelecionado,
                    Login = txtLogin.Text,
                    Senha = txtSenha.Text 
                };

                _usuarioService.Salvar(usuario);

                MessageBox.Show("Utilizador salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                LimparCampos();
                CarregarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (_usuarioIdSelecionado == 0)
            {
                MessageBox.Show("Selecione um utilizador na lista para excluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Tem certeza que deseja excluir este utilizador?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _usuarioService.Excluir(_usuarioIdSelecionado);
                    MessageBox.Show("Utilizador excluído!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                    CarregarGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Segurança", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void SelecionarUsuarioDaGrid()
        {
            if (dgvUsuarios.CurrentRow != null && dgvUsuarios.CurrentRow.Index >= 0)
            {
                DataGridViewRow linha = dgvUsuarios.CurrentRow;
                _usuarioIdSelecionado = Convert.ToInt32(linha.Cells["Id"].Value);
                txtLogin.Text = linha.Cells["Login"].Value.ToString();
                txtSenha.Text = ""; 
                txtLogin.Focus();
            }
        }

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) SelecionarUsuarioDaGrid();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    if (this.ActiveControl is Button) return base.ProcessCmdKey(ref msg, keyData);
                    if (this.ActiveControl == dgvUsuarios)
                    {
                        SelecionarUsuarioDaGrid();
                        return true;
                    }
                    SendKeys.Send("{TAB}");
                    return true; 
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnNovo_Click(object sender, EventArgs e) => LimparCampos();

        private void LimparCampos()
        {
            txtLogin.Clear();
            txtSenha.Clear();
            _usuarioIdSelecionado = 0;
            txtLogin.Focus();
        }
    }
}