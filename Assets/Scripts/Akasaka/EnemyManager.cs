using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;

    private void Start()
    {
        for (int i = 0; i < _enemyPrefabs.Length; i++)
        {
            Debug.Log($"敵プレハブ[{i}]: {_enemyPrefabs[i].name}");
        }
    }
}