using Bai1WithConsole;

namespace Bai1WithConsoleTest
{
    [TestFixture]
    public class Tests
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
        }
        //Test dữ liệu input không đúng định dạng
        [TestCase("")]
        [TestCase("0,1,2")]
        [TestCase("a,b,c,d")]
        [TestCase("-1,0,5,5")]
        [TestCase("10,0,5,5")]
        public void ValidateInput_NullOrEmptyInput_ReturnsFalse(string input)
        {
            // Arrange
            //string input = "";

            // Act
            var result = MainGame.ValidateInput(input, out _, out _, out _, out _, true, board, dataUtil);

            // Assert
            Assert.IsFalse(result);
        }
        //Test input nhập vào đúng định dạng nhưng quân cờ không tồn tại 
        [Test]
        public void ValidateInput_NoPieceAtFromPosition_ReturnsFalse()
        {
            // Arrange
            string input = "0,1,2,2"; // Không có quân cờ tại (0,1)

            // Act
            var result = MainGame.ValidateInput(input, out _, out _, out _, out _, true, board, dataUtil);

            // Assert
            Assert.IsFalse(result);
        }
        //Test logic không hợp lệ theo turn
        [Test]
        public void ValidateInput_PieceBelongsToOpponent_ReturnsFalse()
        {
            // Arrange
            string input = "0,0,0,1"; // Quân cờ ở (0,0) là quân Đỏ, nhưng lượt Đen

            // Act
            var result = MainGame.ValidateInput(input, out _, out _, out _, out _, false, board, dataUtil);

            // Assert
            Assert.IsFalse(result);
        }
        //Test trường hợp input thỏa mãn full
        [Test]
        public void ValidateInput_ReturnTrue()
        {
            // Arrange
            string input = "0,0,0,1";

            // Act
            var result = MainGame.ValidateInput(input, out _, out _, out _, out _, true, board, dataUtil);

            // Assert
            Assert.IsTrue(result);
        }
        

    }
}