using System;

class Program {
    public const int GRID_SIZE = 9;

    static void Main(string[] args) {
        int[,] board = {
            {0,0,0, 1,2,6, 0,0,0},
            {7,8,2, 0,0,4, 0,6,1},
            {4,1,0, 0,9,0, 3,2,5},
            
            {3,6,7, 8,4,5, 0,0,0},
            {1,0,8, 0,7,2, 5,3,6},
            {0,0,0, 3,6,1, 7,8,4},

            {0,3,0, 2,8,7, 0,0,9},
            {8,0,0, 6,5,0, 0,1,0},
            {2,5,9, 4,0,3, 6,7,0},
        };
        if(solveBoard(board)) Console.WriteLine("Solved Successfully!");
        else Console.WriteLine("Unsolvable board(");
        printBoard(board);
    }

    static void printBoard(int[,] board) {
        int count = 0;
        int countRow = 1;
        for(int i = 0; i < GRID_SIZE; i++) {
            for(int j = 0; j < GRID_SIZE; j++) {
                if(count==3) {
                    count=0;
                    Console.Write("|");
                }
                Console.Write(board[i,j]);
                count++;
            }
            if(countRow==3) {
                countRow = 0;
                Console.Write("\n-----------");
            }
            Console.Write("\n");
            count=0;
            countRow++;
        }
    }

    static bool isNumberInRow(int[,] board, int number, int row) {
        for(int i = 0; i < GRID_SIZE; i++) {
            if(board[row,i]==number) return true;
        }
        return false;
    }

    static bool isNumberInColumn(int[,] board, int number, int column) {
        for(int i = 0; i < GRID_SIZE; i++) {
            if(board[i,column]==number) return true;
        }
        return false;
    }

    static bool isNumberInBox(int[,] board, int number, int row,int column) {
        int localBoxRow = row - row % 3;
        int localBoxColumn = column - column % 3;

        for(int i = localBoxRow; i < localBoxRow+3; i++) {
            for(int j = localBoxColumn; j < localBoxColumn+3; j++) {
                if(board[i,j]==number) return true;
            }
        }
        return false;
    }

    static bool isValidPlacement(int[,] board, int number, int row, int column) {
        return !isNumberInRow(board,number,row) && 
        !isNumberInColumn(board,number,column) &&
        !isNumberInBox(board,number,row,column);
    }

    static bool solveBoard(int[,] board) {
        for(int row = 0; row < GRID_SIZE; row++) {
            for(int column = 0; column < GRID_SIZE; column++) {
                if(board[row,column]==0) {
                    for(int numberToTry = 1; numberToTry <= GRID_SIZE; numberToTry++) {
                        if(isValidPlacement(board,numberToTry,row,column)) {
                            board[row,column] = numberToTry;

                            if(solveBoard(board)) return true;
                            else board[row,column] = 0;
                        }
                    }
                    return false;
                }
            }
        }
        return true;
    }
}