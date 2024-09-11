using UnityEngine;
public class HealthBar : MonoBehaviour
{
    public RectTransform healthBarForegroundObject;
    public Transform enemyTransform;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {


        float healthPercent = 0f;
        if (enemyTransform != null)
        {
            //Debug.Log("Health bar position: " + screenPos);
            Enemy enemy = enemyTransform.GetComponent<Enemy>();
            healthPercent = enemy.health / enemy.maxHealth;
        }

        healthBarForegroundObject.localScale = new Vector3(healthPercent, 1, 1);
        transform.rotation = mainCamera.transform.rotation;
    }

}
