using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource uiSource;

    public void PlaySfxSound(AudioClip clip, Transform newTransform)
    {
        AudioSource newSfx = Instantiate(sfxSource, newTransform.position, Quaternion.identity);
        newSfx.clip = clip;
        newSfx.Play();
        
        float clipLength = newSfx.clip.length;
        Destroy(newSfx.gameObject, clipLength);
    }
    
    public void PlayRandomSfxSound(List<AudioClip> clipArray, Transform newTransform)
    {
        int selected = Random.Range(0, clipArray.Count);
        AudioSource newSfx = Instantiate(sfxSource, newTransform.position, Quaternion.identity);
        newSfx.clip = clipArray[selected];
        newSfx.Play();
        
        float clipLength = newSfx.clip.length;
        Destroy(newSfx.gameObject, clipLength);
    }
    
    public void PlayUISound(AudioClip clip, Transform newTransform)
    {
        AudioSource newSfx = Instantiate(uiSource, newTransform.position, Quaternion.identity);
        newSfx.clip = clip;
        newSfx.Play();
        
        float clipLength = newSfx.clip.length;
        Destroy(newSfx.gameObject, clipLength);
    }
    
    public void PlayRandomUISound(List<AudioClip> clipArray, Transform newTransform)
    {
        int selected = Random.Range(0, clipArray.Count);
        AudioSource newSfx = Instantiate(uiSource, newTransform.position, Quaternion.identity);
        newSfx.clip = clipArray[selected];
        newSfx.Play();
        
        float clipLength = newSfx.clip.length;
        Destroy(newSfx.gameObject, clipLength);
    }
}
