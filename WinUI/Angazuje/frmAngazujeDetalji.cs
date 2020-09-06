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
using System.util;
using System.Windows.Forms;

namespace eBus.WinUI.Angazuje
{
    public partial class frmAngazujeDetalji : Form
    {
        private APIService _vozila = new APIService("Vozilo");
        private APIService _Linije = new APIService("Linija");
        private APIService _angazuje = new APIService("Angazuje");
        private int? _id;
        public frmAngazujeDetalji(int? id = null)
        {
            InitializeComponent();
            _id = id;
        }

        private async void frmAngazujeDetalji_Load(object sender, EventArgs e)
        {
            
             await LoadLinije();
             await LoadVozila();

            if (_id.HasValue)
            {
                var ang = await _angazuje.GetById<Model.Angazuje>(_id.Value);

               

                cmbLinije.SelectedValue = int.Parse(ang.LinijaId.ToString());
                cmbVozila.SelectedValue = int.Parse(ang.VoziloId.ToString());
                dtpDatumAngazovanja.Value = ang.DatumAngazovanja;
                dtpDatumIsteka.Value = ang.DatumIsteka;
            }
        }

     

        private async Task LoadVozila()
        {

            try
            {
                var lista = await _vozila.Get<List<Model.Vozilo>>(null);

                lista.Insert(0, new Model.Vozilo());

                cmbVozila.DataSource = lista;
                cmbVozila.ValueMember = "Id";
                cmbVozila.DisplayMember = "VoziloKompanija";
                cmbVozila.SelectedText = "--odaberi vozilo--";

            }
            catch (Exception)
            {

                throw;
            }

        }



        private async Task LoadLinije()
        {

          
                var lista = await _Linije.Get<List<Model.Linija>>(null);

                lista.Insert(0, new Model.Linija());
                cmbLinije.DataSource = lista;
                cmbLinije.ValueMember = "Id";
                cmbLinije.DisplayMember = "Naziv";
                cmbLinije.SelectedIndex = -1;
                cmbLinije.SelectedText = "--odaberi liniju--";


          

        }



        private async void btnSacuvaj_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValidateChildren())
                {
                    var element = new AngazujeUpsertRequest();

                    element.DatumAngazovanja = dtpDatumAngazovanja.Value;
                    element.DatumIsteka = dtpDatumIsteka.Value;

                    var linId = cmbLinije.SelectedValue;

                    if(int.TryParse(linId.ToString(), out int lid))
                    {
                        element.LinijaId = lid;
                    }
                    var vozId = cmbVozila.SelectedValue;
                    if(int.TryParse(vozId.ToString(), out int vid))
                    {
                        element.VoziloId = vid;
                    }
                  

                    if (!_id.HasValue)
                    {
                        await _angazuje.Insert<Model.Angazuje>(element);

                        MessageBox.Show("Angazman je uspjesno dodan");
                    }
                    else
                    {
                        await _angazuje.Update<Model.Angazuje>(_id.Value, element);
                        MessageBox.Show("Angazman je uspjesno izmjenjen");

                    }


                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cmbLinije_Validating(object sender, CancelEventArgs e)
        {
            if (cmbLinije.SelectedIndex == -1)
            {
                e.Cancel = true;
                errorProvider1.SetError(cmbLinije,Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(cmbLinije, null);
            }
        }

        private void cmbVozila_Validating(object sender, CancelEventArgs e)
        {
            if (cmbVozila.SelectedIndex == -1)
            {
                e.Cancel = true;
                errorProvider1.SetError(cmbVozila, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(cmbVozila, null);
            }
        }

        private void dtpDatumAngazovanja_Validating(object sender, CancelEventArgs e)
        {
            if(dtpDatumAngazovanja.Value == null)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpDatumAngazovanja, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(dtpDatumAngazovanja, null);
            }
        }

        private void dtpDatumIsteka_Validating(object sender, CancelEventArgs e)
        {
            if(dtpDatumIsteka.Value < dtpDatumAngazovanja.Value)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpDatumIsteka, "Datum isteka mora biti veci");
            }
            else
            {
                errorProvider1.SetError(dtpDatumIsteka, null);
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
