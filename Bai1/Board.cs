using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai1
{
    // Board.cs - Quản lý trạng thái bàn cờ
    public class Board
    {
        public Cell[,] Cells { get; private set; }

        public Board()
        {
            Cells = new Cell[9, 10];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Cells[x, y] = new Cell(); // Mặc định không có quân cờ
                }
            }

            // Ví dụ: Khởi tạo các quân cờ ban đầu (Xe, Ma, Phao...)
            Cells[0,0] = new Cell(ChessPieceType.Xe, true); // Xe đỏ         
            Cells[1,0] = new Cell(ChessPieceType.Ma, true); 
            Cells[2,0] = new Cell(ChessPieceType.Tinh, true); 
            Cells[3,0] = new Cell(ChessPieceType.Si, true); 
            Cells[4,0] = new Cell(ChessPieceType.Tuong, true); 
            Cells[5,0] = new Cell(ChessPieceType.Si, true); 
            Cells[6,0] = new Cell(ChessPieceType.Tinh, true); 
            Cells[7,0] = new Cell(ChessPieceType.Ma, true); 
            Cells[8,0] = new Cell(ChessPieceType.Xe, true); 
            Cells[1,2] = new Cell(ChessPieceType.Phao, true); 
            Cells[7,2] = new Cell(ChessPieceType.Phao, true); 
            Cells[0,3] = new Cell(ChessPieceType.Tot, true); 
            Cells[2,3] = new Cell(ChessPieceType.Tot, true); 
            Cells[4,3] = new Cell(ChessPieceType.Tot, true); 
            Cells[6,3] = new Cell(ChessPieceType.Tot, true); 
            Cells[8,3] = new Cell(ChessPieceType.Tot, true); 

            Cells[0,9] = new Cell(ChessPieceType.Xe, false); // Xe đen       
            Cells[1,9] = new Cell(ChessPieceType.Ma, false);
            Cells[2,9] = new Cell(ChessPieceType.Tinh, false);
            Cells[3,9] = new Cell(ChessPieceType.Si, false);
            Cells[4,9] = new Cell(ChessPieceType.Tuong, false);
            Cells[5,9] = new Cell(ChessPieceType.Si, false);
            Cells[6,9] = new Cell(ChessPieceType.Tinh, false);
            Cells[7,9] = new Cell(ChessPieceType.Ma, false);
            Cells[8,9] = new Cell(ChessPieceType.Xe, false);

            Cells[1,7] = new Cell(ChessPieceType.Phao, false);
            Cells[7,7] = new Cell(ChessPieceType.Phao, false);
            Cells[0,6] = new Cell(ChessPieceType.Tot, false);
            Cells[2,6] = new Cell(ChessPieceType.Tot, false);
            Cells[4,6] = new Cell(ChessPieceType.Tot, false);
            Cells[6,6] = new Cell(ChessPieceType.Tot, false);
            Cells[8,6] = new Cell(ChessPieceType.Tot, false);
            
        }

        public bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < 9 && y >= 0 && y < 10;
        }

        public Cell GetCell(int x, int y)
        {
            return IsValidPosition(x, y) ? Cells[x, y] : null;
        }

        public void MovePiece(int fromX, int fromY, int toX, int toY)
        {
            if (IsValidPosition(fromX, fromY) && IsValidPosition(toX, toY))
            {
                Cells[toX, toY] = Cells[fromX, fromY];
                Cells[fromX, fromY] = new Cell();
            }
        }
    }
}
