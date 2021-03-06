﻿//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="RaviRajamani">
//     Copyright (c) Ravi Rajamani. All rights reserved.
// </copyright>
// <author>Ravi Rajamani</author>
//-----------------------------------------------------------------------

namespace CodingExercises
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Program to implement shortest path, all paths, distances and counts in a directed graph.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main to demonstrate Bellman Ford algorithm
        /// </summary>
        public static void Main()
        {
            const int NUMVERTICES = 5;
            int[,] graph = new int[NUMVERTICES, NUMVERTICES];
            //// A = 0, B = 1, C = 2, D = 3, E = 4
            graph[0, 1] = 5;
            graph[1, 2] = 4;
            graph[2, 3] = 8;
            graph[3, 2] = 8;
            graph[3, 4] = 6;
            graph[0, 3] = 5;
            graph[2, 4] = 2;
            graph[4, 1] = 3;
            graph[0, 4] = 7;

            Func<string, List<int>> converter = x =>
            {
                var ret = new List<int>();
                x.ToCharArray().ToList().ForEach(c => ret.Add(ToInt(c)));
                return ret;
            };
            List<string> routes = new List<string> {"ABC", "AD", "AEBCD", "AED"};
            routes.ForEach( x => 
                {
                    var distance = GetDistance(graph, NUMVERTICES, converter(x));
                    if (distance == -1)
                        Console.WriteLine("Distance of the route {0}  = NO SUCH ROUTE", x);
                    else
                        Console.WriteLine("Distance of the route {0} = {1}", x, distance);
                });
            
            var candidatePath = new List<int>();
            var candidateDist = new List<int>();
            var pathList = new List<List<int>>();
            var distanceList = new List<List<int>>();
            candidatePath.Add(ToInt('C'));
            candidateDist.Add(0);
            GetAllPaths(graph, NUMVERTICES, ToInt('C'), ToInt('C'), 5, ref candidatePath, ref candidateDist, ref pathList, ref distanceList);
            for (int i = 0; i < pathList.Count; i++) Console.WriteLine(GetPath(pathList[i]));
            Console.WriteLine("Number of trips from 'C' to 'C' is {0}", pathList.Where(x => x.Count <= 4).Count());

            candidatePath = new List<int>();
            candidateDist = new List<int>();
            candidatePath.Add(ToInt('A'));
            candidateDist.Add(0);
            pathList = new List<List<int>>();
            distanceList = new List<List<int>>();
            GetAllPaths(graph, NUMVERTICES, ToInt('A'), ToInt('C'), 5, ref candidatePath, ref candidateDist, ref pathList, ref distanceList);
            int Min = int.MaxValue;
            for (int i = 0; i < pathList.Count; i++)
            {
                Console.WriteLine(GetPath(pathList[i]));
                if (distanceList[i].Sum() < Min)
                    Min = distanceList[i].Sum();
            }
            Console.WriteLine("Length of the shortest route from 'A' to 'C' is {0}", Min );
            Console.WriteLine("Number of trips from 'A' to 'C' with four stops is {0}", pathList.Where(x => x.Count == 5).Count());

            candidatePath = new List<int>();
            candidateDist = new List<int>();
            candidatePath.Add(ToInt('B'));
            candidateDist.Add(0);
            pathList = new List<List<int>>();
            distanceList = new List<List<int>>();
            GetAllPaths(graph, NUMVERTICES, ToInt('B'), ToInt('B'), 5, ref candidatePath, ref candidateDist, ref pathList, ref distanceList);
            Min = int.MaxValue;
            for (int i = 0; i < pathList.Count; i++)
            {
                Console.WriteLine(GetPath(pathList[i]));
                if (distanceList[i].Sum() < Min)
                    Min = distanceList[i].Sum();
            }
            Console.WriteLine("Length of the shortest route from 'B' to 'N' is {0}", Min);
            

            var path = new List<int>() { 0, 0, 0, 0, 0 };
            var parent = new List<int>() { 0, 0, 0, 0, 0 };
            GetShortestPath(graph, NUMVERTICES, ToInt('A'), ref path, ref parent);
            Console.WriteLine("Shortest path from 'A' to 'C' is {0}", path[ToInt('C')]);

            candidatePath = new List<int>();
            candidateDist = new List<int>();
            candidatePath.Add(ToInt('C'));
            candidateDist.Add(0);
            pathList = new List<List<int>>();
            distanceList = new List<List<int>>();
            GetAllPaths(graph, NUMVERTICES, ToInt('C'), ToInt('C'), 12, ref candidatePath, ref candidateDist, ref pathList, ref distanceList);
            pathList = pathList.Where(x => distanceList[pathList.IndexOf(x)].Sum() < 30).ToList();
            for (int i = 0; i < pathList.Count; i++) Console.WriteLine(GetPath(pathList[i]));
            Console.WriteLine("Number of trips from 'C' to 'C' with distance < 30 is {0}", pathList.Count());

        }

        /// <summary>
        /// Implements Bellman Ford Algorithm.
        /// </summary>
        /// <param name="graph">adjacency matrix of graph</param>
        /// <param name="numVertex">number of vertices in graph</param>
        /// <param name="start">source vertex</param>
        /// <param name="distances">vector to hold distances on path from source</param>
        /// <param name="parents">vector to hold parents on path from source</param>
        /// <returns>if a path to all vertices was found</returns>
        public static bool GetShortestPath(int[,] graph, int numVertex, int start, ref List<int> distances, ref List<int> parents)
        {
            // initialize Single Source
            for (int i = 0; i < numVertex; i++)
            {
                distances[i] = int.MaxValue;
                parents[i] = -1;
            }

            distances[start] = 0;

            var allEdges = GetAllEdges(graph, numVertex);
            for (int k = 0; k < numVertex - 1; k++)
            {
                for (int i = 0; i < allEdges.Count; i++)
                {
                    // relax
                    int sum = distances[allEdges[i].Item1] == int.MaxValue ?
                        distances[allEdges[i].Item1] : 
                        distances[allEdges[i].Item1] + allEdges[i].Item3;
                    if (distances[allEdges[i].Item2] > sum)
                    {
                        distances[allEdges[i].Item2] = distances[allEdges[i].Item1] + allEdges[i].Item3;
                        parents[allEdges[i].Item2] = allEdges[i].Item1;
                    }
                }
            }

            for (int i = 0; i < allEdges.Count; i++)
            {
                if (distances[allEdges[i].Item2] > distances[allEdges[i].Item1] + allEdges[i].Item3)
                {
                    return false; // cycle exists;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets adjacent incumbent edges.
        /// </summary>
        /// <param name="graph">adjacency matrix of graph</param>
        /// <param name="numVertex">number of vertices in graph</param>
        /// <param name="destination">destination vertex</param>
        /// <returns>source-distance pairs</returns>
        public static Dictionary<int, int> GetAdjacentIncumbentEdges(int[,] graph, int numVertex, int destination)
        {
            var sourceDistance = new Dictionary<int, int>();
            for (int i = 0; i < numVertex; i++)
            {
                if (graph[i, destination] > 0)
                {
                    sourceDistance.Add(i, graph[i, destination]);
                }
            }

            return sourceDistance;
        }

        /// <summary>
        /// Gets all edges.
        /// </summary>
        /// <param name="graph">adjacency matrix of graph</param>
        /// <param name="numVertex">number of vertices in graph</param>
        /// <returns>source, destination, weight tuple to represent edges</returns>
        public static List<Tuple<int, int, int>> GetAllEdges(int[,] graph, int numVertex)
        {
            var sourceDestDistance = new List<Tuple<int, int, int>>();
            for (int i = 0; i < numVertex; i++)
            {
                foreach (var edge in GetAdjacentIncumbentEdges(graph, numVertex, i))
                {
                    sourceDestDistance.Add(new Tuple<int, int, int>(edge.Key, i, edge.Value));
                }
            }

            return sourceDestDistance;
        }

        /// <summary>
        /// Gets the distance along a path
        /// </summary>
        /// <param name="graph">adjacency matrix of graph</param>
        /// <param name="numVertex">number of vertices in graph</param>
        /// <param name="pathList">list of paths</param>
        /// <param name="distanceList">list of distances</param>
        public static void GetAllShortestPaths(int[,] graph, int numVertex, ref List<List<int>> pathList, ref List<List<int>> distanceList)
        {
            for( int i = 0; i < numVertex; i++)
            {
                //// initialize
                var parents = new List<int>();
                var distances = new List<int>();
                for (int k = 0; k < numVertex; k++)
                {
                    parents.Add(0);
                    distances.Add(0);
                }

                //// add shortest path for this source
                if (GetShortestPath(graph, numVertex, i, ref distances, ref parents))
                {
                    pathList.Add(parents);
                    distanceList.Add(distances);
                }
            }
        }

        /// <summary>
        /// Gets all the paths for a vertex
        /// </summary>
        /// <param name="graph">adjacency matrix of graph</param>
        /// <param name="numVertex">number of vertices in graph</param>
        /// <param name="pathList">list of paths between source and destination</param>
        /// <param name="distanceList">list of paths between </param>
        public static void GetAllPaths(int[,] graph, int numVertex, int source, int destination, int threshold, ref List<int> candidatePath, ref List<int> candidateDist, ref List<List<int>> pathList, ref List<List<int>> distanceList)
        {
            if (candidatePath.Count > threshold) return;
           
            var path = new List<int>();
            var distances = new List<int>();
            GetOutboundEdges(graph, numVertex, source, ref path, ref distances);
            if (path.Contains(destination) && (candidatePath.Count == 0  || (candidatePath.Count > 0 && candidatePath.Last() != destination)))
            {
                candidatePath.Add(destination);
                candidateDist.Add(distances[path.IndexOf(destination)]);
                if (pathList.Contains(candidatePath) == false)
                    pathList.Add(new List<int>(candidatePath));
                if (distanceList.Contains(candidateDist) == false)
                    distanceList.Add(new List<int>(candidateDist));
                candidatePath.RemoveAt(candidatePath.Count - 1);
                candidateDist.RemoveAt(candidateDist.Count - 1);
            }

            for (int i = 1; i < path.Count; i++)
            {
                // if (i != source)
                {
                    candidatePath.Add(path[i]);
                    candidateDist.Add(distances[i]);
                    GetAllPaths(graph, numVertex, path[i], destination, threshold, ref candidatePath, ref candidateDist, ref pathList, ref distanceList);
                    candidatePath.RemoveAt(candidatePath.Count - 1);
                    candidateDist.RemoveAt(candidateDist.Count - 1);
                }
            }
        }

        /// <summary>
        /// Gets the outbound edges for a vertex
        /// </summary>
        /// <param name="graph">adjacency matrix of graph</param>
        /// <param name="numVertex">number of vertices in graph</param>
        /// <param name="parents">list of destinations for outbound edges from source</param>
        /// <param name="distances">list of edge weights</param>
        public static void GetOutboundEdges(int[,] graph, int numVertex, int source, ref List<int> parents, ref List<int> distances)
        {
            var allEdges = GetAllEdges(graph, numVertex);

            //// initialize
            parents.Add(source);
            distances.Add(0);

            for (int i = 0; i < allEdges.Count; i++)
            {
                if (allEdges[i].Item1 == source)
                {
                    parents.Add(allEdges[i].Item2);
                    distances.Add(allEdges[i].Item3);
                }
            }
        }

        /// <summary>
        /// Gets the distance along a path
        /// </summary>
        /// <param name="graph">adjacency matrix of graph</param>
        /// <param name="numVertex">number of vertices in graph</param>
        /// <param name="nodes">path represented by the list of vertices</param>
        /// <returns>distance</returns>
        public static int GetDistance(int[,] graph, int numVertex, List<int> nodes)
        {
            int distance = 0;
            for (int i = 1; i < nodes.Count; i++)
            {
                if (nodes[i - 1] < numVertex && nodes[i] < numVertex)
                {
                    if (graph[nodes[i - 1], nodes[i]] == 0) return -1; 
                    distance += graph[nodes[i - 1], nodes[i]];
                }
            }
            return distance;
        }

        /// <summary>
        /// label for vertex
        /// </summary>
        /// <param name="i">vertex i</param>
        /// <returns>label as string</returns>
        public static string ToString(int i)
        {
            switch (i)
            {
                case 0: return "A";
                case 1: return "B";
                case 2: return "C";
                case 3: return "D";
                case 4: return "E";
                case 5: return "F";
                default: throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// internal representation for vertex
        /// </summary>
        /// <param name="c">vertex label c</param>
        /// <returns>vertex as integer</returns>
        public static int ToInt(char c)
        {
            switch (c)
            {
                case 'A': return 0;
                case 'B': return 1;
                case 'C': return 2;
                case 'D': return 3;
                case 'E': return 4;
                case 'F': return 5;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public static string GetPath(List<int> path)
        {
            Converter<int, string> converter = x => ToString(x);
            var output = path.ConvertAll<string>(converter);
            string ret = string.Empty;
            output.ForEach(x => ret += x);
            return ret;
        }
    }
}
