using Assets.Scripts;
using UnityEngine;

public class HotPlate : TargetingTower
{
<<<<<<< HEAD
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
=======
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
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
                }
            }
        }
    }
}
