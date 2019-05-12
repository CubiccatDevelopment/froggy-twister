using UnityEngine;

public class GameBoundsController : MonoBehaviour
{
    public void SetHeight(float y)
    {
        Vector3 position = transform.position;
        position.y = y;
        transform.position = position;
    }
}
