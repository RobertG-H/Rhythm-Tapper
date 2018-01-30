using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SongManager : MonoBehaviour {

    public double bpm = 140.0F;
    public float gain = 0.5F;
    public float signatureHi = 4f;
    public int signatureLo = 4;
    private double nextTick = 0.0F;
    //private float amp = 0.0F;
    //private float phase = 0.0F;
    private double sampleRate = 0.0F;
    private float accent;
    private bool running = false;
    private float beat;
    void Start()
    {
        accent = signatureHi;
        beat = 0;
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick * sampleRate;
        running = true;
        GetComponent<AudioSource>().Play();
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        double samplesPerTick = sampleRate * 60.0F / bpm * 4.0F / signatureLo;
        double sample = AudioSettings.dspTime * sampleRate;
        int dataLen = data.Length / channels;
        int n = 0;
        while (n < dataLen)
        {
            //float x = gain * amp * Mathf.Sin(phase);
            int i = 0;
            while (i < channels)
            {
                //data[n * channels + i] += x;
                i++;
            }
            while (sample + n >= nextTick)
            {
                nextTick += samplesPerTick;
                //amp = 1.0F;
                accent += 0.5f;
                beat += 0.5f;
                if (accent > signatureHi)
                {
                    accent = 0.5f;
                    //amp *= 2.0F;
                }
                //Debug.Log("Tick: " + accent + "/" + signatureHi);
            }
            //phase += amp * 0.3F;
            // amp *= 0.993F;
            n++;
        }
        
    }

    public float getAccent()
    {
        
        return accent;
    }

    public float getBeat()
    {
        return beat;
    }
    public double getBpm()
    {
        return bpm;
    } 


    public bool isDone()
    {
        return !GetComponent<AudioSource>().isPlaying;
    }
}
