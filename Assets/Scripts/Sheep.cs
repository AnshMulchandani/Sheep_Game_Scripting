using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed;
    public float gotHayDestroyDelay;
    private bool hitByHay;

    public float dropDestroyDelay; // 1
    private Collider myCollider; // 2
    private Rigidbody myRigidbody;
    private SheepSpawner sheepSpawner;
    public float heartOffset; // 1
    public GameObject heartPrefab; // 2
    private bool dropped;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }
    private void HitByHay(){
        sheepSpawner.RemoveSheepFromList(gameObject);
        hitByHay = true; // 1
        runSpeed = 0; // 2
        //TweenDown tweenScale = gameObject.AddComponent<TweenDown>();; // 1
        //tweenScale.targetScale = 0; // 2
        //tweenScale.timeToReachTarget = gotHayDestroyDelay; // 3
        //tweenScale.timeToDestroy = gotHayDestroyDelay;
        GameStateManager.Instance.SavedSheep();
        //Destroy(gameObject, gotHayDestroyDelay); // 3
        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
        SoundManagerScript.Instance.PlaySheepHitClip();
        StartCoroutine(SquishRoutine());

    }

    private IEnumerator SquishRoutine() {
        Vector3 originalScale = transform.localScale;
        // Target scale: Keep X and Z the same, but crush Y down to 0.1
        // (Optional: You can multiply X and Z by 1.2f to make it bulge outward when squished!)
        Vector3 flatScale = new Vector3(originalScale.x*1.2f, 0.1f, originalScale.z*1.2f);
        
        float percent = 0;

        // Loop until the animation finishes
        while (percent < 1f) {
            percent += Time.deltaTime / gotHayDestroyDelay;
            transform.localScale = Vector3.Lerp(originalScale, flatScale, percent);
            
            // Wait for the next frame before looping again
            yield return null; 
        }

        // After it's completely flat, destroy the sheep
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other){
    if (other.CompareTag("Hay") && !hitByHay){
        Destroy(other.gameObject); // 3
        HitByHay(); // 4
        }
    else if (other.CompareTag("DropSheep")){
        Drop();
    }
    }


    // disable kinematic so it afects gravity and trigger to become rigidbody and destroy after delay
    private void Drop(){
        if (dropped) {
            return; 
        }
        
        // 3. Lock the door so it can't run again
        dropped = true;
        if (sheepSpawner != null) {
        sheepSpawner.RemoveSheepFromList(gameObject);
        }
        GameStateManager.Instance.DroppedSheep();
        myRigidbody.isKinematic = false; // 1
        myCollider.isTrigger = false; // 2
        Destroy(gameObject, dropDestroyDelay); // 3
        SoundManagerScript.Instance.PlaySheepDroppedClip();
    }

    public void SetSpawner(SheepSpawner spawner){
        sheepSpawner = spawner;
    }
}
