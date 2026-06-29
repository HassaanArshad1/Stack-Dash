using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("SFX")]
    [SerializeField] private AudioClip collectSFX;
    [SerializeField] private AudioClip bonusCollectSFX;
    [SerializeField] private AudioClip obstacleHitSFX;
    [SerializeField] private AudioClip gameOverSFX;
    [SerializeField] private AudioClip bridgeTileSFX;

    [Header("Music")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private float musicVolume = 0.4f;
    [SerializeField] private float sfxVolume = 1f;

    private AudioSource _musicSource;
    private AudioSource _sfxSource;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.loop = true;
        _musicSource.volume = musicVolume;
        _musicSource.playOnAwake = false;

        _sfxSource = gameObject.AddComponent<AudioSource>();
        _sfxSource.playOnAwake = false;
        _sfxSource.volume = sfxVolume;
    }

    private void OnEnable() => GameManager.OnStateChanged += HandleStateChanged;
    private void OnDisable() => GameManager.OnStateChanged -= HandleStateChanged;

    private void HandleStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Playing)
        {
            _musicSource.clip = backgroundMusic;
            _musicSource.Play();
        }
        else if (state == GameManager.GameState.GameOver)
        {
            _musicSource.Stop();
            PlaySFX(gameOverSFX);
        }
    }

    public void PlayCollectSFX() => PlaySFX(collectSFX);
    public void PlayBonusCollectSFX() => PlaySFX(bonusCollectSFX);
    public void PlayObstacleHitSFX() => PlaySFX(obstacleHitSFX);
    public void PlayBridgeTileSFX() => PlaySFX(bridgeTileSFX);

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        _sfxSource.PlayOneShot(clip);
    }
}