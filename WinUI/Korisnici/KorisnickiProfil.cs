using eBus.Model.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eBus.WinUI.Korisnici
{
    public partial class KorisnickiProfil : Form
    {
        private readonly APIService _korisniciService = new APIService("Korisnici");
        Model.Korisnici korisnik = null;
        public KorisnickiProfil()
        {
            InitializeComponent();
        }

        private async void KorisnickiProfil_Load(object sender, EventArgs e)
        {

            try
            {
                var search = new KorisniciSearchRequest()
                {
                    KorisnickoIme = APIService.KorisnickoIme
                };

                var lista = await _korisniciService.Get<List<Model.Korisnici>>(search);

               

                foreach (var item in lista)
                {
                    if (item.KorisnickoIme == APIService.KorisnickoIme)
                    {
                        korisnik = item;
                        break;
                    }
                }

                txtIme.Text = korisnik.Ime;
                txtPrezime.Text = korisnik.Prezime;
                txtEmail.Text = korisnik.Email;
                cbAktivan.Checked = korisnik.Status.Value;
                pbSlika.Image = Helper.ImageHelper.FromByteToImage(korisnik.Slika);
                txtKorisnickoIme.Text = korisnik.KorisnickoIme;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
          
            

        }

        private async void btnSacuvaj_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValidateChildren())
                {

                    var newkorisnik = new KorisniciUpsertRequest();

                    newkorisnik.Ime = txtIme.Text;
                    newkorisnik.Prezime = txtPrezime.Text;
                    newkorisnik.Email = txtEmail.Text;
                    newkorisnik.Status = cbAktivan.Checked;
                    newkorisnik.Slika = Helper.ImageHelper.FromImageToByte(pbSlika.Image);
                    newkorisnik.KorisnickoIme = txtKorisnickoIme.Text;
                    newkorisnik.Lozinka = txtLozinka.Text;
                    newkorisnik.PotvrdiLozinku = txtPotvrdiLozinku.Text;
                

                    if(newkorisnik.Lozinka != newkorisnik.PotvrdiLozinku)
                    {
                        MessageBox.Show("Lozinke se ne podudaraju");
                    }

                    await _korisniciService.Update<Model.Korisnici>(korisnik.Id, newkorisnik);

                    MessageBox.Show("Uspjesno izmjenjeni podaci");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtIme_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIme.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtIme, Properties.Resources.Upozorenje);
            }else if(!Regex.IsMatch(txtIme.Text, @"^[a-zA-Z]+$"))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtIme, Properties.Resources.NeispravanFormat);
            }
            else
            {
                errorProvider1.SetError(txtIme, null);
            }
        }

        private void txtPrezime_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPrezime.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPrezime, Properties.Resources.Upozorenje);
            }
            else if (!Regex.IsMatch(txtPrezime.Text, @"^[a-zA-Z]+$"))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPrezime, Properties.Resources.NeispravanFormat);
            }
            else
            {
                errorProvider1.SetError(txtPrezime, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, Properties.Resources.Upozorenje);
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z.]+@[a-z]+(?:.[a-z]+).[a-z]+$"))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, Properties.Resources.NeispravanFormat);
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
        }

        private void txtKorisnickoIme_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKorisnickoIme.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtKorisnickoIme, Properties.Resources.Upozorenje);
            }
            else if (!Regex.IsMatch(txtKorisnickoIme.Text, @"^[a-z]+$"))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtKorisnickoIme, Properties.Resources.NeispravanFormat);
            }
            else
            {
                errorProvider1.SetError(txtKorisnickoIme, null);
            }
        }

        private void txtLozinka_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLozinka.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLozinka, Properties.Resources.Upozorenje);
            }
            else if (txtLozinka.Text.Length < 4)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLozinka, Properties.Resources.NeispravanFormat);
            }
            else
            {
                errorProvider1.SetError(txtLozinka, null);
            }
        }

        private void txtPotvrdiLozinku_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPotvrdiLozinku.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPotvrdiLozinku, Properties.Resources.Upozorenje);
            }
            else if (txtPotvrdiLozinku.Text.Length < 4)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPotvrdiLozinku, Properties.Resources.NeispravanFormat);
            }
            else
            {
                errorProvider1.SetError(txtPotvrdiLozinku, null);
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
