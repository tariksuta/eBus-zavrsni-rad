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

namespace eBus.WinUI.Notifikacije
{
    public partial class frmNotifikacije : Form
    {
        private APIService _notifikacije = new APIService("Notifikacije");
        private APIService _novosti = new APIService("Novosti");
        public frmNotifikacije()
        {
            InitializeComponent();
            dgvNotifikacije.AutoGenerateColumns = false;
        }

        private async void frmNotifikacije_Load(object sender, EventArgs e)
        {

            try
            {
                var lista = await _notifikacije.Get<List<Model.Notifikacije>>(null);

               

                dgvNotifikacije.DataSource = lista;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Notifikacija", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
           
        }

        private async void txtNaslov_TextChanged(object sender, EventArgs e)
        {
            var search = new NotifikacijeSearchRequest()
            {
                Naslov = txtNaslov.Text
            };

            var lista = await _notifikacije.Get<List<Model.Notifikacije>>(search);

            dgvNotifikacije.DataSource = lista;
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
