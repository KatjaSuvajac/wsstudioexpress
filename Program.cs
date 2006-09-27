using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace WebServiceStudioExpress
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            using (SpecialServices.SingleProgramInstance spi = new SpecialServices.SingleProgramInstance())
            {
                if (spi.IsSingleInstance)
                {
                    //E' la prima istanza dell'applicazione.
                    //Imposta la lingua dell'applicazione.
                    //try
                    //{
                    //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language);
                    //    Thread.CurrentThread.CurrentCulture = new CultureInfo(Settings.Default.Language);
                    //}
                    //catch (ArgumentException)
                    //{
                    //    //Si è verificato un errore cercando di impostare la lingua
                    //    //dell'interfaccia. Utilizza la lingua predefinita.
                    //    Thread.CurrentThread.CurrentUICulture = new CultureInfo("it-IT");
                    //    Thread.CurrentThread.CurrentCulture = new CultureInfo("it-IT");
                    //}

                    Application.EnableVisualStyles();
                    Application.Run(new frmMain(args));
                }
                else
                {
                    //Il processo è già stato avviato: lo pone in primo piano e
                    //termina questa istanza.
                    spi.RaiseOtherProcess();
                }
            }
        }
    }
}