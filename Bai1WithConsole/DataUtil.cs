using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai1WithConsole
{
    public class DataUtil
    {
        public bool ValidateMove(ChessPieceType pieceType, bool isRed, int fromX, int fromY, int toX, int toY, Board board)
        {
            switch (pieceType)
            {
                case ChessPieceType.Xe:
                    return ValidateXeMove(fromX, fromY, toX, toY, board);
                case ChessPieceType.Ma:
                    return ValidateMaMove(fromX, fromY, toX, toY, board);
                case ChessPieceType.Tinh:
                    return ValidateTinhMove(fromX, fromY, toX, toY, board);
                case ChessPieceType.Si:
                    return ValidateSiMove(fromX, fromY, toX, toY, board);
                case ChessPieceType.Tuong:
                    return ValidateTuongMove(fromX, fromY, toX, toY, board);
                case ChessPieceType.Phao:
                    return ValidatePhaoMove(fromX, fromY, toX, toY, board);
                case ChessPieceType.Tot:
                    return ValidateTotMove(fromX, fromY, toX, toY, board);
                default:
                    return false;
            }
        }

        //logic di chuyển của Xe
        public bool ValidateXeMove(int fromX, int fromY, int toX, int toY, Board board)
        {
            // Xe chỉ đi thẳng theo hàng ngang hoặc dọc
            if (fromX != toX && fromY != toY) return false;

            // Kiểm tra có quân nào chặn đường không
            if (fromX == toX) // Di chuyển dọc
            {
                int start = Math.Min(fromY, toY);
                int end = Math.Max(fromY, toY);
                for (int y = start + 1; y < end; y++)
                {
                    //if (board[fromX, y] != 0) 
                    if (board.GetCell(fromX, y).PieceType != ChessPieceType.None)
                        return false;
                }
            }
            else // Di chuyển ngang
            {
                int start = Math.Min(fromX, toX);
                int end = Math.Max(fromX, toX);
                for (int x = start + 1; x < end; x++)
                {
                    //if (board[x, fromY] != 0) 
                    if (board.GetCell(x, fromY).PieceType != ChessPieceType.None)
                        return false;
                }
            }
            // Kiểm tra ô đích (toX, toY)
            var targetCell = board.GetCell(toX, toY);
            var sourceCell = board.GetCell(fromX, fromY);

            // Nếu ô đích có quân cùng màu, nước đi không hợp lệ
            if (targetCell.PieceType != ChessPieceType.None && targetCell.IsRed == sourceCell.IsRed)
                return false;

            return true;
        }
        //Logic di chuyển của Mã
        public bool ValidateMaMove(int fromX, int fromY, int toX, int toY, Board board)
        {
            // Mã đi hình chữ L: 2 ô theo một hướng và 1 ô theo hướng vuông góc
            int deltaX = Math.Abs(toX - fromX);
            int deltaY = Math.Abs(toY - fromY);
            if (!((deltaX == 2 && deltaY == 1) || (deltaX == 1 && deltaY == 2)))
                return false;

            // Kiểm tra chân mã (quân cản)
            int midX = fromX + (toX - fromX) / 2;
            int midY = fromY;
            if (deltaX == 1) // Di chuyển theo chiều dọc nhiều hơn
            {
                midX = fromX;
                midY = fromY + (toY - fromY) / 2;
            }
            return board.GetCell(midX, midY) == null;
            //return board[midX, midY] == 0;
        }
        //Logic di chuyển cho tượng / tịnh 
        public bool ValidateTinhMove(int fromX, int fromY, int toX, int toY, Board board)
        {
            // Tượng đi chéo 2 ô
            if (Math.Abs(toX - fromX) != 2 || Math.Abs(toY - fromY) != 2)
                return false;

            // Kiểm tra tâm điểm (điểm chặn)
            int midX = fromX + (toX - fromX) / 2;
            int midY = fromY + (toY - fromY) / 2;

            // Không được qua sông
            if (board.GetCell(fromX, fromY).IsRed)
            {
                if (toY > 4) return false; // Quân đỏ không được qua sông
            }
            else
            {
                if (toY < 5) return false; // Quân đen không được qua sông
            }

            return board.GetCell(midX, midY) == null;
        }
        //Logic di chuyển cho Sĩ
        public bool ValidateSiMove(int fromX, int fromY, int toX, int toY, Board board)
        {
            // Sĩ chỉ được đi chéo 1 ô
            if (Math.Abs(toX - fromX) != 1 || Math.Abs(toY - fromY) != 1)
                return false;

            // Kiểm tra trong cung
            if (toX < 3 || toX > 5) return false;
            if (!board.GetCell(fromX, fromY).IsRed)
            {
                if (toY < 7 || toY > 9) return false; // Giới hạn cung đỏ
            }
            else
            {
                if (toY < 0 || toY > 2) return false; // Giới hạn cung đen
            }

            return true;
        }
        //Logic di chuyển của tướng
        public bool ValidateTuongMove(int fromX, int fromY, int toX, int toY, Board board)
        {
            // Tướng chỉ được đi 1 ô, ngang hoặc dọc
            if ((Math.Abs(toX - fromX) == 1 && toY == fromY) ||
                (Math.Abs(toY - fromY) == 1 && toX == fromX))
            {
                // Kiểm tra trong cung
                if (toX < 3 || toX > 5) return false;
                if (!board.GetCell(fromX, fromY).IsRed)
                {
                    if (toY < 7 || toY > 9) return false; // Giới hạn cung đỏ
                }
                else
                {
                    if (toY < 0 || toY > 2) return false; // Giới hạn cung đen
                }
                return true;
            }
            return false;
        }
        //Logic di chuyển của pháo
        public bool ValidatePhaoMove(int fromX, int fromY, int toX, int toY, Board board)
        {
            // Pháo đi thẳng như xe
            if (fromX != toX && fromY != toY) return false;

            int pieceCount = 0;
            if (fromX == toX) // Di chuyển dọc
            {
                int start = Math.Min(fromY, toY);
                int end = Math.Max(fromY, toY);
                for (int y = start + 1; y < end; y++)
                {
                    //if (board[fromX, y] != 0) pieceCount++;
                    if (board.GetCell(fromX, y).PieceType != ChessPieceType.None) pieceCount++;
                }
            }
            else // Di chuyển ngang
            {
                int start = Math.Min(fromX, toX);
                int end = Math.Max(fromX, toX);
                for (int x = start + 1; x < end; x++)
                {
                    //if (board[x, fromY] != 0) pieceCount++;
                    if (board.GetCell(x, fromY).PieceType != ChessPieceType.None) pieceCount++;
                }
            }

            // Nếu ô đích có quân địch, cần đúng 1 quân chặn để ăn
            // Nếu ô đích trống, không được có quân chặn
            return board.GetCell(toX, toY).PieceType != ChessPieceType.None ? pieceCount == 1 : pieceCount == 0;
        }
        //Logic di chuyển của Tốt
        public bool ValidateTotMove(int fromX, int fromY, int toX, int toY, Board board)
        {
            // Lấy hướng di chuyển dựa trên quân cờ
            int direction = (board.GetCell(fromX, fromY).IsRed) ? 1 : -1; // 1 cho quân đỏ, -1 cho quân đen

            // Nếu quân tốt chưa qua sông (y < 5)
            if ((board.GetCell(fromX, fromY).IsRed && fromY <= 4) || // Quân đỏ, y > 4 tức là đã qua sông
                (!board.GetCell(fromX, fromY).IsRed && fromY >= 5)) // Quân đen, y < 5 tức là chưa qua sông
            {
                return toX == fromX && (toY - fromY) == direction; // Di chuyển thẳng 1 ô
            }
            else
            {
                // Quân đã qua sông, có thể di chuyển ngang 1 ô hoặc tiến 1 ô
                if (toY == fromY && Math.Abs(toX - fromX) == 1) return true; // Di chuyển ngang 1 ô
                if (toX == fromX && (toY - fromY) == direction) return true; // Di chuyển thẳng 1 ô
            }

            return false; // Nếu không thỏa mãn các điều kiện trên
        }
    }
}
