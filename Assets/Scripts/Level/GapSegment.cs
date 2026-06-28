using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(BridgePlacer))]
public class GapSegment : Segment
{
    [SerializeField] private float tileSize = 1f;
    [SerializeField] private float segmentWidth = 8f;

    private BoxCollider _trigger;
    private BridgePlacer _bridgePlacer;

    private void Awake()
    {
        _trigger = GetComponent<BoxCollider>();
        _trigger.isTrigger = true;
        _bridgePlacer = GetComponent<BridgePlacer>();
    }

    public void Initialise(int gapWidth, StackCollector collector)
    {
        SegmentLength = gapWidth * tileSize;

        _trigger.size = new Vector3(segmentWidth, 2f, SegmentLength);
        _trigger.center = new Vector3(0, 1f, SegmentLength * 0.5f);

        float gapStartZ = transform.position.z;
        float gapEndZ = transform.position.z + SegmentLength;
        _bridgePlacer.Initialise(collector, gapStartZ, gapEndZ);
        gameObject.SetActive(true);
    }

    public override void Recycle()
    {
        _bridgePlacer.ClearTiles();
        gameObject.SetActive(false);
    }
}