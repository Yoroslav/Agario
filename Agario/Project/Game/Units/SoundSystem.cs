using SFML.Audio;
using System.Collections.Generic;

public class SoundSystem
{
    private Dictionary<string, SoundBuffer> soundBuffers;
    private Dictionary<string, Sound> sounds;

    public SoundSystem()
    {
        soundBuffers = new Dictionary<string, SoundBuffer>();
        sounds = new Dictionary<string, Sound>();
    }

    public void LoadSound(string name, string filePath)
    {
        SoundBuffer buffer = new SoundBuffer(filePath);
        soundBuffers[name] = buffer;
        sounds[name] = new Sound(buffer);
    }

    public void PlaySound(string name)
    {
        if (sounds.ContainsKey(name))
        {
            sounds[name].Play();
        }
    }

    public void StopSound(string name)
    {
        if (sounds.ContainsKey(name))
        {
            sounds[name].Stop();
        }
    }

    public void SetLoop(string name, bool loop)
    {
        if (sounds.ContainsKey(name))
        {
            sounds[name].Loop = loop;
        }
    }
}