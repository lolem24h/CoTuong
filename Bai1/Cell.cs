using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai1
{
    public class Cell
    {
        public ChessPieceType PieceType { get; set; }
        public bool IsRed { get; set; } // True: Đỏ, False: Đen

        public Cell(ChessPieceType pieceType = ChessPieceType.None, bool isRed = false)
        {
            PieceType = pieceType;
            IsRed = isRed;
        }
    }
}
