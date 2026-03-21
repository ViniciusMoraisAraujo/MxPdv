using System;
using System.Linq;
using System.Windows.Forms;
using MxPdv.Data;
using MxPdv.Entities;

namespace MxPdv.Views
{
    public partial class FrmUsuarios : Form
    {
        private int _usuarioIdSelecionado = 0;

        public FrmUsuarios()
        {
            InitializeComponent();
            CarregarGrid();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void CarregarGrid()
        {
            try
            {
                using (var context = new MxPdvContext())
                {
                    var listaDeUsuarios = context.Usuarios
                        .Select(u => new 
                        { 
                            Id = u.Id, 
                            Login = u.Login 
                        })
                        .ToList();

                    dgvUsuarios.DataSource = listaDeUsuarios;
                }
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
                using (var context = new MxPdvContext())
                {
                    bool loginJaExiste = context.Usuarios
                        .Any(u => u.Login.ToLower() == txtLogin.Text.ToLower() && u.Id != _usuarioIdSelecionado);

                    if (loginJaExiste)
                    {
                        MessageBox.Show("Já existe um utilizador com este Login. Escolha outro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (_usuarioIdSelecionado == 0) // MODO INCLUSÃO
                    {
                        var novoUsuario = new Usuario 
                        { 
                            Login = txtLogin.Text,
                            Senha = txtSenha.Text
                        };
                        context.Usuarios.Add(novoUsuario);
                    }
                    else 
                    {
                        var usuarioExistente = context.Usuarios.Find(_usuarioIdSelecionado);
                        if (usuarioExistente != null)
                        {
                            usuarioExistente.Login = txtLogin.Text;
                            usuarioExistente.Senha = txtSenha.Text;
                        }
                    }

                    context.SaveChanges();
                    MessageBox.Show("Utilizador salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
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
            if (_usuarioIdSelecionado == 0)
            {
                MessageBox.Show("Selecione um utilizador na lista para excluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var context = new MxPdvContext())
            {
                if (context.Usuarios.Count() <= 1)
                {
                    MessageBox.Show("Não pode excluir o único utilizador do sistema, senão nunca mais consegue fazer Login!", "Segurança", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }

            if (MessageBox.Show("Tem certeza que deseja excluir este utilizador?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (var context = new MxPdvContext())
                    {
                        var usuario = context.Usuarios.Find(_usuarioIdSelecionado);
                        if (usuario != null)
                        {
                            context.Usuarios.Remove(usuario);
                            context.SaveChanges();
                            MessageBox.Show("Utilizador excluído!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow linha = dgvUsuarios.Rows[e.RowIndex];
                
                _usuarioIdSelecionado = Convert.ToInt32(linha.Cells["Id"].Value);
                txtLogin.Text = linha.Cells["Login"].Value.ToString();
                
                using (var context = new MxPdvContext())
                {
                    var usuario = context.Usuarios.Find(_usuarioIdSelecionado);
                    if (usuario != null)
                    {
                        txtSenha.Text = usuario.Senha;
                    }
                }
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            txtLogin.Clear();
            txtSenha.Clear();
            _usuarioIdSelecionado = 0;
            txtLogin.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                return true; 
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}