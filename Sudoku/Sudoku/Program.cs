using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] board = new int[9, 9];
            
            //initialize
            InitializeBoard(ref board);
            var used = new List<bool>();
            for (int i = 0; i< 9; i++)
                used.Add(false);
            
            //action
            if (FillSudoku(ref board, ref used))
            {
                PrintBoard(ref board);
                GeneratePuzzle(ref board);
            }
        }
        private static bool FillSudoku(ref int[,] board, ref List<bool> used)
        {
            PrintBoard(ref board);
            for (int subgrid = 0; subgrid < 9; subgrid++)
            {
                if (used[subgrid]) continue;
                used[subgrid] = true;
                if (FillSubGrid(ref board, subgrid) &&
                    FillSudoku(ref board, ref used))
                {
                    return true;
                }
                used[subgrid] = false;
                ResetSubgrid(ref board, subgrid);
            }
            if (used.All(x => x == true)) return true;
            return false;
        }
        private static bool FillSudoku(ref int[,] board, ref List<bool> used)
        {
            PrintBoard(ref board);
            for (int subgrid = 0; subgrid < 9; subgrid++)
            {
                if (used[subgrid]) continue;
                used[subgrid] = true;
                if (FillSubGrid(ref board, subgrid) &&
                    FillSudoku(ref board, ref used))
                {
                    return true;
                }
                used[subgrid] = false;
                ResetSubgrid(ref board, subgrid);
            }
            if (used.All(x => x == true)) return true;
            return false;
        }
        private static bool FillSudoku(ref int[,] board, ref List<bool> used)
        {
            PrintBoard(ref board);
            for (int subgrid = 0; subgrid < 9; subgrid++)
            {
                if (used[subgrid]) continue;
                used[subgrid] = true;
                if (FillSubGrid(ref board, subgrid) &&
                    FillSudoku(ref board, ref used))
                {
                    return true;
                }
                used[subgrid] = false;
                ResetSubgrid(ref board, subgrid);
            }
            if (used.All(x => x == true)) return true;
            return false;
        }
        //private static void oldMain()
        //{
        //    int[,] board = new int[9, 9];
        //    InitializeBoard(ref board);
        //    FillSubGrid(ref board, 0);
        //    FillSubGrid(ref board, 4);
        //    FillSubGrid(ref board, 8);
        //    for (int subgrid = 0; subgrid < 9; subgrid++)
        //        switch(subgrid)
        //        {
        //            case 0:
        //            case 4:
        //            case 8:
        //                continue;
        //            default:
        //                FillSubGrid(ref board, subgrid, false);
        //                break;
        //        }
        //    // FillSubGrid(ref board, 4);
        //    // FillSubGrid(ref board, 8);
        //    // FillRemaining(ref board);
        //    Console.ReadLine();
        //}
        private static void InitializeBoard(ref int[,] board)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    board[i,j] = 0;
                }
        }

        private static bool FillSubGrid(ref int[,] board, int subgrid, bool random=true)
        {
            try
            {
                int xStart = (subgrid / 3) * 3;
                int yStart = (subgrid % 3) * 3;
                int xend = xStart + 3;
                int yend = yStart + 3;
                Random rnd = new Random();
                for (int i = xStart; i < xend; i++)
                    for (int j = yStart; j < yend; j++)
                    {
                        int val = random ? rnd.Next(1, 10) : 1;
                        Console.WriteLine("i={0}\t j={1}\t val={2}", i, j, val);
                        List<int> existing = new List<int>();
                        //check row
                        for (int k = i; k < i + 1; k++)
                            for (int l = 0; l < 9; l++)
                                if (k != i || l != j) { existing.Add(board[k, l]); }
                        // check col
                        for (int k = 0; k < 9; k++)
                            for (int l = j; l < j + 1; l++)
                                if (k != i || l != j) { existing.Add(board[k, l]); }
                        var all = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                        existing = existing.Distinct().ToList();
                        var candidates = all.Except(existing).ToList();

                        int count = 0;
                        while (IsDuplicate(ref board, subgrid, i, j, val) == true || existing.Contains(val))
                        {
                            if (candidates.Count == 0) throw new Exception("Solution not possible");
                            val = candidates[rnd.Next(0, candidates.Count)];
                            count++;
                            if (count > 100) throw new Exception("Possibly infinite loop");
                            // while (IsDuplicate(ref board, x, x+3, y, y+3, val) == true ||
                            //       IsDuplicate(ref board, 0, 1, 0, 9, val) == true ||
                            //       IsDuplicate(ref board, 0, 9, 0, 1, val) == true)
                            //val = random ? rnd.Next(1, 10) : (val + 1) % 10;
                            //if (val == 0) val = val + 1;
                            //if (!random)
                            //{
                            //    if (candidates.Count == 0) throw new Exception("Solution not possible");
                            //    val = candidates[rnd.Next(0, candidates.Count)];
                            //}
                            //count++;
                            //if (!random && count > 40) { PrintBoard(ref board); Console.WriteLine("possible infinite loop at i={0}\t j={1}\t val={2}", i, j, val); Console.ReadLine(); }
                            //Console.WriteLine("Subtituting val for i={0}\t j={1}\t val={2}", i, j, val);
                        }
                        board[i, j] = val;
                    }


                //PrintBoard(ref board);
                return true;
            }
            catch (Exception e)
            {
                //ResetSubgrid(ref board, subgrid);
                return false;
            }
        }
        private static void ResetSubgrid(ref int[,] board, int subgrid)
        {
            int xStart = (subgrid / 3) * 3;
            int yStart = (subgrid % 3) * 3;
            int xend = xStart + 3;
            int yend = yStart + 3;
            //check subgrid
            for (int i = xStart; i < xend; i++)
                for (int j = yStart; j < yend; j++)
                    board[i, j] = 0;

        }
        private static bool IsDuplicate(ref int[,] board, int subgrid, int x, int y, int val)
        {
            int xStart = (subgrid / 3) * 3;
            int yStart = (subgrid % 3) * 3;
            int xend = xStart + 3;
            int yend = yStart + 3;
            //check subgrid
            for (int i = xStart; i < xend; i++)
                for (int j = yStart; j < yend; j++)
                    if (board[i, j] == val && (i != x || j != y)) { Console.WriteLine("Duplicate val at {0} and {1}", i,j); return true; }

            //List<int> existing = new List<int>();
            ////check row
            //for (int i = x; i < x + 1; i++)
            //    for (int j = 0; j < 9; j++)
            //        if (i != x || j != y) { existing.Add(board[i, j]); }
            //        //if (board[i, j] == val && (i != x || j != y)) { Console.WriteLine("Duplicte val at x={0} and y={1} and i={2} and j={3}", x, y, i, j); PrintBoard(ref board); Console.ReadLine(); return true; }
            ////check col
            //for (int i = 0; i < 9; i++)
            //    for (int j = y; j < y + 1; j++)
            //        if (i != x || j != y) { existing.Add(board[i, j]); }
            //        //if (board[i, j] == val && (i != x && j != y)) { Console.WriteLine("Duplicte val at x={0} and y={1} and i={2} and j={3}", x, y, i, j); PrintBoard(ref board); Console.ReadLine(); return true; }
            //if (existing.Contains(val))
            //{
            //    return true;
            //}
            return false;
        }
        /*
        private static void FillRemaining(ref int[,] board)
        {
            for (int subgrid = 0; subgrid < 9; subgrid++)
            {
                switch (subgrid)
                {
                    case 0: continue;
                    case 4: continue;
                    case 8: continue;
                    default: 
                            break;
                }
            }
        }
       */
        private static void PrintBoard(ref int[,] board)
        {
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write("\t{0}",board[i,j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.ReadLine();
        }

    }
}
