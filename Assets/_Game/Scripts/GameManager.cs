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

    private void Awake()
    {
        Singleton();

        player.OnJump += cameraPivotController.RotateOnce;

        inputManager.OnPowerRelease += player.Jump;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
