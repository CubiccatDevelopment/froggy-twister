using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Singleton()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public PlayerController player;
    public InputManager inputManager;
    public CameraPivotController cameraPivotController;
    public IslandsManager islandsManager;

    private void Awake()
    {
        Singleton();

        islandsManager.OnIslandMove += () =>
        {
            cameraPivotController.SetHeight(islandsManager.GetAverageHeight());
        };

        player.OnJump += cameraPivotController.RotateOnce;
        player.OnLanding += islandsManager.SetPreviousIslandRandomPosition;

        inputManager.OnPowerRelease += player.Jump;
        inputManager.OnReset += Reset;
    }

    public void Reset()
    {
        player.Reset(islandsManager.GetStartingIslandPosition() + Vector3.up * 2);
        cameraPivotController.Reset();
        islandsManager.Reset();
    }
}
