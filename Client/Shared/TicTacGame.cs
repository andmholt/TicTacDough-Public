public class TicTacGame {
    private List<TicTacSquare> squares = new List<TicTacSquare>();
    private int numRowsOrCols;
    private System.Timers.Timer timer = new System.Timers.Timer();
    private int currTime = 20;
    private List<List<string>> bakedGoods = new List<List<string>>();
    private int[] scores = new int[2];
    private int temperature;

    // Constructor
    public TicTacGame(int numRowsOrCols, int temperature) {
        this.numRowsOrCols = numRowsOrCols;
        this.temperature = temperature;
        int numSquares = numRowsOrCols*numRowsOrCols;

        // add squares to list
        for (int i=0; i<numSquares; ++i) {
            squares.Add(new TicTacSquare(i, true));
        }

        // initialize baked goods
        bakedGoods.Add(new List<string>());
        bakedGoods.Add(new List<string>());

        // timer
        this.timer.AutoReset = true;
        this.timer.Interval = this.getBurnIntervals();
        this.timer.Elapsed += onTimedEvent;
        this.timer.Enabled = true;
    }

    // returns burn second intervals given current temperature
    private int getBurnIntervals() {
        switch (this.temperature) {
            case 450:
                return 1000;
            case 400:
                return 1500;
            case 350:
                return 2000;
            case 300:
                return 2500;
            case 250:
                return 3000;
            default:
                return 2000;
        }
    }

    // returns currTime
    public int getCurrTime() {
        return this.currTime;
    }

    // returns scores
    public int[] getScores() {
        return this.scores;
    } 

    // returns baked goods
    public List<List<string>> getBakedGoods() {
        return this.bakedGoods;
    }

    // called by timer when time elapses
    private void onTimedEvent(Object? source, EventArgs e) {
        // if timer already 1, game is over
        if (this.currTime == 1) {
            this.timer.Stop();
            this.gameOver();
        }

        // decrement timer
        this.currTime--;

        // refresh UI
        GameState.refresh();
    }

    // returns references to squares (for UI only)
    public List<TicTacSquare> getListRef() {
        return squares;
    }

    // returns deep copy of squares
    public List<TicTacSquare> getListCopy() {
        // deep copy squares
        List<TicTacSquare> squaresCopy = new List<TicTacSquare>();
        for (int i=0; i<squares.Count; ++i) {
            TicTacSquare squareCopy = new TicTacSquare(squares[i]);
            squaresCopy.Add(squareCopy);
        }
        return squaresCopy;
    }

    // prints score
    public void printScore() {
        Console.WriteLine("*--SCORE--*");
        Console.WriteLine("x: " + this.scores[0]);
        Console.WriteLine("o: " + this.scores[1]);
        Console.WriteLine();
    }

    // flips specified index for user (x)
    public void flipX(int indexToFlip) {

        TicTacSquare squareToFlip = squares[indexToFlip];

        // if square is already flipped, return
        if (squareToFlip.isFlipped())
            return;
        
        // flip square
        squareToFlip.flip(1);

        // check for line
        this.checkForLine();
    }

    // returns img file name
    private String getDoughImg(int imgCode, int burntLevel) {
        switch (imgCode) {
            case 0:
                return "";
            case 1:
                return "img/croissant-" + burntLevel + ".png";
            case 2:
                return "img/pretzel-" + burntLevel + ".png";
            default:
                return "";
        }
    }

    // flip specified index for adversary (o)
    public void flipO(int indexToFlip) {

        TicTacSquare squareToFlip = squares[indexToFlip];

        // if square is already flipped, return
        if (squareToFlip.isFlipped())
            return;

        // flip square
        squareToFlip.flip(2);

        // check for line
        this.checkForLine();
    }

    // returns number of unflipped squares remaining
    public int getNumUnflipped() {
        int unflippedCount = 0;

        // count num squares unflipped
        for (int i=0; i<squares.Count; ++i) {
            if (!squares[i].isFlipped())
                unflippedCount++;
        }

        // return unflipped
        return unflippedCount;
    }

    // helper function to flip random square for adversary
    private void randO() {
        Random rand = new Random();

        // randomly select square
        while (true) {
            int randIndex = rand.Next(0, squares.Count);
            TicTacSquare randSquare = squares[randIndex];

            // if square is not already flipped, flip and return
            if (!randSquare.isFlipped()) {
                randSquare.flip(2);
                return;
            }

            // else repeat
        }
    }

    // adversary flip
    /*private void adversaryFlip() {
        // look for move that would win for o and play it
        // search rows
        for (int i=0; i<numRowsOrCols; ++i) {
            int emptySquare = oRowNearWin(i);
            // if row almost a win for o, flip and return
            if (emptySquare > 0) {
                squares[emptySquare].flip(2);
                return;
            }
        }
        // search cols
        for (int i=0; i<numRowsOrCols; ++i) {
            int emptySquare = oColNearWin(i);
            // if col almost a win for o, flip and return
            if (emptySquare > 0) {
                squares[emptySquare].flip(2);
                return;
            }
        }
        // search diagonal
        {
            int emptySquare = oDiagonalNearWin();
            // if diagonal almost a win for o, flip and return
            if (emptySquare > 0) {
                squares[emptySquare].flip(2);
                return;
            }
        }

        // look for move that would win for x and play it
        // search rows
        for (int i=0; i<numRowsOrCols; ++i) {
            int emptySquare = xRowNearWin(i);
            // if row almost a win for x, flip and return
            if (emptySquare > 0) {
                squares[emptySquare].flip(2);
                return;
            }
        }
        // search cols
        for (int i=0; i<numRowsOrCols; ++i) {
            int emptySquare = xColNearWin(i);
            // if col almost a win for x, flip and return
            if (emptySquare > 0) {
                squares[emptySquare].flip(2);
                return;
            }
        }
        // search diagonal
        {
            int emptySquare = xDiagonalNearWin();
            // if diagonal almost a win for x, flip and return
            if (emptySquare > 0) {
                squares[emptySquare].flip(2);
                return;
            }
        }

        // if no near wins, randomly flip available square
        randO();
    }*/

    // calculate score given burnt value
    private int calcScore(int burntValue) {
        switch (burntValue) {
            case 0:
                return 1;
            case 1:
                return 3;
            case 2:
                return 1;
            default:
                return 0;
        }
    }


    // flips row blank
    private void flipRowBlank(int row) {
        for (int i=0; i<this.numRowsOrCols; ++i) {

            // get current index
            int currIndex = i+(numRowsOrCols*row);

            // add to score
            this.scores[this.squares[currIndex].getValue()-1] += this.calcScore(this.squares[currIndex].getBurntLevel());

            // add to baked goods
            this.bakedGoods[this.squares[currIndex].getValue()-1].Add(
                this.getDoughImg(this.squares[currIndex].getValue(), this.squares[currIndex].getBurntLevel()));

            // flip square to blank
            this.squares[currIndex].flipBlank();

            // check game status
            this.checkGameStatus();
        }
    }

    // flips column blank
    private void flipColBlank(int col) {
        for (int i=0; i<this.numRowsOrCols; ++i) {

            // get current index
            int currIndex = col+(numRowsOrCols*i);

            // add to score
            this.scores[this.squares[currIndex].getValue()-1] += this.calcScore(this.squares[currIndex].getBurntLevel());

            // add to baked goods
            this.bakedGoods[this.squares[currIndex].getValue()-1].Add(
                this.getDoughImg(this.squares[currIndex].getValue(), this.squares[currIndex].getBurntLevel()));
            
            // flip square to blank
            this.squares[currIndex].flipBlank();

            // check game status
            this.checkGameStatus();
        }
    }

    // flips diagonal with negative slope blank
    private void flipNegDiagonalBlank() {
        for (int i=0; i<this.numRowsOrCols; ++i) {

            // get current index
            int currIndex = (this.numRowsOrCols+1)*i;

            // add to score
            this.scores[this.squares[currIndex].getValue()-1] += this.calcScore(this.squares[currIndex].getBurntLevel());

            // add to baked goods
            this.bakedGoods[this.squares[currIndex].getValue()-1].Add(
                this.getDoughImg(this.squares[currIndex].getValue(), this.squares[currIndex].getBurntLevel()));
            
            // flip square to blank
            this.squares[currIndex].flipBlank();

            // check game status
            this.checkGameStatus();
        }
    }

    // flips diagonal with positive slope blank
    private void flipPosDiagonalBlank() {
        for (int i=0; i<this.numRowsOrCols; ++i) {

            // get current index
            int currIndex = (this.numRowsOrCols-1)*(i+1);

            // add to score
            this.scores[this.squares[currIndex].getValue()-1] += this.calcScore(this.squares[currIndex].getBurntLevel());

            // add to baked goods
            this.bakedGoods[this.squares[currIndex].getValue()-1].Add(
                this.getDoughImg(this.squares[currIndex].getValue(), this.squares[currIndex].getBurntLevel()));
            
            // flip square to blank
            this.squares[currIndex].flipBlank();

            // check game status
            this.checkGameStatus();
        }
    }

    // checks games status
    private void checkGameStatus() {
        
        // check for max baked goods
        for (int i=0; i<2; ++i) {
            if (this.bakedGoods[i].Count >= 20) {
                this.gameOver();
            }
        }
    }

    // ends game
    public void gameOver() {
        // notify UI that game is over
        GameState.gameOver();
    }

    // * -------------------------------------------------- *
    // * Helper funcs to determine game status of rows/cols *
    // * -------------------------------------------------- *
    
    // checks for line and handles accordingly
    private void checkForLine() {

        const int NUM_SQUARES_IN_LINE = 3;

        // if not enough flipped for win, return
        if ((this.numRowsOrCols*this.numRowsOrCols) - this.getNumUnflipped() < 5) {
            return;
        }

        // check rows
        for (int i=0; i<numRowsOrCols; ++i) {
            // inside row
            int numXFound = 0;
            int numOFound = 0;
            for (int j=0; j<numRowsOrCols; ++j) {
                bool blankFound = false;
                switch (this.squares[(i*numRowsOrCols)+j].getValue()) {
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
                // if all x's or all o's
                if (numXFound == NUM_SQUARES_IN_LINE) {
                    this.flipRowBlank(i);
                    break;
                } else if (numOFound == NUM_SQUARES_IN_LINE) {
                    this.flipRowBlank(i);
                    break;
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
                switch (this.squares[(j*numRowsOrCols)+i].getValue()) {
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
                // if all x's or all o's
                if (numXFound == NUM_SQUARES_IN_LINE) {
                    this.flipColBlank(i);
                    break;
                } else if (numOFound == NUM_SQUARES_IN_LINE) {
                    this.flipColBlank(i);
                    break;
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
                switch (this.squares[(numRowsOrCols+1)*i].getValue()) {
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
                // if all x's or all o's
                if (numXFound == NUM_SQUARES_IN_LINE) {
                    this.flipNegDiagonalBlank();
                    break;
                } else if (numOFound == NUM_SQUARES_IN_LINE) {
                    this.flipNegDiagonalBlank();
                    break;
                }
            }
        }

        // second diagonal
        {
            int numXFound = 0;
            int numOFound = 0;
            for (int i=0; i<numRowsOrCols; ++i) {
                bool blankFound = false;
                switch (this.squares[(numRowsOrCols-1)*(i+1)].getValue()) {
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
                // if all x's or all o's
                if (numXFound == NUM_SQUARES_IN_LINE) {
                    this.flipPosDiagonalBlank();
                    break;
                } else if (numOFound == NUM_SQUARES_IN_LINE) {
                    this.flipPosDiagonalBlank();
                    break;
                }
            }
        }

        this.printScore();

        // if all squares flipped, return draw
        /*if (getNumEmptySquares() == 0) {
            this.value = drawValue;
            this.terminalState = true;
        }*/
    }
}