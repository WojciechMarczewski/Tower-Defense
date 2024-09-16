using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("References")]
    public Transform Trans;
    public Transform SpawnPoint;
    public Transform LeakPoint;
    [Tooltip("Reference to the GameObject of the panel blocking the Play button.")]
    public GameObject PlayButtonLockPanel;

    [Header("X Bounds")]
    public float MinimumX = -70;
    public float MaximumX = 70;

    [Header("Y Bounds")]
    public float MinimumY = 18;
    public float MaximumY = 80;

    [Header("Z Bounds")]
    public float MinimumZ = -130;
    public float MaximumZ = 70;

    [Header("Movement")]
    [Tooltip("Distance covered per second of movement triggered by arrow keys.")]
    public float ArrowKeySpeed = 80;
    [Tooltip("Multiplier for movement by dragging the mouse. A higher value will cause the camera to move a greater distance with mouse movement.")]
    public float MouseDragSensitivity = 2.8f;
    [Tooltip("Smoothing applied to camera movement. Should be a value between 0 and 1.")]
    [Range(0, .99f)]
    public float MovementSmoothing = .75f;

    private Vector3 targetPosition;

    [Header("Scrolling")]
    [Tooltip("Distance in Y covered by the camera per unit of mouse wheel rotation.")]
    public float ScrollSensitivity = 1.6f;



    [Tooltip("Distance in Y covered by the camera per unit of mouse wheel rotation.")]
    public float ScrollSensitivity = 1.6f;
    private enum Mode
    {
        Build,
        Play
    }

    private Mode currentMode = Mode.Build;

    [Header("Build Mode")]
    [Tooltip("Current amount of gold for the player. This is the value the player should start with.")]
    public int Gold = 50;
    [Tooltip("Layer mask for raycasting. Should include the game area layer.")]
    public LayerMask StageLayerMask;
    [Tooltip("Reference to the Transform component of the highlighter element.")]
    public Transform Highlighter;
    [Tooltip("Reference to the Tower Selling Panel element.")]
    public RectTransform TowerSellingPanel;
    [Tooltip("Reference to the Text component of the Refund Text in the Tower Selling Panel.")]
    public Text SellRefundText;
    [Tooltip("Reference to the Text component displaying the current amount of gold in the bottom left corner of the UI.")]
    public Text CurrentGoldText;
    [Tooltip("Color used for the selected build button.")]
    public Color SelectedBuildButtonColor = new Color(.2f, .8f, .2f);
    private Vector3 lastMousePosition;
    private int goldLastFrame;
    private bool cursorIsOverStage = false;
    private Tower towerPrefabToBuild = null;
    private Image selectedBuildButtonImage = null;
    private Tower selectedTower = null;
    private Dictionary<Vector3, Tower> towers = new Dictionary<Vector3, Tower>();

    [Header("Play Mode")]
    [Tooltip("Reference to the build button panel to deactivate it when play mode starts.")]
    public GameObject BuildButtonPanel;
    [Tooltip("Reference to the Game Lost Panel element.")]
    public GameObject GameLostPanel;
    public GameObject ExitGamePanel;
    [Tooltip("Reference to the Text component for the info text in the Game Lost Panel.")]
    public Text GameLostPanelInfoText;
    [Tooltip("Reference to the Play Button to deactivate it in play mode.")]
    public GameObject PlayButton;
    [Tooltip("Reference to the Transform component of the Enemy Holder element.")]
    public Transform EnemyHolder;
    [Tooltip("Reference to the Ground Enemy prefab.")]
    public Enemy GroundEnemyPrefab;
    [Tooltip("Reference to the Flying Enemy prefab.")]
    public Enemy FlyingEnemyPrefab;
    [Tooltip("Time in seconds between spawning consecutive Enemies.")]
    public float EnemySpawnRate = .35f;
    [Tooltip("Specifies how often a level with flying Enemies appears. For example, if set to 4, every fourth level is a flying enemy level.")]
    public int FlyingLevelInterval = 4;
    [Tooltip("Number of Enemies spawned in each level.")]
    public int EnemiesPerLevel = 15;
    [Tooltip("Gold awarded to the player at the end of each level.")]
    public int GoldRewardPerLevel = 12;
    public static int Level = 1;

    private int enemiesSpawnedThisLevel = 0;

    public static int RemainingLives = 40;


    public static int RemainingLives = 40;



    void ArrowKeyMovement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            targetPosition.z += ArrowKeySpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            targetPosition.z -= ArrowKeySpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            targetPosition.x += ArrowKeySpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            targetPosition.x -= ArrowKeySpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            targetPosition.z -= ArrowKeySpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            targetPosition.x += ArrowKeySpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {

            targetPosition.x -= ArrowKeySpeed * Time.deltaTime;
        }
    }
    void MouseDragMovement()
    {
        if (Input.GetMouseButton(1))
        {

            Vector3 movement = new Vector3(-Input.GetAxis("Mouse X"), 0, -Input.GetAxis("Mouse Y")) * MouseDragSensitivity;

            if (movement != Vector3.zero)
            {
                targetPosition += movement;
            }
        }
    }
    void Zooming()
    {
        float scrollDelta = -Input.mouseScrollDelta.y;
        if (scrollDelta != 0)
        {
            targetPosition.y += scrollDelta * ScrollSensitivity;
        }
    }
    void MoveTowardsTarget()
    {
        targetPosition.x = Mathf.Clamp(targetPosition.x, MinimumX, MaximumX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, MinimumX, MaximumX);
        targetPosition.z = Mathf.Clamp(targetPosition.z, MinimumX, MaximumX);
        if (Trans.position != targetPosition)

        {
            Trans.position = Vector3.Lerp(Trans.position, targetPosition, 1 - MovementSmoothing);
        if (Trans.position != targetPosition)
        {
            Trans.position = Vector3.Lerp(Trans.position, targetPosition, 1 - MovementSmoothing);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        targetPosition = Trans.position;
        GroundEnemy.Path = new UnityEngine.AI.NavMeshPath();

        UpdateEnemyPath();

    }

    // Update is called once per frame
    void Update()
    {
        ArrowKeyMovement();
        MouseDragMovement();
        Zooming();
        MoveTowardsTarget();
        if (currentMode == Mode.Build)
        {
            BuildModeLogic();
        }
        else
        {
            PlayModeLogic();
        }
    }

    private void PlayModeLogic()
    {
        if (EnemyHolder.childCount == 0 && enemiesSpawnedThisLevel >= EnemiesPerLevel)
        {
            if (RemainingLives > 0)
            {
                GoToBuildMode();
            }
            else
            {
                GameLostPanelInfoText.text = "You had made it to level " + Level + ".";
                GameLostPanel.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscape();
        }

    }

    private void GoToBuildMode()
    {
        currentMode = Mode.Build;
        BuildButtonPanel.SetActive(true);
        PlayButton.SetActive(true);
        enemiesSpawnedThisLevel = 0;
        Level += 1;
        Gold += GoldRewardPerLevel;

    }
    public void StartLevel()
    {
        GoToPlayMode();
        InvokeRepeating("SpawnEnemy", .5f, EnemySpawnRate);
    }
    void GoToPlayMode()
    {
        currentMode = Mode.Play;
        BuildButtonPanel.SetActive(false);
        PlayButton.SetActive(false);
        Highlighter.gameObject.SetActive(false);
    }
    void BuildModeLogic()
    {
        PositionHighlighter();
        PositionSellPanel();
        UpdateCurrentGold();
        if (cursorIsOverStage && Input.GetMouseButtonDown(0))
        {
            OnStageClicked();
        }
        if (Input.GetMouseButtonDown(1))
        {
            DeselectTower();
            DeselectBuildButton();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscape();
        }
    }
    private void OnEscape()
{
        if (ExitGamePanel.activeSelf)
        {
            Time.timeScale = 1;
            ExitGamePanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            ExitGamePanel.SetActive(true);
        }


    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    private void DeselectBuildButton()
    {
        towerPrefabToBuild = null;
        if (selectedBuildButtonImage != null)
        {
            selectedBuildButtonImage.color = Color.white;
            selectedBuildButtonImage = null;
        }
    }

    public void DeselectTower()
    {
        selectedTower = null;
        TowerSellingPanel.gameObject.SetActive(false);
    }

    private void OnStageClicked()
    {
        if (towerPrefabToBuild != null)
        {
            if (!towers.ContainsKey(Highlighter.position) && Gold >= towerPrefabToBuild.GoldCost && !EventSystem.current.IsPointerOverGameObject())
            {
                BuildTower(towerPrefabToBuild, Highlighter.position);
            }
        }
        else
        {
            if (towers.ContainsKey(Highlighter.position))
            {
                selectedTower = towers[Highlighter.position];
                SellRefundText.text = "for " + Mathf.CeilToInt(selectedTower.GoldCost * selectedTower.RefundFactor) + " gold";
                TowerSellingPanel.gameObject.SetActive(true);
            }
        }
    }

    private void BuildTower(Tower prefab, Vector3 position)
    {
        towers[position] = Instantiate<Tower>(prefab, position, Quaternion.identity);
        Gold -= towerPrefabToBuild.GoldCost;
        UpdateEnemyPath();
    }

    private void UpdateEnemyPath()
    {
        Invoke("PerformPathfinding", .1f);
    }
    private void PerformPathfinding()
    {
        UnityEngine.AI.NavMesh.CalculatePath(SpawnPoint.position, LeakPoint.position, UnityEngine.AI.NavMesh.AllAreas, GroundEnemy.Path);
        if (GroundEnemy.Path.status == UnityEngine.AI.NavMeshPathStatus.PathComplete)
        {
            PlayButtonLockPanel.SetActive(false);
        }
        else
        {
            PlayButtonLockPanel.SetActive(true);
        }
    }

    private void UpdateCurrentGold()
    {
        if (Gold != goldLastFrame)
        {
            CurrentGoldText.text = Gold + " gold";
            goldLastFrame = Gold;
        }
    }

    private void PositionSellPanel()
    {
        if (selectedTower != null)
        {
            var screenPosition = Camera.main.WorldToScreenPoint(selectedTower.transform.position + Vector3.forward * 8);
            TowerSellingPanel.position = screenPosition;
        }
    }

    private void PositionHighlighter()
    {
        if (Input.mousePosition != lastMousePosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, StageLayerMask.value))
            {
                Vector3 point = hit.point;
                point.x = Mathf.Round(hit.point.x * .1f) * 10;
                point.z = Mathf.Round(hit.point.z * .1f) * 10;
                point.z = Mathf.Clamp(point.z, -80, 80);
                point.y = .2f;
                Highlighter.position = point;
                Highlighter.gameObject.SetActive(true);
                cursorIsOverStage = true;
            }
            else
            {
                cursorIsOverStage = false;
                Highlighter.gameObject.SetActive(false);
            }
        }
        lastMousePosition = Input.mousePosition;
    }
    public void OnBuildButtonClicked(Tower associatedTower)
    {
        if (towerPrefabToBuild != null)
        {
            DeselectBuildButton();
        }
        towerPrefabToBuild = associatedTower;
        DeselectTower();
    }
    public void SetSelectedBuildButton(Image clickedButtonImage)
    {
        selectedBuildButtonImage = clickedButtonImage;
        clickedButtonImage.color = SelectedBuildButtonColor;
    }
    public void OnSellTowerButtonClicked()
    {
        if (selectedTower != null)
        {
            SellTower(selectedTower);
        }
    }

    private void SellTower(Tower tower)
    {
        DeselectTower();
        Gold += Mathf.CeilToInt(tower.GoldCost * tower.RefundFactor);
        towers.Remove(tower.transform.position);
        Destroy(tower.gameObject);
        UpdateEnemyPath();
    }
    private void SpawnEnemy()
    {
        Enemy enemy = null;

        if (Level % FlyingLevelInterval == 0)
        {
            enemy = Instantiate(FlyingEnemyPrefab, SpawnPoint.position + (Vector3.up * 18), Quaternion.LookRotation(Vector3.back));

        }
        else
        {
            enemy = Instantiate(GroundEnemyPrefab, SpawnPoint.position, Quaternion.LookRotation(Vector3.back));
        }
        enemy.Trans.SetParent(EnemyHolder);
        enemiesSpawnedThisLevel += 1;
        if (enemiesSpawnedThisLevel >= EnemiesPerLevel)
        {
            CancelInvoke("SpawnEnemy");
        }
    }
    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }

}
