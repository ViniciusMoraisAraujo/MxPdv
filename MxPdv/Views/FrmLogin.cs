using System;
using System.Windows.Forms;
using MxPdv.Entities;
using MxPdv.Interfaces;
using MxPdv.Services;

namespace MxPdv.Views
{
    public partial class FrmLogin : Form
    {
        private readonly IUsuarioService _usuarioService;

        public FrmLogin()
        {
            InitializeComponent();
            _usuarioService = new UsuarioService();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AcceptButton = btnEntrar;
            _usuarioService.CriarUsuarioPadraoSeNecessario();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Por favor, preencha o utilizador e a senha.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var usuarioEncontrado = _usuarioService.Autenticar(txtUsuario.Text, txtSenha.Text);

                if (usuarioEncontrado != null)
                {
                    this.Hide(); 
                    var frmPrincipal = new FrmPrincipal();
                    frmPrincipal.ShowDialog(); 
                    this.Close(); 
                }
                else
                {
                    MessageBox.Show("Utilizador ou senha incorretos.", "Erro de Acesso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSenha.Clear(); 
                    txtSenha.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao efetuar login: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}