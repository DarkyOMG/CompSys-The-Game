using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * A Class to manage Audio output by holding references to audiosources, changing volume and changing clips.
 */
[CreateAssetMenu(menuName = "Manager/AudioManager")]
public class AudioManager : SingletonScriptableObject<AudioManager>
{
    [SerializeField] public GameObjectSO _audioSourceMusic;
    [SerializeField] public GameObjectSO _audioSourceSFX;
    [SerializeField] private FloatSO _audioVolumeMusic;
    [SerializeField] private FloatSO _audioVolumeSFX;
    
    // Used when a scene is loaded and adjusts the current Audio-Volume to the values specified in the pause-menu.
    public void InitAudio()
    {
        if (_audioSourceSFX.go)
        {
            _audioSourceSFX.go.GetComponent<AudioSource>().volume = _audioVolumeSFX.value;
        }
        if (_audioSourceSFX.go)
        {
            _audioSourceMusic.go.GetComponent<AudioSource>().volume = _audioVolumeMusic.value;
        }
    }
    /**
     * Playes a given clip through the Soundeffect-audiosource.
     * @param   clip    The clip to be played through the audiosource.
     */
    public void PlaySound(AudioClip clip)
    {
        // Catch the Audiosource of the current gameobject, holding the audiosource.
        AudioSource tempSource = _audioSourceSFX.go.GetComponent<AudioSource>();
        // If it is already playing a sound, stop it.
        if (tempSource.isPlaying)
        {
            tempSource.Stop();
        }
        // Play the new clip.
        tempSource.clip = clip;
        tempSource.Play();
    }

    // Changes the volume of the Soundeffect Audiosource. Can be used with a slider.
    public void ChangeVolumeSFX(float newVolume)
    {
        _audioVolumeSFX.value = newVolume;
        _audioSourceSFX.go.GetComponent<AudioSource>().volume = newVolume;
    }

    // Changes the volume of the backgroundmusic Audiosource. Can be used with a slider.
    public void ChangeVolumeMusic(float newVolume)
    {

        _audioVolumeMusic.value = newVolume;
        _audioSourceMusic.go.GetComponent<AudioSource>().volume = newVolume;
    }
}
