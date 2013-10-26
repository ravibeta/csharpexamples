using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Clusterer;
using Clusterer.Model;

namespace ClustererTest
{
    [TestClass]
    public class ClustererTests
    {
        private List<Doggie> dogs = new List<Doggie>() {
            new Doggie() { Id = 1, Height = 1, Weight = 3 },
            new Doggie() { Id = 1, Height = 2, Weight = 4 },
            new Doggie() { Id = 1, Height = 3, Weight = 5 },
            new Doggie() { Id = 1, Height = 7, Weight = 7 },
            new Doggie() { Id = 1, Height = 8, Weight = 8 },
            new Doggie() { Id = 1, Height = 9, Weight = 9 },
            new Doggie() { Id = 1, Height = 1, Weight = 8 },
            new Doggie() { Id = 1, Height = 1, Weight = 7 },
            new Doggie() { Id = 1, Height = 2, Weight = 9 },
            new Doggie() { Id = 1, Height = 3, Weight = 8 },
        };

        [TestMethod]
        public void TestMethod1()
        {
            var clusterer = new Clusterer.Clusterer();
            clusterer.Doggies = dogs;
            var doggies = clusterer.Classify();
            Assert.IsTrue(doggies.ElementAt(0).Label == Label.Chihuahuas);
            Assert.IsTrue(doggies.ElementAt(1).Label == Label.Chihuahuas);
            Assert.IsTrue(doggies.ElementAt(2).Label == Label.Chihuahuas);
            Assert.IsTrue(doggies.ElementAt(3).Label == Label.Beagles);
            Assert.IsTrue(doggies.ElementAt(4).Label == Label.Beagles);
            Assert.IsTrue(doggies.ElementAt(5).Label == Label.Beagles);
            Assert.IsTrue(doggies.ElementAt(6).Label == Label.Daschunds);
            Assert.IsTrue(doggies.ElementAt(7).Label == Label.Daschunds);
            Assert.IsTrue(doggies.ElementAt(8).Label == Label.Daschunds);
            Assert.IsTrue(doggies.ElementAt(9).Label == Label.Daschunds);
        }
    }
}
