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

namespace eBus.WinUI.Putnik
{
    public partial class frmPutnikDetalji : Form
    {
        private APIService _putnik = new APIService("Putnik");
        private int? _id;
        public frmPutnikDetalji(int? id = null)
        {
            InitializeComponent();
            _id = id;
        }
        PutnikUpsertRequest putnik = new PutnikUpsertRequest();
        private async void btnSačuvaj_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValidateChildren())
                {
                    putnik.DatumRegistracije = DateTime.Now;
                    putnik.Email = txtEmail.Text;
                    putnik.Ime = txtIme.Text;
                    putnik.KorisnickoIme = txtKorisnickoIme.Text;
                    putnik.Lozinka = txtLozinka.Text;
                    putnik.PotvrdiLozinku = txtPotvrdiLozinku.Text;
                    putnik.Prezime = txtPrezime.Text;

                    if (!_id.HasValue)
                    {
                        await _putnik.Insert<Model.Putnik>(putnik);
                        MessageBox.Show("Putnike je dodan");
                    }
                    else
                    {
                        putnik.Slika = Helper.ImageHelper.FromImageToByte(pbSlika.Image);
                        await _putnik.Update<Model.Putnik>(_id.Value, putnik);
                    }
                    
                }
                
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Putnik detalji", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUcitajSliku_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filename = openFileDialog1.FileName;

                txtSlika.Text = filename;
                
                Image img = Image.FromFile(filename);

                putnik.Slika = Helper.ImageHelper.FromImageToByte(img);
                pbSlika.Image = img;
                
            }
        }

        private void txtIme_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtIme.Text))
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
            if (string.IsNullOrEmpty(txtPrezime.Text))
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
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, Properties.Resources.Upozorenje);
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"[a-zA-Z.]+@[a-z]+.[a-z]+"))
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
            if (string.IsNullOrEmpty(txtKorisnickoIme.Text))
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
            if (string.IsNullOrEmpty(txtLozinka.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLozinka, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(txtLozinka, null);
            }
        }

        private void txtPotvrdiLozinku_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPotvrdiLozinku.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPotvrdiLozinku, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(txtPotvrdiLozinku, null);
            }
        }

      

       

        private void pbSlika_Validating(object sender, CancelEventArgs e)
        {
            if (pbSlika.Image == null)
            {
                errorProvider1.SetError(pbSlika, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(pbSlika, null);
            }
        }

        private async void frmPutnikDetalji_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                var putnik = await _putnik.GetById<Model.Putnik>(_id.Value);

                txtSlika.Text = "some data";
                txtIme.Text = putnik.Ime;
                txtPrezime.Text = putnik.Prezime;
                txtEmail.Text = putnik.Email;
                txtKorisnickoIme.Text = putnik.KorisnickoIme;
                pbSlika.Image = Helper.ImageHelper.FromByteToImage(putnik.Slika);
            }
        }

        private void txtSlika_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSlika.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(pbSlika, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(pbSlika, null);
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
