using eBus.WinUI.Angazuje;
using eBus.WinUI.Drzava;
using eBus.WinUI.Grad;
using eBus.WinUI.Izvjestaji;
using eBus.WinUI.Karta;
using eBus.WinUI.Kompanija;
using eBus.WinUI.Korisnici;
using eBus.WinUI.Linija;
using eBus.WinUI.Notifikacije;
using eBus.WinUI.Novosti;
using eBus.WinUI.Ocjena;
using eBus.WinUI.Putnik;
using eBus.WinUI.Rezervacija;
using eBus.WinUI.Sjedista;
using eBus.WinUI.Vozilo;
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

namespace eBus.WinUI
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            customDesigne();
        }

        private void customDesigne()
        {
            panelSubMenuIzvjestaj.Visible = false;
            
        }

        private void hideSubMenu()
        {
            if (panelSubMenuIzvjestaj.Visible == true)
                panelSubMenuIzvjestaj.Visible = false;
           
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private Form activeform = null;

        private void openChildForm(Form childForm)
        {

            if (activeform != null)
                activeform.Close();
            activeform = childForm;
            childForm.TopLevel = false;
            
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new frmDrzava());

        }

        private void btnGrad_Click(object sender, EventArgs e)
        {
           // hideSubMenu();
            openChildForm(new frmGrad());
        }

        private void btnKompanija_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new frmKompanija());
        }

        private void btnVozila_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new frmVozila());
        }

        private void btnSjedista_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new frmSjedista());
        }

        private void btn_Izadji_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSmanji_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnIzvjestaji_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubMenuIzvjestaj);
        }

        private void btnLinija_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new frmLinija());


        }

        
        

       
        

      

       

       

        private void btnPutnici_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new frmPutnik());
        }

       

      

       

        private void btnPoKompanijama_Click(object sender, EventArgs e)
        {
            hideSubMenu();

            openChildForm(new frmIzvjestajKompanije());
        }

        private void btnPoLinijama_Click(object sender, EventArgs e)
        {
            hideSubMenu();

            openChildForm(new frmIzvjestajRelacije());
        }

       

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelHeader_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnNovosti_Click_1(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new frmNovosti());
        }

        private void btnRezervacije_Click_1(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new frmRezervacija());
        }

        private void btnKorisnici_Click_1(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new frmKorisnici());
        }

        private void btnPutnici_Click_1(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new frmPutnik());
        }

        private void btnOcjene_Click_1(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new frmOcjena());
        }

        private void btnNotifikacije_Click_1(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new frmNotifikacije());
        }

        private void btnIzvjestaji_Click_2(object sender, EventArgs e)
        {
            showSubMenu(panelSubMenuIzvjestaj);
        }

        private void btnAngazman_Click_1(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new frmAngazuje());
        }

        private void btnPoPutnicima_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new frmIzvjestaj());
        }

        private void btnPoKompanijama_Click_1(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new frmIzvjestajKompanije());
        }

        private void btnPoLinijama_Click_1(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new frmIzvjestajRelacije());
        }

        private void btnKarta_Click_1(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new frmKarta());
        }

        private void btnProfil_Click(object sender, EventArgs e)
        {
            hideSubMenu();

            openChildForm(new KorisnickiProfil());
        }

       
    }
}
