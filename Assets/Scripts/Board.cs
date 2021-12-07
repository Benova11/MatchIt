using UnityEngine;

public class Board : MonoBehaviour
{
  [SerializeField] int width;
  [SerializeField] int height;
  [SerializeField] int borderSize;

  [SerializeField] GameObject tilePrefab;
  public GameObject[] gamePiecePrefabs;

  [SerializeField] float swapTime = 0.25f;

  Tile[,] allTiles;
  GamePiece[,] allGamePieces;

  Tile clickedTile;
  Tile targetTile;

  void Start()
  {
    SetupTiles();
    allGamePieces = new GamePiece[width, height];
    SetCameraSize();
    FillBoardRandomly();
  }

  void SetupTiles()
  {
    allTiles = new Tile[width, height];
    for (int i = 0; i < width; i++)
    {
      for (int j = 0; j < height; j++)
      {
        GameObject tile = Instantiate(tilePrefab, new Vector3(i, j, 0), Quaternion.identity, transform);
        tile.name = "Tile (" + i + "," + j + ")";
        allTiles[i, j] = tile.GetComponent<Tile>();
        allTiles[i, j].Init(i, j, this);
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

  public void PlaceGamePiece(GamePiece gamePiece, int x, int y)
  {
    if (gamePiece == null)
    {
      Debug.LogWarning("BOARD: Ivalid GamePiece!");
      return;
    }

    gamePiece.transform.position = new Vector3(x, y, 0);
    gamePiece.transform.rotation = Quaternion.identity;
    if(IsPlacementAutorized(x,y))
      allGamePieces[x, y] = gamePiece;
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
          randomPiece.GetComponent<GamePiece>().Init(this);
          randomPiece.transform.SetParent(transform);
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
      //Debug.Log($"clicked tile: {tile.name}");
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
      SwitchTilesPieces(clickedTile, targetTile);
    }
    clickedTile = null;
    targetTile = null;

  }

  void SwitchTilesPieces(Tile clickedTile, Tile targetTile)
  {
    if(IsPiecesAdjecent(clickedTile,targetTile))
    {
      GamePiece clickedPiece = allGamePieces[clickedTile.xIndex, clickedTile.yIndex];
      GamePiece targetPiece = allGamePieces[targetTile.xIndex, targetTile.yIndex];

      clickedPiece.Move(targetTile.xIndex, targetTile.yIndex, swapTime);
      targetPiece.Move(clickedTile.xIndex, clickedTile.yIndex, swapTime);
    }
  }

  bool IsPiecesAdjecent(Tile tileA, Tile tileB)
  {
    return (Mathf.Abs(tileA.xIndex - tileB.xIndex) <= 1 && tileA.yIndex == tileB.yIndex)
        || (Mathf.Abs(tileA.yIndex - tileB.yIndex) <= 1 && tileA.xIndex == tileB.xIndex);
  }

  bool IsPlacementAutorized(int x, int y)
  {
    return (x >= 0 && x < width && y >= 0 && y < height);
  }
}
