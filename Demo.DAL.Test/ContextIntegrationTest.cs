using Demo.DAL.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Demo.DAL.Test
{
    [TestClass]
    public class ContextIntegrationTest
    {
        [TestMethod]
        public void CreateDbSchema()
        {
            using (var ctx = new DemoContext())
            {
                var human = new Model.Human
                {
                    Id = 0,
                    Description = "Description",
                    Name = "Name"
                };

                try
                {
                    // ctx.Entities<Model.Human>().Add(human);
                    ctx.Humans.Add(human);
                    ctx.Save();
                }
                catch (Exception e)
                {
                    var x = 1;
                }
            }
        }

        [TestMethod]
        public void TestQueryForAddAndRemoveSameEntity()
        {
            using (var ctx = new DemoContext())
            {
                var human = new Model.Human
                {
                    Id = 0,
                    Description = "Description",
                    Name = "Name"
                };

                try
                {
                    ctx.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                    // Case Initial: test logging
                    var blog = ctx.Humans.FirstOrDefault(x => x.Name == "Name");
                    ctx.SaveChangesAsync().Wait();

                    // Case 1: add only
                    ctx.Humans.Add(human);
                    ctx.Save();

                    // Case 2: remove only
                    ctx.Humans.Remove(human);
                    ctx.Save();

                    // Case 3: nothing will happen when whe add and remove same entity
                    ctx.Humans.Add(human);
                    ctx.Humans.Remove(human);
                    ctx.Save();
                }
                catch (Exception e)
                {
                    var x = 1;
                }
            }
        }
    }
}
