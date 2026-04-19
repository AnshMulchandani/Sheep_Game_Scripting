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

    public Transform modelParent; // 1

    // 2
    public GameObject blueModelPrefab;
    public GameObject yellowModelPrefab;
    public GameObject redModelPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadModel();
    }
    private void LoadModel()
    {
        Destroy(modelParent.GetChild(0).gameObject); // 1

        switch (GameSettings.hayMachineColor) // 2
        {
            case HayMachineColor.Blue:
                Instantiate(blueModelPrefab, modelParent);
            break;

            case HayMachineColor.Yellow:
                Instantiate(yellowModelPrefab, modelParent);
            break;

            case HayMachineColor.Red:
                Instantiate(redModelPrefab, modelParent);
            break;
        }
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
