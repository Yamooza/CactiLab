using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundEffect
{
    public AudioClip[] clips;
    public AudioMixerGroup audioGroup;
    public float volume = 0.5f;
    public float pitch = 1f;
    public float spatialBlend = 1f;         // SpatialBlend: 0 = kuuluu aina, 1 = kuuluu 3D maailmasta, eli siit‰ suunnasta mist‰ ‰‰ni toistettu

    /// <summary>
    /// Get a random Audio Clip from the SoundEffect clips list
    /// </summary>
    /// <returns></returns>
    public AudioClip GetClip()
    {
        if (clips.Length == 0)
            return null;

        return clips[Random.Range(0, clips.Length)];
    }
}
