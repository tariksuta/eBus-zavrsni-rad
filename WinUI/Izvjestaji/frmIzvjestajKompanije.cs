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
    public partial class frmIzvjestajKompanije : Form
    {
        private readonly APIService _kompanijeService = new APIService("Kompanija");
        private readonly APIService _cijenaService = new APIService("Cijena");
        private readonly APIService _rezervacijaService = new APIService("Rezervacija");
        private readonly APIService _voziloService = new APIService("Vozilo");
        private readonly APIService _angazujeService = new APIService("Angazuje");
        private readonly APIService _linijeService = new APIService("Linija");

        List<Model.IzvjestajRelacija> GlavnaLista = new List<Model.IzvjestajRelacija>();

        decimal ukupno = 0;
        public frmIzvjestajKompanije()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            GlavnaLista = new List<IzvjestajRelacija>();
            ukupno = 0;
            var tmp = cmbKompanije.SelectedValue.ToString();

            if(int.TryParse(tmp, out int id))
            {
                await LoadIzvjestaje(id);
            }
        }

        private async void frmIzvjestajKompanije_Load(object sender, EventArgs e)
        {
            await LoadKompanije();
            await LoadIzvjestaje(null);
        }

        private async Task LoadKompanije()
        {
            var lista = await _kompanijeService.Get<List<Model.Kompanija>>(null);

            lista.Insert(0, new Model.Kompanija());
            cmbKompanije.DataSource = lista;
            cmbKompanije.ValueMember = "Id";
            cmbKompanije.DisplayMember = "Naziv";
            cmbKompanije.SelectedText = "--odaberi kompaniju--";
        }

        private async Task LoadIzvjestaje(int? kompanijaId = null)
        {
            var listaRezervacija = await _rezervacijaService.Get<List<Model.Rezervacija>>(null);

            var listaKarata = listaRezervacija.Where(l => l.Otkazana == false).Select(x => x.Karta).Cast<Model.Karta>().ToList();
            var listaPutnika = listaRezervacija.Where(l => l.Otkazana == false).Select(x => x.Putnik).Cast<Model.Putnik>().ToList();

           
            int brojac = 0;
            foreach (var item in listaKarata)
            {
                var ang = await _angazujeService.GetById<Model.Angazuje>(item.AngazujeId);

                

                var searchCijena = new CijenaSearchRequest()
                {
                    LinijaID = ang.LinijaId,
                    KompanijaID = ang.Vozilo.KompanijaId
                };

                var listaCijena = await _cijenaService.Get<List<Model.Cijena>>(searchCijena);


                var v = await _voziloService.GetById<Model.Vozilo>(ang.VoziloId);
                var l = await _linijeService.GetById<Model.Linija>(ang.LinijaId);
                Model.Kompanija k = null;

                var request = new IzvjestajRelacija();

                decimal zbir = 0;
                if (kompanijaId.HasValue)
                {
                    if (v.KompanijaId == kompanijaId.Value)
                    {

                        k = await _kompanijeService.GetById<Model.Kompanija>(kompanijaId.Value);

                    }
                }
                else
                {
                    k = await _kompanijeService.GetById<Model.Kompanija>(ang.Vozilo.KompanijaId.Value);
                }

                if(k!= null)
                {
                    request.BrojKarte = item.BrojKarte;
                    request.DatumIzdavanja = item.DatumIzdavanja.ToString("dd.MM.yyyy");
                    request.Kompanija = k.Naziv;
                    request.Linija = l.Naziv;
                    request.Vozilo = v.Model;


                    if (DateTime.Now.Date.Year - listaPutnika[brojac].DatumRodjenja.Value.Date.Year > 50)
                    {
                        zbir += listaCijena[0].Popust == 0 ? listaCijena[0].Iznos : listaCijena[0].Iznos - (listaCijena[0].Iznos * decimal.Parse(listaCijena[0].Popust.ToString()));
                    }
                    else
                    {
                        zbir += listaCijena[0].Iznos;
                    }

                    request.Cijena = zbir.ToString();

                    GlavnaLista.Add(request);

                    ukupno += zbir;
                    brojac++;
                }
             
            }

            dgvPodaci.DataSource = GlavnaLista;
            txtUkupno.Text = Math.Round(ukupno,2).ToString();
        }

        public void kreirajPDF(DataGridView dgv, string nazivFajla)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.EMBEDDED);
            PdfPTable pdfTable = new PdfPTable(dgv.Columns.Count);

            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;

            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);

            //header
            foreach (DataGridViewColumn item in dgv.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(item.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            // redovi
            foreach (DataGridViewRow item in dgv.Rows)
            {
                foreach  (DataGridViewCell cell in item.Cells)
                {
                    pdfTable.AddCell(new Phrase(cell.Value.ToString(), text));
                }
            }

            var saveFileDialog = new SaveFileDialog();

            saveFileDialog.FileName = nazivFajla;
            saveFileDialog.DefaultExt = ".pdf";

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(pdfTable);
                    pdfdoc.Close();
                    stream.Close();
                }
            }
        }

        private void btnPreuzmi_Click(object sender, EventArgs e)
        {
            kreirajPDF(dgvPodaci, "IzvjestajKompanije");
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
