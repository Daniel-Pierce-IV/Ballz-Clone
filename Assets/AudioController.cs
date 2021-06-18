using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* I created AudioController to minimize annoying instances where simultaneously
 * playing audio clips overlap and become very loud for a split second.
 * Mostly occurred when there were lots of collisions happening at once.
 * */
public class AudioController : MonoBehaviour
{
    public enum AudioType
    {
        ball,
        pickup
    }

    public static AudioController instance;

    [SerializeField] private AudioClip ballBounce;
    [SerializeField] private AudioClip pickupCollect;
    [SerializeField] private int audioQueueLimit = 10;
    [SerializeField] private float audioSoundBuffer = 0.1f;

    private List<AudioSource> audioSources;
    private Queue<AudioType> audioQueue = new Queue<AudioType>();
    private Dictionary<AudioType, AudioClip> audioDict = new Dictionary<AudioType, AudioClip>();
    private bool canPlayAudio = true;

    private void Start()
    {
        instance = this;
        audioSources = new List<AudioSource>(GetComponents<AudioSource>());
        audioDict.Add(AudioType.ball, ballBounce);
        audioDict.Add(AudioType.pickup, pickupCollect);
    }

    // Update is called once per frame
    void Update()
    {
        while(GetAvailableAudioSource() != null && audioQueue.Count > 0 && canPlayAudio)
        {
            canPlayAudio = false;
            GetAvailableAudioSource().PlayOneShot(audioDict[audioQueue.Dequeue()]);
            StartCoroutine(EnablePlaying());
        }
    }

    public void QueueAudioClip(AudioType type)
    {
        if (audioQueue.Count < audioQueueLimit
            || type == AudioType.pickup) audioQueue.Enqueue(type);
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach(AudioSource audio in audioSources)
        {
            if (!audio.isPlaying) return audio;
        }

        return null;
    }

    private IEnumerator EnablePlaying()
    {
        yield return new WaitForSeconds(audioSoundBuffer);
        canPlayAudio = true;
    }
}
