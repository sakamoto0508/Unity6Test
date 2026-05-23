using UnityEngine;

public class CubeArray : MonoBehaviour
{
    private GameObject[] _cubes;

    private void Start()
    {
        _cubes = new GameObject[5];

        for (int i = 0; i < _cubes.Length; i++)
        {
            _cubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _cubes[i].transform.position = new Vector3(i * 2f, 0f, 0f);
        }
    }

    private void Update()
    {
        foreach (GameObject cube in _cubes)
        {
            cube.transform.Rotate(0f, 90f * Time.deltaTime, 0f);
        }
    }
}