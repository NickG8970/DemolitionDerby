using System.Linq;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;

    private AudioSource source;
    public bool looping;

    [Range(0f, 1f)] public float volume;
    [Range(0.5f, 1.5f)] public float pitch;

    [Range(0f, 0.5f)] public float volumeVariance = 0.1f;
    [Range(0f, 0.5f)] public float pitchVariance = 0.1f;

    public void SetSource(AudioSource source)
    {
        this.source = source;
        source.clip = clip;
    }

    public void PlayAudio()
    {
        source.volume = volume * (1 + Random.Range(-volumeVariance / 2f, volumeVariance / 2f));
        source.pitch = pitch * (1 + Random.Range(-pitchVariance / 2f, pitchVariance / 2f));
        if (looping)
        {
            source.loop = true;
            source.Play();
        }
        else
        {
            source.loop = false;
            source.Play();
            AudioManager.instance.WaitForSoundExit(source);
        }
    }

    public void StopAudio()
    {
        source.Stop();
        AudioManager.instance.WaitForSoundExit(source); // even though we know it's over, it will detect not playing so we might as well.
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private Sound[] sounds;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySound(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].soundName == name)
            {
                GameObject go = new GameObject("SOUND" + i + "" + sounds[i].soundName);
                go.transform.SetParent(transform);
                sounds[i].SetSource(go.AddComponent<AudioSource>());
                sounds[i].PlayAudio();
                return;
            }
        }

        Debug.LogWarning(string.Format("No Sound found with name {0}", name));
    }

    public void WaitForSoundExit(AudioSource source)
    {
        StartCoroutine(WaitForSoundToFinish(source));
    }

    IEnumerator WaitForSoundToFinish(AudioSource source)
    {
        yield return new WaitUntil(() => !source.isPlaying);
        Destroy(source.gameObject);
        yield return null;
    }

    public void StopSound(string name)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<AudioSource>().clip.name == sounds.Where(x => x.soundName == name).FirstOrDefault().clip.name)
            {
                sounds.Where(x => x.soundName == name).FirstOrDefault().StopAudio();
            }
        }

        //Debug.LogWarning(string.Format("No sound playing with name {0}", name));
    }

    public bool CheckIsPlaying(string name)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<AudioSource>().clip.name == sounds.Where(x => x.soundName == name).FirstOrDefault().clip.name)
            {
                return true;
            }
        }

        return false;
    }

    public void StopAllSounds()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].StopAudio();
        }
    }
}