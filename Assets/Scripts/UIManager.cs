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
    private Slider _fuelSlider;
    [SerializeField]
    private Image _fuelSliderFill;
    [SerializeField]
    private Text _overheatedText;
    [SerializeField]
    private Text _waveStartText;
    [SerializeField]
    private Text _finalWaveText;

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
        _overheatedText.gameObject.SetActive(false);
        _waveStartText.gameObject.SetActive(false);
        _finalWaveText.gameObject.SetActive(false);

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

    public void UpdateAmmo(int playerAmmo)
    {
        _ammoCountText.text = "Ammo Count: " + playerAmmo.ToString();

        if(playerAmmo == 0)
        {
            StartCoroutine(OutOfAmmoFlickerRoutine());

            Debug.Log("Coroutine Run");
        }
        else if(playerAmmo > 0)
        {
            StopCoroutine(OutOfAmmoFlickerRoutine());

            Debug.Log("Coroutine Stop");
        }
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

    public void UpdateFuel(float fuelValue)
    {
        if (fuelValue >= 13 && fuelValue <= 60)
        {
            _fuelSlider.value = fuelValue;
        }

        if(fuelValue < 13.0f)
        {
            _overheatedText.gameObject.SetActive(true);
        }
        else if(fuelValue > 30.0f)
        {
            _overheatedText.gameObject.SetActive(false);
        }
    }

    public void UpdateWave(int waveCount)
    {
        if(waveCount <= 6)
        {
            StartCoroutine(WaveStartTextRoutine());
        }
        else if(waveCount == 7)
        {
            StartCoroutine(FinalWaveRoutine());
        }
    }

    IEnumerator WaveStartTextRoutine()
    {
        while(true)
        {
            _waveStartText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            _waveStartText.gameObject.SetActive(false);
            yield return new WaitForSeconds(20.5f);
        }
    }

    IEnumerator FinalWaveRoutine()
    {
        while(true)
        {
            _finalWaveText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            _finalWaveText.gameObject.SetActive(false);
        }
    }
}
