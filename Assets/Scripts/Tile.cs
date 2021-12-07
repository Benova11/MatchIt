using UnityEngine;

public class Tile : MonoBehaviour
{
  public int xIndex;
  public int yIndex;

  Board motherBoard;

  [SerializeField] SpriteRenderer spriteRenderer;

  void Start()
  {
    SetAlphaValue(0);
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.T))
      SetAlphaValue(spriteRenderer.color.a == 0 ? 255 : 0);
  }

  public void Init(int x, int y, Board board)
  {
    xIndex = x;
    yIndex = y;
    motherBoard = board;
  }

  public void SetAlphaValue(float newValue)
  {
    spriteRenderer.color = new Color(255, 255, 255, newValue);
  }

  void OnMouseDown()
  {
    if(motherBoard!=null)
      motherBoard.ClickTile(this);
  }

  void OnMouseEnter()
  {
    if (motherBoard != null)
      motherBoard.DragToTile(this);
  }

  void OnMouseUp()
  {
    if (motherBoard != null)
      motherBoard.ReleaseTile();
  }
}
