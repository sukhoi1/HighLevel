using Demo.DAL.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
    }
}
