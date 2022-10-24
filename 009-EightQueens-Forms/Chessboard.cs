using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic {

    internal struct Coordinate {

        public int x;
        public int y;

        public Coordinate(int x, int y) {

            this.x = x;
            this.y = y;
        }
    }

    internal static class Chessboard {

        public const int SIZE = 8;
    }
}
