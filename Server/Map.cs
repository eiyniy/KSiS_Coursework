namespace Server;
public static class Map
{
    public enum CellTypes { SolidBlock, SoftBlock, Empty }

    private const int Width = 18;

    private const int Height = 10;

    public static CellTypes[,] MapTemplate = new CellTypes[Height, Width];


    public static void GenerateMap()
    {
        Random random = new Random();

        for (var row = 0; row < Width; row++)
        {
            for (var col = 0; col < Height; col++)
            {
                MapTemplate[row, col] = CellTypes.Empty;

                if (col == 0 || row == 0 || row == Height || col == Height)
                {
                    MapTemplate[row, col] = CellTypes.SolidBlock;
                    continue;
                }

                if (row % 2 == 0 && col % 2 == 0)
                {
                    MapTemplate[row, col] = CellTypes.SolidBlock;
                    continue;
                }

                if (random.Next(1, 100) < 70)
                    MapTemplate[row, col] = CellTypes.SoftBlock;
            }
        }
    }
}