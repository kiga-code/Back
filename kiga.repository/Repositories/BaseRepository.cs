using System;
using System.Collections.Generic;
using System.Linq;
using kiga.domain.Contracts;
using kiga.domain.Entities;
using kiga.repository.Context;
using Microsoft.EntityFrameworkCore;


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