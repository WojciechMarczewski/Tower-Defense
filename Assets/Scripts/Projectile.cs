using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [HideInInspector] public float Damage;
    [HideInInspector] public float Speed;
    [HideInInspector] public Enemy TargetEnemy;
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
        this.Damage = damage;
        this.Speed = speed;
        this.TargetEnemy = targetEnemy;
        OnSetup();
    }
    protected abstract void OnSetup();
}
