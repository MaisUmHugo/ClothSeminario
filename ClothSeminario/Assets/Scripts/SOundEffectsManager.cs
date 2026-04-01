using UnityEngine;

public class SOundEffectsManager : MonoBehaviour{
    public static SOundEffectsManager instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    public void PlaySoundEffect(AudioClip clip, Transform spawnTransform, float volume) {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        float cliplenght = audioSource.clip.length;
        Destroy(audioSource.gameObject, cliplenght);
    }
}
