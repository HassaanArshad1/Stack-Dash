using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StackCollector))]
public class StackRenderer : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private int maxVisiblePieces = 8;
    [SerializeField] private GameObject stackPiecePrefab;
    [SerializeField] private float stackSpacing = 0.25f;
    [SerializeField] private float followSpeed = 12f;
    [SerializeField] private float stackOffsetBehind = 1f;
    [SerializeField] private float stackBaseHeight = 0.75f;
    
    private readonly List<Transform> _pieces = new List<Transform>();
    

    private void OnEnable()
    {
        StackCollector.OnStackChanged += SyncVisuals;
    }

    private void OnDisable()
    {
        StackCollector.OnStackChanged -= SyncVisuals;
    }

    private void Update()
    {
        for (int i = 0; i < _pieces.Count; i++)
        {
            Vector3 target = transform.position 
                             + Vector3.back * stackOffsetBehind
                             + Vector3.up * (stackBaseHeight + i * stackSpacing);
            _pieces[i].position = Vector3.Lerp(
                _pieces[i].position, target, Time.deltaTime * followSpeed);
        }
    }

    private void SyncVisuals(int newCount)
    {
        int visualCount = Mathf.Min(newCount, maxVisiblePieces);
        
        while (_pieces.Count < visualCount)
        {
            var piece = Instantiate(stackPiecePrefab, transform.position, Quaternion.identity);
            _pieces.Add(piece.transform);
        }

        while (_pieces.Count > visualCount)
        {
            Destroy(_pieces[^1].gameObject);
            _pieces.RemoveAt(_pieces.Count - 1);
        }
    }
}