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

namespace eBus.WinUI.Ocjena
{
    public partial class frmOcjena : Form
    {
        private APIService _ocjene = new APIService("Ocjena");
        private APIService _kompanije = new APIService("Kompanija");
        private APIService _putnici = new APIService("Putnik");
        public frmOcjena()
        {
            InitializeComponent();
            dgvOcjene.AutoGenerateColumns = false;
        }

        private async  void frmOcjena_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadKompanije();
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task LoadKompanije()
        {
            var lista = await _kompanije.Get<List<Model.Kompanija>>(null);
            lista.Insert(0, new Model.Kompanija());
            

            cmbKompanije.DataSource = lista;
            cmbKompanije.ValueMember = "Id";
            cmbKompanije.DisplayMember = "Naziv";
            cmbKompanije.SelectedText = "---Odaberi kompaniju---";
            
        }

        private async void cmbKompanije_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if( int.TryParse(cmbKompanije.SelectedValue.ToString(),out int id))
                {

                    if(id > 0)
                    {
                        var search = new OcjenaSearchRequest()
                        {
                            KompanijaId = id
                        };

                        var lista = await _ocjene.Get<List<Model.Ocjena>>(search);


                        dgvOcjene.DataSource = lista;
                    }
                    
                }
               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Ocjena", MessageBoxButtons.OK,MessageBoxIcon.Error);
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
