using System;
using System.Linq;
using System.Windows.Forms;
using MxPdv.Data;
using MxPdv.Entities;

namespace MxPdv.Views
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            CriarUsuarioPadraoSeNecessario();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AcceptButton = btnEntrar;
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Por favor, preencha o usuário e a senha.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var context = new MxPdvContext())
                {
                    var usuarioEncontrado = context.Usuarios
                        .FirstOrDefault(u => u.Login == txtUsuario.Text && u.Senha == txtSenha.Text);

                    if (usuarioEncontrado != null)
                    {
                        this.Hide(); 

                        var frmPrincipal = new FrmPrincipal();
                        frmPrincipal.ShowDialog(); 

                        this.Close(); 
                    }
                    else
                    {
                        MessageBox.Show("Usuário ou senha incorretos.", "Erro de Acesso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSenha.Clear(); 
                        txtSenha.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar com o banco de dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CriarUsuarioPadraoSeNecessario()
        {
            try
            {
                using (var context = new MxPdvContext())
                {
                    if (!context.Usuarios.Any())
                    {
                        var novoUsuario = new Usuario 
                        { 
                            Login = "admin", 
                            Senha = "admin" 
                        };
                        
                        context.Usuarios.Add(novoUsuario);
                        context.SaveChanges(); 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao criar usuário padrão: " + ex.Message);
            }
        }
    }
}