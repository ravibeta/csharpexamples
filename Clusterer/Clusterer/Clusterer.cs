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
        private Doggie chihuahuaCentroid { get; set; }
        private Doggie beaglesCentroid { get; set; }
        private Doggie daschundCentroid { get; set; }

        public List<Doggie> Classify() 
        {
            if (Doggies == null || Doggies.Count() < 3) return Doggies;

            // initial assignment- Three clusters seeds
            double minHeight = Doggies.Min(x => x.Height);
            double maxHeight = Doggies.Max(x => x.Height);
            double maxWeight = Doggies.Max(x => x.Weight);
            double minWeight = Doggies.Min(x => x.Weight);
 
            // seed
            chihuahuaCentroid = Doggies.FirstOrDefault(x => x.Weight == minWeight);
            chihuahuaCentroid.Label = Label.Chihuahuas;
            beaglesCentroid = Doggies.FirstOrDefault(x => x.Height == maxHeight);
            beaglesCentroid.Label = Label.Beagles;
            daschundCentroid = Doggies.Where(x => x.Height - minHeight < 1 ).OrderBy(x => x.Weight).Last();
            daschundCentroid.Label = Label.Daschunds;

            
            AssignLabel();

            FixCentroids(Label.Chihuahuas);
            FixCentroids(Label.Beagles);
            FixCentroids(Label.Daschunds);

            AssignLabel();

            return Doggies;
        }

        private double GetEuclideanDistance(Doggie x, Doggie y)
        {
            var distance = Math.Sqrt(
                (x.Height - y.Height) * (x.Height - y.Height) +
                (x.Weight - y.Weight) * (x.Weight - y.Weight));
            return distance;
        }

        private void AssignLabel()
        {
            Doggies.ForEach(x =>
            {
                var distances = new List<double> () {   GetEuclideanDistance(x, chihuahuaCentroid), 
                                                        GetEuclideanDistance(x, beaglesCentroid), 
                                                        GetEuclideanDistance(x, daschundCentroid) };
                if (distances[0] == distances.Min()) x.Label = Label.Chihuahuas;
                else if (distances[1] == distances.Min()) x.Label = Label.Beagles;
                else x.Label = Label.Daschunds;

                FixCentroids(x.Label);

            });
        }

        private void FixCentroids(Label category)
        {
            var members = Doggies.Where( x => x.Label == category).ToList();
            var centroid = new Doggie() { Height = members.Sum(x => x.Height) / members.Count, Weight = members.Sum(x => x.Weight) / members.Count , Label = category};
            switch (category)
            {
                case Label.Beagles:
                    beaglesCentroid = centroid;
                    break;

                case Label.Chihuahuas:
                    chihuahuaCentroid = centroid;
                    break;

                case Label.Daschunds:
                    daschundCentroid = centroid;
                    break;
            }
            
        }

    }
}
