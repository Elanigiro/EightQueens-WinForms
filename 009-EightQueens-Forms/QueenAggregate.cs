using System.Drawing;
using System.Windows.Forms;

namespace BusinessLogic
{

    internal class QueenAggregate {

        public const int QUEEN_MAX = 8;

        private FlowLayoutPanel ChessboardGUI = null;
        private TextBox Counter = new TextBox();

        public int AggregateLen { get; private set; }
        private Queen[] Queens;

        public QueenAggregate(Queen first, FlowLayoutPanel chessboardGUI, TextBox counter) {

            Queens = new Queen[QUEEN_MAX];
            Queens[0] = first;
            AggregateLen = 1;
            this.ChessboardGUI = chessboardGUI;
            this.Counter = counter;
        }

        public bool MakeMove() {

            //Base Case - won
            if (AggregateLen == QUEEN_MAX) {

                return true;
            }

            //Recursive Case - NOTE: I start from the first row to make this loop compatible with any starting position
            for (int i = 0; i < Chessboard.SIZE; ++i) {

                for (int j = 0; j < Chessboard.SIZE; ++j) {

                    Queen nextQueen = new Queen(new Coordinate(i, j));

                    if (IsValidMove(nextQueen.Position)) {

                        AddMove(nextQueen);                 

                        if (MakeMove() == true) {

                            return true;
                        }

                        CancelMove(nextQueen);
                    }
                }
            }

            //Base Case - lost
            return false;
        }

        private void CancelMove(Queen move) {

            --AggregateLen;

            //UPDATE THE FORM - if present
            if (ChessboardGUI != null) {

                Button b = ChessboardGUI.Controls[$"btn{move.Position.x}{move.Position.y}"] as Button;
                b.BackColor = Color.LightPink;
                b.Text = "";
            }

        }

        private void AddMove(Queen move) {

            Queens[AggregateLen] = move;
            ++AggregateLen;

            //UPDATE THE FORM - if present
            if (ChessboardGUI != null) {

                Button b = ChessboardGUI.Controls[$"btn{move.Position.x}{move.Position.y}"] as Button;
                b.BackColor = Color.LightGreen;
                b.Text = (this.AggregateLen - 1).ToString();

                Counter.Text = (int.Parse(Counter.Text) + 1).ToString();

                //Force Rendering of Events
                Application.DoEvents();
            }
        }

        /// <summary>
        /// Checks if the move is valid
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        private bool IsValidMove(Coordinate move) {

            //Out of Bounds - IT SHOULDN'T BE NECESSARY!
            //if (((move.x < 0) || (move.x > Chessboard.SIZE)) || ((move.y < 0) || (move.y > Chessboard.SIZE))) {

            //    return false;
            //}

            //Cell Not Free
            if (!IsFreeCell(move)) {

                return false;
            }

            //Check by other Queen
            if (IsCheck(move)) {

                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if the queen is in Check
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        private bool IsCheck(Coordinate move) {

            for (int i = 0; (i < AggregateLen); ++i) {

                //diagonal lines
                if (((Queens[i].Position.x + Queens[i].Position.y) == (move.x + move.y)) || ((Queens[i].Position.x - Queens[i].Position.y) == (move.x - move.y))) {

                    return true;
                }

                //vertical/horizontal lines
                if ((Queens[i].Position.x == move.x) || (Queens[i].Position.y == move.y)) {

                    return true;
                }
            }

            return false;
        }

        private bool IsFreeCell(Coordinate cell) {

            for (int i = 0; i < AggregateLen; ++i) {

                if ((Queens[i].Position.x == cell.x) && (Queens[i].Position.y == cell.y)) {

                    return false;
                }
            }

            return true;
        }
    }
}
