using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloqueo : MonoBehaviour
{
    public List<EnemyNew> enemies = new List<EnemyNew>();
    MeshRenderer mesh;
    BoxCollider boxCol;
    public GameObject col;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        boxCol = GetComponent<BoxCollider>();
    }

    public void RegisterEnemy(EnemyNew enemy)
    {
        enemies.Add(enemy);
    }

    public void EnemyDefeated(EnemyNew enemy)
    {
        enemies.Remove(enemy);

        if (enemies.Count == 0)
        {
            mesh.enabled = false;
            boxCol.enabled = false;
            col.SetActive(false);
        }
    }
}
