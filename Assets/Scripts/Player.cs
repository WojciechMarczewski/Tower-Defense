using Assets.Scripts;
<<<<<<< HEAD
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

=======
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [Header("References")]
    public Transform trans;
    public Transform spawnPoint;
    public Transform leakPoint;
    [Tooltip("Odwo³anie do elementu GameObject panelu blokuj¹cego przycisk Play.")]
    public GameObject playButtonLockPanel;
    [Header("X Bounds")]
    public float minimumX = -70;
    public float maximumX = 70;
    [Header("Y Bounds")]
    public float minimumY = 18;
    public float maximumY = 80;
    [Header("Z Bounds")]
    public float minimumZ = -130;
    public float maximumZ = 70;
    [Header("Movement")]
    [Tooltip("Odleg³oœæ pokonywana w ci¹gu sekundy ruchu wywo³anego przez klawisze ze strza³kami.")]
    public float arrowKeySpeed = 80;
    [Tooltip("Mno¿nik dla ruchu przez przeci¹ganie mysz¹. Wy¿sza wartoœæ spowoduje ruch kamery na wiêksz¹ odleg³oœæ przy ruchu mysz¹.")]
    public float mouseDragSensitivity = 2.8f;
    [Tooltip("Wielkoœæ wyg³adzania stosowana wobec ruchu kamery. Powinna byæ wartoœci¹ pomiêdzy 0 a 1.")]
    [Range(0, .99f)]
    public float movementSmoothing = .75f;
    private Vector3 targetPosition;
    [Header("Scrolling")]
    [Tooltip("Wielkoœæ odleg³oœci Y pokonywanej przez kamerê przy jednostce obrotu kó³ka myszy.")]
    public float scrollSensitivity = 1.6f;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
    private enum Mode
    {
        Build,
        Play
    }
<<<<<<< HEAD

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

=======
    private Mode mode = Mode.Build;
    [Header("Build Mode")]
    [Tooltip("Bie¿¹ca iloœæ z³ota dla gracza. Jest to wartoœæ, od której gracz powinien rozpocz¹æ.")]
    public int gold = 50;
    [Tooltip("Maska warstw dla rzutowania promienia. Powinna obejmowaæ warstwê z obszarem gry.")]
    public LayerMask stageLayerMask;
    [Tooltip("Odwo³anie do sk³adnika Transform elementu highlighter")]
    public Transform highlighter;
    [Tooltip("Odwo³anie do elementu Tower Selling Panel.")]
    public RectTransform towerSellingPanel;
    [Tooltip("Odwo³anie do sk³adnika Text elementu Refund Text w Tower Selling Panel.")]
    public Text sellRefundText;
    [Tooltip("Odwo³anie do sk³adnika Text wyœwietlaj¹cego aktualn¹ iloœæ z³ota w lewym dolnym rogu interfejsu u¿ytkownika.")]
    public Text currentGoldText;
    [Tooltip("Kolor u¿ywany wobec zaznaczonego przycisku budowania.")]
    public Color selectedBuildButtonColor = new Color(.2f, .8f, .2f);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
    private Vector3 lastMousePosition;
    private int goldLastFrame;
    private bool cursorIsOverStage = false;
    private Tower towerPrefabToBuild = null;
    private Image selectedBuildButtonImage = null;
    private Tower selectedTower = null;
    private Dictionary<Vector3, Tower> towers = new Dictionary<Vector3, Tower>();
<<<<<<< HEAD

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

=======
    [Header("Play Mode")]
    [Tooltip("Odwo³aniedo elementu build button panel, aby go zdezaktywowaæ, gdy rozpoczyna siê tryb gry.")]
    public GameObject buildButtonPanel;
    [Tooltip("Odwo³anie do elementu Game Lost Panel.")]
    public GameObject gameLostPanel;
    public GameObject exitGamePanel;
    [Tooltip("Odwo³anie do sk³adnika Text dla tekstu informacyjnego w panel Game Lost Panel.")]
    public Text gameLostPanelInfoText;
    [Tooltip("Odwo³anie do przycisku Play Button, aby zdezaktywowaæ go w trybie gry.")]
    public GameObject playButton;
    [Tooltip("Odwo³anie do sk³adnika Transform elementu Enemy Holder.")]
    public Transform enemyHolder;
    [Tooltip("Odwo³anie do prefabrykatu Ground Enemy.")]
    public Enemy groundEnemyPrefab;
    [Tooltip("Odwo³anie do prefabrykatu Flying Enemy.")]
    public Enemy flyingEnemyPrefab;
    [Tooltip("Czas w sekundach pomiêdzy wygenerowaniami kolejnych wrogów.")]
    public float enemySpawnRate = .35f;
    [Tooltip("Okreœla, jak czêsto pojawia siê poziom z wrogami lataj¹cymi. Na przyk³ad, jeœli bêdzie to ustawione na 4, to co czwarty poziom jest poziomem z wrogami lataja¹cymi.")]
    public int flyingLevelInterval = 4;
    [Tooltip("Liczba wrogów generowanych w ka¿dym poziomie.")]
    public int enemiesPerLevel = 15;
    [Tooltip("Z³oto przyznawane graczowi na koñcu ka¿dego poziomu.")]
    public int goldRewardPerLevel = 12;
    public static int level = 1;
    private int enemiesSpawnedThisLevel = 0;
    public static int remainingLives = 40;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f



    void ArrowKeyMovement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
<<<<<<< HEAD
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
=======
            targetPosition.z += arrowKeySpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            targetPosition.z -= arrowKeySpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            targetPosition.x += arrowKeySpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            targetPosition.x -= arrowKeySpeed * Time.deltaTime;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        }
    }
    void MouseDragMovement()
    {
        if (Input.GetMouseButton(1))
        {
<<<<<<< HEAD
            Vector3 movement = new Vector3(-Input.GetAxis("Mouse X"), 0, -Input.GetAxis("Mouse Y")) * MouseDragSensitivity;
=======
            Vector3 movement = new Vector3(-Input.GetAxis("Mouse X"), 0, -Input.GetAxis("Mouse Y")) * mouseDragSensitivity;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
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
<<<<<<< HEAD
            targetPosition.y += scrollDelta * ScrollSensitivity;
=======
            targetPosition.y += scrollDelta * scrollSensitivity;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        }
    }
    void MoveTowardsTarget()
    {
<<<<<<< HEAD
        targetPosition.x = Mathf.Clamp(targetPosition.x, MinimumX, MaximumX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, MinimumX, MaximumX);
        targetPosition.z = Mathf.Clamp(targetPosition.z, MinimumX, MaximumX);
        if (Trans.position != targetPosition)
        {
            Trans.position = Vector3.Lerp(Trans.position, targetPosition, 1 - MovementSmoothing);
=======
        targetPosition.x = Mathf.Clamp(targetPosition.x, minimumX, maximumX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minimumY, maximumY);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minimumZ, maximumZ);
        if (trans.position != targetPosition)
        {
            trans.position = Vector3.Lerp(trans.position, targetPosition, 1 - movementSmoothing);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        }
    }
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        targetPosition = Trans.position;
        GroundEnemy.Path = new UnityEngine.AI.NavMeshPath();
=======
        targetPosition = trans.position;
        GroundEnemy.path = new NavMeshPath();
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        UpdateEnemyPath();

    }

    // Update is called once per frame
    void Update()
    {
        ArrowKeyMovement();
        MouseDragMovement();
        Zooming();
        MoveTowardsTarget();
<<<<<<< HEAD
        if (currentMode == Mode.Build)
=======
        if (mode == Mode.Build)
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
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
<<<<<<< HEAD
        if (EnemyHolder.childCount == 0 && enemiesSpawnedThisLevel >= EnemiesPerLevel)
        {
            if (RemainingLives > 0)
=======
        if (enemyHolder.childCount == 0 && enemiesSpawnedThisLevel >= enemiesPerLevel)
        {
            if (remainingLives > 0)
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
            {
                GoToBuildMode();
            }
            else
            {
<<<<<<< HEAD
                GameLostPanelInfoText.text = "You had made it to level " + Level + ".";
                GameLostPanel.SetActive(true);
=======
                gameLostPanelInfoText.text = "You had made it to level " + level + ".";
                gameLostPanel.SetActive(true);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscape();
        }

    }

    private void GoToBuildMode()
    {
<<<<<<< HEAD
        currentMode = Mode.Build;
        BuildButtonPanel.SetActive(true);
        PlayButton.SetActive(true);
        enemiesSpawnedThisLevel = 0;
        Level += 1;
        Gold += GoldRewardPerLevel;
=======
        mode = Mode.Build;
        buildButtonPanel.SetActive(true);
        playButton.SetActive(true);
        enemiesSpawnedThisLevel = 0;
        level += 1;
        gold += goldRewardPerLevel;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f

    }
    public void StartLevel()
    {
        GoToPlayMode();
<<<<<<< HEAD
        InvokeRepeating("SpawnEnemy", .5f, EnemySpawnRate);
    }
    void GoToPlayMode()
    {
        currentMode = Mode.Play;
        BuildButtonPanel.SetActive(false);
        PlayButton.SetActive(false);
        Highlighter.gameObject.SetActive(false);
=======
        InvokeRepeating("SpawnEnemy", .5f, enemySpawnRate);
    }
    void GoToPlayMode()
    {
        mode = Mode.Play;
        buildButtonPanel.SetActive(false);
        playButton.SetActive(false);
        highlighter.gameObject.SetActive(false);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
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
<<<<<<< HEAD
        if (ExitGamePanel.activeSelf)
        {
            Time.timeScale = 1;
            ExitGamePanel.SetActive(false);
=======
        if (exitGamePanel.activeSelf)
        {
            Time.timeScale = 1;
            exitGamePanel.SetActive(false);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        }
        else
        {
            Time.timeScale = 0;
<<<<<<< HEAD
            ExitGamePanel.SetActive(true);
=======
            exitGamePanel.SetActive(true);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
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
<<<<<<< HEAD
        TowerSellingPanel.gameObject.SetActive(false);
=======
        towerSellingPanel.gameObject.SetActive(false);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
    }

    private void OnStageClicked()
    {
        if (towerPrefabToBuild != null)
        {
<<<<<<< HEAD
            if (!towers.ContainsKey(Highlighter.position) && Gold >= towerPrefabToBuild.GoldCost && !EventSystem.current.IsPointerOverGameObject())
            {
                BuildTower(towerPrefabToBuild, Highlighter.position);
=======
            if (!towers.ContainsKey(highlighter.position) && gold >= towerPrefabToBuild.goldCost && !EventSystem.current.IsPointerOverGameObject())
            {
                BuildTower(towerPrefabToBuild, highlighter.position);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
            }
        }
        else
        {
<<<<<<< HEAD
            if (towers.ContainsKey(Highlighter.position))
            {
                selectedTower = towers[Highlighter.position];
                SellRefundText.text = "for " + Mathf.CeilToInt(selectedTower.GoldCost * selectedTower.RefundFactor) + " gold";
                TowerSellingPanel.gameObject.SetActive(true);
=======
            if (towers.ContainsKey(highlighter.position))
            {
                selectedTower = towers[highlighter.position];
                sellRefundText.text = "for " + Mathf.CeilToInt(selectedTower.goldCost * selectedTower.refundFactor) + " gold";
                towerSellingPanel.gameObject.SetActive(true);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
            }
        }
    }

    private void BuildTower(Tower prefab, Vector3 position)
    {
        towers[position] = Instantiate<Tower>(prefab, position, Quaternion.identity);
<<<<<<< HEAD
        Gold -= towerPrefabToBuild.GoldCost;
=======
        gold -= towerPrefabToBuild.goldCost;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        UpdateEnemyPath();
    }

    private void UpdateEnemyPath()
    {
        Invoke("PerformPathfinding", .1f);
    }
    private void PerformPathfinding()
    {
<<<<<<< HEAD
        UnityEngine.AI.NavMesh.CalculatePath(SpawnPoint.position, LeakPoint.position, UnityEngine.AI.NavMesh.AllAreas, GroundEnemy.Path);
        if (GroundEnemy.Path.status == UnityEngine.AI.NavMeshPathStatus.PathComplete)
        {
            PlayButtonLockPanel.SetActive(false);
        }
        else
        {
            PlayButtonLockPanel.SetActive(true);
=======
        NavMesh.CalculatePath(spawnPoint.position, leakPoint.position, NavMesh.AllAreas, GroundEnemy.path);
        if (GroundEnemy.path.status == NavMeshPathStatus.PathComplete)
        {
            playButtonLockPanel.SetActive(false);
        }
        else
        {
            playButtonLockPanel.SetActive(true);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        }
    }

    private void UpdateCurrentGold()
    {
<<<<<<< HEAD
        if (Gold != goldLastFrame)
        {
            CurrentGoldText.text = Gold + " gold";
            goldLastFrame = Gold;
=======
        if (gold != goldLastFrame)
        {
            currentGoldText.text = gold + " gold";
            goldLastFrame = gold;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        }
    }

    private void PositionSellPanel()
    {
        if (selectedTower != null)
        {
            var screenPosition = Camera.main.WorldToScreenPoint(selectedTower.transform.position + Vector3.forward * 8);
<<<<<<< HEAD
            TowerSellingPanel.position = screenPosition;
=======
            towerSellingPanel.position = screenPosition;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        }
    }

    private void PositionHighlighter()
    {
        if (Input.mousePosition != lastMousePosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
<<<<<<< HEAD
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, StageLayerMask.value))
=======
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, stageLayerMask.value))
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
            {
                Vector3 point = hit.point;
                point.x = Mathf.Round(hit.point.x * .1f) * 10;
                point.z = Mathf.Round(hit.point.z * .1f) * 10;
                point.z = Mathf.Clamp(point.z, -80, 80);
                point.y = .2f;
<<<<<<< HEAD
                Highlighter.position = point;
                Highlighter.gameObject.SetActive(true);
=======
                highlighter.position = point;
                highlighter.gameObject.SetActive(true);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
                cursorIsOverStage = true;
            }
            else
            {
                cursorIsOverStage = false;
<<<<<<< HEAD
                Highlighter.gameObject.SetActive(false);
=======
                highlighter.gameObject.SetActive(false);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
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
<<<<<<< HEAD
        clickedButtonImage.color = SelectedBuildButtonColor;
=======
        clickedButtonImage.color = selectedBuildButtonColor;
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
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
<<<<<<< HEAD
        Gold += Mathf.CeilToInt(tower.GoldCost * tower.RefundFactor);
=======
        gold += Mathf.CeilToInt(tower.goldCost * tower.refundFactor);
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        towers.Remove(tower.transform.position);
        Destroy(tower.gameObject);
        UpdateEnemyPath();
    }
    private void SpawnEnemy()
    {
        Enemy enemy = null;

<<<<<<< HEAD
        if (Level % FlyingLevelInterval == 0)
        {
            enemy = Instantiate(FlyingEnemyPrefab, SpawnPoint.position + (Vector3.up * 18), Quaternion.LookRotation(Vector3.back));
=======
        if (level % flyingLevelInterval == 0)
        {
            enemy = Instantiate(flyingEnemyPrefab, spawnPoint.position + (Vector3.up * 18), Quaternion.LookRotation(Vector3.back));
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f

        }
        else
        {
<<<<<<< HEAD
            enemy = Instantiate(GroundEnemyPrefab, SpawnPoint.position, Quaternion.LookRotation(Vector3.back));
        }
        enemy.Trans.SetParent(EnemyHolder);
        enemiesSpawnedThisLevel += 1;

        if (enemiesSpawnedThisLevel >= EnemiesPerLevel)
=======
            enemy = Instantiate(groundEnemyPrefab, spawnPoint.position, Quaternion.LookRotation(Vector3.back));
        }
        enemy.trans.SetParent(enemyHolder);
        enemiesSpawnedThisLevel += 1;

        if (enemiesSpawnedThisLevel >= enemiesPerLevel)
>>>>>>> 0a223684d01e66273f07a98baa2aafaf5a43148f
        {
            CancelInvoke("SpawnEnemy");
        }
    }
    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }

}
