This is how Google's page rank is supposed to work.
To apply this to keyword extraction, we could use WordNet C# for semantic relationship.

SemanticSimilarity semsim=new SemanticSimilarity() ;
    float score=semsim.GetScore(word1, word2);
                               
Here is a link for the same : http://www.codeproject.com/Articles/11835/WordNet-based-semantic-similarity-measurement 
ScoreSum <- 0;

foreach (X[i] in X){
  bestCandidate <- -1;
  bestScore <- -maxInt;
  foreach (Y[j] in Y){ 
    if (Y[j] is still free && r[i, j] > bestScore){
        bestScore <- R[i, j]; 
        bestCandidate <- j;                
      }  
  }

  if (bestCandidate != -1){
      mark the bestCandidate as matched item.
      scoreSum <- scoreSum + bestScore;
  }
}
