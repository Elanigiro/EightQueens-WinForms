using System;
using System.Drawing;
using System.Windows.Forms;

using BusinessLogic;

namespace _009_EightQueens_Forms
{
    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();

            progressBar1.Visible = false;

            //initialize chessboard
            for (int i = 0; i < Chessboard.SIZE * Chessboard.SIZE; ++i) {

                Button b = new Button();
                b.Name = "btn" 
                        + ((i < Chessboard.SIZE) ? "0" : "")
                        + Convert.ToString(i, Chessboard.SIZE);
                b.BackColor = Color.LightPink;
                b.Size = new System.Drawing.Size(34, 34);
                b.Padding = Padding.Empty; 
                b.TabIndex = 0;
                b.Enabled = false;

                this.flowLayoutPanel1.Controls.Add(b);
            }

            numRow.Maximum = Chessboard.SIZE;
            numCol.Maximum = Chessboard.SIZE;

            txtCounter.Text = 0.ToString();
        }

        private void btnExecute_Click(object sender, EventArgs e) {

            //disable Clear button
            btnClear.Enabled = false;            
            //disable Execute button
            btnExecute.Enabled = false;            
            //disable numRow/numCol button
            numRow.Enabled = false;
            numCol.Enabled = false;

            //cleanup
            cleanUp();

            //setup progressBar
            progressBar1.Visible = true;

            //starting position
            Queen firstQueen = new Queen(new Coordinate(((int)numRow.Value - 1), ((int)numCol.Value - 1)));
            QueenAggregate queenAggregate = new QueenAggregate(firstQueen, this.flowLayoutPanel1, this.txtCounter);

            //UPDATE THE FORM - FIRST QUEEN
            Button b = flowLayoutPanel1.Controls.Find($"btn{firstQueen.Position.x}{firstQueen.Position.y}", true)[0] as Button;
            b.BackColor = Color.LightGreen;
            b.Text = 0.ToString();

            DateTime dtStart = DateTime.Now;
            //Recursion
            String result;
            if (queenAggregate.MakeMove() == true) {

                result = "Problem Solved!";
            }
            else {

                result = "I could not solve the problem!";
            }
            //hide progressBar
            progressBar1.Visible = false;

            DateTime dtEnd = DateTime.Now;
            MessageBox.Show(result + "\nElapsed time: " + dtEnd.Subtract(dtStart).ToString());

            //enable Clear button
            btnClear.Enabled = true;
            //enable Execute button
            btnExecute.Enabled = true;
            //enable numRow/numCol button
            numRow.Enabled = true;
            numCol.Enabled = true;
        }

        private void btnClear_Click(object sender, EventArgs e) {

            cleanUp();
        }

        private void cleanUp() {

            foreach (Button b in flowLayoutPanel1.Controls) {

                b.BackColor = Color.LightPink;
                b.Text = "";
            }

            txtCounter.Text = 0.ToString();
        }

    }
}
