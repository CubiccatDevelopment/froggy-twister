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
    public GameBoundsController gameBounds;
    public ScoreManager scoreManager;

    private void Awake()
    {
        Singleton();

        islandsManager.OnIslandMove += () =>
        {
            float islandsAvgHeight = islandsManager.GetAverageHeight();
            cameraPivotController.SetHeight(islandsAvgHeight);
            gameBounds.SetHeight(islandsAvgHeight);
        };

        player.OnJump += cameraPivotController.RotateOnce;
        player.OnLanding += islandsManager.SetPreviousIslandRandomPosition;
        player.OnLanding += scoreManager.AddPoint;

        inputManager.OnPowerRelease += player.Jump;
        inputManager.OnReset += Reset;
    }

    public void Reset()
    {
        player.Reset(islandsManager.GetStartingIsland().GetWaterLily());
        cameraPivotController.Reset();
        islandsManager.Reset();
        gameBounds.SetHeight(0);
        scoreManager.ChangeScore(0);
    }
}
