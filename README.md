csharpexamples
==============

FuzzySKWIC : This extracts keywords and categorizes documents.
1. choose n = 200 keywords based on IDF.
2. Compute document frequency vectors xi based on the occurance of ith term in documents.
3. Compute cik as the document frequency of the kth component of the ith cluster center vector.
4. compute the cosine based dissimilarity as 1/n - xjk.cik
5. 
