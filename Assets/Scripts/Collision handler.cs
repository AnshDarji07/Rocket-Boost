using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Collisionhandler : MonoBehaviour
{
    [SerializeField] public int delay = 3;

    private AudioSource audiosource;
    [SerializeField] private AudioClip sfxCollision;
    [SerializeField] private AudioClip sfxSuccess;

    [SerializeField] private ParticleSystem vfxCrash;
    [SerializeField] private ParticleSystem vfxSuccess;

    bool isControllabe = false;

    void Start(){
        audiosource=GetComponent<AudioSource>();
    }

    private void Update(){
        CheckDebugKeys();
    }
    private void OnCollisionEnter(Collision collision){
        if(isControllabe){
            return;
        }

        switch (collision.gameObject.tag){
            case "Friendly":
                Debug.Log("Its Friendly");
                break;

            case "Fuel":
                Debug.Log("Its Fuel");
                break;

            case "Finish":
                Debug.Log("Its Finish");
                Startfinishsequence();
                break;
            default:
                Debug.Log("Exploded");
                Startcrashsequence();
                break;
        }
    }

    private void Startfinishsequence(){
        isControllabe = true;
        vfxSuccess.Play();
        audiosource.Stop();

        GetComponent<Movement>().enabled = false;
        Invoke("Loadnextlevel", 4);
        audiosource.PlayOneShot(sfxSuccess);
    }

    private void Startcrashsequence(){
        isControllabe = true;
        vfxCrash.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", delay);
        audiosource.PlayOneShot(sfxCollision);
    }

    private void ReloadScene(){
        int currentsceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentsceneIndex);
    }

    private void Loadnextlevel(){
        int currentsceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentsceneIndex + 1;

        if(currentsceneIndex == SceneManager.sceneCountInBuildSettings - 1){
            //nextIndex = 0;
            return;
        }
        SceneManager.LoadScene(nextIndex);  
    }

    private void CheckDebugKeys(){
        if(Keyboard.current.lKey.wasPressedThisFrame){
            Loadnextlevel();
        }

        if(Keyboard.current.cKey.wasPressedThisFrame){
            isControllabe = !isControllabe; 
        }

        if(Keyboard.current.qKey.wasPressedThisFrame){
            Application.Quit();
        }
    }
}
