using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessProjectNEA
{
    public partial class ChessForm : Form
    {
        private string colour;
        private string currentlyselected;
        Dictionaries dictionaries = new Dictionaries();

        public ChessForm()
        {
            InitializeComponent();

        }
        private void showBoardWithMessageBox()
        {
            string test = "";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    test = test + "{" + i.ToString() + "," + j.ToString() + "}: " + dictionaries.getBoard(i, j) + " ";
                }
            }
            MessageBox.Show(test);
        }

        private void initializeDict()
        {

            if (colour == "white")
            {
                //Set the black pieces at the top
                dictionaries.setBoard(0,0,"BR");
                dictionaries.setBoard(1,0,"BH");
                dictionaries.setBoard(2,0,"BB");
                dictionaries.setBoard(3,0,"BQ");
                dictionaries.setBoard(4,0,"BK");
                dictionaries.setBoard(5,0,"BB");
                dictionaries.setBoard(6,0,"BH");
                dictionaries.setBoard(7,0,"BR");
                for (int i=0;i<8;i++)
                {
                    dictionaries.setBoard(i, 1,"BP");
                }
                //Set white pieces at bottom
                dictionaries.setBoard(0, 7, "WR");
                dictionaries.setBoard(1, 7, "WH");
                dictionaries.setBoard(2, 7, "WB");
                dictionaries.setBoard(3, 7, "WQ");
                dictionaries.setBoard(4, 7, "WK");
                dictionaries.setBoard(5, 7, "WB");
                dictionaries.setBoard(6, 7, "WH");
                dictionaries.setBoard(7, 7, "WR");
                for (int i = 0; i < 8; i++)
                {
                    dictionaries.setBoard(i, 6, "WP");
                }
            }
            else
            {
                //Do the code to set layout for black
                //Set the black pieces at the top
                dictionaries.setBoard(0, 0, "WR");
                dictionaries.setBoard(1, 0, "WH");
                dictionaries.setBoard(2, 0, "WB");
                dictionaries.setBoard(3, 0, "WK");
                dictionaries.setBoard(4, 0, "WQ");
                dictionaries.setBoard(5, 0, "WB");
                dictionaries.setBoard(6, 0, "WH");
                dictionaries.setBoard(7, 0, "WR");
                for (int i = 0; i < 8; i++)
                {
                    dictionaries.setBoard(i, 1, "WP");
                }
                //Set white pieces at bottom
                dictionaries.setBoard(0, 7, "BR");
                dictionaries.setBoard(1, 7, "BH");
                dictionaries.setBoard(2, 7, "BB");
                dictionaries.setBoard(3, 7, "BK");
                dictionaries.setBoard(4, 7, "BQ");
                dictionaries.setBoard(5, 7, "BB");
                dictionaries.setBoard(6, 7, "BH");
                dictionaries.setBoard(7, 7, "BR");
                for (int i = 0; i < 8; i++)
                {
                    dictionaries.setBoard(i, 6, "BP");
                }
            }

        }

        private void setImages()
        {
            //showBoardWithMessageBox();
            //For loop to iterate through the board co-ords and set the image of the button in the location
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    string abbreviation = dictionaries.getBoard(i, j);
                    string piecename = dictionaries.getPieceName(abbreviation);
                    string coordstring = i.ToString() + j.ToString();
                    if (piecename != "")
                    {
                        object obj = Properties.Resources.ResourceManager.GetObject(piecename);
                        Image image = (Bitmap)obj;
                        ((Button)Controls.Find("Button" + coordstring, true)[0]).Image = image;
                    } else
                    {
                        ((Button)Controls.Find("Button" + coordstring, true)[0]).Image = null;
                    }
                }
            }
            return;
        }
        private void initializeButtonConfig()
        {
            //This function should only be called once upon start.
            for (int i=0;i<8;i++)
            {
                for (int j=0;j<8;j++)
                {
                    string coordstring = i.ToString() + j.ToString();
                    ((Button)Controls.Find("Button" + coordstring, true)[0]).FlatAppearance.MouseOverBackColor = Color.Transparent;
                    ((Button)Controls.Find("Button" + coordstring, true)[0]).FlatAppearance.MouseDownBackColor = Color.Aqua;
                    ((Button)Controls.Find("Button" + coordstring, true)[0]).Click += new EventHandler(NewButton_Click);
                }
            }
        }
        private void clearUnselected()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    string buttonname = "Button" + x.ToString() + y.ToString();
                    if (Currentlyselected != x.ToString() + y.ToString())
                    {
                        Button button = (Button)Controls.Find(buttonname, true)[0];
                        button.FlatAppearance.MouseOverBackColor = Color.Transparent;
                        button.BackColor = Color.Transparent;
                    }
                }
            }
        }
        private void NewButton_Click(object sender, EventArgs e)
        {
            clearUnselected();
            Button btn = (Button)sender;
            string name = btn.Name;
            string coordstring = name.Substring(6);
            int i = (int)char.GetNumericValue(coordstring[0]);
            int j = (int)char.GetNumericValue(coordstring[1]);
            string pieceabbrev = dictionaries.getBoard(i, j);
            string piececolour = dictionaries.getPieceColour(pieceabbrev);
            string piecename = dictionaries.getPieceName(pieceabbrev);
            //MessageBox.Show(piecename);
            if (piecename!="" && Currentlyselected=="")
            {
                //MessageBox.Show("You selected " + piecename +".");
                Currentlyselected = coordstring;
                btn.BackColor = Color.Blue;
                btn.FlatAppearance.MouseOverBackColor = Color.Blue;
                pieceselected(piecename, coordstring);
            } else if (piecename!=""&&Currentlyselected!="")
            {
                if (piececolour==dictionaries.getPieceColourWithCoordString(Currentlyselected))
                {
                    //Selecting different piece
                    ((Button)Controls.Find("Button" + Currentlyselected, true)[0]).BackColor = Color.Transparent;
                    ((Button)Controls.Find("Button" + Currentlyselected, true)[0]).FlatAppearance.MouseOverBackColor = Color.Transparent;
                    Currentlyselected = coordstring;
                    btn.BackColor = Color.Blue;
                    btn.FlatAppearance.MouseOverBackColor = Color.Blue;
                    pieceselected(piecename, coordstring);
                } else
                {
                    //Trying to take a piece. Check if move is viable.
                    bool moveisviable = Viable(coordstring);
                    //MessageBox.Show(moveisviable.ToString());
                    Button button = (Button)Controls.Find("Button" + Currentlyselected, true)[0];
                    button.BackColor = Color.Transparent;
                    button.FlatAppearance.MouseOverBackColor = Color.Transparent;
                    if (moveisviable)
                    {
                        move(i, j);
                    } else
                    {
                        Currentlyselected = "";
                    }
                }

                //MessageBox.Show("Are you trying to take the " + piecename + " on " + coordstring + " with your " + dictionaries.getPieceWithCoordString(Currentlyselected));
            } else if (piecename == "" && Currentlyselected != "")
            {
                //Selected a blank button, but already got a piece selected. Check if move can be done
                if (Viable(coordstring))
                {
                    move(i, j);
                } else
                {
                    Button button = (Button)Controls.Find("Button" + Currentlyselected, true)[0];
                    button.BackColor = Color.Transparent;
                    button.FlatAppearance.MouseOverBackColor = Color.Transparent;
                    Currentlyselected = "";
                }
            } else
            {
                //Selected blank, nothing in currently selected. Do nothing
            }
        }
        private void move(int i, int j)
        {
            dictionaries.setBoard(i, j, dictionaries.getPieceAbbrevWithCoordString(Currentlyselected));
            int currentlyi = (int)char.GetNumericValue(Currentlyselected[0]);
            int currentlyj = (int)char.GetNumericValue(Currentlyselected[1]);
            Currentlyselected = "";
            dictionaries.setBoard(currentlyi, currentlyj, "");
            setImages();
            clearUnselected();
        }
        private bool Viable(string coordstring)
        {
            bool viable = true;
            string pieceabbrev = dictionaries.getPieceAbbrevWithCoordString(Currentlyselected);
            string piececolour = dictionaries.getPieceColourWithCoordString(Currentlyselected);
            int i = (int)char.GetNumericValue(coordstring[0]);
            int j = (int)char.GetNumericValue(coordstring[1]);
            int curi = (int)char.GetNumericValue(Currentlyselected[0]);
            int curj = (int)char.GetNumericValue(Currentlyselected[1]);
            int idiff = Math.Abs(curi-i);
            int jdiff = Math.Abs(curj-j);
            (int i, int j) attemptedmove = (i-curi, j-curj);
            //MessageBox.Show(attemptedmove.Item1.ToString() + attemptedmove.Item2.ToString());
            if (pieceabbrev == "WH" || pieceabbrev == "BH")
            {
                if (idiff == 2)
                {
                    if (jdiff != 1) { return false; }
                    else { return true; }
                }
                else if (idiff == 1)
                {
                    if (jdiff != 2) { return false; }
                    else { return true; }
                }
                else
                {
                    return false;
                }
            }
            else if (pieceabbrev == "WP" || pieceabbrev == "BP")
            {
                //MessageBox.Show(curi.ToString() + curj.ToString());
                idiff = curi - i;
                jdiff = curj - j;
                if (piececolour != colour)
                {
                    if (jdiff < 0)
                    {
                        if (jdiff == -1 && idiff == 0)
                        {
                            if (dictionaries.getPieceWithCoordString(coordstring) != "") { return false; }
                            else { return true; }
                        }
                        else if (jdiff == -1 && Math.Abs(idiff) == 1)
                        {
                            if (dictionaries.getPieceWithCoordString(coordstring) != "") { return true; }
                            else { return false; }
                        }
                        else if (jdiff == -1)
                        {
                            return false;
                        }
                        else if (jdiff == -2)
                        {
                            if (idiff != 0) { return false; }
                            if (curj==1) { return true; }
                            else { return false; }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else { return false; }
                }
                else
                {
                    if (idiff > 1) { return false; }
                    else if (idiff == 1||idiff==-1)
                    {
                        if (jdiff != 1) { return false; }
                        if (dictionaries.getPieceWithCoordString(coordstring)=="") { return false; };
                    }
                    else
                    {
                        if (jdiff == 1) { return true; }
                        if (jdiff < 0) { return false; }
                        if (jdiff>2) { return false; }
                        if (jdiff==2)
                        {
                            if (curj==6)
                            {
                                return true;
                            }
                            else
                            {
                                 return false;
                            }
                        }
                    }
                    
                    return true;
                }
            }
            else if (pieceabbrev=="BR" || pieceabbrev=="WR")
            {
                List<(int,int)>possiblemoves = new List<(int,int)> ();
                int upwardsmax = Math.Abs(0 - curj) + 1;
                int downwardsmax = Math.Abs(7 - curj) + 1;
                int leftmax = Math.Abs(0 - curi) + 1;
                int rightmax = Math.Abs(7 - curi) + 1;
                for (int x = 1; x < upwardsmax; x++)
                {
                    if (dictionaries.getPieceColourWithCoords(curi, curj - x) != piececolour)
                    {
                        possiblemoves.Add((0, -x));
                        if (dictionaries.getPieceColourWithCoords(curi, curj - x) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < downwardsmax; x++)
                {
                    if (dictionaries.getPieceColourWithCoords(curi, curj + x) != piececolour)
                    {
                        possiblemoves.Add((0, x));
                        if (dictionaries.getPieceColourWithCoords(curi, curj + x) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < leftmax; x++)
                {
                    if (dictionaries.getPieceColourWithCoords(curi - x, curj) != piececolour)
                    {
                        possiblemoves.Add((-x, 0));
                        if (dictionaries.getPieceColourWithCoords(curi - x, curj) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < rightmax; x++)
                {
                    if (dictionaries.getPieceColourWithCoords(curi + x, curj) != piececolour)
                    {
                        possiblemoves.Add((x, 0));
                        if (dictionaries.getPieceColourWithCoords(curi + x, curj) != "") { break; }
                    }
                    else { break; }
                }
                if (possiblemoves.Contains(attemptedmove)) {
                    return true; 
                } else {
                    return false; 
                }
            }
            else if (pieceabbrev=="BB" || pieceabbrev=="WB")
            {
                List<(int, int)> possiblemoves = new List<(int, int)>();
                int upwardsmax = Math.Abs(0 - curj) + 1;
                int downwardsmax = Math.Abs(7 - curj) + 1;
                int leftmax = Math.Abs(0 - curi) + 1;
                int rightmax = Math.Abs(7 - curi) + 1;
                for (int x = 1; x < leftmax; x++)
                {
                    //This loop is for left and down
                    if (x == downwardsmax) { break; }
                    if (dictionaries.getPieceColourWithCoords(curi - x, curj + x) != piececolour)
                    {
                        possiblemoves.Add((-x, x));
                        if (dictionaries.getPieceColourWithCoords(curi - x, curj + x) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < leftmax; x++)
                {
                    //This loop is for left and up
                    if (x == upwardsmax) { break; }
                    if (dictionaries.getPieceColourWithCoords(curi - x, curj - x) != piececolour)
                    {
                        possiblemoves.Add((-x, -x));
                        if (dictionaries.getPieceColourWithCoords(curi - x, curj - x) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < rightmax; x++)
                {
                    //This loop is for right and down
                    if (x == downwardsmax) { break; }
                    if (dictionaries.getPieceColourWithCoords(curi + x, curj + x) != piececolour)
                    {
                        possiblemoves.Add((x, x));
                        if (dictionaries.getPieceColourWithCoords(curi + x, curj + x) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < rightmax; x++)
                {
                    //This loop is for right and up
                    if (x == upwardsmax) { break; }
                    if (dictionaries.getPieceColourWithCoords(curi + x, curj - x) != piececolour)
                    {
                        possiblemoves.Add((x, -x));
                        if (dictionaries.getPieceColourWithCoords(curi + x, curj - x) != "") { break; }
                    }
                    else { break; }
                }
                if (possiblemoves.Contains(attemptedmove))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (pieceabbrev=="BQ" || pieceabbrev=="WQ")
            {
                List<(int, int)> possiblemoves = new List<(int, int)>();
                int upwardsmax = Math.Abs(0 - curj) + 1;
                int downwardsmax = Math.Abs(7 - curj) + 1;
                int leftmax = Math.Abs(0 - curi) + 1;
                int rightmax = Math.Abs(7 - curi) + 1;
                #region rookcodewithinqueen
                for (int x = 1; x < upwardsmax; x++)
                {
                    if (dictionaries.getPieceColourWithCoords(curi, curj - x) != piececolour)
                    {
                        possiblemoves.Add((0, -x));
                        if (dictionaries.getPieceColourWithCoords(curi, curj - x) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < downwardsmax; x++)
                {
                    if (dictionaries.getPieceColourWithCoords(curi, curj + x) != piececolour)
                    {
                        possiblemoves.Add((0, x));
                        if (dictionaries.getPieceColourWithCoords(curi, curj + x) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < leftmax; x++)
                {
                    if (dictionaries.getPieceColourWithCoords(curi - x, curj) != piececolour)
                    {
                        possiblemoves.Add((-x, 0));
                        if (dictionaries.getPieceColourWithCoords(curi - x, curj) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < rightmax; x++)
                {
                    if (dictionaries.getPieceColourWithCoords(curi + x, curj) != piececolour)
                    {
                        possiblemoves.Add((x, 0));
                        if (dictionaries.getPieceColourWithCoords(curi + x, curj) != "") { break; }
                    }
                    else { break; }
                }
                #endregion rook
                #region bishopcodewithinqueen
                for (int x = 1; x < leftmax; x++)
                {
                    //This loop is for left and down
                    if (x == downwardsmax) { break; }
                    if (dictionaries.getPieceColourWithCoords(curi - x, curj + x) != piececolour)
                    {
                        possiblemoves.Add((-x, x));
                        if (dictionaries.getPieceColourWithCoords(curi - x, curj + x) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < leftmax; x++)
                {
                    //This loop is for left and up
                    if (x == upwardsmax) { break; }
                    if (dictionaries.getPieceColourWithCoords(curi - x, curj - x) != piececolour)
                    {
                        possiblemoves.Add((-x, -x));
                        if (dictionaries.getPieceColourWithCoords(curi - x, curj - x) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < rightmax; x++)
                {
                    //This loop is for right and down
                    if (x == downwardsmax) { break; }
                    if (dictionaries.getPieceColourWithCoords(curi + x, curj + x) != piececolour)
                    {
                        possiblemoves.Add((x, x));
                        if (dictionaries.getPieceColourWithCoords(curi + x, curj + x) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < rightmax; x++)
                {
                    //This loop is for right and up
                    if (x == upwardsmax) { break; }
                    if (dictionaries.getPieceColourWithCoords(curi + x, curj - x) != piececolour)
                    {
                        possiblemoves.Add((x, -x));
                        if (dictionaries.getPieceColourWithCoords(curi + x, curj - x) != "") { break; }
                    }
                    else { break; }
                }
                #endregion
                if (possiblemoves.Contains(attemptedmove))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (pieceabbrev=="BK" || pieceabbrev=="WK")
            {
                //Havent even made this viable function yet shush
                List<(int, int)> possiblemoves = new List<(int, int)>();
                if (curi > 0)
                {
                    if (dictionaries.getPieceColourWithCoords(curi - 1, curj) != piececolour) { possiblemoves.Add((-1, 0)); }
                    if (curj > 0)
                    {
                        if (dictionaries.getPieceColourWithCoords(curi - 1, curj - 1) != piececolour) { possiblemoves.Add((-1, -1)); }
                    }
                    if (curj < 7)
                    {
                        if (dictionaries.getPieceColourWithCoords(curi - 1, curj + 1) != piececolour) { possiblemoves.Add((-1, 1)); }
                    }
                }
                if (curi < 7)
                {
                    if (dictionaries.getPieceColourWithCoords(curi + 1, curj) != piececolour) { possiblemoves.Add((1, 0)); }
                    if (curj > 0)
                    {
                        if (dictionaries.getPieceColourWithCoords(curi + 1, curj - 1) != piececolour) { possiblemoves.Add((1, -1)); }
                    }
                    if (curj < 7)
                    {
                        if (dictionaries.getPieceColourWithCoords(curi + 1, curj + 1) != piececolour) { possiblemoves.Add((1, 1)); }
                    }
                }
                if (curj < 7)
                {
                    if (dictionaries.getPieceColourWithCoords(curi, curj + 1) != piececolour) { possiblemoves.Add((0, 1)); }
                }
                if (curj > 0)
                {
                    if (dictionaries.getPieceColourWithCoords(curi, curj - 1) != piececolour) { possiblemoves.Add((0, -1)); }
                }
                if (possiblemoves.Contains(attemptedmove))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return viable;
        }
        private string Currentlyselected
        {
            get { return currentlyselected; }
            set { currentlyselected = value; }
        }

        private void highlight_poss(List<(int i, int j)> possiblemoves, string coordstring)
        {
            int currenti = (int)char.GetNumericValue(coordstring[0]);
            int currentj = (int)char.GetNumericValue(coordstring[1]);
            //This function is made to highlight possible moves that can be done. Not sure how I intend to undo this later but ah well.
            foreach ((int i, int j) curpossiblemove in possiblemoves)
            {
                int newi = curpossiblemove.Item1 + currenti;
                int newj = curpossiblemove.Item2 + currentj;
                string possiblemove = newi.ToString() + newj.ToString();
                if (dictionaries.getPieceColourWithCoordString(possiblemove) != dictionaries.getPieceColourWithCoordString(Currentlyselected))
                {
                    ((Button)Controls.Find("Button" + possiblemove, true)[0]).BackColor = Color.BlueViolet;
                    ((Button)Controls.Find("Button" + possiblemove, true)[0]).FlatAppearance.MouseOverBackColor = Color.BlueViolet;
                }
            }
        }

        private void pieceselected(string piecename,string coordstring)
        {
            int i = (int)char.GetNumericValue(coordstring[0]);
            int j = (int)char.GetNumericValue(coordstring[1]);
            List<(int i, int j)> possiblemoves = new List<(int i, int j)>();
            string piececolour = dictionaries.getPieceColourWithCoordString(Currentlyselected);
            if (piecename =="whiteknight" || piecename=="blackknight")
            {
                if (i > 1)
                {
                    if (j > 0) { possiblemoves.Add((-2,-1)); }
                    if (j < 7) { possiblemoves.Add((-2,1)); }
                }
                if (i < 6)
                {
                    if (j > 0) { possiblemoves.Add((2, -1)); }
                    if (j < 7) { possiblemoves.Add((2, 1)); }
                }
                if (j > 1)
                {
                    if (i > 0) { possiblemoves.Add((-1, -2)); }
                    if (i < 7) { possiblemoves.Add((1, -2)); }
                }
                if (j < 6)
                {
                    if (i > 0) { possiblemoves.Add((-1, 2)); }
                    if (i < 7) { possiblemoves.Add((1, 2)); }
                }
            } 
            else if (piecename=="whitepawn"|| piecename =="blackpawn")
            {
                if (piececolour!=colour)
                {
                    if (j==1) { possiblemoves.Add((0,2)); }
                    if (j < 7)
                    {
                        if (dictionaries.getPieceWithCoords(i, j + 1) == "") { possiblemoves.Add((0, 1)); }
                    }
                    if (i>0&&j<7)
                    {
                        if (dictionaries.getPieceWithCoords(i-1,j+1)!="") { possiblemoves.Add((-1, 1)); }
                    }
                    if (i<7&&j<7)
                    {
                        if (dictionaries.getPieceWithCoords(i+1,j+1)!="") { possiblemoves.Add((1,1)); }
                    }
                } 
                else
                {
                    if (j==6) { possiblemoves.Add((0, -2)); }
                    if (j>0)
                    {
                        if (dictionaries.getPieceWithCoords(i,j-1)=="") { possiblemoves.Add((0, -1)); }
                        if (i>0)
                        {
                            if (dictionaries.getPieceWithCoords(i-1,j-1)!="") { possiblemoves.Add((-1, -1)); }
                        }
                        if (i<7)
                        {
                            if (dictionaries.getPieceWithCoords(i+1,j-1)!="") { possiblemoves.Add((1, -1)); }
                        }
                    }
                }
            }
            else if (piecename=="whiterook"|| piecename == "blackrook")
            {
                int upwardsmax = Math.Abs(0 - j)+1;
                int downwardsmax = Math.Abs(7 - j) + 1;
                int leftmax = Math.Abs(0 - i) + 1;
                int rightmax=Math.Abs(7 - i) + 1;
                for (int x=1; x<upwardsmax; x++)
                {
                    if (dictionaries.getPieceColourWithCoords(i,j-x)!=piececolour) { 
                        possiblemoves.Add((0, -x));
                        if (dictionaries.getPieceColourWithCoords(i, j - x) != "") { break; }
                    }
                    else { break; }
                }
                for (int x =1;x<downwardsmax; x++)
                {
                    if (dictionaries.getPieceColourWithCoords(i, j + x) != piececolour) {
                        possiblemoves.Add((0, x));
                        if (dictionaries.getPieceColourWithCoords(i,j+x)!="") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < leftmax; x++)
                {
                    if (dictionaries.getPieceColourWithCoords(i-x, j) != piececolour) { 
                        possiblemoves.Add((-x, 0)); 
                        if (dictionaries.getPieceColourWithCoords(i - x, j)!="") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < rightmax; x++)
                {
                    if (dictionaries.getPieceColourWithCoords(i+x, j) != piececolour) { 
                        possiblemoves.Add((x, 0)); 
                        if (dictionaries.getPieceColourWithCoords(i + x, j)!="") { break; }
                    }
                    else { break; }
                }
            }
            else if (piecename=="whitebishop" || piecename == "blackbishop")
            {
                int upwardsmax = Math.Abs(0 - j) + 1;
                int downwardsmax = Math.Abs(7 - j) + 1;
                int leftmax = Math.Abs(0 - i) + 1;
                int rightmax = Math.Abs(7 - i) + 1;
                for (int x = 1; x < leftmax; x++)
                {
                    //This loop is for left and down
                    if (x==downwardsmax) { break; }
                    if (dictionaries.getPieceColourWithCoords(i - x, j+x) != piececolour)
                    {
                        possiblemoves.Add((-x, x));
                        if (dictionaries.getPieceColourWithCoords(i - x, j+x) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < leftmax; x++)
                {
                    //This loop is for left and up
                    if (x == upwardsmax) { break; }
                    if (dictionaries.getPieceColourWithCoords(i - x, j - x) != piececolour)
                    {
                        possiblemoves.Add((-x, -x));
                        if (dictionaries.getPieceColourWithCoords(i - x, j - x) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < rightmax; x++)
                {
                    //This loop is for right and down
                    if (x == downwardsmax) { break; }
                    if (dictionaries.getPieceColourWithCoords(i + x, j + x) != piececolour)
                    {
                        possiblemoves.Add((x, x));
                        if (dictionaries.getPieceColourWithCoords(i + x, j + x) != "") { break; }
                    }
                    else { break; }
                }
                //This code has a bug. [Patched]
                for (int x = 1; x < rightmax; x++)//This was set to leftmax not rightmax. This was the entire bug. This is now fixed.
                {
                    //This loop is for right and up
                    if (x == upwardsmax) { break; }
                    if (dictionaries.getPieceColourWithCoords(i + x, j - x) != piececolour)
                    {
                        possiblemoves.Add((x, -x));
                        if (dictionaries.getPieceColourWithCoords(i + x, j - x) != "") { break; }
                    }
                    else { break; }
                }

            }
            else if (piecename=="whitequeen" || piecename=="blackqueen")
            {
                int upwardsmax = Math.Abs(0 - j) + 1;
                int downwardsmax = Math.Abs(7 - j) + 1;
                int leftmax = Math.Abs(0 - i) + 1;
                int rightmax = Math.Abs(7 - i) + 1;
                #region rookcodewithinqueen
                for (int x = 1; x < upwardsmax; x++)
                {
                    if (dictionaries.getPieceColourWithCoords(i, j - x) != piececolour)
                    {
                        possiblemoves.Add((0, -x));
                        if (dictionaries.getPieceColourWithCoords(i, j - x) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < downwardsmax; x++)
                {
                    if (dictionaries.getPieceColourWithCoords(i, j + x) != piececolour)
                    {
                        possiblemoves.Add((0, x));
                        if (dictionaries.getPieceColourWithCoords(i, j + x) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < leftmax; x++)
                {
                    if (dictionaries.getPieceColourWithCoords(i - x, j) != piececolour)
                    {
                        possiblemoves.Add((-x, 0));
                        if (dictionaries.getPieceColourWithCoords(i - x, j) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < rightmax; x++)
                {
                    if (dictionaries.getPieceColourWithCoords(i + x, j) != piececolour)
                    {
                        possiblemoves.Add((x, 0));
                        if (dictionaries.getPieceColourWithCoords(i + x, j) != "") { break; }
                    }
                    else { break; }
                }
                #endregion
                #region bishopcodewithinqueen
                for (int x = 1; x < leftmax; x++)
                {
                    //This loop is for left and down
                    if (x == downwardsmax) { break; }
                    if (dictionaries.getPieceColourWithCoords(i - x, j + x) != piececolour)
                    {
                        possiblemoves.Add((-x, x));
                        if (dictionaries.getPieceColourWithCoords(i - x, j + x) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < leftmax; x++)
                {
                    //This loop is for left and up
                    if (x == upwardsmax) { break; }
                    if (dictionaries.getPieceColourWithCoords(i - x, j - x) != piececolour)
                    {
                        possiblemoves.Add((-x, -x));
                        if (dictionaries.getPieceColourWithCoords(i - x, j - x) != "") { break; }
                    }
                    else { break; }
                }
                for (int x = 1; x < rightmax; x++)
                {
                    //This loop is for right and down
                    if (x == downwardsmax) { break; }
                    if (dictionaries.getPieceColourWithCoords(i + x, j + x) != piececolour)
                    {
                        possiblemoves.Add((x, x));
                        if (dictionaries.getPieceColourWithCoords(i + x, j + x) != "") { break; }
                    }
                    else { break; }
                }
                //This code has a bug. [Patched]
                for (int x = 1; x < rightmax; x++)//This was set to leftmax not rightmax. This was the entire bug. This is now fixed.
                {
                    //This loop is for right and up
                    if (x == upwardsmax) { break; }
                    if (dictionaries.getPieceColourWithCoords(i + x, j - x) != piececolour)
                    {
                        possiblemoves.Add((x, -x));
                        if (dictionaries.getPieceColourWithCoords(i + x, j - x) != "") { break; }
                    }
                    else { break; }
                }
                #endregion

            }
            else if (piecename=="whiteking" || piecename=="blackking")
            {
                if (i > 0)
                {
                    if (dictionaries.getPieceColourWithCoords(i - 1, j) != piececolour) { possiblemoves.Add((-1, 0)); }
                    if (j>0)
                    {
                        if (dictionaries.getPieceColourWithCoords(i - 1, j - 1) != piececolour) { possiblemoves.Add((-1, -1)); }
                    }
                    if (j<7)
                    {
                        if (dictionaries.getPieceColourWithCoords(i - 1, j + 1) != piececolour) { possiblemoves.Add((-1, 1)); }
                    }
                }
                if (i < 7)
                {
                    if (dictionaries.getPieceColourWithCoords(i + 1, j) != piececolour) { possiblemoves.Add((1, 0)); }
                    if (j>0)
                    {
                        if (dictionaries.getPieceColourWithCoords(i + 1, j - 1) != piececolour) { possiblemoves.Add((1, -1)); }
                    }
                    if (j<7)
                    {
                        if (dictionaries.getPieceColourWithCoords(i + 1, j + 1) != piececolour) { possiblemoves.Add((1, 1)); }
                    }
                }
                if (j < 7)
                {
                    if (dictionaries.getPieceColourWithCoords(i, j + 1) != piececolour) { possiblemoves.Add((0, 1)); }
                }
                if (j>0)
                {
                    if (dictionaries.getPieceColourWithCoords(i, j - 1) != piececolour) { possiblemoves.Add((0, -1)); }
                }
            }
            highlight_poss(possiblemoves, coordstring);
        }

        private void ChessForm_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int num = rnd.Next(2);
            if (num==0) { colour = "white"; }
            else { colour = "black"; }
            currentlyselected = "";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var b = new Button();
                    b.Text = "";
                    b.Name=string.Format("Button{0}{1}",i,j);
                    //MessageBox.Show(b.Name);
                    b.Size = new Size(100, 100);
                    b.Location = new Point((i*100),(j*100));
                    b.Visible = true;
                    b.FlatStyle = FlatStyle.Flat;
                    b.BackColor = Color.Transparent;
                    b.BringToFront();
                    Controls.Add(b);
                }
            }
            initializeDict();
            setImages();
            initializeButtonConfig();
        }
    }
}
