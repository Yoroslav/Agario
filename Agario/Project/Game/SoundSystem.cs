using SFML.Audio;
using System.Resources;

public class SoundSystem : IDisposable
{
    private Dictionary<string, SoundBuffer> _soundBuffers;
    private Dictionary<string, Sound> _sounds;
    private ResourceManager _resourceManager;

    public SoundSystem(ResourceManager resourceManager)
    {
        _soundBuffers = new Dictionary<string, SoundBuffer>();
        _sounds = new Dictionary<string, Sound>();
        _resourceManager = resourceManager;
    }

    public void LoadSound(string name)
    {
        if (!_soundBuffers.ContainsKey(name))
        {
            try
            {
                var soundStream = _resourceManager.GetStream(name);
                if (soundStream == null)
                {
                    Console.WriteLine($"Sound resource {name} not found!");
                    return;
                }

                byte[] soundData = new byte[soundStream.Length];
                soundStream.Read(soundData, 0, (int)soundStream.Length);

                using (var stream = new MemoryStream(soundData))
                {
                    var buffer = new SoundBuffer(stream);
                    _soundBuffers[name] = buffer;
                    _sounds[name] = new Sound(buffer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading sound {name}: {ex.Message}");
            }
        }
    }




    public void PlaySound(string name, bool loop = false)
    {
        if (_sounds.TryGetValue(name, out var sound))
        {
            sound.Loop = loop;
            sound.Play();
        }
        else
        {
            Console.WriteLine($"Sound {name} not found!");
        }
    }

    public void StopSound(string name)
    {
        if (_sounds.TryGetValue(name, out var sound))
        {
            sound.Stop();
        }
    }

    public void SetVolume(string name, float volume)
    {
        if (_sounds.TryGetValue(name, out var sound))
        {
            sound.Volume = Math.Clamp(volume, 0, 100);
        }
    }

    public void Dispose()
    {
        foreach (var sound in _sounds.Values)
            sound.Dispose();

        foreach (var buffer in _soundBuffers.Values)
            buffer.Dispose();

        _sounds.Clear();
        _soundBuffers.Clear();
    }
}
