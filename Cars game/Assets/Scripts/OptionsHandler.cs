using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OptionsHandler : MonoBehaviour
{
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle vsyncToggle;

    void Awake()
    {
        resolutionDropdown.ClearOptions();
        List<string> resolutions = new List<string>();
        foreach (Resolution r in Screen.resolutions)
        {
            resolutions.Add(string.Format("{0}x{1} ({2})", r.width, r.height, r.refreshRate));
        }
        resolutionDropdown.AddOptions(resolutions);
    }

    public void ApplySettings()
    {
        Resolution res = Screen.resolutions[resolutionDropdown.value];
        Screen.SetResolution(res.width, res.height, fullscreenToggle.isOn, res.refreshRate);
        QualitySettings.vSyncCount = vsyncToggle.isOn ? 1 : 0;
    }
}
