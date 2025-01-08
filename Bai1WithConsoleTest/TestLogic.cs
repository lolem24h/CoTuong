using Bai1WithConsole;

namespace Bai1WithConsoleTest
{
    [TestFixture]
    public class TestLogic
    {
        private Board board;
        private DataUtil dataUtil;
        private Cell cell;
        private ChessPieceType pieceType;
        private MainGame game;
        [SetUp]
        public void Setup()
        {
            board = new Board();
            dataUtil = new DataUtil();
            cell = new Cell();
            pieceType = new ChessPieceType();
            game = new MainGame();
            board.Cells[2, 0] = new Cell(ChessPieceType.Tinh, true); // Tượng đỏ
            board.Cells[2, 9] = new Cell(ChessPieceType.Tinh, false); // Tượng đen

        }

        //Test các nước đi không hợp lệ báo đỏ - hợp lệ báo xanh
        [TestCase(0,0,0,2)]//true
        [TestCase(0,0,1,0)]
        [TestCase(0,0,0,4)]
        
        
        public void ValidateXeMove_ReturnTrue(int fromX, int fromY, int toX, int toY)
        {
            // assign
            // act
            // assert
            Assert.IsTrue(dataUtil.ValidateMove(ChessPieceType.Xe, true, fromX, fromY, toX, toY, board));
        }
        [TestCase(1,0,3,1)]//true
        [TestCase(3,1,2,3)]
        [TestCase(1,0,2,2)]
        [TestCase(1,0,0,8)]
        public void ValidateMaMove_ReturnTrue(int fromX, int fromY, int toX, int toY)
        {
            // assign
            // act
            // assert
            Assert.IsTrue(dataUtil.ValidateMove(ChessPieceType.Ma, true, fromX, fromY, toX, toY, board));
        }
        // Quân tịnh chỉ có tối đa 7 vị trí được đi và không đi qua sông, không được đi nếu có cản ở giữa cạnh cung
        [TestCase(2, 0, 4, 2)]//true
        [TestCase(2, 0, 2, 2)]//true
        
        public void ValidateTinhMove_ReturnTrue(int fromX, int fromY, int toX, int toY)
        {
            // assign
            // act
            // assert
            Assert.IsTrue(dataUtil.ValidateMove(ChessPieceType.Tinh, true, fromX, fromY, toX, toY, board));
        }
        [TestCase(2, 0, 4, 2)]//false
        public void ValidateTinhMove_DiagonalMoveBlocked_ReturnsFalse(int fromX, int fromY, int toX, int toY)
        {
            // Đặt quân cờ chắn ở tâm điểm
            board.Cells[3, 1] = new Cell(ChessPieceType.Tuong, true); // Quân chắn đường
            Assert.IsTrue(dataUtil.ValidateMove(ChessPieceType.Tinh, true, fromX, fromY, toX, toY, board));
        }
        //Quân sĩ chỉ trong cung, đi chéo
        [TestCase(3, 0, 4, 1)]//true
        [TestCase(3, 2, 2, 3)]
        [TestCase(3, 2, 2, 2)]
        
        public void ValidateSiMove_ReturnTrue(int fromX, int fromY, int toX, int toY)
        {
            // assign
            // act
            // assert
            Assert.IsTrue(dataUtil.ValidateMove(ChessPieceType.Si, true, fromX, fromY, toX, toY, board));
        }
        //Quân tướng chỉ đi trong cung, đi ngang/dọc từng ô một
        [TestCase(4, 0, 4, 1)]//true
        [TestCase(4, 0, 5, 1)]
        [TestCase(4, 2, 4, 3)]
        
        public void ValidateTuongMove_ReturnTrue(int fromX, int fromY, int toX, int toY)
        {
            // assign
            // act
            // assert
            Assert.IsTrue(dataUtil.ValidateMove(ChessPieceType.Tuong, true, fromX, fromY, toX, toY, board));
        }
        
        [TestCase(1, 2, 1, 5)]//true
        [TestCase(1, 2, 3, 2)]//true
        [TestCase(1, 2, 1, 9)]//true
        [TestCase(1, 2, 2, 3)]
        public void ValidatePhaoMove_ReturnTrue(int fromX, int fromY, int toX, int toY)
        {
            // assign
            // act
            // assert
            Assert.IsTrue(dataUtil.ValidateMove(ChessPieceType.Phao, true, fromX, fromY, toX, toY, board));
        }

        [TestCase(0, 3, 0, 4)]//true
        [TestCase(0, 3, 0, 5)]
        [TestCase(0, 3, 1, 3)]
        

        public void ValidateTotMove_ReturnTrue_Disspass(int fromX, int fromY, int toX, int toY)
        {
            // assign
            // act
            // assert
            Assert.IsTrue(dataUtil.ValidateMove(ChessPieceType.Tot, true, fromX, fromY, toX, toY, board));
        }
        [TestCase(0, 5, 0, 6)]//true
        [TestCase(0, 5, 1, 5)]//true
        
        public void ValidateTotMove_ReturnTrue_Pass(int fromX, int fromY, int toX, int toY)
        {
            // assign
            // act
            board.MovePiece(0, 4, 0, 5); // Qua sông
            // assert
            Assert.IsTrue(dataUtil.ValidateMove(ChessPieceType.Tot, true, fromX, fromY, toX, toY, board));
        }

    }
}
