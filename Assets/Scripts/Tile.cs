using UnityEngine;

public class Tile : MonoBehaviour
{
  public int xIndex;
  public int yIndex;

  Board motherBoard;

  void Start()
  {

  }

  public void Init(int x,int y, Board board)
  {
    xIndex = x;
    yIndex = y;
    motherBoard = board;
  }

}
