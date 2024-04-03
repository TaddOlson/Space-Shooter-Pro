using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Text _mainMenuText;
    [SerializeField]
    private Text _ammoCountText;
    [SerializeField]
    private Text _ammoDepletionText;
    [SerializeField]
    private Slider _thrusterFuel;
    [SerializeField]
    private Sprite[] _fuelSprites;


    private GameManager _gameManager;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _mainMenuText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("Player is NULL.");
        }

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL.");
        }

        _ammoCountText.text = "Ammo Count: " + 15;
        _ammoDepletionText.gameObject.SetActive(false);
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _livesSprites[currentLives];

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameManager.MainMenu();
        _restartText.gameObject.SetActive(true);
        _gameOverText.gameObject.SetActive(true);
        _mainMenuText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    public void UpdateShields(int currentShields)
    {

    }

    public void UpdateAmmo(int playerAmmo)
    {
        _ammoCountText.text = "Ammo Count: " + playerAmmo.ToString();
    }

    public void OutofAmmoFlickerRoutine()
    {
        StartCoroutine(OutOfAmmoFlickerRoutine());
    }

    IEnumerator OutOfAmmoFlickerRoutine()
    {
        while(true)
        {
            _ammoDepletionText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _ammoDepletionText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void UpdateFuel(float currentFuel)
    {

    }
}
