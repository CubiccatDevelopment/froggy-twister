using UnityEngine;

public class IslandController : MonoBehaviour
{
    [SerializeField] Transform waterLily;
    public Transform GetWaterLily()
    {
        return waterLily;
    }
    [SerializeField] DiamondController diamond;
    public DiamondController GetDiamond()
    {
        return diamond;
    }

    public void ActivateDiamond()
    {
        if(!diamond.IsActive)
            diamond.Activate();
    }
}
