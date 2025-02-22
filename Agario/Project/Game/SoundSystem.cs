﻿using SFML.Audio;
using System;
using System.Collections.Generic;
using System.IO;

public class SoundSystem : IDisposable
{
    private Dictionary<string, SoundBuffer> _soundBuffers;
    private Dictionary<string, Sound> _sounds;
    private string _soundsFolderPath;
    public SoundSystem(string soundsFolderPath)
    {
        _soundBuffers = new Dictionary<string, SoundBuffer>();
        _sounds = new Dictionary<string, Sound>();
        _soundsFolderPath = soundsFolderPath;
        if (!Directory.Exists(_soundsFolderPath))
            Directory.CreateDirectory(_soundsFolderPath);
    }
    public void LoadSound(string name)
    {
        if (!_soundBuffers.ContainsKey(name))
        {
            try
            {
                string soundPath = Path.Combine(_soundsFolderPath, $"{name}.wav");
                if (!File.Exists(soundPath))
                {
                    Console.WriteLine($"Sound file {name}.wav not found in {_soundsFolderPath}!");
                    return;
                }
                var buffer = new SoundBuffer(soundPath);
                _soundBuffers[name] = buffer;
                _sounds[name] = new Sound(buffer);
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
            sound.Stop();
    }
    public void SetVolume(string name, float volume)
    {
        if (_sounds.TryGetValue(name, out var sound))
            sound.Volume = Math.Clamp(volume, 0, 100);
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