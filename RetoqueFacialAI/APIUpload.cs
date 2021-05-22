using RetoqueFacialAI.Form;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Threading;

namespace RetoqueFacialAI
{
    public class APIUpload
    {
        public static List<DTOarquivos> RequestAPIRetoqueFacial(List<FileInfo> arquivos)
        {
            List<DTOarquivos> ArquivosEnviadosAPI = new List<DTOarquivos>();
            List<FileInfo> ArquivosErroAPI = new List<FileInfo>();

            foreach (var arquivo in arquivos)
            {
                try
                {                    
                    FileStream fs = new FileStream(arquivo.FullName, FileMode.Open, FileAccess.Read);
                    byte[] data = new byte[fs.Length];
                    fs.Read(data, 0, data.Length);
                    fs.Close();

                    
                    Dictionary<string, object> postParameters = new Dictionary<string, object>();
                    postParameters.Add("Alg", "slow");
                    postParameters.Add("scaleRadio", "2");
                    postParameters.Add("myfile", new FormUpload.FileParameter(data, arquivo.Name, "image/jpeg"));

                    
                    string postURL = "https://access.bgeraser.com:6708/upload";
                    string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.128 Safari/537.36";
                    HttpWebResponse webResponse = FormUpload.MultipartFormDataPost(postURL, userAgent, postParameters);

                    
                    StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                    string fullResponse = responseReader.ReadToEnd();
                    webResponse.Close();
                    //Response.Write(fullResponse);

                    Console.WriteLine(string.Format("{0} - {1}", fullResponse, arquivo.Name));

                    DTOarquivos ArquivoDTO = new DTOarquivos
                    {
                        Arquivo = arquivo,
                        NomeArquivoAPI = fullResponse
                    };

                    ArquivosEnviadosAPI.Add(ArquivoDTO);
                }
                catch
                {
                    ArquivosErroAPI.Add(arquivo);
                    Console.WriteLine(string.Format("{0} - {1}", arquivo.Name, "erro"));
                }            
                
            }

            if (ArquivosErroAPI.Count() > 0)
            {
                ArquivosEnviadosAPI.AddRange(RequestAPIRetoqueFacial(ArquivosErroAPI));
            }

            return ArquivosEnviadosAPI;
        }
        
        public class DTOarquivos
        {
            public FileInfo Arquivo { get; set; }
            public string NomeArquivoAPI { get; set; }
        }
    }
}
