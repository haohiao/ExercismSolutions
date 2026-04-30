public static class SaddlePoints
{
    public static IEnumerable<(int, int)> Calculate(int[,] matrix)
    {
        int row_len = matrix.GetLength(0);
        int col_len = matrix.GetLength(1);

        ValueType[,] suitable = new ValueType[row_len, col_len];

        int[] row_max_list = new int[row_len];
        for (int row = 0; row < row_len; row++)
        {
            row_max_list[row] = matrix.MaxInRow(row);
        }

        int[] col_min_list = new int[col_len];
        for (int col = 0; col < col_len; col++)
        {
            col_min_list[col] = matrix.MinInCol(col);
        }

        for (int row = 0; row < row_len; row++)
        {
            for (int col = 0; col < col_len; col++)
            {

                if (row_max_list[row] == col_min_list[col])
                {
                    yield return (row + 1, col + 1);
                }
            }
         }
    }
}

public static class ArrayExtensions
{
    public static int MaxInRow(this int[,] matrix, int row_index)
    {
        int col_len = matrix.GetLength(1);
        if (col_len == 0)
        {
            throw new ArgumentException("Col is empty", nameof(matrix));
        }

        int? max_in_row = matrix[row_index, 0];
        for (int col_index = 0; col_index < col_len; col_index++)
        {
            int value = matrix[row_index, col_index];
            if (value > max_in_row)
            {
                max_in_row = value;
            }
        }

        return max_in_row.Value;
    }

    public static int MinInCol(this int[,] matrix, int col_index)
    {
        int row_len = matrix.GetLength(0);
        if (row_len == 0)
        {
            throw new ArgumentException("Raw is empty", nameof(matrix));
        }

        int? min_in_col = matrix[0, col_index];
        for (int row_index = 0; row_index < row_len; row_index++)
        {
            int value = matrix[row_index, col_index];
            if (value < min_in_col)
            {
                min_in_col = value;
            }
        }

        return min_in_col.Value;
    }
}
