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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
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

            // 🕒 Wait a short delay before showing Game Over (so sound can play)
            StartCoroutine(DelayGameOver());
        }
    }

    private System.Collections.IEnumerator DelayGameOver()
    {
        yield return new WaitForSeconds(0.2f); // wait 0.4 seconds for sound
        FindObjectOfType<GameManager>().GameOver();
    }
}
