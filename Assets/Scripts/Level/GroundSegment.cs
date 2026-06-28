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

        for (int i = 0; i < count; i++)
        {
            Vector3 localPos = new Vector3(
                Random.Range(minX, maxX),
                0.2f,
                Random.Range(minZ, maxZ)
            );

            Vector3 worldPos = transform.position + localPos;

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