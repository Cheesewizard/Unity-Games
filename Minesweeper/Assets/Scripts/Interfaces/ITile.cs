public interface ITile
{
    bool IsRevealed
    {
        get;
        set;
    }

    void Reveal();
}