using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Mayın_Tarlası
{
    internal class Mayin_Tarlasi
    {
        Size buyukluk_;
        Dictionary<Point, Mayin> mayinlar;
        int dolu_mayin_sayisi;
        Random rnd = new Random();

        public Mayin_Tarlasi(Size buyukluk, int Mayin_Sayisi)
        {
            mayinlar = new Dictionary<Point, Mayin>();
            dolu_mayin_sayisi = Mayin_Sayisi;
            buyukluk_ = buyukluk;

            // Mayınları başlatmak
            for (int x = 0; x < buyukluk.Width; x += 20)
            {
                for (int y = 0; y < buyukluk.Height; y += 20)
                {
                    Mayin m = new Mayin(new Point(x, y));
                    Mayin_ekle(m);
                }
            }

            Mayinlari_doldur();
        }

        public void Mayin_ekle(Mayin m)
        {
            mayinlar[m.konum_al] = m;
        }

        private void Mayinlari_doldur()
        {
            int sayi = 0;
            while (sayi < dolu_mayin_sayisi)
            {
                int i = rnd.Next(0, mayinlar.Count);
                var item = mayinlar.Values.ElementAt(i);
                if (!item.mayin_var_mi)
                {
                    item.mayin_var_mi = true;
                    sayi++;
                }
            }
        }

        public Size buyuklugu
        {
            get { return buyukluk_; }
        }

        public Mayin mayin_al_loc(Point loc)
        {
            mayinlar.TryGetValue(loc, out Mayin mayin);
            return mayin ?? new Mayin(new Point(-100, -100)); // Geçersiz bir nokta döndür
        }

        public List<Mayin> GetAllMayin
        {
            get
            {
                return new List<Mayin>(mayinlar.Values);
            }
        }
    }
}
