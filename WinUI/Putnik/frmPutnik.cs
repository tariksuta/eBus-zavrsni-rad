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

namespace eBus.WinUI.Putnik
{
    public partial class frmPutnik : Form
    {

        private APIService _putnici = new APIService("Putnik");
        public frmPutnik()
        {
            InitializeComponent();
            dgvPutnici.AutoGenerateColumns = false;
        }

        private async void frmPutnik_Load(object sender, EventArgs e)
        {
            try
            {
                var lista = await _putnici.Get<List<Model.Putnik>>(null);

                dgvPutnici.DataSource = lista;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private async void btnPretrazi_Click(object sender, EventArgs e)
        {
            var search = new PutnikSearchRequest()
            {
                Ime = txtIme.Text,
                Prezime = txtPrezime.Text
            };

            var lista = await _putnici.Get<List<Model.Putnik>>(search);

         
          

            dgvPutnici.DataSource = lista;
            
        }

        private void dgvPutnici_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var el = dgvPutnici.SelectedRows[0].Cells[0].Value.ToString();
            if(int.TryParse(el, out int id))
            {
                frmPutnikDetalji frm = new frmPutnikDetalji(id);
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

        private void button2_Click(object sender, EventArgs e)
        {
            if(dgvPutnici.SelectedRows.Count > 0)
            {
                var el = dgvPutnici.SelectedRows[0].Cells[0].Value.ToString();
                if (int.TryParse(el, out int id))
                {
                    frmPutnikDetalji frm = new frmPutnikDetalji(id);
                  
                    frm.Show();
                }
            }
           
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            frmPutnikDetalji frm = new frmPutnikDetalji();
            this.Close();
            frm.Show();
        }
    }
}
