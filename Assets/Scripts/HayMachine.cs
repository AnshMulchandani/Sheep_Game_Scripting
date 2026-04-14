using UnityEngine;

public class HayMachine : MonoBehaviour
{
    public float movementSpeed;
    public float maxMovement = 22.0f;
    //Reference to the Hay Bale prefab
    public GameObject hayBalePrefab;
    //The point from which the hay will to be shot.
    public Transform haySpawnpoint;
    //The smallest amount of time between shots
    public float shootInterval;
    //A timer that to keep track whether the machine can shoot
    private float shootTimer; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateShooting();
        Debug.Log(transform.position.x);
    }
    private void UpdateMovement(){
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // 1
        if (horizontalInput < 0 && transform.position.x > -maxMovement){
        transform.Translate(transform.right * - movementSpeed * Time.deltaTime);
        }
        else if (horizontalInput > 0  && transform.position.x < maxMovement){ // 3
        transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }
    }
    private void ShootHay(){
        //Creates instances of the prefab
        Instantiate(hayBalePrefab, haySpawnpoint.position,
        Quaternion.identity);
        SoundManagerScript.Instance.PlayShootClip();
    }
    private void UpdateShooting() {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space)) {
            shootTimer = shootInterval;
            ShootHay();
        }
    }
}
