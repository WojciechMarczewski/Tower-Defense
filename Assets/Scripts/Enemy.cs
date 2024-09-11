using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    public Transform trans;
    public Transform projectileSeekPoint;
    [Header("Stats")]
    public float maxHealth;
    [HideInInspector] public float health;
    [HideInInspector] public bool alive = true;
    public float healthGainPerLevel;
    private HealthBar healthBar;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        maxHealth = maxHealth + (healthGainPerLevel * (Player.level - 1));
        health = maxHealth;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.enemyTransform = gameObject.transform;


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(float amount)
    {
        if (amount > 0)
        {
            health = Mathf.Max(health - amount, 0);
            if (health == 0)
            {
                Die();
            }
        }
    }
    public void Die()
    {
        if (alive)
        {
            alive = false;
            Destroy(gameObject);

        }
    }
    public void Leak()
    {
        Player.remainingLives -= 1;
        Destroy(gameObject);
    }
}
