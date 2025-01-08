using Bai1WithConsole;

namespace Bai1WithConsoleTest
{
    [TestFixture]
    public class TestStatusBoard
    {
        private Board board;
        
        [SetUp]
        public void Setup()
        {
            board = new Board();
        }
        [Test]
        public void IsValidPosition_ValidPosition_ReturnsTrue()
        {
            Assert.IsTrue(board.IsValidPosition(0, 0));
            Assert.IsTrue(board.IsValidPosition(8, 9));
        }

        [Test]
        public void IsValidPosition_InvalidPosition_ReturnsFalse()
        {
            Assert.IsFalse(board.IsValidPosition(-1, 0));
            Assert.IsFalse(board.IsValidPosition(9, 10));
            Assert.IsFalse(board.IsValidPosition(0, -1));
        }

        [Test]
        public void GetCell_ValidPosition_ReturnsCorrectCell()
        {
            var cell = board.GetCell(0, 0);
            Assert.IsNotNull(cell);
            Assert.AreEqual(ChessPieceType.Xe, cell.PieceType);
            Assert.IsTrue(cell.IsRed);
        }

        [Test]
        public void GetCell_InvalidPosition_ReturnsNull()
        {
            var cell = board.GetCell(-1, 0);
            Assert.IsNull(cell);

            cell = board.GetCell(9, 10);
            Assert.IsNull(cell);
        }

        [Test]
        public void RemovePiece_ValidPositionWithPiece_RemovesPieceAndReturnsType()
        {
            var pieceType = board.RemovePiece(0, 0);
            Assert.AreEqual(ChessPieceType.Xe, pieceType);

            var cell = board.GetCell(0, 0);
            Assert.AreEqual(ChessPieceType.None, cell.PieceType);
        }

        [Test]
        public void RemovePiece_EmptyCellOrInvalidPosition_ReturnsNone()
        {
            var pieceType = board.RemovePiece(4, 4); // Ô trống
            Assert.AreEqual(ChessPieceType.None, pieceType);

            pieceType = board.RemovePiece(-1, 0); // Vị trí không hợp lệ
            Assert.AreEqual(ChessPieceType.None, pieceType);
        }

        [Test]
        public void MovePiece_ValidMove_UpdatesBoardCorrectly()
        {
            board.MovePiece(0, 0, 4, 4);

            var fromCell = board.GetCell(0, 0);
            var toCell = board.GetCell(4, 4);

            Assert.AreEqual(ChessPieceType.None, fromCell.PieceType);
            Assert.AreEqual(ChessPieceType.Xe, toCell.PieceType);
            Assert.IsTrue(toCell.IsRed);
        }

        [Test]
        public void MovePiece_CapturePiece_UpdatesBoardCorrectly()
        {
            // Di chuyển Xe đỏ (0, 0) đến vị trí của Tot đỏ (0, 3)
            board.MovePiece(0, 0, 0, 3);

            var fromCell = board.GetCell(0, 0);
            var toCell = board.GetCell(0, 3);

            Assert.AreEqual(ChessPieceType.None, fromCell.PieceType);
            Assert.AreEqual(ChessPieceType.Xe, toCell.PieceType);
            Assert.IsTrue(toCell.IsRed);
        }

        [Test]
        public void MovePiece_InvalidMove_DoesNotChangeBoard()
        {
            // Di chuyển từ vị trí không hợp lệ
            board.MovePiece(-1, 0, 4, 4);

            var fromCell = board.GetCell(0, 0);
            var toCell = board.GetCell(4, 4);

            Assert.AreEqual(ChessPieceType.Xe, fromCell.PieceType); // Ô gốc không đổi
            Assert.AreEqual(ChessPieceType.None, toCell.PieceType); // Ô đích không đổi
        }
    }
}
