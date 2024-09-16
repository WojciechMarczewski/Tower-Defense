using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
<<<<<<< HEAD
    [HideInInspector] public float Damage;
    [HideInInspector] public float Speed;
    [HideInInspector] public Enemy TargetEnemy;
=======
    [HideInInspector] public float damage;
    [HideInInspector] public float speed;
    [HideInInspector] public Enemy targetEnemy;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Setup(float damage, float speed, Enemy targetEnemy)
    {
<<<<<<< HEAD
        this.Damage = damage;
        this.Speed = speed;
        this.TargetEnemy = targetEnemy;
=======
        this.damage = damage;
        this.speed = speed;
        this.targetEnemy = targetEnemy;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        OnSetup();
    }
    protected abstract void OnSetup();
}
