// service to control/get game UI state
public static class GameState {
    private static Action? refreshCB;
    private static Action? gameOverCB;
    private static Action<int>? changeTempFunc;

    // set new refresh callback
    public static void assignRefreshCB(Action callback) {
        refreshCB = callback;
    }

    // set new game over callback
    public static void assignGameOverCB(Action callback) {
        gameOverCB = callback;
    }

    // set new temperature change
    public static void assignTempChange(Action<int> func) {
        changeTempFunc = func;
    }

    // refresh game UI
    public static void refresh() {
        // if callback has been assigned, refresh UI
        if (!(refreshCB is null)) {
            refreshCB();
        }
    }

    // notify UI that game is over
    public static void gameOver() {
        // if game over callback has been assigned, notify UI that game is over
        if (!(gameOverCB is null)) {
            gameOverCB();
        }
    }

    // change temp
    public static void changeTemp(int newTemp) {
        // if temp change func has been assigned, change temp
        if (!(changeTempFunc is null)) {
            changeTemp(newTemp);
        }
    }
}