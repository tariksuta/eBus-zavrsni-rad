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

namespace eBus.WinUI.Sjedista
{
    public partial class frmSjedistaDetalji : Form
    {
        private APIService _vozila = new APIService("Vozilo");
        private APIService _sjedista = new APIService("Sjediste");
        private int? _id;
       
        public frmSjedistaDetalji(int? id= null)
        {
            InitializeComponent();
            _id = id;
        }

        private async void frmSjedistaDetalji_Load(object sender, EventArgs e)
        {
            await LoadVozila();
         

            if (_id.HasValue)
            {
                var sjediste = await _sjedista.GetById<Model.Sjediste>(_id.Value);

                cmbVozila.SelectedValue = sjediste.VoziloId;

                txtKolona.Text = sjediste.Kolona.ToString();
                txtRed.Text = sjediste.Red.ToString();

            }
        }

        private async Task LoadVozila()
        {
            var lista = await _vozila.Get<List<Model.Vozilo>>(null);

            lista.Insert(0, new Model.Vozilo());
            cmbVozila.DataSource = lista;
            cmbVozila.DisplayMember = "Model";
            cmbVozila.ValueMember = "Id";
            cmbVozila.SelectedText = "--Odaberi vozilo--";
        }

        private async void btnSpasi_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValidateChildren())
                {
                    var sjediste = new SjedisteUpsertRequest();

                    sjediste.Red = int.Parse(txtRed.Text);
                    sjediste.Kolona = int.Parse(txtKolona.Text);
                    sjediste.VoziloId = int.Parse(cmbVozila.SelectedValue.ToString());

                    var searchSjedista = new SjedisteSearchRequest()
                    {
                        VoziloId = sjediste.VoziloId.Value
                    };

                    var listaSjedista = await _sjedista.Get<List<Model.Sjediste>>(searchSjedista);

                    var vozilo = await _vozila.GetById<Model.Vozilo>(sjediste.VoziloId.Value);

                    

                    if( listaSjedista.Count != 0 &&  vozilo.BrojSjedista == listaSjedista.Count)
                    {
                        MessageBox.Show("Dodali ste sva sjedista za vozilo");
                    }else
                    {
                        if (!await ProvjeriSjediste(sjediste))
                        {
                            await _sjedista.Insert<Model.Sjediste>(sjediste);

                          
                            MessageBox.Show("Dodali ste sjedište");
                        }
                        else
                        {
                            MessageBox.Show("Sjediste je vec dodano za to vozilo");
                        }
                    }      

                    
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Sjedista detalji", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private async Task<bool> ProvjeriSjediste(SjedisteUpsertRequest request)
        {

            var search = new SjedisteSearchRequest()
            {
                VoziloId = request.VoziloId.Value
            }; 
            var lista = await _sjedista.Get<List<Model.Sjediste>>(search);

            foreach (var item in lista)
            {
                if(request.Red == item.Red && request.Kolona == item.Kolona)
                {
                    return true;
                }
            }
            return false;
        }

        private void txtRed_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtRed.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtRed, Properties.Resources.Upozorenje);
            }else if(!Regex.IsMatch(txtRed.Text, @"[1-9]"))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtRed, Properties.Resources.NeispravanFormat);
            }
            else
            {
                errorProvider1.SetError(txtRed, null);
            }
        }

        private void txtKolona_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtKolona.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtKolona, Properties.Resources.Upozorenje);
            }
            else if (!Regex.IsMatch(txtKolona.Text, @"[1-9]"))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtKolona, Properties.Resources.NeispravanFormat);
            }
            else if (int.TryParse(txtKolona.Text, out int kolona)? kolona < 1 || kolona > 4 : true)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtKolona, "Ne pravilan unos broja kolona");
            }
            else
            {
                errorProvider1.SetError(txtKolona, null);
            }
        }

        private Model.Vozilo vozilo = null;
        private async void cmbVozila_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var tap = cmbVozila.SelectedValue.ToString();

                if(int.TryParse(tap, out int id))
                {
                  vozilo =  await _vozila.GetById<Model.Vozilo>(id);

                    if (vozilo != null)
                        txtBrojRedova.Text = (vozilo.BrojSjedista.Value / 4).ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cmbVozila_Validating(object sender, CancelEventArgs e)
        {
            if(cmbVozila.SelectedIndex == -1)
            {
                e.Cancel = true;
                errorProvider1.SetError(cmbVozila, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(cmbVozila, null);
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
