using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header ("Input Actions")]
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;

    [Header ("Component References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip sfxThrust;
    [SerializeField] private ParticleSystem vfxLeftThrust;
    [SerializeField] private ParticleSystem vfxRightThrust;
    [SerializeField] private ParticleSystem vfxCenterThrust;

    [Header ("Movement Power")]
    [SerializeField] private float thrushtPower;
    [SerializeField] private float rotationPower;

    private void OnEnable(){
            thrust.Enable();
            rotation.Enable();
    }

    private void Start(){
        rb = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }

    private void FixedUpdate(){
        ProcessThrust();
    }

    private void Update(){
        ProcessRotation();
    }

    private void ProcessThrust(){
        

        if(thrust.IsPressed()){
            vfxCenterThrust.Play();
            vfxLeftThrust.Play();
            vfxRightThrust.Play();
            if(!audiosource.isPlaying){
                audiosource.PlayOneShot(sfxThrust);
            }
            rb.AddRelativeForce(Vector3.up * thrushtPower * Time.deltaTime);
        }
        else{
            vfxCenterThrust.Stop();
            vfxLeftThrust.Stop();
            vfxRightThrust.Stop();
            audiosource.Stop();
        }
    }

    private void ProcessRotation(){
        float rotationValue = rotation.ReadValue<float>();
        if(rotationValue != 0){
            rb.freezeRotation = true;
            transform.Rotate(rotationValue * Vector3.forward * rotationPower * Time.deltaTime);
            rb.freezeRotation = false;
        }

        if(rotationValue == -1){
            vfxLeftThrust.Play();
        }

        if(rotationValue == 1){
            vfxRightThrust.Play();
        }
    }
}