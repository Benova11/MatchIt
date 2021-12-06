using System.Collections;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
  public int xIndex;
  public int yIndex;

  void Start()
  {

  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.RightArrow))
      Move((int)transform.position.x + 1, (int)transform.position.y, 1);
    if (Input.GetKeyDown(KeyCode.LeftArrow))
      Move((int)transform.position.x - 1, (int)transform.position.y, 1);
  }

  public void SetCoord(int x, int y)
  {
    xIndex = x;
    yIndex = y;
  }

  public void Move(int xDest, int yDest, float timeToMove)
  {
    StartCoroutine(MoveRutine(new Vector3(xDest, yDest, 0), timeToMove));
  }

  IEnumerator MoveRutine(Vector3 destination, float timeToMove)
  {
    Vector3 startPosition = transform.position;
    bool reachedDestination = false;
    float elapsedTime = 0;

    while (!reachedDestination)
    {
      if (Vector3.Distance(transform.position, destination) < 0.01f)
      {
        reachedDestination = true;
        transform.position = destination;
      }

      elapsedTime += Time.deltaTime;
      float t = Mathf.Clamp(elapsedTime / timeToMove, 0, 1);

      transform.position =  Vector3.Lerp(startPosition, destination, t);
      yield return null;
    }
  }

}
