using UnityEngine;

public class controller : MonoBehaviour
{
    private bool[,] curState;
    private bool[,] nextState;
    private GameObject[,] grid;
    private float timer = 0f;
    private Camera mainCam;
    private int[,] cellParents;

    public bool isGameActive = false;
    public float intervalToUpdate = 0.5f;
    public int width = 50;
    public int height = 50;
    public GameObject cellPrefab;
    public pvpGame pvp;
    void Start()
    {

        mainCam = Camera.main;
        MakeGrid();
        cellParents = new int[width, height];
    }
    void Update()
    {
        if (isGameActive)
        {
            timer += Time.deltaTime;
            if (timer >= intervalToUpdate)
            {
                CalculateNextState();
                SetNextState();
                timer = 0f;
            }
        } else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Click();
            }
        }
    }
    private void MakeGrid()
    {
        curState = new bool[width, height];
        nextState = new bool[width, height];
        grid = new GameObject[width, height];
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                curState[i, j] = false;

                Vector3 pos = new Vector3(i, j, 0);
                GameObject oneCell = Instantiate(cellPrefab, pos, Quaternion.identity);
                oneCell.name = $"Cell_{i}_{j}";
                cell Cell = oneCell.GetComponent<cell>();
                Cell.SetAlive(false);
                grid[i, j] = oneCell;
            }
        }
    }
    private void CalculateNextState()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; ++j)
            {
                int cntNeighbours = CountAliveNeighbours(i, j);
                cellParents[i, j] = pvp.DetermineCellParent(grid, i, j, width, height);
                if (curState[i, j])
                {
                    nextState[i, j] = (cntNeighbours == 2 || cntNeighbours == 3);
                }
                else
                {
                    if (cntNeighbours == 3)
                    {

                        nextState[i, j] = true;
                    }
                    else
                    {
                        nextState[i, j] = false;
                    }
                }

            }
        }
    }
    private void SetNextState()
    {

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                curState[i, j] = nextState[i, j];
                grid[i, j].GetComponent<cell>().SetAlive(curState[i, j], cellParents[i, j]);

            }
        }
    }
    private int CountAliveNeighbours(int x, int y)
    {
        int res = 0;
        for (int i = -1; i <= 1; ++i)
        {
            for (int j = -1; j <= 1; ++j)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                if (x + i >= 0 && x + i < width && y + j >= 0 && y + j < height && curState[x + i, y + j])
                {
                    ++res;
                }
            }
        }

        return res;
    }
    private void Click()
    {
        Vector3 mouse = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouse2D = new Vector2(mouse.x, mouse.y);
        RaycastHit2D hit = Physics2D.Raycast(mouse2D, Vector2.zero);
        if (hit.collider != null)
        {
            GameObject clickedCell = hit.collider.gameObject;
            cell cellScript = clickedCell.GetComponent<cell>();
            cellScript.Toggle();
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    if (grid[i, j] == clickedCell)
                    {
                        curState[i, j] = cellScript.GetAlive();
                        break;
                    }
                }
            }
        }
    }

    public void ClearGrid()
    {
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                curState[i, j] = false;
                grid[i, j].GetComponent<cell>().SetAlive(false);
            }
        }
    }

    public void RandomGrid()
    {
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                bool alive = Random.Range(0, 2) == 1;
                curState[i, j] = alive;
                grid[i, j].GetComponent<cell>().SetAlive(alive);
            }
        }
    }

    public void SetSpeed(float speed)
    {
        intervalToUpdate = 0.5f - (speed * 0.49f);
    }

    public void PlayPause()
    {
        isGameActive = !isGameActive;
    }
}
