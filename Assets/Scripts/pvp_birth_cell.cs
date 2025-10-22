using UnityEngine;

public class pvpGame: MonoBehaviour
{
    public int DetermineCellParent(GameObject[,] grid, int x, int y, int width, int height)
    {
        int neighbours1 = 0;
        int neighbours2 = 0;
        for (int i = -1; i <= 1; ++i)
        {
            for (int j = -1; j <= 1; ++j)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }

                if (x + i >= 0 && x + i < width && y + j >= 0 && y + j < height && grid[x + i, y + j])
                {
                    cell curCell = grid[x + i, y + j].GetComponent<cell>();
                    if (curCell.GetParent() == 1 && curCell.GetAlive())
                    {
                        ++neighbours1;
                    }
                    else if (curCell.GetParent() == 2 && curCell.GetAlive())
                    {
                        ++neighbours2;
                    }
                }
            }
        }
        if (!grid[x, y].GetComponent<cell>().GetAlive())
        {
            if ((neighbours1 + neighbours2) == 3)
            {
                if (neighbours1 > neighbours2)
                {
                    return 1;
                }
                return 2;
            }
            return 0;
        } else
        {
            return grid[x, y].GetComponent<cell>().GetParent();
        }
        
    }
}
