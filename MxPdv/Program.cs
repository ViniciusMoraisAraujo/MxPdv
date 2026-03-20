using System;
using System.Windows.Forms;
using MxPdv.Views; // Garante que ele enxerga a pasta Views

namespace MxPdv
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            MessageBox.Show("O sistema está iniciando...", "Teste", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            Application.Run(new FrmLogin());
        }
    }
}