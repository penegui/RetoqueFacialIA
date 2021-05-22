using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RetoqueFacialAI.Form
{
    public class Imagens
    {
        public static List<FileInfo> PastaImagens()
        {
            DirectoryInfo diretorio = new DirectoryInfo(@"C:\Users\USER\OneDrive\DeepFake\deepFake\DeepFaceLab_NVIDIA\workspace\data_dst\merged"); //caminho do diretório que contem as imagens

            List<FileInfo> arquivos = diretorio.GetFiles("*.*").ToList();

            return arquivos;            
        }
    }
}
