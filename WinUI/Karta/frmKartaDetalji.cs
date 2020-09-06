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

namespace eBus.WinUI.Karta
{
    public partial class frmKartaDetalji : Form
    {
        private APIService _angazuje = new APIService("Angazuje");
        private APIService _sjedista = new APIService("Sjediste");
        private APIService _karta = new APIService("Karta");
        private int? _id;
        public frmKartaDetalji(int? id = null)
        {
            InitializeComponent();
            _id = id;
        }

        private async void frmKartaDetalji_Load(object sender, EventArgs e)
        {
           await LoadAngazuje();
           await LoadSjedista(null);
            GenerisiBrojKarte(9, true);
            if (_id.HasValue)
            {
                var karta = await _karta.GetById<Model.Karta>(_id.Value);


                cmbAngazuje.SelectedValue = int.Parse( karta.AngazujeId.ToString());

                var ang = await _angazuje.GetById<Model.Angazuje>(karta.AngazujeId);

                if (ang != null)
                {
                    var search = new SjedisteSearchRequest()
                    {
                        VoziloId = ang.VoziloId
                    };

                    await LoadSjedista(search);
                }
                cmbSjediste.SelectedValue = int.Parse( karta.SjedisteId.ToString());
                txtBrojKarte.Text = karta.BrojKarte;
                dtpDatumIzdavanja.Value = karta.DatumIzdavanja;
                txtVrijemePolaska.Text = karta.VrijemePolaska.ToString();
                txtVrijemeDolaska.Text = karta.VrijemeDolaska.ToString();
                
               
            }
        }

        private async Task LoadAngazuje()
        {
            var lista = await _angazuje.Get<List<Model.Angazuje>>(null);

            cmbAngazuje.DisplayMember = "PodaciAngazovani";
            cmbAngazuje.ValueMember = "Id";
            cmbAngazuje.DataSource = lista;
            cmbAngazuje.SelectedIndex = -1;
            cmbAngazuje.SelectedText = "--Odaberi--";

        }

        private async Task LoadSjedista(SjedisteSearchRequest searchRequest = null)
        {
            var lista = await _sjedista.Get<List<Model.Sjediste>>(searchRequest);
            

            cmbSjediste.DisplayMember = "Mjesto";
            cmbSjediste.ValueMember = "Id";
            cmbSjediste.DataSource = lista;
            cmbSjediste.SelectedIndex = -1;
            cmbSjediste.SelectedText = "--Odaberi sjedište--";
        }
        private void GenerisiBrojKarte(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
            {
                txtBrojKarte.Text = $"{builder.ToString().ToLower()}-{ DateTime.Now.Year.ToString()}";
            }
            else
            {
                txtBrojKarte.Text = builder.ToString();
            }


        }

       
        private async void btnKreiraj_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateChildren())
                {
                    var karta = new KartaUpsertRequest();

                    karta.BrojKarte = txtBrojKarte.Text;
                    karta.DatumIzdavanja = dtpDatumIzdavanja.Value.Date;
                    karta.AngazujeId = int.Parse(cmbAngazuje.SelectedValue.ToString());
                    karta.SjedisteId = int.Parse(cmbSjediste.SelectedValue.ToString());

              
                    karta.VrijemePolaska = new TimeSpan(int.Parse(txtVrijemePolaska.Text.Substring(0,2)) , int.Parse(txtVrijemePolaska.Text.Substring(2,2)),0);
                    karta.VrijemeDolaska = new TimeSpan(int.Parse(txtVrijemeDolaska.Text.Substring(0, 2)), int.Parse(txtVrijemeDolaska.Text.Substring(2, 2)), 0);





                    if (!_id.HasValue)
                    {

                        if(await AngazmanSjedista(karta.AngazujeId, karta.SjedisteId, karta.VrijemePolaska)) // vrijeme polaska dodao da poredi
                        {
                            MessageBox.Show("Ovo sjediste ste vec dodali za liniju");
                        }
                        else
                        {
                            await _karta.Insert<Model.Karta>(karta);

                            MessageBox.Show("Karta je uspješno kreirana");

                            GenerisiBrojKarte(9, true);
                        }
                        
                    }
                    else
                    {
                        await _karta.Update<Model.Karta>(_id.Value, karta);
                    }

                   

                }
              

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Karta detalji", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task<bool> AngazmanSjedista(int ang, int sjed, TimeSpan vrmP)
        {
            var search = new KartaSearchRequest()
            {
                PoAngzuje = false,
                PoVremenu = true,
                VrijemePolaska = vrmP,
                DatumIzdavanja = dtpDatumIzdavanja.Value.Date,
                PoVozilu = false
            };

            var listaKarata = await _karta.Get<List<Model.Karta>>(search);

            foreach (var item in listaKarata)
            {
                if(item.AngazujeId == ang && item.SjedisteId == sjed)
                {
                    return true;
                }
            }

            return false;

        }

        private async void cmbAngazuje_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbAngazuje.SelectedItem != null)
            {
                var el = cmbAngazuje.SelectedValue.ToString();

                if (int.TryParse(el, out int id))
                {
                    var ang = await _angazuje.GetById<Model.Angazuje>(id);

                    if (ang != null)
                    {
                        var search = new SjedisteSearchRequest()
                        {
                            VoziloId = ang.VoziloId
                        };

                        await LoadSjedista(search);
                    }

                }
            }
           
        }

        private void dtpDatumIzdavanja_Validating(object sender, CancelEventArgs e)
        {
            if(dtpDatumIzdavanja.Value == null)
            {
                errorProvider1.SetError(dtpDatumIzdavanja, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(dtpDatumIzdavanja, null);
            }
        }

        private void cmbAngazuje_Validating(object sender, CancelEventArgs e)
        {
            if (cmbAngazuje.SelectedIndex == -1)
            {
                e.Cancel = true;
                errorProvider1.SetError(cmbAngazuje, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(cmbAngazuje, null);
            }
        }

        private void cmbSjediste_Validating(object sender, CancelEventArgs e)
        {
            if (cmbSjediste.SelectedIndex == -1)
            {
                e.Cancel = true;
                errorProvider1.SetError(cmbSjediste, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(cmbSjediste, null);
            }
        }

        private void txtVrijemePolaska_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtVrijemePolaska.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtVrijemePolaska, Properties.Resources.NeispravanFormat);
            }
            else
            {
                errorProvider1.SetError(txtVrijemePolaska, null);
            }
        }

        private void txtVrijemeDolaska_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtVrijemeDolaska.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtVrijemeDolaska, Properties.Resources.NeispravanFormat);
            }
            else
            {
                errorProvider1.SetError(txtVrijemeDolaska, null);
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
