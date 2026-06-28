using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GapTile : MonoBehaviour
{
    private StackCollector _stackCollector;
    private bool _consumed;

    public void Initialise()
    {
        _consumed = false;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_consumed) return;
        var collector = other.GetComponentInParent<StackCollector>();
        if (collector == null) return;

        _consumed = true;
        bool survived = collector.ConsumeStack(1);
        if (!survived)
            GameManager.Instance.TriggerGameOver();
    }
}