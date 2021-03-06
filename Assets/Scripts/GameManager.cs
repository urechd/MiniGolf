using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _ui;
    [SerializeField] private GameObject _pauseUI;

    [SerializeField] private Transform _player;

    private BallController _ballController;
    private PanZoom _panZoom;

    private int _levelBuildOffset = 1;

    private void Start()
    {
        DisableUI();

        _panZoom = Camera.main.GetComponent<PanZoom>();

        if (_player != null)
        {
            _ballController = _player.GetComponent<BallController>();

            Goal.scoreDelegate += EndLevel;
        }
    }

    private void OnDisable()
    {
        Goal.scoreDelegate -= EndLevel;
    }

    public void GoToHome()
    {
        LoadLevel(0);
    }

    public void GoToLevelSelect()
    {
        LoadLevel(1);
    }

    public void RestartLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EndLevel()
    {
        EnableUI(false);

        int levelNumber = SceneManager.GetActiveScene().buildIndex - _levelBuildOffset;
        if (!GameState.IsLevelComplete(levelNumber))
        {
            GameState.SetLevelComplete(levelNumber);
        }
    }

    public void EnableUI(bool isPause)
    {
        SetUI(true, isPause);
    }

    public void DisableUI()
    {
        SetUI(false);
    }

    private void SetUI(bool showUI, bool isPause = false)
    {
        if (_panZoom != null)
        {
            _panZoom.enabled = !showUI;
        }

        if (_ballController != null)
        {
            _ballController.enabled = !showUI;
        }

        if (_pauseUI != null)
        {
            _pauseUI.SetActive(showUI && isPause);
        }
        
        if (_ui != null)
        {
            _ui.SetActive(showUI && !isPause);
        }
    }
}
