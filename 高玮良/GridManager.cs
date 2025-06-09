using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private int gridSize = 4;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform gridParent;
    [SerializeField] private float tileMoveSpeed = 0.1f;

    private Tile[,] grid;
    private bool isMoving = false;
    private int score = 0;

    public delegate void ScoreUpdated(int newScore);
    public event ScoreUpdated OnScoreUpdated;

    public delegate void GameOver();
    public event GameOver OnGameOver;

    public delegate void GameWon();
    public event GameWon OnGameWon;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        InitializeGrid();
        SpawnInitialTiles();
    }

    private void InitializeGrid()
    {
        grid = new Tile[gridSize, gridSize];
    }

    private void SpawnInitialTiles()
    {
        SpawnRandomTile();
        SpawnRandomTile();
    }

    private void SpawnRandomTile()
    {
        List<Vector2Int> availablePositions = GetAvailablePositions();

        if (availablePositions.Count > 0)
        {
            Vector2Int randomPosition = availablePositions[Random.Range(0, availablePositions.Count)];
            int value = Random.value < 0.9f ? 2 : 4;

            GameObject tileObject = Instantiate(tilePrefab, GetWorldPosition(randomPosition), Quaternion.identity);
            tileObject.transform.SetParent(gridParent);

            Tile tile = tileObject.GetComponent<Tile>();
            tile.Initialize(randomPosition, value);

            grid[randomPosition.x, randomPosition.y] = tile;
        }
    }

    private List<Vector2Int> GetAvailablePositions()
    {
        List<Vector2Int> availablePositions = new List<Vector2Int>();

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if (grid[x, y] == null)
                {
                    availablePositions.Add(new Vector2Int(x, y));
                }
            }
        }

        return availablePositions;
    }

    private Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        float offsetX = (gridSize - 1) / 2f;
        float offsetY = (gridSize - 1) / 2f;

        return new Vector3(gridPosition.x - offsetX, gridPosition.y - offsetY, 0);
    }

    public void Move(Direction direction)
    {
        if (isMoving)
            return;

        bool moved = false;

        switch (direction)
        {
            case Direction.Up:
                moved = MoveUp();
                break;
            case Direction.Down:
                moved = MoveDown();
                break;
            case Direction.Left:
                moved = MoveLeft();
                break;
            case Direction.Right:
                moved = MoveRight();
                break;
        }

        if (moved)
        {
            StartCoroutine(AfterMoveRoutine());
        }
        else
        {
            CheckGameOver();
        }
    }

    private bool MoveUp()
    {
        bool moved = false;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 1; y < gridSize; y++)
            {
                if (grid[x, y] != null)
                {
                    Vector2Int newPosition = new Vector2Int(x, y);

                    for (int i = y - 1; i >= 0; i--)
                    {
                        if (grid[x, i] == null)
                        {
                            newPosition.y = i;
                        }
                        else if (grid[x, i].CanMergeWith(grid[x, y].Value))
                        {
                            newPosition.y = i;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (newPosition.y != y)
                    {
                        MoveTile(grid[x, y], newPosition);
                        moved = true;
                    }
                }
            }
        }

        return moved;
    }

    private bool MoveDown()
    {
        bool moved = false;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = gridSize - 2; y >= 0; y--)
            {
                if (grid[x, y] != null)
                {
                    Vector2Int newPosition = new Vector2Int(x, y);

                    for (int i = y + 1; i < gridSize; i++)
                    {
                        if (grid[x, i] == null)
                        {
                            newPosition.y = i;
                        }
                        else if (grid[x, i].CanMergeWith(grid[x, y].Value))
                        {
                            newPosition.y = i;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (newPosition.y != y)
                    {
                        MoveTile(grid[x, y], newPosition);
                        moved = true;
                    }
                }
            }
        }

        return moved;
    }

    private bool MoveLeft()
    {
        bool moved = false;

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 1; x < gridSize; x++)
            {
                if (grid[x, y] != null)
                {
                    Vector2Int newPosition = new Vector2Int(x, y);

                    for (int i = x - 1; i >= 0; i--)
                    {
                        if (grid[i, y] == null)
                        {
                            newPosition.x = i;
                        }
                        else if (grid[i, y].CanMergeWith(grid[x, y].Value))
                        {
                            newPosition.x = i;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (newPosition.x != x)
                    {
                        MoveTile(grid[x, y], newPosition);
                        moved = true;
                    }
                }
            }
        }

        return moved;
    }

    private bool MoveRight()
    {
        bool moved = false;

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = gridSize - 2; x >= 0; x--)
            {
                if (grid[x, y] != null)
                {
                    Vector2Int newPosition = new Vector2Int(x, y);

                    for (int i = x + 1; i < gridSize; i++)
                    {
                        if (grid[i, y] == null)
                        {
                            newPosition.x = i;
                        }
                        else if (grid[i, y].CanMergeWith(grid[x, y].Value))
                        {
                            newPosition.x = i;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (newPosition.x != x)
                    {
                        MoveTile(grid[x, y], newPosition);
                        moved = true;
                    }
                }
            }
        }

        return moved;
    }

    private void MoveTile(Tile tile, Vector2Int newPosition)
    {
        Vector2Int oldPosition = tile.GridPosition;

        // Check if there's a tile to merge with
        if (grid[newPosition.x, newPosition.y] != null &&
            grid[newPosition.x, newPosition.y].CanMergeWith(tile.Value))
        {
            // Merge the tiles
            StartCoroutine(MergeTiles(grid[newPosition.x, newPosition.y], tile, newPosition));
        }
        else
        {
            // Move the tile to the new position
            grid[oldPosition.x, oldPosition.y] = null;
            grid[newPosition.x, newPosition.y] = tile;

            tile.MoveTo(newPosition, GetWorldPosition(newPosition), tileMoveSpeed);
        }
    }

    private IEnumerator MergeTiles(Tile target, Tile source, Vector2Int position)
    {
        isMoving = true;

        // Move the source tile to the target position
        source.MoveTo(position, GetWorldPosition(position), tileMoveSpeed);
        yield return new WaitForSeconds(tileMoveSpeed);

        // Merge the tiles
        int mergedValue = target.Value + source.Value;
        score += mergedValue;
        OnScoreUpdated?.Invoke(score);

        target.SetValue(mergedValue);

        // Destroy the source tile
        grid[position.x, position.y] = target;
        Destroy(source.gameObject);

        // Check if the player has reached 2048
        if (mergedValue >= 2048)
        {
            OnGameWon?.Invoke();
        }

        isMoving = false;
    }

    private IEnumerator AfterMoveRoutine()
    {
        // Wait for all moves to complete
        yield return new WaitForSeconds(tileMoveSpeed * 1.5f);

        // Spawn a new tile
        SpawnRandomTile();

        // Check if the game is over
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        // Check if there are any empty positions
        if (GetAvailablePositions().Count > 0)
            return;

        // Check if there are any possible moves
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Tile currentTile = grid[x, y];

                // Check right
                if (x < gridSize - 1 && grid[x + 1, y] != null &&
                    currentTile.CanMergeWith(grid[x + 1, y].Value))
                {
                    return;
                }

                // Check down
                if (y < gridSize - 1 && grid[x, y + 1] != null &&
                    currentTile.CanMergeWith(grid[x, y + 1].Value))
                {
                    return;
                }
            }
        }

        // If no moves are possible, game over
        OnGameOver?.Invoke();
    }

    public void RestartGame()
    {
        // Clear the grid
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if (grid[x, y] != null)
                {
                    Destroy(grid[x, y].gameObject);
                }
            }
        }

        // Reset variables
        InitializeGrid();
        score = 0;
        OnScoreUpdated?.Invoke(score);

        // Spawn initial tiles
        SpawnInitialTiles();
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}