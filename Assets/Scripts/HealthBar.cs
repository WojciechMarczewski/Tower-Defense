using UnityEngine;
public class HealthBar : MonoBehaviour
{
    public RectTransform HealthBarForegroundObject;
    [HideInInspector]
    public Transform EnemyTransform;
    [HideInInspector]
    public Camera MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {


        float healthPercent = 0f;
        if (EnemyTransform != null)
        {
            //Debug.Log("Health bar position: " + screenPos);
            Enemy enemy = EnemyTransform.GetComponent<Enemy>();
            healthPercent = enemy.Health / enemy.MaxHealth;
        }

        HealthBarForegroundObject.localScale = new Vector3(healthPercent, 1, 1);
        transform.rotation = MainCamera.transform.rotation;
    }

}
