class TicTacState {
    private List<TicTacSquare> gameBoard = new List<TicTacSquare>();
    private List<int> emptySquares = new List<int>();
    private int numSquares;
    private int numRowsOrCols;
    private int mostRecentO;
    private int depth;
    private int value = 0;
    private bool terminalState = false;

    // constructor from TicTacGame
    public TicTacState(TicTacGame game) {
        this.gameBoard = game.getListCopy();
        this.numSquares = gameBoard.Count;
        //Console.WriteLine("numsquares: " + this.numSquares);
        this.numRowsOrCols = (int)Math.Sqrt(numSquares);
        //Console.WriteLine("numrowsorcols: " + this.numRowsOrCols);
        //this.print();

        // iterate through squares, adding to list all empty square indexes
        for (int i=0; i<numSquares; ++i) {
            if (!gameBoard[i].isFlipped()) {
                this.emptySquares.Add(i);
            }
        }
        //Console.WriteLine("numempty: " + this.getNumEmptySquares());

        this.depth = numSquares - getNumEmptySquares();
        //Console.WriteLine("depth: " + this.depth);
    }

    // create TicTacState from a TicTacState (deep copy)
    public TicTacState(TicTacState state) {
        this.numSquares = state.numSquares;
        this.numRowsOrCols = state.numRowsOrCols;

        // deep copy gameBoard
        for (int i=0; i<numSquares; ++i) {
            TicTacSquare squareCopy = new TicTacSquare(state.gameBoard[i]);
            this.gameBoard.Add(squareCopy);
        }
        this.depth = state.depth+1;

        // copy empty squares from parent state
        this.emptySquares = state.emptySquares.ToList();
    }

    // test constructor 3x3
    public TicTacState(String a, String b, String c, String d, String e, String f, String g, String h, String i) {
        this.numSquares = 9;
        this.numRowsOrCols = 3;
        for (int z=0; z<9; ++z) {
            gameBoard.Add(new TicTacSquare(0));
        }
        switch (a) {
            case "":
                break;
            case "x":
                gameBoard[0].flip(1);
                break;
            case "o":
                gameBoard[0].flip(2);
                break;
        }
        switch (b) {
            case "":
                break;
            case "x":
                gameBoard[1].flip(1);
                break;
            case "o":
                gameBoard[1].flip(2);
                break;
        }
        switch (c) {
            case "":
                break;
            case "x":
                gameBoard[2].flip(1);
                break;
            case "o":
                gameBoard[2].flip(2);
                break;
        }
        switch (d) {
            case "":
                break;
            case "x":
                gameBoard[3].flip(1);
                break;
            case "o":
                gameBoard[3].flip(2);
                break;
        }
        switch (e) {
            case "":
                break;
            case "x":
                gameBoard[4].flip(1);
                break;
            case "o":
                gameBoard[4].flip(2);
                break;
        }
        switch (f) {
            case "":
                break;
            case "x":
                gameBoard[5].flip(1);
                break;
            case "o":
                gameBoard[5].flip(2);
                break;
        }
        switch (g) {
            case "":
                break;
            case "x":
                gameBoard[6].flip(1);
                break;
            case "o":
                gameBoard[6].flip(2);
                break;
        }
        switch (h) {
            case "":
                break;
            case "x":
                gameBoard[7].flip(1);
                break;
            case "o":
                gameBoard[7].flip(2);
                break;
        }
        switch (i) {
            case "":
                break;
            case "x":
                gameBoard[8].flip(1);
                break;
            case "o":
                gameBoard[8].flip(2);
                break;
        }

        // iterate through squares, adding to list all empty square indexes
        for (int z=0; z<numSquares; ++z) {
            if (!gameBoard[z].isFlipped()) {
                this.emptySquares.Add(z);
            }
        }

        this.depth = numSquares - getNumEmptySquares();

        // print gameBoard
        for (int z=0; z<9; ++z) {
            Console.Write(gameBoard[z].getFace());
            if (gameBoard[z].getFace().Equals(""))
                Console.Write("_");
            if ((z+1)%3 == 0) {
                Console.WriteLine();
            }
        }
    }

    // test constructor 4x4
    public TicTacState(String a, String b, String c, String d, String e, String f, String g, String h, String i, String j, String k, String l, String m, String n, String o, String p) {
        this.numSquares = 16;
        this.numRowsOrCols = 4;
        for (int z=0; z<16; ++z) {
            gameBoard.Add(new TicTacSquare(0));
        }
        switch (a) {
            case "":
                break;
            case "x":
                gameBoard[0].flip(1);
                break;
            case "o":
                gameBoard[0].flip(2);
                break;
        }
        switch (b) {
            case "":
                break;
            case "x":
                gameBoard[1].flip(1);
                break;
            case "o":
                gameBoard[1].flip(2);
                break;
        }
        switch (c) {
            case "":
                break;
            case "x":
                gameBoard[2].flip(1);
                break;
            case "o":
                gameBoard[2].flip(2);
                break;
        }
        switch (d) {
            case "":
                break;
            case "x":
                gameBoard[3].flip(1);
                break;
            case "o":
                gameBoard[3].flip(2);
                break;
        }
        switch (e) {
            case "":
                break;
            case "x":
                gameBoard[4].flip(1);
                break;
            case "o":
                gameBoard[4].flip(2);
                break;
        }
        switch (f) {
            case "":
                break;
            case "x":
                gameBoard[5].flip(1);
                break;
            case "o":
                gameBoard[5].flip(2);
                break;
        }
        switch (g) {
            case "":
                break;
            case "x":
                gameBoard[6].flip(1);
                break;
            case "o":
                gameBoard[6].flip(2);
                break;
        }
        switch (h) {
            case "":
                break;
            case "x":
                gameBoard[7].flip(1);
                break;
            case "o":
                gameBoard[7].flip(2);
                break;
        }
        switch (i) {
            case "":
                break;
            case "x":
                gameBoard[8].flip(1);
                break;
            case "o":
                gameBoard[8].flip(2);
                break;
        }
        switch (j) {
            case "":
                break;
            case "x":
                gameBoard[9].flip(1);
                break;
            case "o":
                gameBoard[9].flip(2);
                break;
        }
        switch (k) {
            case "":
                break;
            case "x":
                gameBoard[10].flip(1);
                break;
            case "o":
                gameBoard[10].flip(2);
                break;
        }
        switch (l) {
            case "":
                break;
            case "x":
                gameBoard[11].flip(1);
                break;
            case "o":
                gameBoard[11].flip(2);
                break;
        }
        switch (m) {
            case "":
                break;
            case "x":
                gameBoard[12].flip(1);
                break;
            case "o":
                gameBoard[12].flip(2);
                break;
        }
        switch (n) {
            case "":
                break;
            case "x":
                gameBoard[13].flip(1);
                break;
            case "o":
                gameBoard[13].flip(2);
                break;
        }
        switch (o) {
            case "":
                break;
            case "x":
                gameBoard[14].flip(1);
                break;
            case "o":
                gameBoard[14].flip(2);
                break;
        }
        switch (p) {
            case "":
                break;
            case "x":
                gameBoard[15].flip(1);
                break;
            case "o":
                gameBoard[15].flip(2);
                break;
        }

        // iterate through squares, adding to list all empty square indexes
        for (int z=0; z<numSquares; ++z) {
            if (!gameBoard[z].isFlipped()) {
                this.emptySquares.Add(z);
            }
        }

        this.depth = numSquares - getNumEmptySquares();

        // print gameBoard
        for (int z=0; z<16; ++z) {
            Console.Write(gameBoard[z].getFace());
            if (gameBoard[z].getFace().Equals(""))
                Console.Write("_");
            if ((z+1)%4 == 0) {
                Console.WriteLine();
            }
        }
    }

    // returns depth of this state
    public int getDepth() {
        return this.depth;
    }

    // returns numSquares of this state
    public int getNumSquares() {
        return numSquares;
    }

    // prints gameBoard
    public void print() {
        // print gameBoard
        for (int z=0; z<numSquares; ++z) {
            Console.Write(gameBoard[z].getFace());
            if (gameBoard[z].getFace().Equals(""))
                Console.Write("_");
            if ((z+1)%numRowsOrCols == 0) {
                Console.WriteLine();
            }
        }
    }

    // flips square
    private void flip(int index, int newValue) {
        gameBoard[index].flip(newValue);

        // if flipping to o, save as most recent o flip
        if (newValue == 2) {
            this.mostRecentO = index;
        }

        // remove index from empty squares
        emptySquares.Remove(index);
    }

    // returns a list of indexes of all empty squares in state
    public List<int> getEmptySquares() {
        return emptySquares.ToList();
    }

    // returns number of rows or cols
    public int getNumRowsOrCols() {
        return this.numRowsOrCols;
    }

    // returns number of empty squares in state
    public int getNumEmptySquares() {
        return emptySquares.Count;
    }

    // returns index of the most recent o flip
    public int getMostRecentFlipO() {
        return this.mostRecentO;
    }

    // returns list of all possible successors of this state given
    // new value to be used
    public List<TicTacState> getSuccessors(int newValue) {
        List<TicTacState> successors = new List<TicTacState>();
        for (int i=0; i<emptySquares.Count; ++i) {
            // copy this state
            TicTacState newState = new TicTacState(this);
            // flip square
            newState.flip(emptySquares[i], newValue);
            // run analysis
            newState.run();
            // add state to successors
            successors.Add(newState);
        }
        return successors;
    }

    // analyzes value of state and if state is terminal
    private void run() {

        // speed computation up
        if (this.depth <= 5) {
            return;
        }

        // init weights
        int losingValue = (-1*numRowsOrCols*numRowsOrCols / (this.depth+1)) + this.depth;
        //int losingValue = -10;
        //int winningValue = ((numRowsOrCols*numRowsOrCols) / (this.depth+1)) + this.depth;
        int winningValue = 100000000;
        //int winningValue = 10;
        int drawValue = this.depth;
        //int drawValue = 0;

        // check rows
        for (int i=0; i<numRowsOrCols; ++i) {
            // inside row
            int numXFound = 0;
            int numOFound = 0;
            for (int j=0; j<numRowsOrCols; ++j) {
                bool blankFound = false;
                switch (gameBoard[(i*numRowsOrCols)+j].getValue()) {
                    case 0:
                        // blank square, go to next row
                        blankFound = true;
                        break;
                    case 1:
                        // x
                        numXFound++;
                        break;
                    case 2:
                        // o
                        numOFound++;
                        break;
                }
                // if blank square found, go to next iteration
                if (blankFound) {
                    break;
                }
                // if at least 1 x and at least 1 o found, go to next iteration
                if (numXFound >= 1 && numOFound >= 1) {
                    break;
                }
                // if all x's or all o's, return
                if (numXFound == numRowsOrCols) {
                    this.value = losingValue;
                    this.terminalState = true;
                    return;
                } else if (numOFound == numRowsOrCols) {
                    this.value = winningValue;
                    this.terminalState = true;
                    return;
                }
            }
        }

        // check cols
        for (int i=0; i<numRowsOrCols; ++i) {
            // inside col
            int numXFound = 0;
            int numOFound = 0;
            for (int j=0; j<numRowsOrCols; ++j) {
                bool blankFound = false;
                switch (gameBoard[(j*numRowsOrCols)+i].getValue()) {
                    case 0:
                        // blank square, go to next col
                        blankFound = true;
                        break;
                    case 1:
                        // x
                        numXFound++;
                        break;
                    case 2:
                        // o
                        numOFound++;
                        break;
                }
                // if blank square found, go to next iteration
                if (blankFound) {
                    break;
                }
                // if at least 1 x and at least 1 o found, go to next iteration
                if (numXFound >= 1 && numOFound >= 1) {
                    break;
                }
                // if all x's or all o's, return
                if (numXFound == numRowsOrCols) {
                    this.value = losingValue;
                    this.terminalState = true;
                    return;
                } else if (numOFound == numRowsOrCols) {
                    this.value = winningValue;
                    this.terminalState = true;
                    return;
                }
            }
        }

        // check diagonals
        // first diagonal
        {
            int numXFound = 0;
            int numOFound = 0;
            for (int i=0; i<numRowsOrCols; ++i) {
                bool blankFound = false;
                switch (gameBoard[(numRowsOrCols+1)*i].getValue()) {
                    case 0:
                        // blank square, break
                        blankFound = true;
                        break;
                    case 1:
                        // x
                        numXFound++;
                        break;
                    case 2:
                        // o
                        numOFound++;
                        break;
                }
                // if blank square found, break
                if (blankFound) {
                    break;
                }
                // if at least 1 x and at least 1 o found, break
                if (numXFound >= 1 && numOFound >= 1) {
                    break;
                }
                // if all x's or all o's, return
                if (numXFound == numRowsOrCols) {
                    this.value = losingValue;
                    this.terminalState = true;
                    return;
                } else if (numOFound == numRowsOrCols) {
                    this.value = winningValue;
                    this.terminalState = true;
                    return;
                }
            }
        }

        // second diagonal
        {
            int numXFound = 0;
            int numOFound = 0;
            for (int i=0; i<numRowsOrCols; ++i) {
                bool blankFound = false;
                switch (gameBoard[(numRowsOrCols-1)*(i+1)].getValue()) {
                    case 0:
                        // blank square, break
                        blankFound = true;
                        break;
                    case 1:
                        // x
                        numXFound++;
                        break;
                    case 2:
                        // o
                        numOFound++;
                        break;
                }
                // if blank square found, break
                if (blankFound) {
                    break;
                }
                // if at least 1 x and at least 1 o found, break
                if (numXFound >= 1 && numOFound >= 1) {
                    break;
                }
                // if all x's or all o's, return
                if (numXFound == numRowsOrCols) {
                    this.value = losingValue;
                    this.terminalState = true;
                    return;
                } else if (numOFound == numRowsOrCols) {
                    this.value = winningValue;
                    this.terminalState = true;
                    return;
                }
            }
        }

        // if all squares flipped, return draw
        if (getNumEmptySquares() == 0) {
            this.value = drawValue;
            this.terminalState = true;
        }

        // not a terminal state, return depth
        this.value = 0 + this.depth;
    }

    // returns value of state
    public int getValue() {
        return this.value;
    }

    public bool isTerminal() {
        return this.terminalState;
    }
}