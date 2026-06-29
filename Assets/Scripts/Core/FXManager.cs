using UnityEngine;

public class FXManager : MonoBehaviour
{
    public static FXManager Instance { get; private set; }

    [SerializeField] private GameObject collectBurstFX;
    [SerializeField] private GameObject obstacleHitFX;
    [SerializeField] private GameObject bonusCollectFX;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void PlayCollectFX(Vector3 position)
    {
        Instantiate(collectBurstFX, position, Quaternion.identity);
    }
    
    public void PlayBonusCollectFX(Vector3 position)
    {
        Instantiate(bonusCollectFX, position, Quaternion.identity);
    }

    public void PlayObstacleHitFX(Vector3 position)
    {
        Instantiate(obstacleHitFX, position, Quaternion.identity);
    }
}