using eBus.Model.Requests;
using eBus.WinUI.Helper;
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

namespace eBus.WinUI.Korisnici
{
    public partial class frmKorisniciDetalji : Form
    {
        private APIService _korinsici = new APIService("Korisnici");
        private APIService _uloge = new APIService("Uloga");
        private int? _id;
        public frmKorisniciDetalji(int? id = null)
        {
            InitializeComponent();
            _id = id;
        }

        private async void frmKorisniciDetalji_Load(object sender, EventArgs e)
        {
            var lista = await _uloge.Get<List<Model.Uloga>>(null);

            clbKorinsiciUloge.DataSource = lista;
            clbKorinsiciUloge.DisplayMember = "Naziv";
            if (_id.HasValue)
            {
                try
                {
                    var korisnik = await _korinsici.GetById<Model.Korisnici>(_id.Value);

                    txtSlika.Text = "some data";
                    txtEmail.Text = korisnik.Email;
                    txtIme.Text = korisnik.Ime;
                    txtKorisnickoIme.Text = korisnik.KorisnickoIme;
                    txtPrezime.Text = korisnik.Prezime;
                    pbUcitajSliku.Image = Helper.ImageHelper.FromByteToImage(korisnik.Slika);
                }
                catch (Exception ex)
                {

                    MessageBox.Show( ex.Message,"Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        KorisniciUpsertRequest korisnik = new KorisniciUpsertRequest();
        private async void btnSpasi_Click(object sender, EventArgs e)
        {

            try
            {

                if (ValidateChildren())
                {
                    var roleList = clbKorinsiciUloge.CheckedItems.Cast<Model.Uloga>().Select(x => x.Id).ToList();

                    korisnik.Ime = txtIme.Text;
                    korisnik.Prezime = txtPrezime.Text;
                    korisnik.Email = txtEmail.Text;
                    korisnik.KorisnickoIme = txtKorisnickoIme.Text;
                    korisnik.Status = cbStatus.Checked;
                    korisnik.Lozinka = txtLozinka.Text;
                    korisnik.PotvrdiLozinku = txtPotvrdiLozinku.Text;
                    korisnik.Uloge = roleList;

                    if (!_id.HasValue)
                    {
                        await _korinsici.Insert<Model.Korisnici>(korisnik);

                        MessageBox.Show("Korisnik je dodan");
                    }
                    else
                    {
                        korisnik.Slika = Helper.ImageHelper.FromImageToByte(pbUcitajSliku.Image);
                        await _korinsici.Update<Model.Korisnici>(_id.Value, korisnik);
                        MessageBox.Show("Korisnik je izmjenjen");

                    }


                }

              

            }
            catch (Exception ex)
            {

                MessageBox.Show( ex.Message, "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnUcitaj_Click(object sender, EventArgs e)
        {

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = openFileDialog.FileName;

                txtSlika.Text = filename;

                Image img = Image.FromFile(filename);

                pbUcitajSliku.Image = img;

                korisnik.Slika = ImageHelper.FromImageToByte(img);
            }
           
             
        }

       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            char znak = new char();

            if (txtLozinka.PasswordChar == znak)
            {
                txtLozinka.PasswordChar = '*';
            }
            else
            {
                txtLozinka.PasswordChar = znak;
            }
        }

        private void txtIme_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIme.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtIme, Properties.Resources.Upozorenje);
                errorProvider1.SetError(txtIme, null);
                errorProvider2.SetError(txtIme, null);
            }
            else
            {
                errorProvider.SetError(txtIme, null);
                errorProvider1.SetError(txtIme, "Uredu");
                errorProvider2.SetError(txtIme, null);
            }
        }

        private void txtPrezime_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPrezime.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtPrezime, Properties.Resources.Upozorenje);
                errorProvider1.SetError(txtPrezime, null);
                errorProvider2.SetError(txtPrezime, null);
            }
            else
            {
                errorProvider.SetError(txtPrezime, null);
                errorProvider1.SetError(txtPrezime, "Uredu");
                errorProvider2.SetError(txtPrezime, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtEmail, Properties.Resources.Upozorenje);
                errorProvider1.SetError(txtEmail, null);
                errorProvider2.SetError(txtEmail, null);
            }
            else
            {
                errorProvider.SetError(txtEmail, null);
                errorProvider1.SetError(txtEmail, "Uredu");
                errorProvider2.SetError(txtEmail, null);
            }
        }

        private void pbUcitajSliku_Validating(object sender, CancelEventArgs e)
        {
            if(pbUcitajSliku.Image == null)
            {
                e.Cancel = true;
                errorProvider.SetError(pbUcitajSliku, Properties.Resources.Upozorenje);
                errorProvider1.SetError(pbUcitajSliku, null);
                errorProvider2.SetError(pbUcitajSliku, null);
            }
            else
            {
                errorProvider.SetError(pbUcitajSliku, null);
                errorProvider1.SetError(pbUcitajSliku, "Uredu");
                errorProvider2.SetError(pbUcitajSliku, null);
            }
        }

        private async Task<bool> ProvjeriKorisnickoIme(string NovoKorisnickoIme)
        {
            var lista = await _korinsici.Get<List<Model.Korisnici>>(null);

            foreach (var item in lista)
            {
                if (item.KorisnickoIme == NovoKorisnickoIme)
                    return true;
            }

            return false;
        }

        private async void txtKorisnickoIme_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKorisnickoIme.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtKorisnickoIme, Properties.Resources.Upozorenje);
                errorProvider1.SetError(txtKorisnickoIme, null);
                errorProvider2.SetError(txtKorisnickoIme, null);
            }
            else if( txtKorisnickoIme.Text.Length < 4)
            {
                e.Cancel = true;
                errorProvider.SetError(txtKorisnickoIme, null);
                errorProvider1.SetError(txtKorisnickoIme, null);
                errorProvider2.SetError(txtKorisnickoIme, "Polje treba da sadrzi najmanje 4 karaktera");

            }
            else if(await ProvjeriKorisnickoIme(txtKorisnickoIme.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtKorisnickoIme, null);
                errorProvider1.SetError(txtKorisnickoIme, null);
                errorProvider2.SetError(txtKorisnickoIme, "Ovo korisničko ime nije dozvoljeno!");
            }
            else
            {
                errorProvider.SetError(txtKorisnickoIme, null);
                errorProvider1.SetError(txtKorisnickoIme, "Uredu");
                errorProvider2.SetError(txtKorisnickoIme, null);
            }
        }

       

        private void txtLozinka_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLozinka.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtLozinka, Properties.Resources.Upozorenje);
                errorProvider1.SetError(txtLozinka, null);
                errorProvider2.SetError(txtLozinka, null);
            }
            else if (txtLozinka.Text.Length < 4)
            {
                e.Cancel = true;
                errorProvider.SetError(txtLozinka, null);
                errorProvider1.SetError(txtLozinka, null);
                errorProvider2.SetError(txtLozinka, "Polje treba da sadrzi najmanje 4 karaktera");
            }
            else
            {
                
                errorProvider.SetError(txtLozinka, null);
                errorProvider1.SetError(txtLozinka, "Uredu");
                errorProvider2.SetError(txtLozinka, null);
            }
        }

        private void txtPotvrdiLozinku_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPotvrdiLozinku.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtPotvrdiLozinku, Properties.Resources.Upozorenje);
                errorProvider1.SetError(txtPotvrdiLozinku, null);
                errorProvider2.SetError(txtPotvrdiLozinku, null);
            }
            else if (txtPotvrdiLozinku.Text.Length < 4)
            {
                e.Cancel = true;
                errorProvider.SetError(txtPotvrdiLozinku, "Polje mora sadržavati više od 4 karaktera");
                errorProvider1.SetError(txtPotvrdiLozinku, null);
                errorProvider2.SetError(txtPotvrdiLozinku, "Polje treba da sadrzi najmanje 4 karaktera");
            }
            else
            {
                errorProvider.SetError(txtPotvrdiLozinku, null);
                errorProvider1.SetError(txtPotvrdiLozinku, "Uredu");
                errorProvider2.SetError(txtPotvrdiLozinku, null);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSlika_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSlika.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(pbUcitajSliku, Properties.Resources.Upozorenje);
                errorProvider1.SetError(pbUcitajSliku, null);
                errorProvider2.SetError(pbUcitajSliku, null);
            }
            else
            {
                errorProvider.SetError(pbUcitajSliku, null);
                errorProvider1.SetError(pbUcitajSliku, "Uredu");
                errorProvider2.SetError(pbUcitajSliku, null);
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
