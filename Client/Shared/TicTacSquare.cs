public class TicTacSquare {
    private int value;
    private int index;
    private System.Timers.Timer timer = new System.Timers.Timer();
    private int burntLevel = 0;

    // keeps track of if this square is a "real square", to be used
    // in the actual game (not just minimax)
    private bool realSquare = false;

    // basic constructor
    public TicTacSquare(int index) {
        this.value = 0;
        this.index = index;
    }

    // constructor for squares to be used in the game (not just minimax)
    public TicTacSquare(int index, bool realSquare) {
        this.value = 0;
        this.index = index;
        this.realSquare = realSquare;
        this.burntLevel = 0;

        // timer
        this.timer.Enabled = false;
        this.timer.AutoReset = true;
        this.timer.Interval = 2000;
        this.timer.Elapsed += onTimedEvent;
    }

    // create new TicTacSquare from TicTacSquare (deep copy)
    public TicTacSquare(TicTacSquare square) {
        this.value = square.value;
        this.index = square.index;
    }

    // called by timer when time elapses
    private void onTimedEvent(Object? source, EventArgs e) {
        // if burntValue < 2, cook longer
        if (this.burntLevel < 2) {
            this.burntLevel++;
            GameState.refresh();
        }
        // else, dough has burned (flip blank)
        else {
            this.flipBlank();
        }
    }

    // returns burnt level
    public int getBurntLevel() {
        return this.burntLevel;
    }

    // returns square index
    public int getIndex() {
        return index;
    }

    // returns stored value of square
    public int getValue() {
        return value;
    }

    // returns face of square
    public String getFace() {
        switch (value) {
            case 1:
                return "X";
            case 2:
                return "O";
            default:
                return "";
        }
    }

    public void flipBlank() {
        // flip
        this.value = 0;

        // if real square, stop timer and refresh state
        if (this.realSquare) {
            this.timer.Stop();
            GameState.refresh();

            // reset burnt level
            this.burntLevel = 0;
        }
    }

    // attempt to flip square
    public void flip(int newValue) {
        if (value != 0)
            return;
        else value = newValue;

        // if real square, start timer
        if (this.realSquare)
            timer.Enabled = true;
    }

    // returns true if square is flipped
    public bool isFlipped() {
        return value != 0;
    }
}