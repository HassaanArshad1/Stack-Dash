using System.Collections.Generic;
using UnityEngine;

public class GroundSegment : Segment
{
    [SerializeField] private Transform[] pickupSpawnPoints;
    [SerializeField] private float segmentLength = 10f;

    private readonly List<PlatformPickup> _activePickups = new List<PlatformPickup>();
    private StackCollector _collector;

    public void Initialise(StackCollector collector, int pickupCount,
        GameObject pickupPrefab)
    {
        SegmentLength = segmentLength;
        _collector = collector;
        ClearPickups();
        SpawnPickups(pickupCount, pickupPrefab);
        gameObject.SetActive(true);
    }

    private void SpawnPickups(int count, GameObject prefab)
    {
        int spawnCount = Mathf.Min(count, pickupSpawnPoints.Length);
        int[] indices = GetShuffledIndices(pickupSpawnPoints.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            var go = Instantiate(prefab,
                pickupSpawnPoints[indices[i]].position,
                Quaternion.identity, transform);

            var pickup = go.GetComponent<PlatformPickup>();
            pickup.Initialise(_collector);
            _activePickups.Add(pickup);
        }
    }

    private void ClearPickups()
    {
        foreach (var p in _activePickups)
            if (p != null) Destroy(p.gameObject);
        _activePickups.Clear();
    }

    private int[] GetShuffledIndices(int length)
    {
        int[] indices = new int[length];
        for (int i = 0; i < length; i++) indices[i] = i;
        for (int i = length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (indices[i], indices[j]) = (indices[j], indices[i]);
        }
        return indices;
    }

    public override void Recycle()
    {
        ClearPickups();
        gameObject.SetActive(false);
    }
}