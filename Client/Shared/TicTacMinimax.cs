class TicTacMinimax {

    // do Minimax on TicTacGame
    // o maximizes, x minimizes
    // so we are looking for max here, since adversary is o
    // returns index of square of next best move for o
    public static int getNextMoveO(TicTacGame game) {

        // create game state from game
        TicTacState state = new TicTacState(game);

        // get all possible successors of state
        List<TicTacState> successors = state.getSuccessors(2);

        // if enough successors remaining, will choose randomly
        /*if (state.getNumSquares()-successors.Count+1 < (state.getNumRowsOrCols()-1)+(state.getNumRowsOrCols()-2)) {
            Console.WriteLine("Randomly choosing...");
            Console.WriteLine(successors.Count);
            Random rand = new Random();
            return successors[rand.Next(successors.Count)].getMostRecentFlipO();
        }*/

        // loop through possible successors, finding the one with the maximum value
        int alpha = int.MinValue;
        int beta = int.MaxValue;
        int maxValue = int.MinValue;
        int maxIndex = -1;
        for (int i=0; i<successors.Count; ++i) {
            int currValue = min(successors[i], alpha, beta, state.getDepth());
            //Console.WriteLine("index " + successors[i].getMostRecentFlipO() + " has value " + currValue);
            // if greater value, make current best move
            if (currValue > maxValue) {
                maxValue = currValue;
                maxIndex = successors[i].getMostRecentFlipO();
            }
            // if of equal value, randomly decide to take this move or keep current move
            else if (currValue == maxValue) {
                Random rand = new Random();
                if (rand.Next(i+1) == i) {
                    maxIndex = successors[i].getMostRecentFlipO();
                }
            }

        }

        // return index with max value
        return maxIndex;
    }

    public static int getNextMoveO(TicTacState state) {

        // get all possible successors of state
        List<TicTacState> successors = state.getSuccessors(2);

        // if enough successors remaining, will choose randomly
        /*if (state.getNumSquares()-successors.Count+1 < (state.getNumRowsOrCols()-1)+(state.getNumRowsOrCols()-2)) {
            Console.WriteLine("Randomly choosing...");
            Console.WriteLine(successors.Count);
            Random rand = new Random();
            return successors[rand.Next(successors.Count)].getMostRecentFlipO();
        }*/

        // loop through possible successors, finding the one with the maximum value
        int alpha = int.MinValue;
        int beta = int.MaxValue;
        int maxValue = int.MinValue;
        int maxIndex = -1;
        for (int i=0; i<successors.Count; ++i) {
            int currValue = min(successors[i], alpha, beta, state.getDepth());
            //Console.WriteLine("index " + successors[i].getMostRecentFlipO() + " has value " + currValue);
            // if greater value, make current best move
            if (currValue > maxValue) {
                maxValue = currValue;
                maxIndex = successors[i].getMostRecentFlipO();
            }
            // if of equal value, randomly decide to take this move or keep current move
            else if (currValue == maxValue) {
                Random rand = new Random();
                if (rand.Next(i+1) == i) {
                    maxIndex = successors[i].getMostRecentFlipO();
                }
            }
        }

        // return index with max value
        return maxIndex;
    }

    // returns maximized value of given state
    private static int max(TicTacState state, int alpha, int beta, int depth) {

        // if state is terminal, return value of state
        int value = state.getValue();
        if (state.isTerminal()) {
            return value;
        }

        // if depth of this state being searched is >5 of current depth and playing on
        // a big board, return (we are too deep)
        /*if (state.getDepth()-depth > 3 && state.getNumSquares()>9) {
            return value;
        }*/
        if (state.getDepth()-depth >= state.getNumRowsOrCols() && state.getNumEmptySquares() > state.getNumRowsOrCols()) {
            return value;
        }

        // else, value = -infinity
        else {
            value = int.MinValue;
        }

        // else, find max child node
        List<TicTacState> successors = state.getSuccessors(2);
        for (int i=0; i<successors.Count; ++i) {
            value = Math.Max(value, min(successors[i], alpha, beta, depth));
            // if this move would be a worse move for x than another possible option
            // already found, we can assume x will choose the other move and don't
            // have to explore this path (successors) any further
            if (value >= beta) {
                return value;
            }
            // save o's best option
            alpha = Math.Max(alpha, value);
        }

        // return maximized value (best move for o)
        return value;
    }

    // returns minimized value of given state
    private static int min(TicTacState state, int alpha, int beta, int depth) {

        // if state is terminal, return value of state
        int value = state.getValue();
        if (state.isTerminal()) {
            return value;
        }

        // if depth of this state being searched is >5 of current depth and playing on
        // a big board, return (we are too deep)
        /*if (state.getDepth()-depth > 3 && state.getNumSquares()>9) {
            return value;
        }*/
        if (state.getDepth()-depth >= state.getNumRowsOrCols() && state.getNumEmptySquares() > state.getNumRowsOrCols()) {
            return value;
        }

        // else, value = +infinity
        else {
            value = int.MaxValue;
        }

        // else, find min child node
        List<TicTacState> successors = state.getSuccessors(1);
        for (int i=0; i<successors.Count; ++i) {
            value = Math.Min(value, max(successors[i], alpha, beta, depth));
            // if this move would be a worse move for o than another possible option
            // already found, we can assume o will choose the other move and don't
            // have to explore this path (successors) any further
            if (value <= alpha) {
                return value;
            }
            // save o's best option
            beta = Math.Min(beta, value);
        }

        // return minimized value (best move for x)
        return value;
    }
}