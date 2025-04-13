using System;
using tpmodul8_103022300081;

namespace tpmodul8_103022300081
{
    class Program
    {
        static void Main(string[] args)
        {
            CovidConfig config = new CovidConfig();

            Console.WriteLine($"Berapa suhu badan anda saat ini? Dalam nilai <{config.GetConfig("satuan_suhu")}>");
            string suhuInput = Console.ReadLine();

            Console.WriteLine($"Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala demam?");
            string hariInput = Console.ReadLine();

            bool suhuValid = false;
            bool hariValid = false;

            if (config.GetConfig("satuan_suhu") == "celcius")
            {
                double suhu;
                if (double.TryParse(suhuInput, out suhu))
                {
                    suhuValid = (suhu >= 36.5 && suhu <= 37.5);
                }
            }
            else if (config.GetConfig("satuan_suhu") == "fahrenheit")
            {
                double suhu;
                if (double.TryParse(suhuInput, out suhu))
                {
                    suhuValid = (suhu >= 97.7 && suhu <= 99.5);
                }
            }

            int hari;
            if (int.TryParse(hariInput, out hari))
            {
                hariValid = (hari < int.Parse(config.GetConfig("batas_hari_demam")));
            }

            if (suhuValid && hariValid)
            {
                Console.WriteLine(config.GetConfig("pesan_diterima"));
            }
            else
            {
                Console.WriteLine(config.GetConfig("pesan_ditolak"));
            }

            Console.WriteLine("\nMengubah satuan suhu...");
            config.UbahSatuan();
            Console.WriteLine($"Satuan suhu sekarang: {config.GetConfig("satuan_suhu")}");

            Console.WriteLine("\nTekan tombol apapun untuk keluar...");
            Console.ReadKey();
        }
    }
}