using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private DifficultyConfig difficultyConfig;
    [SerializeField] private Transform player;
    [SerializeField] private StackCollector stackCollector;

    [Header("Prefabs")]
    [SerializeField] private GameObject groundSegmentPrefab;
    [SerializeField] private GameObject gapSegmentPrefab;
    [SerializeField] private GameObject pickupPrefab;
    [SerializeField] private GameObject gapTilePrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnAheadDistance = 30f;
    [SerializeField] private float recycleDistance = 10f;
    [SerializeField] private int initialSegmentCount = 5;

    private readonly Queue<GroundSegment> _groundPool = new Queue<GroundSegment>();
    private readonly Queue<GapSegment> _gapPool = new Queue<GapSegment>();
    private readonly List<Segment> _activeSegments = new List<Segment>();

    private float _nextSpawnZ;
    private float _distanceTraveled;
    private bool _lastSegmentWasGap = false;

    private void Start()
    {
        _nextSpawnZ = 0f;
        SpawnInitialSegments();
    }

    private void Update()
    {
        if (GameManager.Instance.State != GameManager.GameState.Playing) return;

        _distanceTraveled = player.position.z;

        SpawnSegmentsAhead();
        RecycleSegmentsBehind();
        UpdatePlayerSpeed();
    }

    private void SpawnInitialSegments()
    {
        for (int i = 0; i < initialSegmentCount; i++)
            SpawnGroundSegment();
    }

    private void SpawnSegmentsAhead()
    {
        while (_nextSpawnZ < player.position.z + spawnAheadDistance)
        {
            float spawnChance = difficultyConfig.GetGapSpawnChance(_distanceTraveled);
            bool spawnGap = !_lastSegmentWasGap  // never two gaps in a row
                            && Random.value < spawnChance 
                            && _distanceTraveled > difficultyConfig.minDistanceBeforeFirstGap;

            if (spawnGap)
            {
                SpawnGapSegment();
                _lastSegmentWasGap = true;
            }
            else
            {
                SpawnGroundSegment();
                _lastSegmentWasGap = false;
            }
        }
    }

    private void SpawnGroundSegment()
    {
        var segment = GetGroundSegment();
        segment.transform.position = new Vector3(0, 0, _nextSpawnZ);
        int pickupCount = difficultyConfig.pickupsPerSegment;
        segment.Initialise(stackCollector, pickupCount, pickupPrefab);
        _nextSpawnZ += segment.SegmentLength;
        _activeSegments.Add(segment);
    }

    private void SpawnGapSegment()
    {
        var segment = GetGapSegment();
        segment.transform.position = new Vector3(0, 0, _nextSpawnZ);
        int gapWidth = difficultyConfig.GetGapWidth(_distanceTraveled);
        segment.Initialise(gapWidth, gapTilePrefab);
        _nextSpawnZ += segment.SegmentLength;
        _activeSegments.Add(segment);
    }

    private void RecycleSegmentsBehind()
    {
        for (int i = _activeSegments.Count - 1; i >= 0; i--)
        {
            var seg = _activeSegments[i];
            if (seg.EndZ < player.position.z - recycleDistance)
            {
                seg.Recycle();

                if (seg is GroundSegment ground)
                    _groundPool.Enqueue(ground);
                else if (seg is GapSegment gap)
                    _gapPool.Enqueue(gap);

                _activeSegments.RemoveAt(i);
            }
        }
    }

    private void UpdatePlayerSpeed()
    {
        // Hook into PlayerController speed via DifficultyConfig
        // Implemented in PlayerController update next
    }

    private GroundSegment GetGroundSegment()
    {
        if (_groundPool.Count > 0)
            return _groundPool.Dequeue();

        var go = Instantiate(groundSegmentPrefab);
        return go.GetComponent<GroundSegment>();
    }

    private GapSegment GetGapSegment()
    {
        if (_gapPool.Count > 0)
            return _gapPool.Dequeue();

        var go = Instantiate(gapSegmentPrefab);
        return go.GetComponent<GapSegment>();
    }
}