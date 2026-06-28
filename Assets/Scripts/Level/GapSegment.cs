using System.Collections.Generic;
using UnityEngine;

public class GapSegment : Segment
{
    [SerializeField] private float tileSize = 1f;

    private readonly List<GapTile> _tiles = new List<GapTile>();

    public void Initialise(int gapWidth, GameObject gapTilePrefab)
    {
        ClearTiles();
        SegmentLength = gapWidth * tileSize;

        for (int i = 0; i < gapWidth; i++)
        {
            float zOffset = i * tileSize + tileSize * 0.5f;
            var go = Instantiate(gapTilePrefab,
                transform.position + Vector3.forward * zOffset,
                Quaternion.identity, transform);

            var tile = go.GetComponent<GapTile>();
            tile.Initialise();
            _tiles.Add(tile);
        }

        gameObject.SetActive(true);
    }

    private void ClearTiles()
    {
        foreach (var t in _tiles)
            if (t != null) Destroy(t.gameObject);
        _tiles.Clear();
        SegmentLength = 0;
    }

    public override void Recycle()
    {
        ClearTiles();
        gameObject.SetActive(false);
    }
}