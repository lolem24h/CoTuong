using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai1
{
    internal class MainGame
    {
        private Board board;
        private DataUtil dataUtil;
        private bool isRedTurn;

        public MainGame()
        {
            board = new Board();
            dataUtil = new DataUtil();
            isRedTurn = true; // Đỏ đi trước
        }
        public void Start()
        {
            Board board = new Board();
            DataUtil dataUtil = new DataUtil();

            StartGame(board, dataUtil);
            Console.ReadKey();
        }
        public static bool ValidateInput(string input, out int fromX, out int fromY, out int toX, out int toY, bool isRedTurn, Board board, DataUtil dataUtil)
        {
            fromX = fromY = toX = toY = -1;

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Không có đầu vào hợp lệ. Vui lòng nhập lại!");
                return false;
            }

            if (input.ToLower() == "exit")
            {
                Console.WriteLine("Game đã kết thúc!");
                return false;
            }

            var parts = input.Split(',');

            if (parts.Length != 4 ||
                !int.TryParse(parts[0].Trim(), out fromX) ||
                !int.TryParse(parts[1].Trim(), out fromY) ||
                !int.TryParse(parts[2].Trim(), out toX) ||
                !int.TryParse(parts[3].Trim(), out toY))
            {
                Console.WriteLine("Đầu vào không hợp lệ. Vui lòng nhập lại!");
                return false;
            }

            if (fromX < 0 || fromY < 0 || toX < 0 || toY < 0)
            {
                Console.WriteLine("Tọa độ phải là số nguyên dương. Vui lòng nhập lại!");
                return false;
            }

            var piece = board.GetCell(fromX, fromY);
            if (piece == null || piece.PieceType == ChessPieceType.None || piece.IsRed != isRedTurn)
            {
                Console.WriteLine("Nước đi không hợp lệ: Sai lượt hoặc không có quân cờ!");
                return false;
            }

            if (!dataUtil.ValidateMove(piece.PieceType, piece.IsRed, fromX, fromY, toX, toY, board))
            {
                Console.WriteLine("Nước đi không hợp lệ theo luật di chuyển!");
                return false;
            }

            Console.WriteLine($"Nước đi hợp lệ: từ ({fromX}, {fromY}) đến ({toX}, {toY})");
            Console.ReadKey();
            return true;
        }
        public static void StartGame(Board board, DataUtil dataUtil)
        {
            string input;
            int fromX, fromY, toX, toY;
            bool isRedTurn = true; 
            
            while (true) 
            {
                Console.WriteLine($"Lượt của {(isRedTurn ? "Người chơi ĐỎ" : "Người chơi ĐEN")}");

                do
                {
                    if (isRedTurn)
                    {
                        // Người chơi Đỏ nhập nước đi
                        Console.Write("Người chơi ĐỎ, nhập nước đi (x1, y1, x2, y2): ");
                        input = Console.ReadLine();

                        // Gọi ValidateInput để kiểm tra nước đi
                        if (ValidateInput(input, out fromX, out fromY, out toX, out toY, isRedTurn, board, dataUtil))
                        {
                            board.MovePiece(fromX, fromY, toX, toY); // Thực hiện di chuyển quân cờ
                            isRedTurn = !isRedTurn; // Đổi lượt
                            Console.WriteLine("Đổi lượt thành công!\n");
                            break;
                        }
                    }
                    else
                    {
                        // Máy tính (người chơi Đen) thực hiện nước đi
                        var (compFromX, compFromY, compToX, compToY) = GetComputerMove(isRedTurn, board, dataUtil);
                        Console.WriteLine($"Người chơi ĐEN (máy tính) chọn nước đi: ({compFromX}, {compFromY}) đến ({compToX}, {compToY})");

                        // Di chuyển quân cờ (nước đi đã được validate trong GetComputerMove)
                        board.MovePiece(compFromX, compFromY, compToX, compToY);
                        isRedTurn = !isRedTurn; // Đổi lượt
                        Console.WriteLine("Đổi lượt thành công!\n");
                        break;
                    }
                }
                while (true); // Lặp lại nếu nước đi không hợp lệ
                Console.ReadKey();
            }
        }
        
        private static (int fromX, int fromY, int toX, int toY) GetComputerMove(bool isRedTurn, Board board, DataUtil dataUtil)
        {
            Random rnd = new Random();
            int fromX, fromY, toX, toY;

            while (true)
            {
                fromX = rnd.Next(0, 9);
                fromY = rnd.Next(0, 9);
                toX = rnd.Next(0, 9);
                toY = rnd.Next(0, 9);

                // Chuyển nước đi thành chuỗi input để sử dụng ValidateInput
                string input = $"{fromX},{fromY},{toX},{toY}";

                if (ValidateInput(input, out _, out _, out _, out _, isRedTurn, board, dataUtil))
                {
                    return (fromX, fromY, toX, toY);
                }
            }
        }
    }
}
