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

namespace eBus.WinUI.Izvjestaji
{
    public partial class frmIzvjestaj : Form
    {
        private readonly APIService _putniciService = new APIService("Putnik");
        public frmIzvjestaj()
        {
            InitializeComponent();
        }

        private async void frmIzvjestaj_Load(object sender, EventArgs e)
        {
            await LoadPutnike();
        }

        private async Task LoadPutnike()
        {
            var lista = await _putniciService.Get<List<Model.Putnik>>(null);

            lista.Insert(0, new Model.Putnik());
            cmbPutnici.DataSource = lista;
            cmbPutnici.DisplayMember = "Podaci";
            cmbPutnici.ValueMember = "Id";
        }

        private void btnPrikazi_Click(object sender, EventArgs e)
        {
            var tap = cmbPutnici.SelectedValue.ToString();

            if(int.TryParse(tap,out int id))
            {

                frmKarteIzvjestaj frm = new frmKarteIzvjestaj(id);
                frm.Show();
            }
        }

        private void btnSmanji_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_Izadji_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
