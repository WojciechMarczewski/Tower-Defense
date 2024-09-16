using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [Tooltip("Collider component of the Targeter element. It can be a box or a spherical bounding volume.")]
    public Collider Col;
    [HideInInspector] public List<Enemy> Enemies = new List<Enemy>();
    public bool TargetsAreAvailable { get { return Enemies.Count > 0; } }

    public void SetRange(float range)
    {
        if (Col is BoxCollider)
        {
            (Col as BoxCollider).size = new Vector3(range * 2, 30, range * 2);
            (Col as BoxCollider).center = new Vector3(0, 15, 0);
        }
        else if (Col is SphereCollider)
        {
            (Col as SphereCollider).radius = range;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            Enemies.Add(enemy);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            Enemies.Remove(enemy);
        }
    }
    public Enemy GetClosestEnemy(Vector3 point)
    {
        float lowestDistance = Mathf.Infinity;
        Enemy enemyWithLowestDistance = null;

        for (int i = 0; i < Enemies.Count; i++)
        {
            var enemy = Enemies[i];
            if (enemy == null || !enemy.Alive)
            {
                Enemies.RemoveAt(i);
                i -= 1;
            }
            else
            {
                float dist = Vector3.Distance(point, enemy.Trans.position);
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
