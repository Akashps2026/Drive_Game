using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Move;
    public float Horizontal;
    public float Vertical;
    private float speed=20f;
    public  Rigidbody rb;

    public AudioSource hitSound; 
    public AudioSource Sound;
    private bool hasHit = false;
   

   
    void Start()
    {
        rb.GetComponent<Rigidbody>();
    }

   
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

       bool Break= Input.GetKey(KeyCode.Space);
        
           
        
        Vector3 Move=new Vector3(Horizontal,0,Vertical);
        transform.Translate(Move*speed*Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!hasHit && collision.gameObject.CompareTag("Obstacle"))
        {
            hasHit = true;

            // 🔊 Play hit sound
            if (hitSound != null)
            {
                hitSound.Play();
            }

           
            StartCoroutine(DelayGameOver());
        }
    }

    private System.Collections.IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(0.2f); 
        FindObjectOfType<GameManager>().GameOver();
    }
}
