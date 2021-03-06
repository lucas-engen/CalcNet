﻿using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace CalcNetServer
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        ///        

        [STAThread]
        static void Main(string[] Args)
        {
            bool bypass = false;

            bypass = true;

            if (Args.Length == 1)
            {
                foreach(string arg in Args)
                {
                    if(arg == "--developer")
                    {
                        bypass = true;
                        break;
                    }
                }
            }

            if (Connections.isConnectedToInternet())
            {
                MessageBox.Show("Não podemos utilizar esta rede, pois ela possui conexão ativa com a internet", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if(!bypass)
                    return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Run(new frmMain());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Uma falha grave ocorreu no aplicativo e ele terá que ser fechado para evitar maiores problemas\n\nFonte: {sender.ToString()}", "Erro fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show($"Uma exceção não tratada ocorreu em: {sender.ToString()}\n\n{e.Exception.Message}", "Erro fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
    }
}
