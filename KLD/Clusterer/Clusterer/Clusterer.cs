using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clusterer
{

    // TODO: Add pairwise term co-occurrence clustering
    // Step1 : Preprocessing and stemming
    // Step2 : Selection of frequent terms
    // Step3 : Clustering frequent terms
    // Step4 : Calculation of expected probability
    // Step5 : Calculation of chi-square value
    // Step6 : Output Keywords
    public class Clusterer
    {
        public List<Document> Documents { get; set; }
        private Document newsCentroid { get; set; }
        private Document reviewsCentroid { get; set; }
        private Document editorialCentroid { get; set; }
        private Distance distance { get; set; }

        public List<Document> Classify()
        {
            if (Documents == null || Documents.Count() < 3) return Documents;
            distance = new Distance();

            Document oldNewsCentroid = null;
            Document oldReviewsCentroid = null;
            Document oldEditorialCentroid = null;

            // initial assignment- Three clusters seeds
            // seed
            newsCentroid = Documents.FirstOrDefault();
            newsCentroid.Label = Label.news;
            reviewsCentroid = Documents.ElementAt(Documents.Count()/2);
            reviewsCentroid.Label = Label.reviews;
            editorialCentroid = Documents.ElementAt(Documents.Count() - 1);
            editorialCentroid.Label = Label.editorials;


            AssignLabel();

            while (oldNewsCentroid != newsCentroid || oldReviewsCentroid != reviewsCentroid || oldEditorialCentroid != editorialCentroid)
            {

                oldNewsCentroid = newsCentroid;
                oldReviewsCentroid = reviewsCentroid;
                oldEditorialCentroid = editorialCentroid;

                FixCentroids(Label.news);
                FixCentroids(Label.reviews);
                FixCentroids(Label.editorials);

                AssignLabel();
            }
            return Documents;
        }

        internal void AssignLabel()
        {
            Documents.ForEach(x =>
            {
                var distances = new List<double>() {   distance.GetNormalizedDistance(x, newsCentroid), 
                                                        distance.GetNormalizedDistance(x, reviewsCentroid), 
                                                        distance.GetNormalizedDistance(x, editorialCentroid) };
                if (distances[0] == distances.Min()) x.Label = Label.news;
                else if (distances[1] == distances.Min()) x.Label = Label.reviews;
                else x.Label = Label.editorials;

                FixCentroids(x.Label);

            });
        }

        internal void FixCentroids(Label category)
        {
            var measure = new List<Double>();
            var members = Documents.Where(x => x.Label == category).ToList();
            int index = -1;
            switch (category)
            {
                case Label.reviews:
                    members.ForEach(x => measure.Add(distance.GetNormalizedDistance(x, reviewsCentroid)));
                    index = GetNewCentroidIndex(measure);
                    if (index > 0 && index < measure.Count)
                        reviewsCentroid = members[index];
                    break;

                case Label.news:
                    members.ForEach(x => measure.Add(distance.GetNormalizedDistance(x, newsCentroid)));
                    index = GetNewCentroidIndex(measure);
                    if (index > 0 && index < measure.Count)
                        newsCentroid = members[index];
                    break;

                case Label.editorials: members.ForEach(x => measure.Add(distance.GetNormalizedDistance(x, editorialCentroid)));
                    index = GetNewCentroidIndex(measure);
                    if (index > 0 && index < measure.Count)
                        editorialCentroid = members[index];
                    break;
            }
        }

        internal int GetNewCentroidIndex(List<double> measure)
        {
            var average = measure.Sum() / measure.Count();
            var delta = measure.Min(x => Math.Abs(x - average));
            var candidate = measure.FirstOrDefault(x => Math.Abs(x - (average + delta)) < 0.1d);
            var index = measure.IndexOf(candidate);
            return index;
        }
    }
}
