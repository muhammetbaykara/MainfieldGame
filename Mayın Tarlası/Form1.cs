using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayın_Tarlası
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Mayin_Tarlasi mayin_tarlamiz;
        List<Mayin> mayinlarimiz;
        bool gameOver = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde yapılacak işlemleri burada tanımlayabilirsiniz.
            mayin_tarlamiz = new Mayin_Tarlasi(new Size(400, 400), 60);
            panel1.Size = mayin_tarlamiz.buyuklugu;
            Mayin_ekle();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (mayin_tarlamiz == null)
            {
                mayin_tarlamiz = new Mayin_Tarlasi(new Size(400, 400), 60);
                panel1.Size = mayin_tarlamiz.buyuklugu;
                Mayin_ekle();
            }
        }

        public void Mayin_ekle()
        {
            for (int x = 0; x < panel1.Width; x += 20)
            {
                for (int y = 0; y < panel1.Height; y += 20)
                {
                    Button_ekle(new Point(x, y));
                }
            }
        }

        public void Button_ekle(Point loc)
        {
            Button btn = new Button
            {
                Name = "mayin" + ";" + loc.X + ";" + loc.Y,
                Size = new Size(20, 20),
                Location = loc
            };
            btn.Click += new EventHandler(btn_Click);
            panel1.Controls.Add(btn);
        }

        public void btn_Click(object sender, EventArgs e)
        {
            if (gameOver) return;

            Button btn = sender as Button;
            Mayin myn = mayin_tarlamiz.mayin_al_loc(btn.Location);
            mayinlarimiz = new List<Mayin>();

            if (myn.mayin_var_mi)
            {
                // Mayın patladığında yapılacak işlemler
                btn.BackColor = Color.Red;  // Patlayan mayının rengini kırmızı yapıyoruz
                MessageBox.Show("BOOM! Mayın Patladı!");
                gameOver = true;

                // Tüm mayınları gizleyelim ve patlayanları gösterelim
                foreach (var item in mayin_tarlamiz.GetAllMayin)
                {
                    Button btnx = panel1.Controls.Find(item.konum_al.X + " " + item.konum_al.Y, false).FirstOrDefault() as Button;
                    if (btnx != null)
                    {
                        if (item.mayin_var_mi)
                        {
                            btnx.BackColor = Color.Red;  // Patlayan mayınları kırmızı yapıyoruz
                        }
                        btnx.Enabled = false;  // Tüm butonları devre dışı bırakıyoruz
                    }
                }
            }
            else
            {
                int mayinSayisi = etrafta_kac_mayin_var(myn);
                if (mayinSayisi == 0)
                {
                    btn.Enabled = false; // Butonu devre dışı bırakıyoruz
                    cevresindekileri_ekle(myn);
                }
                else
                {
                    btn.Text = mayinSayisi.ToString();
                    btn.Enabled = false; // Butonu devre dışı bırakıyoruz
                }
            }
        }

        public int etrafta_kac_mayin_var(Mayin m)
        {
            int sayi = 0;
            Point[] directions = new Point[]
            {
                new Point(-20, 0), new Point(20, 0), new Point(0, -20), new Point(0, 20),
                new Point(-20, -20), new Point(20, -20), new Point(-20, 20), new Point(20, 20)
            };

            foreach (var direction in directions)
            {
                Mayin nearbyMayin = mayin_tarlamiz.mayin_al_loc(new Point(m.konum_al.X + direction.X, m.konum_al.Y + direction.Y));
                if (nearbyMayin.mayin_var_mi) sayi++;
            }

            return sayi;
        }

        public void cevresindekileri_ekle(Mayin m)
        {
            Point[] directions = new Point[]
            {
                new Point(-20, 0), new Point(20, 0), new Point(0, -20), new Point(0, 20),
                new Point(-20, -20), new Point(20, -20), new Point(-20, 20), new Point(20, 20)
            };

            foreach (var direction in directions)
            {
                Mayin nearbyMayin = mayin_tarlamiz.mayin_al_loc(new Point(m.konum_al.X + direction.X, m.konum_al.Y + direction.Y));
                if (!mayinlarimiz.Contains(nearbyMayin) && nearbyMayin != null)
                    mayinlarimiz.Add(nearbyMayin);
            }
        }
    }
}
