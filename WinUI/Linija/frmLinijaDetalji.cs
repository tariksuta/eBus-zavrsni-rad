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
    public partial class frmLinijaDetalji : Form
    {
        private APIService _gradovi = new APIService("Grad");
        private APIService _korisnici = new APIService("Korisnici");
        private APIService _linija = new APIService("Linija");

        private int? _id;
        public frmLinijaDetalji(int? id = null)
        {
            InitializeComponent();
            _id = id;
        }

        private async void frmLinijaDetalji_Load(object sender, EventArgs e)
        {
            await LoadPolaziste();
           await LoadOdrediste();
            

            if (_id.HasValue)
            {
                var linija = await _linija.GetById<Model.Linija>(_id.Value);

                txtNaziv.Text = linija.Naziv;
                cmbPolaziste.SelectedValue = int.Parse(linija.PolazisteId.ToString());
                cmbOdrediste.SelectedValue = int.Parse(linija.OdredisteId.ToString());
             
            }
        }

        private async Task LoadPolaziste()
        {
            var lista = await _gradovi.Get<List<Model.Grad>>(null);

            lista.Insert(0, new Model.Grad());
            cmbPolaziste.DataSource = lista;
            cmbPolaziste.DisplayMember = "Naziv";
            cmbPolaziste.ValueMember = "Id";
            cmbPolaziste.SelectedText = "--Odaberi grad--";

        
        }

        private async Task LoadOdrediste()
        {
            var lista = await _gradovi.Get<List<Model.Grad>>(null);

            lista.Insert(0, new Model.Grad());
            cmbOdrediste.DataSource = lista;
            cmbOdrediste.DisplayMember = "Naziv";
            cmbOdrediste.ValueMember = "Id";
            cmbOdrediste.SelectedText = "--Odaberi grad--";

        }
      

        private async void btnKreiraj_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValidateChildren())
                {
                    var linija = new LinijaUpsertRequest();

                    linija.Naziv = txtNaziv.Text;
                    linija.PolazisteId = int.Parse(cmbPolaziste.SelectedValue.ToString());
                    linija.OdredisteId = int.Parse(cmbOdrediste.SelectedValue.ToString());
                    

                    var search = new KorisniciSearchRequest()
                    {
                        KorisnickoIme = APIService.KorisnickoIme
                    };

                    var listaKorisnika = await _korisnici.Get<List<Model.Korisnici>>(search);

                    Model.Korisnici korisnik = null;

                    foreach (var item in listaKorisnika)
                    {
                        if(item.KorisnickoIme == APIService.KorisnickoIme)
                        {
                            korisnik = item;
                            break;
                        }
                    }

                    linija.KorisnikId = korisnik.Id;


                    if (!_id.HasValue)
                    {
                      var l =  await _linija.Insert<Model.Linija>(linija);

                        MessageBox.Show("Linija je uspješno kreirana, molimo unesite cijenu linije");

                        frmCijenaDetalji frm = new frmCijenaDetalji(l.Id);

                        frm.Show();
                    }
                    else
                    {
                        await _linija.Update<Model.Linija>(_id.Value, linija);
                    }
                }
              
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Linija detalji", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        //TREBA PROVJERITI VALIDACIJE KOMBOBOXOVA
        private void cmbPolaziste_Validating(object sender, CancelEventArgs e)
        {
            if(cmbPolaziste.SelectedIndex == -1)
            {
                e.Cancel = true;
                errorProvider1.SetError(cmbPolaziste, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(cmbPolaziste, null);
            }
        }

        private void cmbOdrediste_Validating(object sender, CancelEventArgs e)
        {
            if (cmbOdrediste.SelectedIndex == -1)
            {
                e.Cancel = true;
                errorProvider1.SetError(cmbOdrediste, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(cmbOdrediste, null);
            }
        }

        private void btn_Izadji_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSmanji_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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
