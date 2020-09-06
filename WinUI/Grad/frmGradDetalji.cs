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

namespace eBus.WinUI.Grad
{
    public partial class frmGradDetalji : Form
    {
        private APIService _grad = new APIService("Grad");
        private APIService _drzave = new APIService("Drzava");

        private int? _id;
        public frmGradDetalji(int ? id = null)
        {
            InitializeComponent();

            _id = id;
        }

        private async void frmGradDetalji_Load(object sender, EventArgs e)
        {
            var lista = await _drzave.Get<List<Model.Drzava>>(null);

            lista.Insert(0, new Model.Drzava());
            cmbDrzave.DataSource = lista;
            cmbDrzave.DisplayMember = "Naziv";
            cmbDrzave.ValueMember = "Id";
            cmbDrzave.SelectedText = "--Odaberi državu--";

            if (_id.HasValue)
            {
                var entitet = await _grad.GetById<Model.Grad>(_id.Value);

                txtNaziv.Text = entitet.Naziv;
                cmbDrzave.SelectedValue = entitet.DrzavaId;
            }
        }

        private async void btnSacuvaj_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ValidateChildren())
                {
                    var grad = new GradUpsertRequest();

                    grad.Naziv = txtNaziv.Text;
                    grad.DrzavaId = int.Parse(cmbDrzave.SelectedValue.ToString());


                    if (!_id.HasValue)
                    {
                        await _grad.Insert<Model.Grad>(grad);

                        MessageBox.Show("Grad je uspješno dodan");
                    }
                    else
                    {
                        await _grad.Update<Model.Grad>(_id.Value, grad);
                        MessageBox.Show("Grad je uspješno izmjenjen");

                    }

                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Grad", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void txtNaziv_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNaziv.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(txtNaziv, Properties.Resources.Upozorenje);
            }else if (!Regex.IsMatch(txtNaziv.Text, @"^[a-zA-Z -]+$"))
            {
                e.Cancel = true;
                errorProvider.SetError(txtNaziv, Properties.Resources.NeispravanFormat);
            }
            else
            {
                errorProvider.SetError(txtNaziv,null);
            }
        }

        private void cmbDrzave_Validating(object sender, CancelEventArgs e)
        {
            if (cmbDrzave.SelectedIndex == -1)
            {
                e.Cancel = true;
                errorProvider.SetError(cmbDrzave, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider.SetError(cmbDrzave, null);
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
