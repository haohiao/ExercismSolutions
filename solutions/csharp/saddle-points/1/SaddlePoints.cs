public static class SaddlePoints
{
    public static IEnumerable<(int, int)> Calculate(int[,] matrix)
    {
        int row_len = matrix.GetLength(0);
        int col_len = matrix.GetLength(1);

        for (int row = 0; row < row_len; row++)
        {
            int[] row_values = matrix.GetRow(row);
            int[] col_indexes_of_max_in_row = row_values.IndexesOf(row_values.Max());

            foreach (int col_index_of_max_in_row in col_indexes_of_max_in_row)
            {
                int[] col_values = matrix.GetColumn(col_index_of_max_in_row);
                int row_index_of_min_in_col = row + Array.IndexOf(col_values.Skip(row).ToArray(), col_values.Min());

                if (row_index_of_min_in_col == row)
                {
                    yield return (row_index_of_min_in_col + 1, col_index_of_max_in_row + 1);
                }
            }
        }
    }
}

public static class ArrayExtensions
{
    public static int[] GetColumn(this int[,] matrix, int col_index)
    {
        int row_len = matrix.GetLength(0);
        int[] col_values = new int[row_len];

        for (int row_index = 0; row_index < row_len; row_index++)
        {
            int value = matrix[row_index, col_index];
            col_values[row_index] = value;
        }

        return col_values;
    }

    public static int[] GetRow(this int[,] matrix, int row_index)
    {
        int col_len = matrix.GetLength(1);
        int[] row_values = new int[col_len];

        for (int col_index = 0; col_index < col_len; col_index++)
        {
            int value = matrix[row_index, col_index];
            row_values[col_index] = value;
        }

        return row_values;
    }

    public static int[] IndexesOf(this int[] array, int value)
    {
        List<int> indexes = new List<int>();
        for (int index = 0; index < array.Length; index++)
        {
            if (array[index] == value)
            {
                indexes.Add(index);
            }
        }
        return indexes.ToArray();
    }
}
