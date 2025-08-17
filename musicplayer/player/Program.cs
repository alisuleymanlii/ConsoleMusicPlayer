using System;
using System.Collections.Generic;
using NAudio.Wave;

Console.WriteLine("Console Music Player");

List<string> playlist = new List<string>();
List<string> historylist = new List<string>();
string history = "history.txt";

if (File.Exists(history))
{
    string[] historys = File.ReadAllLines(history);
    for (int i = 0; historys.Length > i; i++)
    {
        historylist.Add(historys[i]);
    }
}

while (true)
{
    Console.Write("Add music file (or type 'done' to finish): ");
    string input = Console.ReadLine();
    if (input.ToLower() == "done")
        break;

    if (System.IO.File.Exists(input))
    {
        playlist.Add(input);
        historylist.Add(input);
        File.AppendAllText(history, input);
    }

    else
        Console.WriteLine("File not found!");
}

if (playlist.Count == 0)
{
    Console.WriteLine("No music added. Exiting...");
    return;
}

int currentIndex = 0;

using (var outputDevice = new WaveOutEvent())
{
    AudioFileReader audioFile = new AudioFileReader(playlist[currentIndex]);
    outputDevice.Init(audioFile);
    outputDevice.Play();
    audioFile.Volume = 0.5f;

    bool musicFinished = false;
    outputDevice.PlaybackStopped += (s, e) => { musicFinished = true; };

    Console.WriteLine("\n▶ Playing playlist...");
    Console.WriteLine("Commands: play | pause | next | prev | volume [0-100] | exit | history");

    while (true)
    {
        string command = Console.ReadLine().ToLower();
        

        if (command == "play")
            outputDevice.Play();
        else if (command == "pause")
            outputDevice.Pause();
        else if (command.StartsWith("volume"))
        {
            string[] parts = command.Split(' ');
            if (parts.Length == 2 && float.TryParse(parts[1], out float vol))
            {
                vol = Math.Clamp(vol, 0, 100);
                audioFile.Volume = vol / 100f;
                Console.WriteLine($"Volume: {vol}");
            }
            else
                Console.WriteLine("Usage: volume 0-100");
        }
        else if (command == "next")
        {
            outputDevice.Stop();
            audioFile.Dispose();
            currentIndex = (currentIndex + 1) % playlist.Count;
            audioFile = new AudioFileReader(playlist[currentIndex]);
            outputDevice.Init(audioFile);
            outputDevice.Play();
            musicFinished = false;
            Console.WriteLine($"▶ Now playing: {System.IO.Path.GetFileName(playlist[currentIndex])}");
        }
        else if (command == "prev")
        {
            outputDevice.Stop();
            audioFile.Dispose();
            currentIndex = (currentIndex - 1 + playlist.Count) % playlist.Count;
            audioFile = new AudioFileReader(playlist[currentIndex]);
            outputDevice.Init(audioFile);
            outputDevice.Play();
            musicFinished = false;
            Console.WriteLine($"▶ Now playing: {System.IO.Path.GetFileName(playlist[currentIndex])}");
        }
        else if (command == "exit")
        {
            outputDevice.Stop();
            audioFile.Dispose();
            break;
        }
        else if (command == "history")
        {
            Console.WriteLine("/////////HISTORY///////");
            for (int i = 0; historylist.Count > i; i++)
            {
                Console.WriteLine($"{i + 1}-{historylist[i]}");
            }
        }
        else
            Console.WriteLine("Unknown command");

        if (musicFinished)
        {
            audioFile.Dispose();
            currentIndex = (currentIndex + 1) % playlist.Count;
            audioFile = new AudioFileReader(playlist[currentIndex]);
            outputDevice.Init(audioFile);
            outputDevice.Play();
            musicFinished = false;
            Console.WriteLine($"▶ Now playing: {System.IO.Path.GetFileName(playlist[currentIndex])}");
        }
    }
}
