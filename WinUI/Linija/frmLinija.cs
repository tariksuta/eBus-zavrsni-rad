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

namespace eBus.WinUI.Linija
{
    public partial class frmLinija : Form
    {
        private APIService _gradovi = new APIService("Grad");
        private APIService _linije = new APIService("Linija");
        
        public frmLinija()
        {
            InitializeComponent();
            dgvLinije.AutoGenerateColumns = false;
        }

        private async void frmLinija_Load(object sender, EventArgs e)
        {
            try
            {
                 await LoadPolazista();
                await LoadOdredista();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Linija", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async Task LoadPolazista()
        {

            try
            {
                var lista = await _gradovi.Get<List<Model.Grad>>(null);
                lista.Insert(0, new Model.Grad());
                cmbPolaziste.DataSource = lista;
                cmbPolaziste.ValueMember = "Id";
                cmbPolaziste.DisplayMember = "Naziv";
                cmbPolaziste.Text = "---Odaberi grad---";
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        private async Task LoadOdredista()
        {

            try
            {
                var lista = await _gradovi.Get<List<Model.Grad>>(null);
                lista.Insert(0, new Model.Grad());

                cmbOdrediste.DataSource = lista;
                cmbOdrediste.ValueMember = "Id";
                cmbOdrediste.DisplayMember = "Naziv";
                cmbOdrediste.Text = "---Odaberi grad---";
            }
            catch (Exception)
            {

                throw;
            }

        }

        private async void btnPretrazi_Click(object sender, EventArgs e)
        {
            try
            {
                var search = new LinijaSearchRequest()
                {
                    OdredisteId = int.Parse(cmbOdrediste.SelectedValue.ToString()),
                    PolazisteId = int.Parse(cmbPolaziste.SelectedValue.ToString()),
                   
                };
                var lista = await _linije.Get<List<Model.Linija>>(search);

                

                dgvLinije.DataSource = lista;


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Linija", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvLinije_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.ColumnIndex == 4)
                {
                    //var linija = dgvLinije.SelectedRows[0].DataBoundItem as Model.Linija;
                    var id = int.Parse(dgvLinije.SelectedRows[0].Cells[0].Value.ToString());
                    frmCijena frm = new frmCijena(id);
                    frm.Show();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show( ex.Message, "Linija", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                var p = cmbPolaziste.SelectedValue;
                var zamjena = p;
                cmbPolaziste.SelectedValue = cmbOdrediste.SelectedValue;
                cmbOdrediste.SelectedValue = zamjena;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Linija switch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void dgvLinije_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var el = dgvLinije.SelectedRows[0].Cells[0].Value.ToString();

            if(int.TryParse(el, out int id))
            {
                frmLinijaDetalji frm = new frmLinijaDetalji(id);
                frm.Show();
                
            }
        }

        private void btn_Izadji_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSmanji_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

            if(dgvLinije.SelectedRows.Count > 0)
            {
                var el = dgvLinije.SelectedRows[0].Cells[0].Value.ToString();

                if (int.TryParse(el, out int id))
                {
                    frmLinijaDetalji frm = new frmLinijaDetalji(id);
            
                    frm.Show();

                }
            }
            
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            frmLinijaDetalji frm = new frmLinijaDetalji();
            this.Close();
            frm.Show();
        }

       
    }
}


