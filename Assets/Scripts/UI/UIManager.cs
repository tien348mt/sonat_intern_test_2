using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject UISoundSetting;

    public void TurnOffUISound()
    {
        UISoundSetting.SetActive(false);
    }
    public void TurnOnUISound()
    {
        UISoundSetting.SetActive(true);
    }
    public void login()
    {
        SceneManager.LoadScene("GameScene");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.moveBlock);
    }
    public void exitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Login");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.moveBlock);
    }
    public void RestartLevel()
    {
        GridManager.Instance.winUI.SetActive(false);
        GridManager.Instance.loseUI.SetActive(false);
        GridManager.Instance.isWin = false;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.moveBlock);
        GridManager.Instance.LoadLevel(GridManager.Instance.currentLevelIndex);
    }
}
