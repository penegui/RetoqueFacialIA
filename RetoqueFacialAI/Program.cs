using RetoqueFacialAI.Form;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using static RetoqueFacialAI.APIUpload;
using System.Linq;

namespace RetoqueFacialAI
{
    class Program
    {
        static void Main(string[] args)
        {
            List<FileInfo> arquivos = Imagens.PastaImagens();

            List<DTOarquivos> ArquivosEnviadosAPI = APIUpload.RequestAPIRetoqueFacial(arquivos);

            Status(ArquivosEnviadosAPI);            
        }

        public static void Status(List<DTOarquivos> ArquivosEnviadosAPI)
        {
            List<FileInfo> ArquivosErro = new List<FileInfo>();

            foreach (var ArquivoEnviadoAPI in ArquivosEnviadosAPI)
            {
                try
                {
                    var status = "waiting";
                    int tentativas = 0;

                    while (status == "waiting")
                    {
                        status = APIStatus.RequestAPIRetoqueFacialStatus(ArquivoEnviadoAPI.NomeArquivoAPI);

                        if (status == "success")
                        {
                            APIDownload.RequestAPIRetoqueFacialDownlod(ArquivoEnviadoAPI.NomeArquivoAPI, ArquivoEnviadoAPI.Arquivo.Name.Split('.').FirstOrDefault());
                        }
                        else
                        {
                            tentativas++;
                        }

                        if (tentativas > 5)
                        {
                            ArquivosErro.Add(ArquivoEnviadoAPI.Arquivo);
                            break;
                        }
                    } 
                }
                catch
                {
                    ArquivosErro.Add(ArquivoEnviadoAPI.Arquivo);
                    Console.WriteLine(string.Format("{0} - {1}", ArquivoEnviadoAPI.NomeArquivoAPI, "erro"));
                }
            }

            if (ArquivosErro.Count() > 0)
            {
                Status(RequestAPIRetoqueFacial(ArquivosErro));
            }
        }

    }
}
