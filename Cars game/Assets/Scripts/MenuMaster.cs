using TMPro;
using UnityEngine;

public class MenuMaster : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;

    public void Play()
    {
        PhotonNetwork.offlineMode = true;
        sceneFader.FadeTo("MissionSelect");
    }

    public void Options()
    {
        Debug.Log("WIP");
        sceneFader.FadeTo("menu");
        //sceneFader.FadeTo("Options");
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