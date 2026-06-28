using System.Collections.Generic;
using UnityEngine;

public class GroundSegment : Segment
{
    [SerializeField] private float segmentLength = 10f;
    [SerializeField] private float segmentWidth = 8f;
    [SerializeField] private float edgePadding = 0.8f;

    private readonly List<PlatformPickup> _activePickups = new List<PlatformPickup>();

    public override void Recycle()
    {
        ClearPickups();
        gameObject.SetActive(false);
    }

    public void Initialise(StackCollector collector, int pickupCount,
        GameObject pickupPrefab)
    {
        SegmentLength = segmentLength;
        ClearPickups();
        SpawnPickups(collector, pickupCount, pickupPrefab);
        gameObject.SetActive(true);
    }

    private void SpawnPickups(StackCollector collector, int count, GameObject prefab)
    {
        float minX = -segmentWidth * 0.5f + edgePadding;
        float maxX =  segmentWidth * 0.5f - edgePadding;
        float minZ = edgePadding;
        float maxZ = segmentLength - edgePadding;

        float minDistance = 1.5f; // minimum distance between pickups
        int maxAttempts = 20;     // prevent infinite loop

        List<Vector3> spawnedPositions = new List<Vector3>();

        for (int i = 0; i < count; i++)
        {
            Vector3 worldPos = Vector3.zero;
            bool validPosition = false;

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                Vector3 localPos = new Vector3(
                    Random.Range(minX, maxX),
                    0.2f,
                    Random.Range(minZ, maxZ)
                );

                worldPos = transform.position + localPos;

                // check against all already spawned positions
                bool tooClose = false;
                foreach (var pos in spawnedPositions)
                {
                    if (Vector3.Distance(worldPos, pos) < minDistance)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (!tooClose)
                {
                    validPosition = true;
                    break;
                }
            }

            if (!validPosition) continue; // skip if no valid spot found

            spawnedPositions.Add(worldPos);
            var go = Instantiate(prefab, worldPos, Quaternion.identity, transform);
            var pickup = go.GetComponent<PlatformPickup>();
            pickup.Initialise(collector);
            _activePickups.Add(pickup);
        }
    }
    private void ClearPickups()
    {
        foreach (var p in _activePickups)
            if (p != null) Destroy(p.gameObject);
        _activePickups.Clear();
    }
}