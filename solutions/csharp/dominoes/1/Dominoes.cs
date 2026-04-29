public static class Dominoes
{
    public static bool CanChain(IEnumerable<(int, int)> dominoes)
    {
        bool can_chain = false;
        if (dominoes.Count() == 0)
        {
            can_chain = true;
        }
        else
        {
            (int, int)[] list = dominoes.ToArray();
            bool[] used = new bool[list.Length];

            int root_index = 0;
            (int, int) root = list[root_index];
            used[root_index] = true;

            can_chain = TryChain(root, root, list, used, list.Length - 1);
        }

        return can_chain;
    }

    private static bool TryChain((int, int) root, (int, int) current, (int, int)[] dominoes, bool[] used, int remaining)
    {
        bool can_chain = false;
        if (remaining <= 0)
        {
            can_chain = root.Item1 == current.Item2;
        }
        else
        {
            for (int i = 0; i < dominoes.Length; i++)
            {
                if (used[i])
                {
                    continue;
                }

                (int, int) domino = dominoes[i];

                used[i] = true;
                if ((current.Item2 == domino.Item1 && TryChain(root, domino, dominoes, used, remaining-1)) ||
                    (current.Item2 == domino.Item2 && TryChain(root, (domino.Item2, domino.Item1), dominoes, used, remaining-1)))
                {
                    can_chain = true;
                    break;
                }
                used[i] = false;
            }
        }

        return can_chain;
    }
}