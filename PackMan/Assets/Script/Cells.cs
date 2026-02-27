

public class Cells
{
    public enum CellState { Empty, Wall, Trail, Captured }

    public struct Cell
    {
        public CellState State;
    }
}
