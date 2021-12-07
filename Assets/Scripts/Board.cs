using UnityEngine;

public class Board : MonoBehaviour
{
  [SerializeField] int width;
  [SerializeField] int height;
  [SerializeField] int borderSize;

  [SerializeField] GameObject tilePrefab;
  public GameObject[] gamePiecePrefabs;

  Tile[,] m_allTiles;
  GamePiece[,] m_allGamePieces;

  Tile clickedTile;
  Tile targetTile;

  void Start()
  {
    SetupTiles();
    m_allGamePieces = new GamePiece[width, height];
    SetCameraSize();
    FillBoardRandomly();
  }

  void SetupTiles()
  {
    m_allTiles = new Tile[width, height];
    for (int i = 0; i < width; i++)
    {
      for (int j = 0; j < height; j++)
      {
        GameObject tile = Instantiate(tilePrefab, new Vector3(i, j, 0), Quaternion.identity, transform);
        tile.name = "Tile (" + i + "," + j + ")";
        m_allTiles[i, j] = tile.GetComponent<Tile>();
        m_allTiles[i, j].Init(i, j, this);
      }
    }
  }

  void SetCameraSize()
  {
    Camera.main.transform.position = new Vector3((float)(width - 1) / 2, (float)(height - 1) / 2, -10);

    float aspectRatio = (float)Screen.width / (float)Screen.height;

    float verticalSize = (float)height / 2 + (float)borderSize;

    float horizontalSize = ((float)width / 2 + (float)borderSize) / aspectRatio;

    Camera.main.orthographicSize = (verticalSize > horizontalSize) ? verticalSize : horizontalSize;
  }

  GameObject GetRandomGamePiece()
  {
    int randomIdx = Random.Range(0, gamePiecePrefabs.Length);
    if (gamePiecePrefabs[randomIdx] == null)
      Debug.LogWarning("BOARD:  " + randomIdx + "does not contaion a valid GamePiece prefab!");
    return gamePiecePrefabs[randomIdx];
  }

  void PlaceGamePiece(GamePiece gamePiece, int x, int y)
  {
    if (gamePiece == null)
    {
      Debug.LogWarning("BOARD: Ivalid GamePiece!");
      return;
    }

    gamePiece.transform.position = new Vector3(x, y, 0);
    gamePiece.transform.rotation = Quaternion.identity;
    gamePiece.SetCoord(x, y);
  }

  void FillBoardRandomly()
  {
    for (int i = 0; i < width; i++)
    {
      for (int j = 0; j < height; j++)
      {
        GameObject randomPiece = Instantiate(GetRandomGamePiece(), Vector3.zero, Quaternion.identity);
        if (randomPiece != null)
        {
          PlaceGamePiece(randomPiece.GetComponent<GamePiece>(), i, j);
        }
      }
    }
  }

  public void ClickTile(Tile tile)
  {
    if (clickedTile == null)
    {
      clickedTile = tile;
      Debug.Log($"clicked tile: {tile.name}");
    }
  }

  public void DragToTile(Tile tile)
  {
    if (clickedTile != null)
      targetTile = tile;
  }

  public void ReleaseTile()
  {
    if (clickedTile != null && targetTile != null)
    {
      SwitchTiles(clickedTile, targetTile);
    }
    clickedTile = null;
    targetTile = null;

  }

  void SwitchTiles(Tile clickedTile, Tile targetTile)
  {
    //todo

    //clickedTile = null;
    //targetTile = null;
  }
}
