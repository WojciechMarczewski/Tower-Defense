using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
<<<<<<< HEAD
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
=======
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
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
<<<<<<< HEAD
            Enemies.Add(enemy);
=======
            enemies.Add(enemy);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        }
    }
    public void OnTriggerExit(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
<<<<<<< HEAD
            Enemies.Remove(enemy);
=======
            enemies.Remove(enemy);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        }
    }
    public Enemy GetClosestEnemy(Vector3 point)
    {
        float lowestDistance = Mathf.Infinity;
        Enemy enemyWithLowestDistance = null;
<<<<<<< HEAD
        for (int i = 0; i < Enemies.Count; i++)
        {
            var enemy = Enemies[i];
            if (enemy == null || !enemy.Alive)
            {
                Enemies.RemoveAt(i);
=======
        for (int i = 0; i < enemies.Count; i++)
        {
            var enemy = enemies[i];
            if (enemy == null || !enemy.alive)
            {
                enemies.RemoveAt(i);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
                i -= 1;
            }
            else
            {
<<<<<<< HEAD
                float dist = Vector3.Distance(point, enemy.Trans.position);
=======
                float dist = Vector3.Distance(point, enemy.trans.position);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
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
