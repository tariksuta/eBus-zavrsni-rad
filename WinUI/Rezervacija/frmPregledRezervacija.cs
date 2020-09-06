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
    public partial class frmPregledRezervacija : Form
    {
        private APIService _rezervacija = new APIService("Rezervacija");
        
        private int _id;
        public frmPregledRezervacija(int id)
        {
            InitializeComponent();
            _id = id;
        }

        private async void frmPregledRezervacija_Load(object sender, EventArgs e)
        {
            var rez = await _rezervacija.GetById<Model.Rezervacija>(_id);

           

            txtDatumIsteka.Text = rez.DatumIsteka.ToString("dd.MM.yyyy");
            txtDatumKreiranja.Text = rez.DatumKreiranja.ToString("dd.MM.yyyy");
            

            pbQRcode.Image = Helper.ImageHelper.FromByteToImage(rez.Qrcode);
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
