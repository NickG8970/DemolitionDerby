using UnityEngine;
using UnityEngine.UI;

public class MissionSelector : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private Button[] missionButtons;

    void Start()
    {
        int missionReached = PlayerPrefs.GetInt("missionReached", 1);
        for (int i = 0; i < missionButtons.Length; i++)
        {
            if ((i + 1) > missionReached)
            {
                missionButtons[i].interactable = false;
            }
        }
    }

    public void SelectMission(string missionName)
    {
        sceneFader.FadeTo(missionName);
    }
}