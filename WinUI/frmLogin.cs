using eBus.Model.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eBus.WinUI
{
    public partial class frmLogin : Form
    {
        private readonly APIService _korisniciService = new APIService("Korisnici");
        private readonly APIService _putnikService = new APIService("Putnik");
        private APIService _service = new APIService("Uloga");
        public frmLogin()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void frmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                APIService.KorisnickoIme = txtKorisnickoIme.Text;
                APIService.Lozinka = txtLozinka.Text;
                await _service.Get<dynamic>(null);

                var search = new PutnikSearchRequest()
                {
                    KorisnickoIme = APIService.KorisnickoIme
                };

                var listaPutnika = await _putnikService.Get<List<Model.Putnik>>(search);

                bool prolaz = true;

                foreach (var item in listaPutnika)
                {
                    if (item.KorisnickoIme == APIService.KorisnickoIme)
                    {
                        prolaz = false;
                        break;
                    }
                }

                if (prolaz)
                {
                   

                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Nema te pravo pristupa");
                }


            }
            catch (Exception ex)
            {

                // MessageBox.Show(ex.Message,"Login", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
