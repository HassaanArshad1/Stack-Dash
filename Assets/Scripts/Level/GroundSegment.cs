using System.Collections.Generic;
using UnityEngine;

public class GroundSegment : Segment
{
    [SerializeField] private float segmentLength = 10f;
    [SerializeField] private float segmentWidth = 8f;
    [SerializeField] private float edgePadding = 0.8f;

    private readonly List<PlatformPickup> _activePickups = new List<PlatformPickup>();
    private readonly List<BonusPickup> _activeBonusPickups = new List<BonusPickup>();
    private readonly List<Obstacle> _activeObstacles = new List<Obstacle>();

    private readonly List<Vector3> _usedPositions = new List<Vector3>();
    
    public override void Recycle()
    {
        ClearAll();
        gameObject.SetActive(false);
    }

    public void Initialise(StackCollector collector, int pickupCount,
        GameObject pickupPrefab, GameObject bonusPrefab,
        GameObject obstaclePrefab, DifficultyConfig config)
    {
        SegmentLength = segmentLength;
        ClearPickups();
        SpawnPickups(collector, pickupCount, pickupPrefab);
        
        if (Random.value < config.bonusPickupChance)
            SpawnBonusPickup(collector, bonusPrefab);

        for (int i = 0; i < config.obstaclesPerSegment; i++)
            SpawnObstacle(obstaclePrefab);
        
        gameObject.SetActive(true);
    }

    private Vector3 GetRandomPosition(float yOffset = 0.2f)
    {
        float minX = -segmentWidth * 0.5f + edgePadding;
        float maxX =  segmentWidth * 0.5f - edgePadding;
        float minZ = edgePadding;
        float maxZ = segmentLength - edgePadding;
        float minDistance = 1.5f;
        int maxAttempts = 20;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector3 candidate = transform.position + new Vector3(
                Random.Range(minX, maxX),
                yOffset,
                Random.Range(minZ, maxZ)
            );

            bool tooClose = false;
            foreach (var pos in _usedPositions)
            {
                if (Vector3.Distance(candidate, pos) < minDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                _usedPositions.Add(candidate);
                return candidate;
            }
        }

        return Vector3.zero; // fallback — caller should check for this
    }

    private void SpawnPickups(StackCollector collector, int count, GameObject prefab)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 worldPos = GetRandomPosition();
            if (worldPos == Vector3.zero) continue;

            var go = Instantiate(prefab, worldPos, Quaternion.identity, transform);
            var pickup = go.GetComponent<PlatformPickup>();
            pickup.Initialise(collector);
            _activePickups.Add(pickup);
        }
    }

    private void SpawnBonusPickup(StackCollector collector, GameObject prefab)
    {
        Vector3 worldPos = GetRandomPosition(0.4f);
        if (worldPos == Vector3.zero) return;

        var go = Instantiate(prefab, worldPos, Quaternion.identity, transform);
        var bonus = go.GetComponent<BonusPickup>();
        bonus.Initialise(collector);
        _activeBonusPickups.Add(bonus);
    }

    private void SpawnObstacle(GameObject prefab)
    {
        Vector3 worldPos = GetRandomPosition(1f);
        if (worldPos == Vector3.zero) return;

        var go = Instantiate(prefab, worldPos, Quaternion.identity, transform);
        var obstacle = go.GetComponent<Obstacle>();
        obstacle.Initialise();
        _activeObstacles.Add(obstacle);
    }
    private void ClearAll()
    {
        ClearPickups();
        foreach (var b in _activeBonusPickups)
            if (b != null) Destroy(b.gameObject);
        _activeBonusPickups.Clear();

        foreach (var o in _activeObstacles)
            if (o != null) Destroy(o.gameObject);
        _activeObstacles.Clear();
        
        _usedPositions.Clear();
    }

    
    private void ClearPickups()
    {
        foreach (var p in _activePickups)
            if (p != null) Destroy(p.gameObject);
        _activePickups.Clear();
    }
}