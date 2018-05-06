using Demo.DAL.Context;
using Demo.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Demo.DAL.Repository
{
    public class GenericRepository<TModel> where TModel : BaseModel, new()
    {
        private readonly DemoContext _ctx;

        public GenericRepository() // should be used for single entity update
        {
            _ctx = new DemoContext();
        }

        public GenericRepository(DemoContext ctx) // should be used for multi-entity update (shared context)
        {
            _ctx = ctx;
        }

        public IEnumerable<TModel> GetEntities()
        {
            List<TModel> modelList = _ctx.Entities<TModel>().ToList();
            return modelList;
        }

        public T UnProxy<T>(T proxyObject) where T : class
        {
            var proxyCreationEnabled = _ctx.Configuration.ProxyCreationEnabled;

            try
            {
                _ctx.Configuration.ProxyCreationEnabled = false;
                T poco = _ctx.Entry(proxyObject).CurrentValues.ToObject() as T;
                return poco;
            }
            finally
            {
                _ctx.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
            }
        }

        public IEnumerable<TOtherModel> GetEntities<TOtherModel>()
            where TOtherModel : class
        {
            List<TOtherModel> modelList = _ctx.Entities<TOtherModel>().ToList();
            return modelList;
        }

        public IEnumerable<TModel> GetEntitiesInclude(params string[] path)
        {
            var inludes = path.ToList();

            DbQuery<TModel> query;
            if (inludes.Count == 0)
            {
                query = _ctx.Entities<TModel>();
            }
            else if (inludes.Count == 1)
            {
                query = _ctx.Entities<TModel>().Include(inludes[0]);
            }
            else
            {
                query = _ctx.Entities<TModel>();
                inludes.ForEach(x => { query = query.Include(x); });
            }

            List<TModel> modelList = query.ToList();
            return modelList;
        }

        public void AddOrUpdate(TModel model)
        {
            if (model.Id <= 0)
            {
                _ctx.Entities<TModel>().Add(model);
            }
            else
            {
                _ctx.Entry(model).State = EntityState.Modified;
            }
        }

        public void UpdatePartially(TModel model, PartialUpdateEnum updateType, params string[] propertiesToUpdate)
        {
            if (propertiesToUpdate == null)
            {
                throw new NullReferenceException("Parameter propertiesToUpdate can not be null.");
            }

            if (updateType == PartialUpdateEnum.IncludeProperties)
            {
                _ctx.Entities<TModel>().Attach(model);
            }
            else
            {
                _ctx.Entry(model).State = EntityState.Modified;
            }

            foreach (var property in propertiesToUpdate)
            {
                _ctx.Entry(model).Property(property).IsModified = updateType == PartialUpdateEnum.IncludeProperties;
            }
        }

        public void Delete(int modelId)
        {
            var model = new TModel { Id = modelId };
            _ctx.Entities<TModel>().Attach(model);
            _ctx.Entities<TModel>().Remove(model);
            _ctx.SaveChanges();
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }
    }
}
