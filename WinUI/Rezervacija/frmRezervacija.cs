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

namespace eBus.WinUI.Rezervacija
{
    public partial class frmRezervacija : Form
    {

        private APIService _rezervacije = new APIService("Rezervacija");
        private APIService _putnici = new APIService("Putnik");
        private APIService _karte = new APIService("Karta");
        public frmRezervacija()
        {
            InitializeComponent();
            dgvRezervacije.AutoGenerateColumns = false;
            
        }

        private async  void frmRezervacija_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadPutnike();
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Rezervacija", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
           
        }

        private async Task LoadPutnike()
        {

            try
            {
                var lista = await _putnici.Get<List<Model.Putnik>>(null);

                lista.Insert(0, new Model.Putnik());
                cmbPutnici.DataSource = lista;
                cmbPutnici.DisplayMember = "Podaci";
                cmbPutnici.ValueMember = "Id";
                cmbPutnici.Text = "--Odaberi putnika--";
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private async void cmbPutnici_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(cmbPutnici.SelectedValue.ToString(), out int id))
                {
                    var search = new RezervacijaSearchRequest()
                    {
                        PoAngazmanu = false,
                        PutnikId = id
                    };

                    var lista = await _rezervacije.Get<List<Model.Rezervacija>>(search);

                   

                    dgvRezervacije.DataSource = lista;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dgvRezervacije_DoubleClick(object sender, EventArgs e)
        {
            var tmp = dgvRezervacije.SelectedRows[0].Cells[0].Value.ToString();

            if(int.TryParse(tmp, out int id))
            {
                frmPregledRezervacija frm = new frmPregledRezervacija(id);
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

            if(dgvRezervacije.SelectedRows.Count > 0)
            {
                var tmp = dgvRezervacije.SelectedRows[0].Cells[0].Value.ToString();

                if (int.TryParse(tmp, out int id))
                {
                    frmPregledRezervacija frm = new frmPregledRezervacija(id);
                    
                    frm.Show();
                }
            }
           
        }
    }
}
