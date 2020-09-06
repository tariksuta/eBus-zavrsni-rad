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

namespace eBus.WinUI.Sjedista
{
    public partial class frmSjedista : Form
    {

        private APIService _sjedista = new APIService("Sjediste");
        private APIService _vozila = new APIService("Vozilo");
        public frmSjedista()
        {
            InitializeComponent();
            dgvSjedista.AutoGenerateColumns = false;
        }

        private async void frmSjedista_Load(object sender, EventArgs e)
        {
            try
            {
                await  LoadVozila();
                var lista = await _sjedista.Get<List<Model.Sjediste>>(null);

                

                dgvSjedista.DataSource = lista;
            }
            catch (Exception ex)
            {

                MessageBox.Show( ex.Message, "Sjediste", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

        private async Task LoadVozila()
        {
            var lista = await _vozila.Get<List<Model.Vozilo>>(null);

            lista.Insert(0, new Model.Vozilo());

            cmbVozila.DataSource = lista;
            cmbVozila.DisplayMember = "Model";
            cmbVozila.ValueMember = "Id";
            cmbVozila.SelectedText = "---Odaberi vozilo---";
            
            
        }

        private async void cmbVozila_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
               if(int.TryParse( cmbVozila.SelectedValue.ToString() ,out int id))
                {
                    var search = new SjedisteSearchRequest()
                    {
                        VoziloId = id
                    };

                    var lista = await _sjedista.Get<List<Model.Sjediste>>(search);

                    
                    //dgvSjedista.DataSource = null;
                    dgvSjedista.DataSource = lista;
                }
           


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Sjediste", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSjedista_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var obj = dgvSjedista.SelectedRows[0].Cells[0].Value;

            if(int.TryParse(obj.ToString() , out int id))
            {
                frmSjedistaDetalji frm = new frmSjedistaDetalji(id);
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

            if(dgvSjedista.SelectedRows.Count > 0)
            {
                var obj = dgvSjedista.SelectedRows[0].Cells[0].Value;

                if (int.TryParse(obj.ToString(), out int id))
                {
                    frmSjedistaDetalji frm = new frmSjedistaDetalji(id);
                    frm.Show();
                }
            }
            
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            frmSjedistaDetalji frm = new frmSjedistaDetalji();
            this.Close();
            frm.Show();
        }
    }
}
