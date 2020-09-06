using eBus.Model.Requests;
using Microsoft.Reporting.WinForms;
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

namespace eBus.WinUI.Izvjestaji
{
    public partial class frmKarteIzvjestaj : Form
    {
        private readonly APIService _rezervacijeService = new APIService("Rezervacija");
        private readonly APIService _cijenaService = new APIService("Cijena");
        private readonly APIService _linijeService = new APIService("Linija");
        private readonly APIService _voziloService = new APIService("Vozilo");
        private readonly APIService _angazujeService = new APIService("Angazuje");
       

        private int? _id;
        public frmKarteIzvjestaj(int? putnikID = null)
        {
            InitializeComponent();
            _id = putnikID;
        }

        private async void frmKarteIzvjestaj_Load(object sender, EventArgs e)
        {
            
            await LoadIzvjestaj();
            this.reportViewer1.RefreshReport();
        }
        int redova = 0;
        private async Task LoadIzvjestaj()
        {
            var search = new RezervacijaSearchRequest()
            {
                PoAngazmanu = false,
                PutnikId = _id.Value
            };
            var listaRezervacija = await _rezervacijeService.Get < List < Model.Rezervacija>>(search);

            ReportParameterCollection rpc = new ReportParameterCollection();


            List<object> lista = new List<object>();


            DSkarta.DataTable1DataTable tbl = new DSkarta.DataTable1DataTable();

            tbl.Rows.Clear();


            redova = 0;

            decimal ukupno = 0;

          

            foreach (var item in listaRezervacija)
            {
                DSkarta.DataTable1Row red = tbl.NewDataTable1Row();
                var a = await _angazujeService.GetById<Model.Angazuje>(item.Karta.AngazujeId);

                var searchCijena = new CijenaSearchRequest()
                {
                    LinijaID = a.LinijaId,
                    KompanijaID = a.Vozilo.KompanijaId
                };

                var listaCijena = await _cijenaService.Get<List<Model.Cijena>>(searchCijena);

              
                var v = await _voziloService.GetById<Model.Vozilo>(a.VoziloId);
                var l = await _linijeService.GetById<Model.Linija>(a.LinijaId);

                //lista.Add(new
                //{
                //    BrojKarte = item.Karta.BrojKarte,
                //    DatumIzdavanja = item.Karta.DatumIzdavanja,
                //    Vozilo = v.Model,
                //    Linija = l.Naziv,
                //    Cijena = c == null ? 0 : c.Iznos,
                //});
                red.BrojKarte = item.Karta.BrojKarte;
                red.DatumIzdavanja = item.Karta.DatumIzdavanja;
                red.Vozilo = v.Model;
                red.Linija = l.Naziv;
                red.Cijena = listaCijena.Count == 0 ? 0 : listaCijena[0].Iznos;
                red.Kompanija = listaCijena.Count == 0 ? "N/N" : listaCijena[0].Kompanija.Naziv;

                tbl.Rows.Add(red);

                redova++;

                ukupno += listaCijena.Count == 0 ? 0 : listaCijena[0].Iznos;
            }

            rpc.Add(new ReportParameter("Ukupno", ukupno.ToString()));
            rpc.Add(new ReportParameter("DatumKreiranja", DateTime.Now.ToString("dd.MM.yyyy")));

            ReportDataSource rds = new ReportDataSource();
            rds.Name = "Karta";
            rds.Value = tbl;

            reportViewer1.LocalReport.SetParameters(rpc);
            reportViewer1.LocalReport.DataSources.Add(rds);


            this.reportViewer1.RefreshReport();
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
