using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script;
using System.Web.Script.Serialization;

public class Test
{
    public static void Main()
    {
        var autocompleteResult = GetSuggestions("Headphones", "Wireless");
        var dobj = new JavaScriptSerializer().Serialize(autocompleteResult);
        Console.WriteLine(dobj.ToString());

        var searchRequest = GetSampleSearchRequest();
        var searchResult = GetSearchResult(searchRequest);
        dobj = new JavaScriptSerializer().Serialize(searchResult);
        Console.WriteLine(dobj.ToString());

        TestJsonSearchRequest();
        TestNativeSearchRequest();
        TestDistinct();
    }

    // field names as per json requested
    public class Product
    {
        public string brandId { get; set; }
        public string title { get; set; }
        public string productId { get; set; }
        public string brandName { get; set; }
        public string categoryId { get; set; }
        public string categoryName { get; set; }
    }
    
    public class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product p1, Product p2)
        {
            if (p2 == null && p1 == null)
                return true;
            else if (p1 == null || p2 == null)
                return false;
            else if (p1.brandId == p2.brandId)
                return true;
            else
                return false;
        }

        public int GetHashCode(Product p1)
        {
            int hCode = p1.brandId.GetHashCode()
                        ^ p1.brandName.GetHashCode()
                        ^ p1.title.GetHashCode()
                        ^ p1.productId.GetHashCode()
                        ^ p1.categoryName.GetHashCode()
                        ^ p1.categoryId.GetHashCode();
            return hCode.GetHashCode();
        }
    }

    public class Suggestion
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class AutocompleteResult
    {
        public String type { get; set; }
        public Suggestion[] suggestions { get; set; }
    }

    public class SearchResult
    {
        public Pagination pagination { get; set; }
        public Product[] products { get; set; }
    }

    public class Pagination
    {
        public int from { get; set; }
        public int size { get; set; }
    }

    public class Condition
    {
        public string fieldName { get; set; }
        public string[] values { get; set; }
    }

    public class SearchRequest
    {
        public Pagination pagination { get; set; }
        public Condition[] conditions { get; set; }
    }

    public static SearchResult GetSearchResult(SearchRequest request)
    {
        SearchResult result = new SearchResult() { pagination = request.pagination, products = new Product[] { } };
        var results = FromJson().AsEnumerable();
        var aggregatedResults = new List<Product>();
        foreach (var condition in request.conditions)
        {
            switch (condition.fieldName)
            {
                case "title":
                    StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase;
                    foreach (var value in condition.values)
                    {
                        var selected = results.Where(x => x.title.IndexOf(value, stringComparison) >= 0);
                        aggregatedResults.AddRange(selected);
                    }
                    break;
                case "brandId":
                    foreach (var value in condition.values)
                    {
                        var selected = results.Where(x => x.brandId == value);
                        aggregatedResults.AddRange(selected);
                    }
                    break;
                case "categoryId":
                    foreach (var value in condition.values)
                    {
                        var selected = results.Where(x => x.categoryId == value);
                        aggregatedResults.AddRange(selected);
                    }
                    break;
                case "productId":
                    foreach (var value in condition.values)
                    {
                        var selected = results.Where(x => x.productId == value);
                        aggregatedResults.AddRange(selected);
                    }
                    break;
                case "brandName":
                    foreach (var value in condition.values)
                    {
                        var selected = results.Where(x => x.brandName == value);
                        aggregatedResults.AddRange(selected);
                    }
                    break;
                case "categoryName":
                    foreach (var value in condition.values)
                    {
                        var selected = results.Where(x => x.categoryName == value);
                        aggregatedResults.AddRange(selected);
                    }
                    break;
                default:
                    break;
            }
        }

        result.products = new Product[]{};
        aggregatedResults = aggregatedResults.Distinct(new ProductComparer()).ToList();
        if (request.pagination != null)
        {
            result.pagination = request.pagination;
            if (request.pagination.from < aggregatedResults.Count &&
                request.pagination.size < aggregatedResults.Count)
                result.products = aggregatedResults.GetRange(request.pagination.from,
                                                             request.pagination.size).ToArray();
        }
        else
        {
            result.pagination = new Pagination() { from = 1, size = 0 };
            result.products = aggregatedResults.ToArray();
        }
        return result;

    }
    /*
    // GET /api/product/autocomplete?type=<brandName>&searchTermPrefix=<term>
    {
                 "type": "brand"
                  "suggestions" : [ {
                   "id": "<BrandId>",
                   "name": "<BrandName>"
                 }]
    }
    */
    public static AutocompleteResult GetSuggestions(String type, string prefix)
    {
        AutocompleteResult result = new AutocompleteResult();
        result.type = type;
        var items = new List<Suggestion>();
        result.suggestions = items.ToArray();
        if (prefix.Length < 3)
        {
            return result;
        }
        var results = GetMatchedProducts(type, prefix);
        results = results.Distinct(new ProductComparer()).ToList();
        results.ForEach(x => items.Add(new Suggestion() { id = x.brandId, name = x.title }));
        result.suggestions = items.ToArray();
        return result;
    }

    public static List<Product> GetMatchedProducts(string type, string prefix)
    {
        var results = FromJson();
        results = results
                .Where(x => x.categoryName.StartsWith(type, StringComparison.InvariantCultureIgnoreCase))
                .Where(x => x.title.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase)).ToList();
        return results;
    }

    
    public static List<Product> FromJson()
    {
        var csv = new List<string[]>();
        var lines = System.IO.File.ReadAllLines(@"sample_product_data.tsv");
        foreach (string line in lines)
            csv.Add(line.Split('\t'));
        var serializer = new
            System.Web.Script.Serialization.JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;
        var json = serializer.Serialize(csv);

        dynamic dobj = serializer.Deserialize<dynamic>(json);
        
        var results = new List<Product>();

        for (int i = 0; i < 50000; i++)
        {
            var obj = dobj[i];
            var p = new Product();
            p.brandId = obj[0];
            p.title = obj[1];
            p.productId = obj[2];
            p.brandName = obj[3];
            p.categoryId = obj[4];
            p.categoryName = obj[5];
            results.Add(p);
        }

        return results;
    }

    public static String SampleSearchRequest()
    {
        return "{\"conditions\": [{\"fieldName\": \"title\",\"values\": [\"Wireless Bluetooth\",\"Buttonsmith Van Gogh\"]},{\"fieldName\": \"brandId\",\"values\": [\"B011R5U07E\",\"B00O5KPNXA\"]},{\"fieldName\": \"categoryId\",\"values\": [\"204\",\"189\"]}],\"pagination\": {\"from\": 1, \"size\": 50}}";
    }

    public static SearchRequest GetSampleSearchRequest()
    {
        var request = new SearchRequest();
        request.pagination = new Pagination();
        request.pagination.from = 1;
        request.pagination.size = 50;
        var conditions = new List<Condition>();
        request.conditions = conditions.ToArray();
        var c = new Condition();
        c.fieldName = "title";
        c.values = new string[]{"Wireless Bluetooth", "Buttonsmith Van Gogh"};
        var c1 = new Condition();
        c1.fieldName = "brandId";
        c1.values = new string[] { "B011R5U07E", "B00O5KPNXA" };
        var c2 = new Condition();
        c2.fieldName = "categoryId";
        c2.values = new string[] { "204", "189" };
        conditions.Add(c);
        conditions.Add(c1);
        conditions.Add(c2);
        request.conditions = conditions.ToArray();
        return request;
    }

    public static SearchRequest GetSearchRequestFromJson()
    {
        var request = new SearchRequest() { pagination = new Pagination(), conditions = new Condition[] { } };
        try
        {
            var serializer = new
                System.Web.Script.Serialization.JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            var json = SampleSearchRequest();
            dynamic dobj = serializer.Deserialize<dynamic>(json);
            request.pagination = new Pagination();
            request.pagination.from = dobj["pagination"]["from"];
            request.pagination.size = dobj["pagination"]["size"];
            var conditions = new List<Condition>();
            request.conditions = conditions.ToArray();
            for (int i = 0; i < dobj["conditions"].Length; i++)
            {
                var obj = dobj["conditions"][i];
                var c = new Condition();
                c.fieldName = obj["fieldName"];
                var values = new List<String>();
                c.values = values.ToArray();
                for (int j = 0; j < obj.Count; j++)
                {
                    values.Add(obj["values"][j]);
                }
                c.values = values.ToArray();
                conditions.Add(c);
            }
            request.conditions = conditions.ToArray();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return request;
    }

    private static void TestNativeSearchRequest()
    {
        var searchRequest = GetSampleSearchRequest();
        var searchRequest1 = GetSearchRequestFromJson();
        Console.WriteLine(searchRequest.pagination.from == searchRequest.pagination.from);
        Console.WriteLine(searchRequest.pagination.size == searchRequest.pagination.size);
        for (int i = 0; i < Math.Min(searchRequest.conditions.Count(), searchRequest1.conditions.Count()); i++)
        {
            Console.WriteLine(searchRequest.conditions[i].fieldName == searchRequest1.conditions[i].fieldName);
            for (int j = 0; j < Math.Min(searchRequest.conditions[i].values.Count(), searchRequest1.conditions[i].values.Count()); j++)
            {
                Console.WriteLine(searchRequest.conditions[i].values[j] == searchRequest1.conditions[i].values[j]);
            }
        }
    }

    private static void TestJsonSearchRequest()
    {
        var searchRequest = GetSearchRequestFromJson();
        var searchResult = GetSearchResult(searchRequest);
        var dobj = new JavaScriptSerializer().Serialize(searchResult);
        Console.WriteLine(dobj.ToString());
    }

    private static void TestDistinct()
    {
        var searchRequest = GetSearchRequestFromJson();
        var searchResult = GetSearchResult(searchRequest);
        var l = searchResult.products.ToList();
        l = l.Distinct().ToList();
        Console.WriteLine("{0}", l.Count);
        l = l.Distinct(new ProductComparer()).ToList();
        Console.WriteLine("{0}", l.Count);
        Console.WriteLine(l.Count == 50);
    }
}
