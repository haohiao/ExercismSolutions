public static class SaddlePoints
{
    public static IEnumerable<(int, int)> Calculate(int[,] matrix)
    {
        int row_len = matrix.GetLength(0);
        int col_len = matrix.GetLength(1);

        ValueType[,] suitable = new ValueType[row_len, col_len];

        for (int row = 0; row < row_len; row++)
        {
            int[] row_values = matrix.GetRow(row);
            int[] col_indexes_of_max_in_row = row_values.IndexesOf(row_values.Max());
            
            foreach (int col_index_of_max_in_row in col_indexes_of_max_in_row)
            {
                suitable[row, col_index_of_max_in_row] |= ValueType.MaxInRow;
            }
        }

        for (int col = 0; col < col_len; col++)
        {
            int[] col_values = matrix.GetColumn(col);
            int[] row_indexes_of_min_in_col = col_values.IndexesOf(col_values.Min());
            
            foreach (int row_index_of_min_in_col in row_indexes_of_min_in_col)
            {
                suitable[row_index_of_min_in_col, col] |= ValueType.MinInCol;
            }
        }

        for (int row = 0; row < row_len; row++)
        {
            for (int col = 0; col < col_len; col++)
            {
                if (suitable[row, col] == (ValueType.MaxInRow | ValueType.MinInCol))
                {
                    yield return (row + 1, col + 1);
                }
            }
         }
    }
}

[Flags]
public enum ValueType
{
    MaxInRow = 0b01,
    MinInCol = 0b10,
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
