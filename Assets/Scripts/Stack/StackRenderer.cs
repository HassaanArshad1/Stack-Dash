using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StackCollector))]
public class StackRenderer : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private GameObject stackPiecePrefab;
    [SerializeField] private float stackSpacing = 0.25f;
    [SerializeField] private float followSpeed = 12f;
    [SerializeField] private float stackOffsetBehind = 1f;
    [SerializeField] private float stackBaseHeight = 0.75f;

    private StackCollector _collector;
    private readonly List<Transform> _pieces = new List<Transform>();

    private void Awake()
    {
        _collector = GetComponent<StackCollector>();
    }

    private void OnEnable()
    {
        _collector.OnStackChanged += SyncVisuals;
    }

    private void OnDisable()
    {
        _collector.OnStackChanged -= SyncVisuals;
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
        while (_pieces.Count < newCount)
        {
            var piece = Instantiate(stackPiecePrefab, transform.position, Quaternion.identity);
            _pieces.Add(piece.transform);
        }

        while (_pieces.Count > newCount)
        {
            Debug.Log("Stack Being COnsumed");
            Destroy(_pieces[^1].gameObject);
            _pieces.RemoveAt(_pieces.Count - 1);
        }
    }
}