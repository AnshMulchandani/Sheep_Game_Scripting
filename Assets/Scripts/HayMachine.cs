using UnityEngine;

public class HayMachine : MonoBehaviour
{
    public float movementSpeed;
    public float maxMovement = 22.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        Debug.Log(transform.position.x);
    }
    private void UpdateMovement(){
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // 1
        if (horizontalInput < 0 && transform.position.x > -maxMovement) // 2
        {
        transform.Translate(transform.right * - movementSpeed * Time.deltaTime);
        }
        else if (horizontalInput > 0  && transform.position.x < maxMovement){ // 3
        transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }
        }
}
