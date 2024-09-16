using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]

    public Transform Trans;
    public Transform ProjectileSeekPoint;
    [Header("Stats")]
    public float MaxHealth;
    [HideInInspector] public float Health;
    [HideInInspector] public bool Alive = true;
    public float HealthGainPerLevel;

    private HealthBar healthBar;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        MaxHealth = MaxHealth + (HealthGainPerLevel * (Player.Level - 1));
        Health = MaxHealth;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.EnemyTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(float amount)
    {
        if (amount > 0)
        {
            Health = Mathf.Max(Health - amount, 0);
            if (Health == 0)
            {
                Die();
            }
        }
    }
    public void Die()
    {
        if (Alive)
        {
            Alive = false;
            Destroy(gameObject);

        }
    }
    public void Leak()
    {
        Player.RemainingLives -= 1;
        Destroy(gameObject);
    }
}
