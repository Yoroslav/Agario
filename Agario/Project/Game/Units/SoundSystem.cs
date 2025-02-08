using SFML.Audio;

public class SoundSystem : IDisposable
{
    private Dictionary<string, SoundBuffer> _soundBuffers;
    private Dictionary<string, Sound> _sounds;

    public SoundSystem()
    {
        _soundBuffers = new Dictionary<string, SoundBuffer>();
        _sounds = new Dictionary<string, Sound>();
    }

    public void LoadSound(string name, string filePath)
    {
        if (!_soundBuffers.ContainsKey(name))
        {
            if (File.Exists(filePath))
            {
                try
                {
                    var buffer = new SoundBuffer(filePath);
                    _soundBuffers[name] = buffer;
                    _sounds[name] = new Sound(buffer);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки звука {name}: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Файл звука не найден: {filePath}");
            }
        }
    }

    public void PlaySound(string name)
    {
        if (_sounds.TryGetValue(name, out var sound))
        {
            sound.Play();
        }
    }

    public void StopSound(string name)
    {
        if (_sounds.TryGetValue(name, out var sound))
        {
            sound.Stop();
        }
    }

    public void SetLoop(string name, bool loop)
    {
        if (_sounds.TryGetValue(name, out var sound))
        {
            sound.Loop = loop;
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
