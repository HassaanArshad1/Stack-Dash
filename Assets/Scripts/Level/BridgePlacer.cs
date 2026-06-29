using System.Collections.Generic;
using UnityEngine;

public class BridgePlacer : MonoBehaviour
{
    [SerializeField] private float tileSize = 1f;
    [SerializeField] private GameObject bridgeTilePrefab;

    private StackCollector _collector;
    private Transform _player;
    private bool _isInGap;
    private float _lastTilePlacedZ;
    private float _gapEndZ;
    private readonly List<GameObject> _placedTiles = new List<GameObject>();

    public void Initialise(StackCollector collector, float gapStartZ, float gapEndZ)
    {
        _collector = collector;
        _player = collector.transform;
        _isInGap = false;
        _lastTilePlacedZ = gapStartZ; // gap geometry, not player position
        _gapEndZ = gapEndZ;
        ClearTiles();
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!_isInGap || _player == null) return;

        while (_player.position.z - _lastTilePlacedZ >= tileSize)
        {
            bool survived = _collector.ConsumeStack(1);

            if (!survived)
            {
                ScreenShake.Instance.Shake(0.5f, 0.3f);
                GameManager.Instance.TriggerGameOver();
                _isInGap = false;
                return;
            }

            Vector3 tilePos = new Vector3(
                _player.position.x,
                0.09f,
                _lastTilePlacedZ + tileSize * 0.5f
            );

            var tile = Instantiate(bridgeTilePrefab, tilePos, Quaternion.identity, transform);
            _placedTiles.Add(tile);
            _lastTilePlacedZ += tileSize;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var collector = other.GetComponentInParent<StackCollector>();
        if (collector == null) return;

        _isInGap = true;
    }

    private void OnTriggerExit(Collider other)
    {
        var collector = other.GetComponentInParent<StackCollector>();
        if (collector == null) return;
        _isInGap = false;
    }

    public void ClearTiles()
    {
        foreach (var t in _placedTiles)
            if (t != null) Destroy(t);
        _placedTiles.Clear();
    }
}