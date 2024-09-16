using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
<<<<<<< HEAD
    public Transform Trans;
    public Transform ProjectileSeekPoint;
    [Header("Stats")]
    public float MaxHealth;
    [HideInInspector] public float Health;
    [HideInInspector] public bool Alive = true;
    public float HealthGainPerLevel;
=======
    public Transform trans;
    public Transform projectileSeekPoint;
    [Header("Stats")]
    public float maxHealth;
    [HideInInspector] public float health;
    [HideInInspector] public bool alive = true;
    public float healthGainPerLevel;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
    private HealthBar healthBar;

    // Start is called before the first frame update
    protected virtual void Start()
    {
<<<<<<< HEAD
        MaxHealth = MaxHealth + (HealthGainPerLevel * (Player.Level - 1));
        Health = MaxHealth;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.EnemyTransform = gameObject.transform;
=======
        maxHealth = maxHealth + (healthGainPerLevel * (Player.level - 1));
        health = maxHealth;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.enemyTransform = gameObject.transform;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(float amount)
    {
        if (amount > 0)
        {
<<<<<<< HEAD
            Health = Mathf.Max(Health - amount, 0);
            if (Health == 0)
=======
            health = Mathf.Max(health - amount, 0);
            if (health == 0)
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
            {
                Die();
            }
        }
    }
    public void Die()
    {
<<<<<<< HEAD
        if (Alive)
        {
            Alive = false;
=======
        if (alive)
        {
            alive = false;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
            Destroy(gameObject);

        }
    }
    public void Leak()
    {
<<<<<<< HEAD
        Player.RemainingLives -= 1;
=======
        Player.remainingLives -= 1;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        Destroy(gameObject);
    }
}
