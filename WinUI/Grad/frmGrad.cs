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

namespace eBus.WinUI.Grad
{
    public partial class frmGrad : Form
    {
        private APIService _gradovi = new APIService("Grad");
        private APIService _drzave = new APIService("Drzava");
        public frmGrad()
        {
            InitializeComponent();
            dgvGradovi.AutoGenerateColumns = false;
        }

        private async void frmGrad_Load(object sender, EventArgs e)
        {

            try
            {
                var lista = await _gradovi.Get<List<Model.Grad>>(null);

              

                dgvGradovi.DataSource = lista;

                var lista2 = await _drzave.Get<List<Model.Drzava>>(null);

                lista2.Insert(0, new Model.Drzava());
                cmbDrzava.DataSource = lista2;
                cmbDrzava.ValueMember = "Id";
                cmbDrzava.DisplayMember = "Naziv";
                cmbDrzava.SelectedText = "--Odaberi državu--";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Grad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

            
        }

        private async void btnPrikazi_Click(object sender, EventArgs e)
        {
            var search = new GradSearchRequest()
            {
                Drzavaid = int.Parse(cmbDrzava.SelectedValue.ToString())
            };

            var lista = await _gradovi.Get<List<Model.Grad>>(search);

          

            dgvGradovi.DataSource = null;
            dgvGradovi.DataSource = lista;
        }

        private void dgvGradovi_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var el = dgvGradovi.SelectedRows[0].Cells[0].Value.ToString();
            if(int.TryParse(el, out int id))
            {
                frmGradDetalji frm = new frmGradDetalji(id);
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

        private void button1_Click(object sender, EventArgs e)
        {

            if(dgvGradovi.SelectedRows.Count > 0)
            {
                var el = dgvGradovi.SelectedRows[0].Cells[0].Value.ToString();
                if (int.TryParse(el, out int id))
                {
                    frmGradDetalji frm = new frmGradDetalji(id);
                    
                    frm.Show();
                }
            }
           
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            frmGradDetalji frm = new frmGradDetalji();
            this.Close();
            frm.Show();
        }
    }
}
