public abstract class Segment : UnityEngine.MonoBehaviour
{
    public float SegmentLength { get; protected set; }
    public float EndZ => transform.position.z + SegmentLength;

    public abstract void Recycle();
}
