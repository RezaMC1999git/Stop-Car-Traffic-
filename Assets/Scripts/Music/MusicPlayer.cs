using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] GameMusics;
    private bool playMusicOnce = true;
    private AudioSource AS;
    private void Awake()
    {
        AS = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(LevelController.instance.IsGameInProgress == true && playMusicOnce)
        {
            int witchMusic = Random.Range(0, GameMusics.Length);
            AS.PlayOneShot(GameMusics[witchMusic]);
            playMusicOnce = false;
        }
    }
}
