using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timerText;
    public Text gameStateText;
    public Text playerScoreText;
    public Text enemyScoreText;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject gunPrefab;
    public GameObject gameOverPanel;
    public Text winnerText;
    
    private float gameStartTime;
    private float switchRoleTime = 60f;
    private GameObject currentGun;
    private GameObject currentEnemy;
    private GameObject player;
    private int playerScore = 0;
    private int enemyScore = 0;
    
    void Start()
    {
        gameStartTime = Time.time;
        SetupInitialState();
        UpdateUI();
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }
    
    void SetupInitialState()
    {
        Vector3 centerPos = Vector2.zero;
        currentGun = Instantiate(gunPrefab, centerPos, Quaternion.identity);
        currentGun.GetComponent<GunController>().isPlayerControlled = true;
        
        Vector3 randomPos = GetRandomPositionInCamera();
        currentEnemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        currentEnemy.GetComponent<EnemyController>().isTarget = true;
    }
    
    void Update()
    {
        UpdateUI();
        
        if (Time.time - gameStartTime >= switchRoleTime && currentGun.GetComponent<GunController>().isPlayerControlled)
        {
            SwitchRoles();
        }
    }
    
    void SwitchRoles()
    {
        Destroy(currentEnemy);
        
        Vector3 gunPos = currentGun.transform.position;
        Destroy(currentGun);
        
        currentGun = Instantiate(gunPrefab, gunPos, Quaternion.identity);
        currentGun.GetComponent<GunController>().isPlayerControlled = false;
        
        Vector3 randomPos = GetRandomPositionInCamera();
        player = Instantiate(playerPrefab, randomPos, Quaternion.identity);
    }
    
    public Vector3 GetRandomPositionInCamera()
    {
        Camera mainCam = Camera.main;
        float height = mainCam.orthographicSize;
        float width = height * mainCam.aspect;
        float padding = 1f;
        
        return new Vector3(
            Random.Range(-width + padding, width - padding),
            Random.Range(-height + padding, height - padding),
            0
        );
    }
    
    public void AddPlayerScore()
    {
        playerScore++;
        UpdateUI();
    }
    
    public void AddEnemyScore()
    {
        enemyScore++;
        UpdateUI();
    }
    
    void UpdateUI()
    {
        float timeRemaining = switchRoleTime - (Time.time - gameStartTime);
        
        if (timeRemaining > 0)
        {
            timerText.text = $"Switch in: {timeRemaining:F1}s";
            gameStateText.text = "SHOOT THE ENEMY!";
        }
        else
        {
            timerText.text = "RUN!";
            gameStateText.text = "SURVIVE THE GUN!";
        }
        
        playerScoreText.text = $"Player Hits: {playerScore}";
        enemyScoreText.text = $"Enemy Hits: {enemyScore}";
    }
    
    public void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            string winner = playerScore > enemyScore ? "Player Wins!" : 
                          playerScore < enemyScore ? "Enemy Wins!" : 
                          "It's a Tie!";
            winnerText.text = $"{winner}\nPlayer Score: {playerScore}\nEnemy Score: {enemyScore}";
        }
        
        // Stop all gameplay
        Time.timeScale = 0;
    }
}