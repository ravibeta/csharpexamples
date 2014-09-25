//-----------------------------------------------------------------------
// <copyright file="CodingExercisesTest.cs" company="RaviRajamani">
//     Copyright (c) Ravi Rajamani. All rights reserved.
// </copyright>
// <author>Ravi Rajamani</author>
//-----------------------------------------------------------------------

namespace CodingExercisesTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CodingExercises;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit Tests for Bellman Ford Algorithm.
    /// </summary>
    [TestClass]
    public class CodingExercisesTest
    {
        /// <summary>
        /// The number of vertices in a graph
        /// </summary>
        private const int NUMVERTICES = 5;

        /// <summary>
        /// The adjacency matrix for a graph
        /// </summary>
        private int[,] graph = new int[NUMVERTICES, NUMVERTICES];

        /// <summary>
        /// Set up for the tests.
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            // A = 0, B = 1, C = 2, D = 3, E = 4
            this.graph[0, 1] = 5;
            this.graph[1, 2] = 4;
            this.graph[2, 3] = 8;
            this.graph[3, 2] = 8;
            this.graph[3, 4] = 6;
            this.graph[0, 3] = 5;
            this.graph[2, 4] = 2;
            this.graph[4, 1] = 3;
            this.graph[0, 4] = 7;
        }

        /// <summary>
        /// Test for the adjacent incumbent edges.
        /// </summary>
        [TestMethod]
        public void TestGetAdjacentIncumbentEdges()
        {
            var edges = Program.GetAdjacentIncumbentEdges(this.graph, NUMVERTICES, 4);
            Assert.IsTrue(edges.Count == 3);
            Assert.IsTrue(edges.Keys.Contains(0));
            Assert.IsTrue(edges.Keys.Contains(2));
            Assert.IsTrue(edges.Keys.Contains(3));
            Assert.IsTrue(edges.Values.Sum() == 15);
            Assert.IsTrue(edges.Values.Contains(7));
            Assert.IsTrue(edges.Values.Contains(2));
            Assert.IsTrue(edges.Values.Contains(6));
        }

        /// <summary>
        /// Test for all the edges.
        /// </summary>
        [TestMethod]
        public void TestGetAllEdges()
        {
            var edges = Program.GetAllEdges(this.graph, NUMVERTICES);
            Assert.IsTrue(edges.Count == 9);
            Assert.IsTrue(edges.Contains(new Tuple<int, int, int>(0, 1, 5)));
            Assert.IsTrue(edges.Contains(new Tuple<int, int, int>(1, 2, 4)));
            Assert.IsTrue(edges.Contains(new Tuple<int, int, int>(2, 3, 8)));
            Assert.IsTrue(edges.Contains(new Tuple<int, int, int>(3, 2, 8)));
            Assert.IsTrue(edges.Contains(new Tuple<int, int, int>(3, 4, 6)));
            Assert.IsTrue(edges.Contains(new Tuple<int, int, int>(0, 3, 5)));
            Assert.IsTrue(edges.Contains(new Tuple<int, int, int>(2, 4, 2)));
            Assert.IsTrue(edges.Contains(new Tuple<int, int, int>(4, 1, 3)));
            Assert.IsTrue(edges.Contains(new Tuple<int, int, int>(0, 4, 7)));
        }

        /// <summary>
        /// Test for the shortest path
        /// </summary>
        [TestMethod]
        public void TestGetShortestPath()
        {
            var distances = new List<int>() { 0, 0, 0, 0, 0 };
            var parents = new List<int>() { 0, 0, 0, 0, 0 };
            Assert.IsTrue(Program.GetShortestPath(this.graph, NUMVERTICES, 0, ref distances, ref parents));
            Assert.IsTrue(parents[1] == 0);
            Assert.IsTrue(parents[2] == 1);
            Assert.IsTrue(distances[1] == 5);
            Assert.IsTrue(distances[2] == 9);
            distances = new List<int>() { 0, 0, 0, 0, 0 };
            parents = new List<int>() { 0, 0, 0, 0, 0 };
            Assert.IsFalse(Program.GetShortestPath(this.graph, NUMVERTICES, 4, ref distances, ref parents));
            Assert.IsTrue(parents[1] == 4);
            Assert.IsTrue(parents[2] == 1);
            Assert.IsTrue(parents[3] == 2);
            Assert.IsTrue(distances[1] == 3);
            Assert.IsTrue(distances[2] == 7);
            Assert.IsTrue(distances[3] == 15);
        }

        [TestMethod]
        public void TestGetDistance()
        {
            Assert.IsTrue(Program.GetDistance(this.graph, NUMVERTICES, new List<int>() { 0, 1, 2 }) == 9);
            Assert.IsTrue(Program.GetDistance(this.graph, NUMVERTICES, new List<int>() { 0, 3 }) == 5);
            Assert.IsTrue(Program.GetDistance(this.graph, NUMVERTICES, new List<int>() { 0, 3, 2 }) == 13);
            Assert.IsTrue(Program.GetDistance(this.graph, NUMVERTICES, new List<int>() { 0, 4, 1, 2, 3 }) == 22);
            Assert.IsTrue(Program.GetDistance(this.graph, NUMVERTICES, new List<int>() { 0, 4, 3 }) == -1); 
        }

        [TestMethod]
        public void TestGetAllShortestPaths()
        {
           var distanceList = new List<List<int>>();
           var pathList = new List<List<int>>();
           Program.GetAllShortestPaths(this.graph, NUMVERTICES, ref pathList, ref distanceList);
           Assert.IsTrue(pathList.Count == 1);
           Assert.IsTrue(distanceList.Count == 1);
        }
    }
}
