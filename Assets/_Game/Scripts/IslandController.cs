using UnityEngine;

public class IslandController : MonoBehaviour
{
    [SerializeField] Transform waterLily;
    public Transform GetWaterLily()
    {
        return waterLily;
    }
}
