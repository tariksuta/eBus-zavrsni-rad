﻿using eBus.Model;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eBus.WinUI.Novosti
{
    public partial class frmNovostiDetalji : Form
    {

        private APIService _korisnici = new APIService("Korisnici");
        private APIService _novosti = new APIService("Novosti");
        private APIService _notifikacije = new APIService("Notifikacije");
        private APIService _putnikNotifikacije = new APIService("PutnikNotifikacije");
        private APIService _putnici = new APIService("Putnik");
        private int? _id;
        public frmNovostiDetalji(int? id = null)
        {
            InitializeComponent();
            _id = id;
        }

        private async void frmNovostiDetalji_Load(object sender, EventArgs e)
        {
           
            if (_id.HasValue)
            {
                var novost = await _novosti.GetById<Model.Novosti>(_id.Value);

                txtNaslov.Text = novost.Naslov;
                txtSadrzaj.Text = novost.Sadrzaj;
                dtpDatumObjave.Value = novost.DatumObjave.Value;
                if(novost.Slika != null && novost.Slika.Length > 0)
                pbSlika.Image = Helper.ImageHelper.FromByteToImage(novost.Slika);
            }
        }

        NovostiUpsertRequest novost = new NovostiUpsertRequest();

        private async void btnSacuvaj_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateChildren())
                {
                  

                    novost.Naslov = txtNaslov.Text;
                    novost.Sadrzaj = txtSadrzaj.Text;
                    novost.DatumObjave = dtpDatumObjave.Value;

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

                    novost.KorisnikId = korisnik.Id;

                    if (!_id.HasValue)
                    {
                      var n=  await _novosti.Insert<Model.Novosti>(novost);

                        MessageBox.Show("Novost je dodana");

                        if(n != null)
                        {
                            NotifikacijaUpsertRequest notif = new NotifikacijaUpsertRequest();

                            notif.DatumSlanja = DateTime.Now;
                            notif.Naslov = novost.Naslov;
                            notif.NovostId = n.Id;
                            notif.Procitano = false;
                           var notifikaicija = await _notifikacije.Insert<Model.Notifikacije>(notif);

                            var listaPutnika = await _putnici.Get<List<Model.Putnik>>(null);

                            foreach (var item in listaPutnika)
                            {
                                await _putnikNotifikacije.Insert<PutnikNotifikacije>(new PutnikNotifikacijeUpsertRequest()
                                {
                                    NotifikacijaId = notifikaicija.Id,
                                    PutnikId = item.Id,
                                    Pregledana = false
                                });
                            }
                        }
                      
                    }
                    else
                    {
                        await _novosti.Update<Model.Novosti>(_id.Value, novost);
                    }
                   
                }
                

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Novosti detalji", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNaslov_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNaslov.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNaslov, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(txtNaslov, null);

            }
        }

        private void txtSadrzaj_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSadrzaj.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSadrzaj, Properties.Resources.Upozorenje);
            }
            else
            {
                errorProvider1.SetError(txtSadrzaj, null);

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filename = openFileDialog1.FileName;

                txtSlika.Text = filename;

                Image img = Image.FromFile(filename);

                pbSlika.Image = img;

                novost.Slika = ImageHelper.FromImageToByte(img);
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
               
                errorProvider1.SetError(pbSlika, "Uredu");
                
            }
        }
    }
}
