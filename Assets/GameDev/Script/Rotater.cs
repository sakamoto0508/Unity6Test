using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 90f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);

    }
}
