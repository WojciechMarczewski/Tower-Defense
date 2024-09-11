using Assets.Scripts;
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
    [Tooltip("Odwo�anie do elementu GameObject panelu blokuj�cego przycisk Play.")]
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
    [Tooltip("Odleg�o�� pokonywana w ci�gu sekundy ruchu wywo�anego przez klawisze ze strza�kami.")]
    public float arrowKeySpeed = 80;
    [Tooltip("Mno�nik dla ruchu przez przeci�ganie mysz�. Wy�sza warto�� spowoduje ruch kamery na wi�ksz� odleg�o�� przy ruchu mysz�.")]
    public float mouseDragSensitivity = 2.8f;
    [Tooltip("Wielko�� wyg�adzania stosowana wobec ruchu kamery. Powinna by� warto�ci� pomi�dzy 0 a 1.")]
    [Range(0, .99f)]
    public float movementSmoothing = .75f;
    private Vector3 targetPosition;
    [Header("Scrolling")]
    [Tooltip("Wielko�� odleg�o�ci Y pokonywanej przez kamer� przy jednostce obrotu k�ka myszy.")]
    public float scrollSensitivity = 1.6f;
    private enum Mode
    {
        Build,
        Play
    }
    private Mode mode = Mode.Build;
    [Header("Build Mode")]
    [Tooltip("Bie��ca ilo�� z�ota dla gracza. Jest to warto��, od kt�rej gracz powinien rozpocz��.")]
    public int gold = 50;
    [Tooltip("Maska warstw dla rzutowania promienia. Powinna obejmowa� warstw� z obszarem gry.")]
    public LayerMask stageLayerMask;
    [Tooltip("Odwo�anie do sk�adnika Transform elementu highlighter")]
    public Transform highlighter;
    [Tooltip("Odwo�anie do elementu Tower Selling Panel.")]
    public RectTransform towerSellingPanel;
    [Tooltip("Odwo�anie do sk�adnika Text elementu Refund Text w Tower Selling Panel.")]
    public Text sellRefundText;
    [Tooltip("Odwo�anie do sk�adnika Text wy�wietlaj�cego aktualn� ilo�� z�ota w lewym dolnym rogu interfejsu u�ytkownika.")]
    public Text currentGoldText;
    [Tooltip("Kolor u�ywany wobec zaznaczonego przycisku budowania.")]
    public Color selectedBuildButtonColor = new Color(.2f, .8f, .2f);
    private Vector3 lastMousePosition;
    private int goldLastFrame;
    private bool cursorIsOverStage = false;
    private Tower towerPrefabToBuild = null;
    private Image selectedBuildButtonImage = null;
    private Tower selectedTower = null;
    private Dictionary<Vector3, Tower> towers = new Dictionary<Vector3, Tower>();
    [Header("Play Mode")]
    [Tooltip("Odwo�aniedo elementu build button panel, aby go zdezaktywowa�, gdy rozpoczyna si� tryb gry.")]
    public GameObject buildButtonPanel;
    [Tooltip("Odwo�anie do elementu Game Lost Panel.")]
    public GameObject gameLostPanel;
    public GameObject exitGamePanel;
    [Tooltip("Odwo�anie do sk�adnika Text dla tekstu informacyjnego w panel Game Lost Panel.")]
    public Text gameLostPanelInfoText;
    [Tooltip("Odwo�anie do przycisku Play Button, aby zdezaktywowa� go w trybie gry.")]
    public GameObject playButton;
    [Tooltip("Odwo�anie do sk�adnika Transform elementu Enemy Holder.")]
    public Transform enemyHolder;
    [Tooltip("Odwo�anie do prefabrykatu Ground Enemy.")]
    public Enemy groundEnemyPrefab;
    [Tooltip("Odwo�anie do prefabrykatu Flying Enemy.")]
    public Enemy flyingEnemyPrefab;
    [Tooltip("Czas w sekundach pomi�dzy wygenerowaniami kolejnych wrog�w.")]
    public float enemySpawnRate = .35f;
    [Tooltip("Okre�la, jak cz�sto pojawia si� poziom z wrogami lataj�cymi. Na przyk�ad, je�li b�dzie to ustawione na 4, to co czwarty poziom jest poziomem z wrogami lataja�cymi.")]
    public int flyingLevelInterval = 4;
    [Tooltip("Liczba wrog�w generowanych w ka�dym poziomie.")]
    public int enemiesPerLevel = 15;
    [Tooltip("Z�oto przyznawane graczowi na ko�cu ka�dego poziomu.")]
    public int goldRewardPerLevel = 12;
    public static int level = 1;
    private int enemiesSpawnedThisLevel = 0;
    public static int remainingLives = 40;



    void ArrowKeyMovement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
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
        }
    }
    void MouseDragMovement()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 movement = new Vector3(-Input.GetAxis("Mouse X"), 0, -Input.GetAxis("Mouse Y")) * mouseDragSensitivity;
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
            targetPosition.y += scrollDelta * scrollSensitivity;
        }
    }
    void MoveTowardsTarget()
    {
        targetPosition.x = Mathf.Clamp(targetPosition.x, minimumX, maximumX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minimumY, maximumY);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minimumZ, maximumZ);
        if (trans.position != targetPosition)
        {
            trans.position = Vector3.Lerp(trans.position, targetPosition, 1 - movementSmoothing);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = trans.position;
        GroundEnemy.path = new NavMeshPath();
        UpdateEnemyPath();

    }

    // Update is called once per frame
    void Update()
    {
        ArrowKeyMovement();
        MouseDragMovement();
        Zooming();
        MoveTowardsTarget();
        if (mode == Mode.Build)
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
        if (enemyHolder.childCount == 0 && enemiesSpawnedThisLevel >= enemiesPerLevel)
        {
            if (remainingLives > 0)
            {
                GoToBuildMode();
            }
            else
            {
                gameLostPanelInfoText.text = "You had made it to level " + level + ".";
                gameLostPanel.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscape();
        }

    }

    private void GoToBuildMode()
    {
        mode = Mode.Build;
        buildButtonPanel.SetActive(true);
        playButton.SetActive(true);
        enemiesSpawnedThisLevel = 0;
        level += 1;
        gold += goldRewardPerLevel;

    }
    public void StartLevel()
    {
        GoToPlayMode();
        InvokeRepeating("SpawnEnemy", .5f, enemySpawnRate);
    }
    void GoToPlayMode()
    {
        mode = Mode.Play;
        buildButtonPanel.SetActive(false);
        playButton.SetActive(false);
        highlighter.gameObject.SetActive(false);
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
        if (exitGamePanel.activeSelf)
        {
            Time.timeScale = 1;
            exitGamePanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            exitGamePanel.SetActive(true);
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
        towerSellingPanel.gameObject.SetActive(false);
    }

    private void OnStageClicked()
    {
        if (towerPrefabToBuild != null)
        {
            if (!towers.ContainsKey(highlighter.position) && gold >= towerPrefabToBuild.goldCost && !EventSystem.current.IsPointerOverGameObject())
            {
                BuildTower(towerPrefabToBuild, highlighter.position);
            }
        }
        else
        {
            if (towers.ContainsKey(highlighter.position))
            {
                selectedTower = towers[highlighter.position];
                sellRefundText.text = "for " + Mathf.CeilToInt(selectedTower.goldCost * selectedTower.refundFactor) + " gold";
                towerSellingPanel.gameObject.SetActive(true);
            }
        }
    }

    private void BuildTower(Tower prefab, Vector3 position)
    {
        towers[position] = Instantiate<Tower>(prefab, position, Quaternion.identity);
        gold -= towerPrefabToBuild.goldCost;
        UpdateEnemyPath();
    }

    private void UpdateEnemyPath()
    {
        Invoke("PerformPathfinding", .1f);
    }
    private void PerformPathfinding()
    {
        NavMesh.CalculatePath(spawnPoint.position, leakPoint.position, NavMesh.AllAreas, GroundEnemy.path);
        if (GroundEnemy.path.status == NavMeshPathStatus.PathComplete)
        {
            playButtonLockPanel.SetActive(false);
        }
        else
        {
            playButtonLockPanel.SetActive(true);
        }
    }

    private void UpdateCurrentGold()
    {
        if (gold != goldLastFrame)
        {
            currentGoldText.text = gold + " gold";
            goldLastFrame = gold;
        }
    }

    private void PositionSellPanel()
    {
        if (selectedTower != null)
        {
            var screenPosition = Camera.main.WorldToScreenPoint(selectedTower.transform.position + Vector3.forward * 8);
            towerSellingPanel.position = screenPosition;
        }
    }

    private void PositionHighlighter()
    {
        if (Input.mousePosition != lastMousePosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, stageLayerMask.value))
            {
                Vector3 point = hit.point;
                point.x = Mathf.Round(hit.point.x * .1f) * 10;
                point.z = Mathf.Round(hit.point.z * .1f) * 10;
                point.z = Mathf.Clamp(point.z, -80, 80);
                point.y = .2f;
                highlighter.position = point;
                highlighter.gameObject.SetActive(true);
                cursorIsOverStage = true;
            }
            else
            {
                cursorIsOverStage = false;
                highlighter.gameObject.SetActive(false);
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
        clickedButtonImage.color = selectedBuildButtonColor;
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
        gold += Mathf.CeilToInt(tower.goldCost * tower.refundFactor);
        towers.Remove(tower.transform.position);
        Destroy(tower.gameObject);
        UpdateEnemyPath();
    }
    private void SpawnEnemy()
    {
        Enemy enemy = null;

        if (level % flyingLevelInterval == 0)
        {
            enemy = Instantiate(flyingEnemyPrefab, spawnPoint.position + (Vector3.up * 18), Quaternion.LookRotation(Vector3.back));

        }
        else
        {
            enemy = Instantiate(groundEnemyPrefab, spawnPoint.position, Quaternion.LookRotation(Vector3.back));
        }
        enemy.trans.SetParent(enemyHolder);
        enemiesSpawnedThisLevel += 1;

        if (enemiesSpawnedThisLevel >= enemiesPerLevel)
        {
            CancelInvoke("SpawnEnemy");
        }
    }
    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }

}
