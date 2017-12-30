using TMPro;
using UnityEngine;

public class MenuMaster : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private GameObject optionsPanel;

    void Start()
    {
        optionsPanel.SetActive(false);
    }

    public void Play()
    {
        sceneFader.FadeTo("MissionSelect");
    }

    public void Options()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OnHoverMenuItem(GameObject menuItem)
    {
        menuItem.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Underline;
    }

    public void OnExitHoverMenuItem(GameObject menuItem)
    {
        menuItem.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
    }
}