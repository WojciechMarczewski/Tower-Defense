using Assets.Scripts;
using UnityEngine;

public class HotPlate : TargetingTower
{
    public float damagePerSecond = 10;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (targeter.TargetsAreAvailable)
        {
            for (int i = 0; i < targeter.enemies.Count; i++)
            {
                Enemy enemy = targeter.enemies[i];
                if (enemy is GroundEnemy)
                {
                    enemy.TakeDamage(damagePerSecond * Time.deltaTime);
                }
            }
        }
    }
}
