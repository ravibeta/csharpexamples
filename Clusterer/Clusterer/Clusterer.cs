using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clusterer.Model;

namespace Clusterer
{
    public class Clusterer
    {
        public List<Doggie> Doggies { get; set; }
        
        public List<Doggie> Classify() 
        {
            if (Doggies == null || Doggies.Count() < 3) return Doggies;

            // initial assignment- Three clusters seeds
            double minHeight = Doggies.Min(x => x.Height);
            double maxHeight = Doggies.Max(x => x.Height);
            double maxWeight = Doggies.Max(x => x.Weight);
            double minWeight = Doggies.Min(x => x.Weight);
 
            // seed
            var chihuahuaCentroid = Doggies.FirstOrDefault(x => x.Weight == minWeight);
            chihuahuaCentroid.Label = Label.Chihuahuas;
            var beaglesCentroid = Doggies.FirstOrDefault(x => x.Height == maxHeight);
            beaglesCentroid.Label = Label.Beagles;
            var daschundCentroid = Doggies.Where(x => x.Height - minHeight < 1 ).OrderBy(x => x.Weight).Last();
            daschundCentroid.Label = Label.Daschunds;

            
            AssignLabel(chihuahuaCentroid, beaglesCentroid, daschundCentroid);

            var chihuahuas = Doggies.Where(x => x.Label == Label.Chihuahuas).ToList();
            var daschunds = Doggies.Where(x => x.Label == Label.Daschunds).ToList();
            var beagles = Doggies.Where(x => x.Label == Label.Beagles).ToList();

            chihuahuaCentroid = new Doggie() { Height = chihuahuas.Sum(x => x.Height) / chihuahuas.Count, Weight = chihuahuas.Sum(x => x.Weight) / chihuahuas.Count , Label = Label.Chihuahuas};
            daschundCentroid = new Doggie() { Height = daschunds.Sum(x => x.Height) / daschunds.Count, Weight = daschunds.Sum(x => x.Weight) / daschunds.Count, Label = Label.Daschunds };
            beaglesCentroid = new Doggie() { Height = beagles.Sum(x => x.Height) / beagles.Count, Weight = beagles.Sum(x => x.Weight) / beagles.Count, Label = Label.Beagles };

            AssignLabel(chihuahuaCentroid, beaglesCentroid, daschundCentroid);

            return Doggies;
        }

        private double GetEuclideanDistance(Doggie x, Doggie y)
        {
            var distance = Math.Sqrt(
                (x.Height - y.Height) * (x.Height - y.Height) +
                (x.Weight - y.Weight) * (x.Weight - y.Weight));
            return distance;
        }

        private void AssignLabel(Doggie chihuahuaCentroid, Doggie beaglesCentroid, Doggie daschundCentroid )
        {
            Doggies.ForEach(x =>
            {
                var distances = new List<double> () {   GetEuclideanDistance(x, chihuahuaCentroid), 
                                                        GetEuclideanDistance(x, beaglesCentroid), 
                                                        GetEuclideanDistance(x, daschundCentroid) };
                if (distances.Any(d => d == 0) == false)
                {
                    if (distances[0] == distances.Min()) x.Label = Label.Chihuahuas;
                    else if (distances[1] == distances.Min()) x.Label = Label.Beagles;
                    else x.Label = Label.Daschunds;
                }
            });
        }

    }
}
