using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlatformPickup : MonoBehaviour
{
    private StackCollector _stackCollector;
    private bool _collected;

    public void Initialise(StackCollector collector)
    {
        _stackCollector = collector;
        _collected = false;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_collected) return;
        if (!other.TryGetComponent(out StackCollector collector)) return;

        _collected = true;
        collector.AddToStack();
        gameObject.SetActive(false);
    }
}