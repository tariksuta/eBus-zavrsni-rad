using eBus.Model;
using eBus.Model.Requests;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eBus.WinUI.Izvjestaji
{
    public partial class frmIzvjestajRelacije : Form
    {
        private readonly APIService _linijeService = new APIService("Linija");
        private readonly APIService _rezervacijaService = new APIService("Rezervacija");
        private readonly APIService _cijenaService = new APIService("Cijena");
        private readonly APIService _kompanijeService = new APIService("Kompanija");
        private readonly APIService _angazujeService = new APIService("Angazuje");
        decimal ukupno = 0;
        public frmIzvjestajRelacije()
        {
            InitializeComponent();
            
        }

        private async void frmIzvještajRelacije_Load(object sender, EventArgs e)
        {
            await LoadLinije();
            await LoadIzvjestaj(0, false);
        }

        private async Task LoadLinije()
        {
            var lista = await _linijeService.Get<List<Model.Linija>>(null);

            lista.Insert(0, new Model.Linija());

            cmbLinije.DataSource = lista;
            cmbLinije.ValueMember = "Id";
            cmbLinije.DisplayMember = "Naziv";
            cmbLinije.SelectedText = "--odaberi liniju--";
          
        }

        private async Task ProvjeriUsloveZaIzvjestaj(Model.Karta karta,int angazujeId, Model.Putnik putnik) {

            var ang = await _angazujeService.GetById<Model.Angazuje>(angazujeId);

            var kompanija = await _kompanijeService.GetById<Model.Kompanija>(ang.Vozilo.KompanijaId.Value);

            var cijena = await _cijenaService.Get<List<Model.Cijena>>(new CijenaSearchRequest()
            {
                KompanijaID = kompanija.Id,
                LinijaID = karta.Angazuje.LinijaId
            });

            var izvjestaj = new IzvjestajRelacija()
            {
                BrojKarte = karta.BrojKarte,
                DatumIzdavanja = karta.DatumIzdavanja.ToString("dd.MM.yyyy"),
                Linija = ang.Linija.Naziv,
                Kompanija = kompanija.Naziv,
                Vozilo = ang.Vozilo.Model

            };
            decimal zbir = 0;
            // poredi je li putnik zadovoljio uslove za popust ukoliko ga kompanija izdaje
            if (DateTime.Now.Date.Year - putnik.DatumRodjenja.Value.Date.Year > 50
              || DateTime.Now.Date.Year - putnik.DatumRodjenja.Value.Date.Year < 18)
            {
                izvjestaj.Cijena = cijena[0].Popust == 0 ? cijena[0].Iznos.ToString() : Math.Round((cijena[0].Iznos - (cijena[0].Iznos * decimal.Parse(cijena[0].Popust.Value.ToString()))),2).ToString();
                zbir +=  cijena[0].Popust == 0 ? cijena[0].Iznos : cijena[0].Iznos - (cijena[0].Iznos * decimal.Parse(cijena[0].Popust.ToString()));
            }
            else
            {
                izvjestaj.Cijena = cijena[0].Iznos.ToString();
                zbir += cijena[0].Iznos;
            }

            GlavnaLista.Add(izvjestaj);


            

            ukupno += Math.Round(zbir, 2);

        }
        List<IzvjestajRelacija> GlavnaLista = new List<IzvjestajRelacija>();
     
        private async Task LoadIzvjestaj(int linijaID,bool odreden)
        {
          

            var listaRezervacije = await _rezervacijaService.Get<List<Model.Rezervacija>>(null);

            var listakarata = listaRezervacije.Where(l => l.Otkazana == false).Select(x => x.Karta).Cast<Model.Karta>().ToList();
            var listaputnika = listaRezervacije.Where(l => l.Otkazana == false).Select(x => x.Putnik).Cast<Model.Putnik>().ToList();

            
            int brojac = 0;

            foreach (var item in listakarata)
            {
                if (odreden)
                {
                    if (item.Angazuje.LinijaId == linijaID)
                    {
                       await  ProvjeriUsloveZaIzvjestaj(item, item.AngazujeId, listaputnika[brojac]);
                    }
                   
                }
                else
                {

                    await ProvjeriUsloveZaIzvjestaj(item, item.AngazujeId, listaputnika[brojac]);
                }
                brojac++;

            }

            
            dgvPodaci.DataSource = GlavnaLista;
            txtUkupno.Text = ukupno.ToString();
        }

        private async void btnPretrazi_Click(object sender, EventArgs e)
        {
            ukupno = 0;
            GlavnaLista = new List<IzvjestajRelacija>();
            var klik = cmbLinije.SelectedValue.ToString();

            if(int.TryParse(klik, out int id))
            {
                await LoadIzvjestaj(id, true);
            }
        }

        public void exportGridToPdf(DataGridView dgw, string fileName)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdfptable = new PdfPTable(dgw.Columns.Count);

            pdfptable.DefaultCell.Padding = 3;
            pdfptable.WidthPercentage = 100;
            pdfptable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfptable.DefaultCell.BorderWidth = 1;

            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);

            //header
            foreach (DataGridViewColumn column in dgw.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdfptable.AddCell(cell);
            }


            //datarow
            foreach (DataGridViewRow row in dgw.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    pdfptable.AddCell(new Phrase(cell.Value.ToString(), text));
                }
            }

            

            var savefiledialoge = new SaveFileDialog();
            savefiledialoge.FileName = fileName;
            savefiledialoge.DefaultExt = ".pdf";
            if (savefiledialoge.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefiledialoge.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(pdfptable);
                    pdfdoc.Close();
                    stream.Close();
                }
            }
        }

        private void btnPreuzmi_Click(object sender, EventArgs e)
        {
            exportGridToPdf(dgvPodaci, "IzvjestajRelacije");
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
