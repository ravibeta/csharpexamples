using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingExercises
{
    class Program
    {
        static void Main()
        {
            List<Tuple<string, int, int>> list = new List<Tuple<string, int, int>>();
            list.Add(new Tuple<string, int, int>("a", 1, 5));
            list.Add(new Tuple<string, int, int>("b", 2, 4));
            list.Add(new Tuple<string, int, int>("c", 3, 6));

            List<Tuple<string, int>> pairs = new List<Tuple<string, int>>();
            list.ForEach(x =>
                    {
                        pairs.Add(new Tuple<string, int>(x.Item1, x.Item2));
                        pairs.Add(new Tuple<string, int>(x.Item1, x.Item3));
                    });
            pairs.Sort((a, b) => a.Item2.CompareTo(b.Item2));

            foreach (var element in pairs)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine("{0}", (255 & -255) );
            Console.WriteLine("{0}", (1 << 1));
            Console.WriteLine("{0}", (1 >> 1));
            var Board = new int[8, 8];
            Reset(0, 7, 0, 7, ref Board);
            int count = place8QueensRecursive(0, 7, 0, 7, ref Board);
            Console.WriteLine("Count={0}", count);
            // solve(8);
        }


        public static int place8QueensRecursive(int rstart, int rend, int cstart, int cend, ref int[,] Board)
        {
            if (cstart > cend)
            {
                if (CheckBoardState(ref Board))
                {
                    PrintBoard(ref Board);
                    return 1; // success
                }
                else
                    return 0;
            }
            
            int countOfPossibilities = 0;
            for (int initialr = rstart; initialr < 8; initialr++)
            {
                int initialc = cstart;
                if (Board[initialr, initialc] != 0) continue;
            
                // each Queen has to be in a row by herself and a column by herself
                Board[initialr, initialc] = 2; // marks the position of the queen
                
                // MarkUnavailable(ref Board) or inline it here ;
                for (int i = 0; i < 8; i++)
                    if (Board[i, initialc] == 0) Board[i, initialc] = 1; // unavailable
                for (int j = cstart; j < 8; j++)
                    if (Board[initialr, j] == 0) Board[initialr, j] = 1; // unavailable
                for (int k = -8; k < 8; k++)
                    if ((initialr + k) >= 0 && (initialc + k) >= cstart &&
                        (initialr + k) < 8 && (initialc + k) < 8 &&
                        Board[initialr + k, initialc + k] == 0) Board[initialr + k, initialc + k] = 1;
                
                
                countOfPossibilities += place8QueensRecursive(rstart, rend, cstart + 1, cend, ref Board);

                // MarkAvailable or inline it here
                Board[initialr, initialc] = 0;
                var queenOccupiedRows = new List<int>();
                var queenOccupiedCols = new List<int>();
                for (int l = 0; l < 8; l++)
                    for (int m = 0; m < cstart; m++)
                    {
                        if (Board[m, l] == 2) { queenOccupiedRows.Add(m); queenOccupiedCols.Add(l); }
                    }
                for (int i = 0; i < 8; i++)
                {
                    if (Board[i, initialc] == 1 
                        && queenOccupiedRows.Any(x => x == i) == false
                        ) Board[i, initialc] = 0; // available
                }
                for (int j = cstart; j < 8; j++)
                    if (Board[initialr, j] == 1
                        && queenOccupiedCols.Any(x => x == j) == false) Board[initialr, j] = 0; // available
                for (int k = -8; k < 8; k++)
                    if ((initialr + k) >= 0 && (initialc + k) >= cstart &&
                        (initialr + k) < 8 && (initialc + k) < 8 &&
                        Board[initialr + k, initialc + k] == 1 && queenOccupiedRows.Any(x => x == (initialr + k)) == false
                        ) Board[initialr + k, initialc + k] = 0;
            }

            return countOfPossibilities;
            
        }

        public static void Reset(int rstart, int rend, int cstart, int cend, ref int[,] Board)
        {
            for (int i = rstart; i < rend + 1; i++)
                for (int j = cstart; j < cend + 1; j++)
                    Board[i,j] = 0;
        }

        public static bool CheckBoardState(ref int[,] Board)
        {
            int numQueens = 0;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    switch(Board[i,j])
                    {
                        case 0: throw new InvalidOperationException();
                        case 1: break;
                        case 2: numQueens++;
                            break;
                        default:
                            throw new InvalidOperationException();
                    }
                }

            if (numQueens != 8) 
                return false;
            
            // no row has two queens
            for (int i = 0; i < 8; i++)
            {
                int queensInARow = 0;
                for (int j = 0; j < 8; j++)
                {
                    if (Board[i, j] == 2)
                    {
                        queensInARow++;
                        if (queensInARow > 1) 
                            return false;
                    }
                }
            }

            // no column has two queens
            for (int j = 0; j < 8; j++)
            {
                int queensInACol = 0;
                for (int i = 0; i < 8; i++)
                {
                    if (Board[i, j] == 2)
                    {
                        queensInACol++;
                        if (queensInACol > 1) 
                            return false;
                    }
                }
            }

            // no topleft-to-rightbottom diagonal has two queens
            for (int i = 0; i < 8; i++)
            {
                int j = 0;
                int queensInLRDDiagonal = 0;
                for (int k = -8; k < 8; k++)
                {
                    if (i + k >= 0 && j + k >= 0 && i + k < 8 && j + k < 8 && Board[i + k, j + k] == 2)
                    {
                        queensInLRDDiagonal++;
                        if (queensInLRDDiagonal > 1)
                            return false;
                    }
                }
            }

            for (int j = 0; j < 8; j++)
            {
                int i = 0;
                int queensInLRDDiagonal = 0;
                for (int k = -8; k < 8; k++)
                {
                    if (i + k >= 0 && j + k >= 0 && i + k < 8 && j + k < 8 && Board[i + k, j + k] == 2)
                    {
                        queensInLRDDiagonal++;
                        if (queensInLRDDiagonal > 1)
                            return false;
                    }
                }
            }

            // no topright-to-bottomleft diagonal has two queens
            for (int j = 0; j < 8; j++)
            {
                int i = 0;
                int queensInRLDiagonal = 0;
                for (int k = -8; k < 8; k++)
                {
                    if (i + k >= 0 && j - k >= 0 && i + k < 8 && j - k < 8 && Board[i + k, j - k] == 2)
                    {
                        queensInRLDiagonal++;
                        if (queensInRLDiagonal > 1)
                            return false;
                    }
                }
            }

            for (int i = 0; i < 8; i++)
            {
                int j = 7;
                int queensInRLDiagonal = 0;
                for (int k = -8; k < 8; k++)
                {
                    if (i + k >= 0 && j - k >= 0 && i + k < 8 && j - k < 8 && Board[i + k, j - k] == 2)
                    {
                        queensInRLDiagonal++;
                        if (queensInRLDiagonal > 1)
                            return false;
                    }
                }
            }
            
            return true;
        }

        public static void PrintBoard(ref int[,] Board)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write("{0} ", Board[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void MarkUnavailable(ref int[,] Board)
        {
            var queenPositions = new List<Tuple<int, int>>();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (Board[i, j] == 2) queenPositions.Add(new Tuple<int, int>(i, j));
            Reset(0, 7, 0, 7, ref Board);
            for (int q = 0; q < queenPositions.Count; q++)
            {
                int initialr = queenPositions[q].Item1;
                int initialc = queenPositions[q].Item2;
                Board[initialr, initialc] = 2;
                for (int i = 0; i < 8; i++)
                    if (Board[i, initialc] == 0
                        //&& queenPositions.Any(x => x.Item1 == i) == false
                        //&& queenPositions.Any(x => x.Item2 == initialc) == false
                        ) Board[i, initialc] = 1; // unavailable
                for (int j = 0; j < 8; j++)
                    if (Board[initialr, j] == 0
                        //&& queenPositions.Any(x => x.Item1 == initialr) == false
                        //&& queenPositions.Any(x => x.Item2 == j) == false
                        ) Board[initialr, j] = 1; // unavailable
                for (int k = -8; k < 8; k++)
                    if ((initialr + k) >= 0 && (initialc + k) >= 0 &&
                        (initialr + k) < 8 && (initialc + k) < 8 &&
                        Board[initialr + k, initialc + k] == 0
                        //&& queenPositions.Any(x => x.Item1 == initialr + k) == false 
                        //&& queenPositions.Any(x => x.Item2 == initialc + k) == false
                        ) Board[initialr + k, initialc + k] = 1;
            }

        }

        public static void place8Queens(int row, int ld, int rd, int lim, ref int ans)
        {
            if (row == lim)
            {
                ans++;
                return;
            }
            int pos = lim & (~(row | ld | rd));
            while (pos != 0)
            {
                int p = pos & (-pos);
                pos -= p;
                Console.WriteLine("row={0}, ld={1}, rd={2}, lim={3}, ans={4}", row, ld, rd, lim, ans);
                place8Queens(row + p, (ld + p) << 1, (rd + p) >> 1, lim, ref ans);
            }
        }
        public static int solve(int n)
        {
            int ans = 0;
            int lim = (1 << 8) - 1;
            place8Queens(0, 0, 0, lim, ref ans);
            return ans;
        }
    }

}
