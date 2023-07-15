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
                    ((Button)Controls.Find("Button" + coordstring, true)[0]).FlatAppearance.MouseDownBackColor = Color.Red;
                    ((Button)Controls.Find("Button" + coordstring, true)[0]).Click += new EventHandler(NewButton_Click);
                }
            }
        }
        private void NewButton_Click(object sender, EventArgs e)
        {
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
            } else if (piecename!=""&&Currentlyselected!="")
            {
                if (piececolour==dictionaries.getPieceColourWithCoordString(Currentlyselected))
                {
                    ((Button)Controls.Find("Button" + Currentlyselected, true)[0]).BackColor = Color.Transparent;
                    ((Button)Controls.Find("Button" + Currentlyselected, true)[0]).FlatAppearance.MouseOverBackColor = Color.Transparent;
                    Currentlyselected = coordstring;
                    btn.BackColor = Color.Blue;
                    btn.FlatAppearance.MouseOverBackColor = Color.Blue;
                }

                //MessageBox.Show("Are you trying to take the " + piecename + " on " + coordstring + " with your " + dictionaries.getPieceWithCoordString(Currentlyselected));
            } else if (piecename == "" && Currentlyselected != "")
            {
                //Selected a blank button, but already got a piece selected. Check if move can be done
                MessageBox.Show("Are you trying to move to " + coordstring + " with your " + dictionaries.getPieceWithCoordString(Currentlyselected));
            } else
            {
                //Selected blank, nothing in currently selected. Do nothing
            }
        }
        private string Currentlyselected
        {
            get { return currentlyselected; }
            set { currentlyselected = value; }
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
