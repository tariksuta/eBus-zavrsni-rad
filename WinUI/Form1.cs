using eBus.WinUI.Angazuje;
using eBus.WinUI.Drzava;
using eBus.WinUI.Grad;
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
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            customDesigne();
        }
       

        private void customDesigne() {
            //panelSubMenuKorisnici.Visible = false;
            //panelSumMenuKompanija.Visible = false;
            //panelSubMenuDrzava.Visible = false;
            //panelSubMenuGrad.Visible = false;
            //panelSubMenuLinija.Visible = false;
            //panelSubMenuKarta.Visible = false;
            //panelSubMenuAngazuje.Visible = false;
            //panelSubMenuRezervacije.Visible = false;
            //panelSubMenuNovosti.Visible = false;
            //panelSubMenuNotifkiacije.Visible = false;
            //panelSubMenuPutnici.Visible = false;
            //panelSubMenuOcjene.Visible = false;
            //panelSubMenuVozila.Visible = false;
            //panelSubMenuSjedista.Visible = false;
        }

        private void hideSubMenu()
        {
            //if (panelSubMenuKorisnici.Visible == true)
            //    panelSubMenuKorisnici.Visible = false;
            //if (panelSumMenuKompanija.Visible == true)
            //    panelSumMenuKompanija.Visible = false;
            //if (panelSubMenuDrzava.Visible == true)
            //    panelSubMenuDrzava.Visible = false;
            //if (panelSubMenuGrad.Visible == true)
            //    panelSubMenuGrad.Visible = false;
            //if (panelSubMenuLinija.Visible == true)
            //    panelSubMenuLinija.Visible = false;
            //if (panelSubMenuKarta.Visible == true)
            //    panelSubMenuKarta.Visible = false;
            //if (panelSubMenuAngazuje.Visible == true)
            //    panelSubMenuAngazuje.Visible = false;
            //if (panelSubMenuRezervacije.Visible == true)
            //    panelSubMenuRezervacije.Visible = false;
            //if (panelSubMenuNovosti.Visible == true)
            //    panelSubMenuNovosti.Visible = false;
            //if (panelSubMenuNotifkiacije.Visible == true)
            //    panelSubMenuNotifkiacije.Visible = false;
            //if (panelSubMenuPutnici.Visible == true)
            //    panelSubMenuPutnici.Visible = false;
            //if (panelSubMenuOcjene.Visible == true)
            //    panelSubMenuOcjene.Visible = false;
            //if (panelSubMenuVozila.Visible == true)
            //    panelSubMenuVozila.Visible = false;
            //if (panelSubMenuSjedista.Visible == true)
            //    panelSubMenuSjedista.Visible = false;

        }

        private void showSubMenu(Panel subMenu)
        {
            if(subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        #region Korisnici
        private void btnKorisnici_Click(object sender, EventArgs e)
        {
            //showSubMenu(panelSubMenuKorisnici);

            openChildForm(new frmKorisnici());
        }

        private void btnPregled_Click(object sender, EventArgs e)
        {
            openChildForm(new frmKorisnici());

            hideSubMenu();
        }

        private void btnKorisniciDodaj_Click(object sender, EventArgs e)
        {
            openChildForm(new frmKorisniciDetalji());

            hideSubMenu();
        }
        #endregion

        private Form activeform = null;

        private  void  openChildForm(Form childForm)
        {
            //if (activeform != null)
               // activeform.Close();

            activeform = childForm;
            childForm.TopLevel = false;
            //childForm.FormBorderStyle = FormBorderStyle.None;
            //childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        #region Kompanija
        private void btnKompanija_Click(object sender, EventArgs e)
        {
            //showSubMenu(panelSumMenuKompanija);

            openChildForm(new frmKompanija());
        }

        private void btnKompanijaPregled_Click(object sender, EventArgs e)
        {
            openChildForm(new frmKompanija());

            hideSubMenu();
        }

        private void btnKompanijaDodaj_Click(object sender, EventArgs e)
        {
            openChildForm(new frmKompanijaDetalji());
            hideSubMenu();
        }
        #endregion

        #region Država
        private void btnDrzava_Click(object sender, EventArgs e)
        {
            //showSubMenu(panelSubMenuDrzava);

            openChildForm(new frmDrzava());
        }

        private void btnDrzavaPregled_Click(object sender, EventArgs e)
        {
            openChildForm(new frmDrzava());
            hideSubMenu();
        }

        private void btnDrzavaDodaj_Click(object sender, EventArgs e)
        {
            openChildForm(new frmDrzavaDetalji());
            hideSubMenu();
        }
        #endregion

        #region Grad
        private void btnGrad_Click(object sender, EventArgs e)
        {
            // showSubMenu(panelSubMenuGrad);
            openChildForm(new frmGrad());
        }

        private void btnGradPregled_Click(object sender, EventArgs e)
        {
            openChildForm(new frmGrad());
            hideSubMenu();
        }

        private void btnGradDodaj_Click(object sender, EventArgs e)
        {
            openChildForm(new frmGradDetalji());
            hideSubMenu();
        }
        #endregion

        #region Linija
        private void btnLinija_Click(object sender, EventArgs e)
        {
            //showSubMenu(panelSubMenuLinija);
            openChildForm(new frmLinija());
        }

        private void btnLinijaPregled_Click(object sender, EventArgs e)
        {
            openChildForm(new frmLinija());
            hideSubMenu();
        }

        private void btnLinijaDodaj_Click(object sender, EventArgs e)
        {
            openChildForm(new frmLinijaDetalji());
            hideSubMenu();
        }
        #endregion

        #region Karta
        private void btnKarta_Click(object sender, EventArgs e)
        {
            //showSubMenu(panelSubMenuKarta);

            openChildForm(new frmKarta());
        }

        private void btnKartaPregled_Click(object sender, EventArgs e)
        {
            openChildForm(new frmKarta());
            hideSubMenu();
        }

        private void btnKartaDodaj_Click(object sender, EventArgs e)
        {
            openChildForm(new frmKartaDetalji());
            hideSubMenu();
        }
        #endregion

        #region Angažuje
        private void btnAngazuje_Click(object sender, EventArgs e)
        {
            // showSubMenu(panelSubMenuAngazuje);

            openChildForm(new frmAngazuje());
        }
        private void btnAngazujePregled_Click(object sender, EventArgs e)
        {
            openChildForm(new frmAngazuje());
            hideSubMenu();
        }

        private void btnAngazujeDodaj_Click(object sender, EventArgs e)
        {
            openChildForm(new frmAngazuje());
            hideSubMenu();

        }

        #endregion

        #region Rezervacije
        private void btnRezervacija_Click(object sender, EventArgs e)
        {
            //showSubMenu(panelSubMenuRezervacije);

            openChildForm(new frmRezervacija());
        }

       

        private void btnReervacijaPregled_Click(object sender, EventArgs e)
        {
            openChildForm(new frmRezervacija());
            hideSubMenu();
        }

        private void btnRezervacijaDodaj_Click(object sender, EventArgs e)
        {
            
            hideSubMenu();
        }
        #endregion

        #region Novosti
        private void btnNovosti_Click(object sender, EventArgs e)
        {
            // showSubMenu(panelSubMenuNovosti);
            openChildForm(new frmNovosti());
        }

        private void btnNovostiPregled_Click(object sender, EventArgs e)
        {
            openChildForm(new frmNovosti());
            hideSubMenu();
        }

        private void btnNovostiDodaj_Click(object sender, EventArgs e)
        {
            openChildForm(new frmNovostiDetalji());
            hideSubMenu();
        }
        #endregion

        #region Notifikacije
        private void btnNotifikacije_Click(object sender, EventArgs e)
        {
            //showSubMenu(panelSubMenuNotifkiacije);
            openChildForm(new frmNotifikacije());
        }

        private void btnNotifikacijePrelged_Click(object sender, EventArgs e)
        {
            openChildForm(new frmNotifikacije());
            hideSubMenu();
        }
        #endregion

        #region Putnici
        private void btnPutnici_Click(object sender, EventArgs e)
        {
            //showSubMenu(panelSubMenuPutnici);

            openChildForm(new frmPutnik());
        }

        private void btnPutniciPregled_Click(object sender, EventArgs e)
        {
            openChildForm(new frmPutnik());
            hideSubMenu();
        }

        private void btnPutniciDodaj_Click(object sender, EventArgs e)
        {
            openChildForm(new frmPutnikDetalji());
            hideSubMenu();
        }
        #endregion

        #region Ocjene
        private void btnOcjene_Click(object sender, EventArgs e)
        {
            //showSubMenu(panelSubMenuOcjene);

            openChildForm(new frmOcjena());
        }

        private void btnOcjenePregled_Click(object sender, EventArgs e)
        {
            openChildForm(new frmOcjena());
            hideSubMenu();
        }
        #endregion

        #region Vozila
        private void btnVozila_Click(object sender, EventArgs e)
        {
            //showSubMenu(panelSubMenuVozila);
            openChildForm(new frmVozila());
        }

        private void btnVozilaPregled_Click(object sender, EventArgs e)
        {
            openChildForm(new frmVozila());
            hideSubMenu();
        }

        private void btnVozilaDodaj_Click(object sender, EventArgs e)
        {
            openChildForm(new frmVozilaDetalji());
            hideSubMenu();
        }
        #endregion

        #region Sjedišta
        private void btnSjedista_Click(object sender, EventArgs e)
        {
            //showSubMenu(panelSubMenuSjedista);

            openChildForm(new frmSjedista());
        }

        private void btnSjedistaPregled_Click(object sender, EventArgs e)
        {
            openChildForm(new frmSjedista());
            hideSubMenu();
        }

        private void btnSjedistaDodaj_Click(object sender, EventArgs e)
        {
            openChildForm(new frmSjedistaDetalji());
            hideSubMenu();
        }



        #endregion

       
    }
}
