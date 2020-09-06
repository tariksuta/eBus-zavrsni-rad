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

namespace eBus.WinUI.Drzava
{
    public partial class frmDrzava : Form
    {

        private APIService _drzave = new APIService("Drzava");
        public frmDrzava()
        {
            InitializeComponent();
            dgvDrzave.AutoGenerateColumns = false;
        }

        private async void frmDrzava_Load(object sender, EventArgs e)
        {
            try
            {
                var lista = await _drzave.Get<List<Model.Drzava>>(null);

                dgvDrzave.DataSource = lista;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Drzava", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void txtNaziv_TextChanged(object sender, EventArgs e)
        {
            var search = new DrzavaSearchRequest()
            {
                Naziv = txtNaziv.Text
            };

            var lista = await _drzave.Get<List<Model.Drzava>>(search);

            dgvDrzave.DataSource = lista;
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

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            frmDrzavaDetalji frm = new frmDrzavaDetalji();
            this.Close();
            frm.Show();
        }
    }
}
