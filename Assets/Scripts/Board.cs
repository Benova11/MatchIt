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

  void Start()
  {
    SetupTiles();
    m_allGamePieces = new GamePiece[width,height];
    SetCameraSize();
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
    Camera.main.transform.position = new Vector3((float) (width - 1) / 2, (float) (height - 1) / 2, -10);

    float aspectRatio = (float) Screen.width / (float) Screen.height;

    float verticalSize = (float)height / 2 + (float)borderSize;

    float horizontalSize = ((float)width / 2 + (float)borderSize) / aspectRatio;

    Camera.main.orthographicSize = (verticalSize > horizontalSize) ? verticalSize : horizontalSize;
  }
}
