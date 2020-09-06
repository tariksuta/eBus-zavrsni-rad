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

namespace eBus.WinUI.Karta
{
    public partial class frmKarta : Form
    {
        private APIService _karte = new APIService("Karta");
        
        public frmKarta()
        {
            InitializeComponent();
            dgvKarte.AutoGenerateColumns = false;
        }

        private  void frmKarta_Load(object sender, EventArgs e)
        {

            try
            {
               
                //var lista = await _karte.Get<List<Model.Karta>>(null);

                //dgvKarte.DataSource = lista;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Karta", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

       

       

        private async void btnPretrazi_Click(object sender, EventArgs e)
        {

            try
            {
                var search = new KartaSearchRequest()
                {
                    PoAngzuje = false,
                    PoVozilu = false,
                    DatumIzdavanja = dtpDatumIzdavanja.Value
                };

                var lista = await _karte.Get<List<Model.Karta>>(search);

               

                dgvKarte.DataSource = lista;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Karta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void dgvKarte_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            try
            {
                var el = dgvKarte.SelectedRows[0].Cells[0].Value.ToString();

                if (int.TryParse(el, out int id))
                {
                    frmKartaDetalji frm = new frmKartaDetalji(id);
                    frm.Show();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Karta", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
               

        private void panel1_MouseMove_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if(dgvKarte.SelectedRows.Count > 0)
                {
                    var el = dgvKarte.SelectedRows[0].Cells[0].Value.ToString();

                    if (int.TryParse(el, out int id))
                    {
                        frmKartaDetalji frm = new frmKartaDetalji(id);
                        
                        frm.Show();
                    }
                }
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Karta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            frmKartaDetalji frm = new frmKartaDetalji();
            this.Close();
            frm.Show();
        }
    }
}
