using Assets.Scripts;
using UnityEngine;

public class HotPlate : TargetingTower
{

    public float DamagePerSecond = 10;

    void Update()
    {
        if (Targeter.TargetsAreAvailable)
        {
            for (int i = 0; i < Targeter.Enemies.Count; i++)
            {
                Enemy enemy = Targeter.Enemies[i];
                if (enemy is GroundEnemy)
                {
                    enemy.TakeDamage(DamagePerSecond * Time.deltaTime);
                }
            }
        }
    }
}
