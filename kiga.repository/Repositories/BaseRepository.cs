using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using kiga.domain.Contracts;
using kiga.domain.Entities;
using kiga.repository.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace kiga.repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        private readonly kigaContexto _dbContext;
        private string UsuarioDomain;

        public BaseRepository(kigaContexto kigacontexto)
        {
            _dbContext = kigacontexto;
        }
        public int Atualizar(T dados)
        {
            try
            {
                _dbContext.Set<T>().Update(dados);
                return _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public T BuscarId(int id)
        {
            try
            {
                var chavePrimaria = _dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties[0];
                return _dbContext.Set<T>().FirstOrDefault(e => EF.Property<int>(e, chavePrimaria.Name) == id);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public T BuscarFacebookId(string id)
        {
            try
            {
                var facebookId = _dbContext.Model.FindEntityType(typeof(UsuarioDomain)).FindProperty("facebookId");
                return _dbContext.Set<T>().FirstOrDefault(e => EF.Property<string>(e, facebookId.Name) == id);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Deletar(T dados)
        {
            try
            {
                _dbContext.Set<T>().Remove(dados);
                return _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Inserir(T dados)
        {
            try
            {
                _dbContext.Set<T>().Add(dados);
                return _dbContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<T> Listar()
        {
            try
            {
                return _dbContext.Set<T>().ToList();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Teste(string msg)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.CreateHttp("https://api.wit.ai/message?v=6/3/2018&q="+msg);

            WebReq.Method = "GET";
            WebReq.Headers.Add("Authorization", "Bearer VKULQK77FU3ERLHXTFJZJA6ZNKMV7P7V");

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            Console.WriteLine(WebResp.StatusCode);
            Console.WriteLine(WebResp.Server);

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();



                /* JObject o = JObject.Parse(jsonString);

                var jsonestados = (JArray)o["estados"];

                for (int i = 0; i < jsonestados.Count; i++)
                {
                    var sigla = jsonestados[i].SelectToken("sigla");
                    var nome = jsonestados[i].SelectToken("nome");
                } */
                return jsonString;

            }
        }


        /* 
                public async Task WitGet(string msg, string token)
                {
                    string baseURL = "http://mwolfhoffman.com";
                     string baseURL = "http://api.wit.ai/message"; 

                    using (HttpClient client = new HttpClient())

                    using (HttpResponseMessage res = await client.GetAsync(baseURL))

                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if(data != null){
                            return data;
                        }
                    } */
    }
}