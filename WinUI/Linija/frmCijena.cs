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
    public partial class frmCijena : Form
    {

        private APIService _cijene = new APIService("Cijena");
        private APIService _linije = new APIService("Linija");

        private int? _id;
        public frmCijena(int? id = null)
        {
            InitializeComponent();
            _id = id;

            dgvCijena.AutoGenerateColumns = false;
        }

        private async void frmCijena_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                var linija = await _linije.GetById<Model.Linija>(_id.Value);
                txtNazivLinije.Text = linija.Naziv;
               
            }

            var search = new CijenaSearchRequest()
            {
                LinijaID = _id.Value
            };
            var lista = await _cijene.Get<List<Model.Cijena>>(search);

            var newlista = new List<object>();

            foreach (var item in lista)
            {
                string cijena = null;
                if(item.Iznos > 0)
                {
                    cijena = item.Iznos.ToString() + " KM";
                }

                newlista.Add(new
                {
                    Iznos = cijena,
                    Popust = item.Popust,
                    Kompanija = item.Kompanija
                });
            }
          
            dgvCijena.DataSource = newlista;
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                frmCijenaDetalji frm = new frmCijenaDetalji(_id.Value);
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
