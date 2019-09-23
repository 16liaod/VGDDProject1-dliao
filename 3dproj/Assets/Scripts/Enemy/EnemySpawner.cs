using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region editorvar
    [SerializeField]
    [Tooltip("bounds of spawn location")]
    private Vector3 m_bounds;

    [SerializeField]
    [Tooltip("list of all enemies and info")]
    private EnemySpawn[] m_enemies;
    #endregion

    #region Init
    private void Awake()
    {
        startspawning();
    }
    #endregion

    #region spawn methods
    public void startspawning()
    {
        for(int i = 0; i < m_enemies.Length; i++)
        {
            StartCoroutine(Spawn(i));
        }
    }
    private IEnumerator Spawn(int enemyInd)
    {
        EnemySpawn info = m_enemies[enemyInd];
        int i = 0;
        bool alwaysspawn = false;
        if(info.Numenemiesspawn == 0)
        {
            alwaysspawn = true;
        }
        while (alwaysspawn || i < info.Numenemiesspawn)
        {
            yield return new WaitForSeconds(info.Timetospawn);
            float xVal = m_bounds.x / 2;
            float yVal = m_bounds.y / 2;
            float zVal = m_bounds.z / 2;

            Vector3 spawnpos = new Vector3(
                Random.Range(-xVal, xVal), Random.Range(-yVal, yVal), Random.Range(-zVal, zVal));

            spawnpos += transform.position;
            Instantiate(info.EnemyGO, spawnpos, Quaternion.identity);
            if (!alwaysspawn)
            {
                i++;
            }
        }
    }
    #endregion
}
