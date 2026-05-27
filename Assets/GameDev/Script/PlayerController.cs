using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] private TextMeshProUGUI _winText;
    private Rigidbody _rb;
    private Vector2 _inputVector;
    private int _count = 0;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _textMeshProUGUI.text = "Count: " + _count;
        _winText.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        _rb.AddForce(new Vector3(_inputVector.x, 0, _inputVector.y) * _moveSpeed, ForceMode.Force);
    }

    private void OnMove(InputValue value)
    {
        _inputVector = value.Get<Vector2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Destroy(other.gameObject);
            _count++;
            _textMeshProUGUI.text = "Count: " + _count;
            if (_count >= 4)
            {
                _winText.gameObject.SetActive(true);
            }
        }
    }
}
