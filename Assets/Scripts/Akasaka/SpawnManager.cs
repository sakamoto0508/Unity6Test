using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _interval = 1.5f;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _interval)
        {
            _timer = 0f;
            SpawnAtRandom();
        }
    }

    private void SpawnAtRandom()
    {
        if (_spawnPoints.Length == 0) { return; }

        int index = Random.Range(0, _spawnPoints.Length);
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = _spawnPoints[index].position;
        cube.AddComponent<Rigidbody>();
    }
}