using RetoqueFacialAI.Form;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace RetoqueFacialAI
{
    public class APIDownload
    {
        public static void RequestAPIRetoqueFacialDownlod(string nomeUpload, string nomeArquivo)
        {
            string urlArquivo = string.Format("http://access.bgeraser.com:8889/results/{0}.jpg", nomeUpload);
            

            var caminho = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Img";

            if (!Directory.Exists(caminho))
            {
                Directory.CreateDirectory(caminho);
            }

            string caminhoArquivo = caminho + "\\"+ nomeArquivo + ".jpg";

            WebClient client = new System.Net.WebClient();
            client.DownloadFile(urlArquivo, caminhoArquivo);
        }
    }
}
