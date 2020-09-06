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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eBus.WinUI.Kompanija
{
    public partial class frmKompanijaDetalji : Form
    {
        private APIService _gradovi = new APIService("Grad");
        private APIService _kompanija= new APIService("Kompanija");
        private int? _id;
        public frmKompanijaDetalji(int? id = null)
        {
            InitializeComponent();
            _id = id;
        }

        private async void frmKompanijaDetalji_Load(object sender, EventArgs e)
        {
            LoadGradovi();

            if (_id.HasValue)
            {
                var element = await _kompanija.GetById<Model.Kompanija>(_id.Value);

                txtAdresa.Text = element.Adresa;
                txtEmail.Text = element.Email;
                txtNaziv.Text = element.Naziv;
                txtTelefon.Text = element.KontaktBroj;

                var empty = new byte[0];
                if(element.Slika != null && element.Slika.Length > 0)
                     pbSlika.Image = Helper.ImageHelper.FromByteToImage(element.Slika);

                if (element.GradId.HasValue)
                {
                    cmbGradovi.SelectedValue = element.GradId.Value;
                }
            
            }
        }

        private async void LoadGradovi()
        {
            var lista = await _gradovi.Get<List<Model.Grad>>(null);

            lista.Insert(0, new Model.Grad());
            cmbGradovi.DataSource = lista;
            cmbGradovi.DisplayMember = "Naziv";
            cmbGradovi.ValueMember = "Id";
            cmbGradovi.SelectedText = "--Odaberi grad--";

        }

        private void txtNaziv_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNaziv.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNaziv, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(txtNaziv, null);
            }
        }

        private void txtTelefon_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTelefon.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTelefon, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(txtTelefon, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, Properties.Resources.Upozorenje);
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^[a-z.]+@[a-z]+(?:.[a-z]+).[a-z]+$"))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, Properties.Resources.NeispravanFormat);
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAdresa.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAdresa, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(txtAdresa, null);
            }
        }

        Model.Requests.KompanijaUpsertRequest kompanija = new KompanijaUpsertRequest();
        private async void btnSacuvaj_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValidateChildren())
                {
                 

                    kompanija.Adresa = txtAdresa.Text;
                    kompanija.Email = txtEmail.Text;
                    kompanija.GradId = int.Parse(cmbGradovi?.SelectedValue.ToString());
                    kompanija.KontaktBroj = txtTelefon.Text;
                    kompanija.Naziv = txtNaziv.Text;


                    if (_id.HasValue)
                    {
                        await _kompanija.Update<Model.Kompanija>(_id.Value,kompanija);
                        MessageBox.Show("Podaci su izmjenjeni");

                    }
                    else
                    {
                        await _kompanija.Insert<Model.Kompanija>(kompanija);
                        MessageBox.Show("Kompanije je uspješno dodana");

                    }

                  
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Komanija detalji", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbGradovi_Validating(object sender, CancelEventArgs e)
        {
            if(cmbGradovi.SelectedIndex == -1)
            {
                e.Cancel = true;
                errorProvider1.SetError(cmbGradovi, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(cmbGradovi, null);
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

        private void btnUcitaj_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filename = openFileDialog1.FileName;

                txtSlika.Text = filename;

                Image img = Image.FromFile(filename);

                pbSlika.Image = img;

                kompanija.Slika = ImageHelper.FromImageToByte(img);
            }
        }
    }
}
