using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [Tooltip("Sk³adnik Collider elementu Targeter. Mo¿e byæ prostopad³oœcienn¹ lub kulist¹ bry³¹ ograniczaj¹c¹.")]
    public Collider col;
    [HideInInspector] public List<Enemy> enemies = new List<Enemy>();
    public bool TargetsAreAvailable { get { return enemies.Count > 0; } }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetRange(float range)
    {
        if (col is BoxCollider)
        {
            (col as BoxCollider).size = new Vector3(range * 2, 30, range * 2);
            (col as BoxCollider).center = new Vector3(0, 15, 0);
        }
        else if (col is SphereCollider)
        {
            (col as SphereCollider).radius = range;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemies.Add(enemy);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemies.Remove(enemy);
        }
    }
    public Enemy GetClosestEnemy(Vector3 point)
    {
        float lowestDistance = Mathf.Infinity;
        Enemy enemyWithLowestDistance = null;
        for (int i = 0; i < enemies.Count; i++)
        {
            var enemy = enemies[i];
            if (enemy == null || !enemy.alive)
            {
                enemies.RemoveAt(i);
                i -= 1;
            }
            else
            {
                float dist = Vector3.Distance(point, enemy.trans.position);
                if (dist < lowestDistance)
                {
                    lowestDistance = dist;
                    enemyWithLowestDistance = enemy;
                }
            }
        }
        return enemyWithLowestDistance;
    }
}
