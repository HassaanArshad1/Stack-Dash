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
        var collector = other.GetComponentInParent<StackCollector>();
        if (collector == null) return;

        _collected = true;
        FXManager.Instance.PlayCollectFX((transform.position));
        collector.AddToStack();
        gameObject.SetActive(false);
    }
}