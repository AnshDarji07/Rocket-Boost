using UnityEngine;

public class Osscilation : MonoBehaviour
{
    [SerializeField] Vector3 movementvector;
    [SerializeField] float speed;

    Vector3 StartPosition;
    Vector3 EndPosition;
    float movementFactor;

    private void Start(){
        StartPosition = transform.position;
        EndPosition = StartPosition + movementvector;
    }

    private void Update(){
        movementFactor = Mathf.PingPong(Time.time * speed,1);
        transform.position = Vector3.Lerp(StartPosition, EndPosition, movementFactor);
    }
}
