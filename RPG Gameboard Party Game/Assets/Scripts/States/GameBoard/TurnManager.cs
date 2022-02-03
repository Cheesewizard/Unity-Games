public class TurnManager
{
    private bool _firstTurn;
    private int _totalPlayers;
    private int _maxTurns;
    private int _currenturn;
    private int[] players;

    public TurnManager(bool firstTurn, int totalPlayers, int maxTurns)
    {
        _firstTurn = firstTurn;
        _totalPlayers = totalPlayers;
        _currenturn = maxTurns;
    }

    private int _currentPlayerIndex = 0;

    public int GetNextPlayer()
    {
        _currentPlayerIndex++;
        _currenturn++;
        return _currentPlayerIndex %= _totalPlayers;
    }

    public int CurrentPlayer()
    {
        return _currentPlayerIndex;
    }

    public void IncrementTotalTurns()
    {
        // if total turns == max turns, exit game?
        _currenturn++;
    }
}