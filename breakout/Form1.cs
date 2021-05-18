using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace breakout
{
    public partial class Form1 : Form
    {
        private Random rnd = new Random();
        public static string kAdi = "v for vandet";
        private bool yukariCikar = false;
        private bool panel2YeDegdi = false;
        private bool panel3EDegdi = false;
        private bool panel5EDegdi = false;
        private bool kirildi = false;
        private int puan = 0;
        Dictionary<Panel, PictureBox> myDict = default;
        public Form1()
        {
            InitializeComponent();
            Text = $"Tuğla Kırma Oyunu | Puan: {puan} //AR-GE by v for vandet";
            if (new KullaniciAdi().ShowDialog() != DialogResult.OK)
            {
                Environment.Exit(-1);
            }
            else
            {
                renkleriKaristir();
                GraphicsPath p = new GraphicsPath();
                p.AddEllipse(10, 10, panel1.Width - 10, panel1.Height - 10);
                panel1.Region = new Region(p);
                myDict = new Dictionary<Panel, PictureBox>()
                {
                    { panel6, pictureBox1 },
                    { panel7, pictureBox4 },
                    { panel8, pictureBox6 },
                    { panel9, pictureBox8 },
                    { panel10, pictureBox10 },
                    //
                    { panel15, pictureBox2 },
                    { panel14, pictureBox3 },
                    { panel13, pictureBox5 },
                    { panel12, pictureBox7 },
                    { panel11, pictureBox9 },
                    //
                    { panel20, pictureBox15 },
                    { panel19, pictureBox14 },
                    { panel18, pictureBox13 },
                    { panel17, pictureBox12 },
                    { panel16, pictureBox11 },
                };
                button1.Text = kAdi;
                timer1.Enabled = true;
            }
        }
        
        private void renkleriKaristir()
        {
            foreach (Control cntrl in Controls)
            {
                if (cntrl is PictureBox)
                {
                    cntrl.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                button1.Left -= 10;
            }
            if (e.KeyCode == Keys.D)
            {
                button1.Left += 10;
            }
        }
        private void oyunuBitir(string mesaj)
        {
            timer1.Stop();
            MessageBox.Show(mesaj, "Oyun sona erdi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (puan == 75)
            {
                oyunuBitir("Tebrikler, oyunu kazandınız.");
                return;
            }
            if (yukariCikar == false)
            {
                if (kirildi == true)
                {
                    if (panel2YeDegdi == false && panel3EDegdi == false && panel5EDegdi == false)
                    {
                        panel1.Left -= 5;
                    }
                    else
                    {
                        if (panel3EDegdi == false)
                        {
                            panel1.Left += 5;
                        }
                        else if (panel3EDegdi == true)
                        {
                            panel1.Left -= 5;
                        }
                    }
                }
                else if (kirildi == false)
                {
                    if (panel5EDegdi == false)
                    {
                        panel1.Left -= 5;
                    }
                    else
                    {
                        if (panel3EDegdi == false)
                        {
                            panel1.Left += 5;
                        }
                        else
                        {
                            panel1.Left -= 5;
                        }
                    }
                }
                panel1.Top += 5;
            }
            else if (yukariCikar == true)
            {
                if (kirildi == true)
                {
                    yukariCikar = false;
                    return;
                }
                panel1.Top -= 5;
                if (panel2YeDegdi == true)
                {
                    panel1.Left += 5;
                }
                else if (panel2YeDegdi == false)
                {
                    panel1.Left -= 5;
                }

            }
            if (panel1.Bounds.IntersectsWith(panel4.Bounds))
            {
                oyunuBitir("Kaybettiniz!!");
                return;
            }
            foreach (KeyValuePair<Panel, PictureBox> cntrl in myDict)
            {

                if (panel1.Bounds.IntersectsWith(cntrl.Key.Bounds))
                {
                    if (Controls.Contains(cntrl.Key))
                    {
                        puan += 5;
                        Text = $"Tuğla Kırma Oyunu | Puan: {puan} //AR-GE by v for vandet";
                        Controls.Remove(cntrl.Value);
                        Controls.Remove(cntrl.Key);
                        yukariCikar = true;
                        kirildi = false;
                        myDict.Remove(cntrl.Key);
                        break;
                    }
                }

            }
            if (panel1.Bounds.IntersectsWith(button1.Bounds))
            {
                yukariCikar = true;
                kirildi = false;
            }
            if (panel1.Bounds.IntersectsWith(panel2.Bounds))
            {
                panel2YeDegdi = true;
                panel3EDegdi = false;
            }
            if (panel1.Bounds.IntersectsWith(panel3.Bounds))
            {
                panel2YeDegdi = false;
                panel3EDegdi = true;
            }
            if (panel1.Bounds.IntersectsWith(panel5.Bounds))
            {
                yukariCikar = false;
                panel5EDegdi = true;
                kirildi = false;
            }
            foreach (Control cntrl in Controls)
            {
                if (cntrl is PictureBox)
                {
                    if (panel1.Bounds.IntersectsWith(cntrl.Bounds))
                    {
                        Controls.Remove(cntrl);
                        foreach (KeyValuePair<Panel, PictureBox> pcbx in myDict)
                        {
                            if (pcbx.Value.Name == cntrl.Name)
                            {

                                Controls.Remove(pcbx.Key); myDict.Remove(pcbx.Key); break;
                            }
                        }
                        kirildi = true;
                        puan += 5;
                        Text = $"Tuğla Kırma Oyunu | Puan: {puan} //AR-GE by v for vandet";
                    }
                }
            }
        }
    }
}
