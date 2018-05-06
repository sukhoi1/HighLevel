using Demo.DAL.Context;
using Demo.DAL.Model;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Zest.DAL.Repository
{
    /// <summary>
    /// Trivial non-generic repository, no IDisposable implementation.
    /// </summary>
    public class HumanRepository
    {
        public IEnumerable<Human> GetProducts()
        {
            using (var ctx = new DemoContext())
            {
                return ctx.Humans.ToList();
            }
        }

        public void Add(Human human)
        {
            using (var ctx = new DemoContext())
            {
                ctx.Humans.Add(human);
                ctx.SaveChanges();
            }
        }

        public void Update(Human human)
        {
            using (var ctx = new DemoContext())
            {
                ctx.Humans.AddOrUpdate(human);
                ctx.SaveChanges();
            }
        }

        public void Delete(Human human)
        {
            using (var ctx = new DemoContext())
            {
                var attachedProduct = ctx.Entry(human);
                if (attachedProduct == null)
                {
                    ctx.Humans.Attach(human);
                }
                else
                {
                    ctx.Entry(human);
                }

                ctx.Humans.Remove(human);
                ctx.SaveChanges();
            }
        }
    }
}