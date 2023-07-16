using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectNEA
{
    class Dictionaries
    {
        private Dictionary<string, string> piecenames;
        private Dictionary<(int, int), string> board;
        public Dictionaries()
        {
            Dictionary<string, string> piecenames = new Dictionary<string, string>()
            {
                { "BR", "blackrook" },
                { "BH", "blackknight" },
                { "BB", "blackbishop" },
                { "BQ", "blackqueen" },
                { "BK", "blackking" },
                { "BP", "blackpawn" },
                { "WR", "whiterook" },
                { "WH", "whiteknight" },
                { "WB", "whitebishop" },
                { "WQ", "whitequeen" },
                { "WK", "whiteking" },
                { "WP", "whitepawn" },
                { "", ""}
            };
            Piecenames = piecenames;
            //Coords are as follows: Starts from top left, first value of tuple is x and second is y.
            //In chess board terms this means a8 is (0,0) whereas h1 is (7,7)
            Dictionary<(int, int), string> board = new Dictionary<(int, int), string>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board.Add((i, j), "");
                }
            }
            Board = board;
        }
        public Dictionary<(int, int), string> Board
        {
            get { return board; }
            set { board = value; }
        }
        public Dictionary<string,string> Piecenames
        {
            get { return piecenames; }
            set { piecenames = value; }
        }
        public string getPieceName(string abbreviation)
        {
            return Piecenames[abbreviation];
        }
        public Dictionary<(int,int),string> getBoard()
        {
            return Board;
        }
        public void setBoard(int x, int y,string value)
        {
            string[] possiblevalues = new string[] { "BR", "BH", "BB", "BQ", "BK", "BP", "WR", "WH", "WB", "WQ", "WK", "WK", "WP", ""};
            if (!possiblevalues.Contains(value)) { return; }
            Board[(x, y)] = value;
        }
        public string getPieceColour(string abbreviation)
        {
            string piecename = Piecenames[abbreviation];
            if (piecename=="")
            {
                return "";
            }
            string colour = piecename.Substring(0, 5);
            return colour;
        }
        public string getPieceWithCoordString(string coordstring)
        {
            int i = (int)char.GetNumericValue(coordstring[0]);
            int j = (int)char.GetNumericValue(coordstring[1]);
            string pieceabbrev = getBoard(i, j);
            string piecename = getPieceName(pieceabbrev);
            return piecename;
        }
        public string getPieceWithCoords(int i, int j)
        {
            string pieceabbrev = getBoard(i, j);
            string piecename = getPieceName(pieceabbrev);
            return piecename;
        }
        public string getPieceColourWithCoords(int i,int j)
        {
            string pieceabbrev = getBoard(i,j);
            string piecename = getPieceName(pieceabbrev);
            if (piecename=="") { return ""; }
            string colour = piecename.Substring(0, 5);
            return colour;
        }
        public string getPieceAbbrevWithCoordString(string coordstring)
        {
            int i = (int)char.GetNumericValue(coordstring[0]);
            int j = (int)char.GetNumericValue(coordstring[1]);
            string pieceabbrev = getBoard(i, j);
            return pieceabbrev;
        }
        public string getPieceColourWithCoordString(string coordstring)
        {
            string piecename = getPieceWithCoordString(coordstring);
            if (piecename == "")
            {
                return "";
            }
            string colour = piecename.Substring(0, 5);
            return colour;
        }
        public string getBoard(int x,int y)
        {
            if ((!(x>=0 && x<8)) || (!(y>=0&&y<8))) {
                throw new ArgumentOutOfRangeException("Parameter index is out of range.");
            }
            return Board[(x, y)];
        }
    }
}
