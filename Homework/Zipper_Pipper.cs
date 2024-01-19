//Assalomualeykum ustoz, uy ishini tekshirgani kirib ushbu habarni oqivotgan boses, 
//sizdan iltimos uy ishimmi ertaga ertalabdan tekshirin, soat 3 00 AM gacham tayor qib qoyaman!🙏
//hozircha chala...

using HomeOne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zipper_Pipper
{

    namespace HomeOne
    {
        public class Ziplavor
        {
            //faylni topish

            void FaylniQidir(List<string> Topilmalar, string Path, string FileOrFolder)
            {
                //faylarni tekshirib korish
                string[] Fayllar = Directory.GetFiles(Path);
                string[] Folders = Directory.GetDirectories(Path);

                foreach (string f in Fayllar)
                {
                    string[] GumondorFayl = f.Split('\\');
                    if (GumondorFayl[GumondorFayl.Length - 1] == FileOrFolder)
                        Topilmalar.Add(f);
                }
                foreach (string f in Folders)
                {
                    string[] GumondorFolder = f.Split("\\");
                    if (GumondorFolder[GumondorFolder.Length - 1] == FileOrFolder)
                        Topilmalar.Add(f);
                }

                //folderlarni ichiga kirish

                foreach (string f in Folders)
                {
                    //bir xil folderlarga murojat qilib bolmaydi shuning uchun exception qaytaradi
                    try
                    {
                        FaylniQidir(Topilmalar, f, FileOrFolder);
                    }
                    catch { }
                }

            }


            public Ziplavor(string FileOrFolder)
            {



                //Rootlarni aniqlash
                List<string> Topilmalar = new List<string>();
                DriveInfo[] Disklar = DriveInfo.GetDrives();
                for (int i = 0; i < Disklar.Length; i++)
                {
                    FaylniQidir(Topilmalar, Convert.ToString(Disklar[i])!, FileOrFolder);
                }

                Console.WriteLine("\nNatijalar:");
                foreach (string f in Topilmalar)
                    Console.WriteLine(f);

                //Abu Programmiy
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Ziplavor hello =new Ziplavor("files.txt");  

        }
    }
}
