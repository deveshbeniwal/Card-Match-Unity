using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [System.Serializable]
    public class Sound_data
    {
        public SOUND_TYPE sound_type;
        public AudioClip sound_clip;
    }

    [SerializeField] AudioSource audio_source;
    [SerializeField] Sound_data[] all_sounds;


    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }


    private Sound_data Get_SoundData(SOUND_TYPE _sound)
    {
        return System.Array.Find(all_sounds, x => x.sound_type == _sound);
    }

    public void PlaySound(SOUND_TYPE _sound)
    {
        Sound_data data = Get_SoundData(_sound);
        if (data == null)
            return;

        audio_source.PlayOneShot(data.sound_clip);
    }
}
