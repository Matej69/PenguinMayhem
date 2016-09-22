using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

    Profile profile;

    Vector2 velocity;
    float speed = 5;

	// Use this for initialization
	void Start () {
        profile = gameObject.GetComponent<CharacterAttributes>().profilePointer;
        //Profile.CorectInvalidInput(ref profile.keyConfigMap);
        velocity = new Vector2(0, 0);

    }
	
	// Update is called once per frame
	void LateUpdate () {
        CalculateVelocityOnInput();
        ApplyMovement(velocity);
    }

    void CalculateVelocityOnInput()
    {
        /*
        if (InputManager.keyHold[profile.keyConfigMap[Profile.keyType.LEFT]])
            velocity = new Vector2(-1 * speed * Time.deltaTime, velocity.y);
        else if(InputManager.keyHold[profile.keyConfigMap[Profile.keyType.RIGHT]])
            velocity = new Vector2(speed * Time.deltaTime, velocity.y);
        */
    
        //Debug.Log(InputManager.keyHold[profile.keyConfigMap[Profile.keyType.LEFT]]);

        //******************************* CHECK WHATS UP WITH RETARDED INPUT ************************************************************************************************
                        Debug.Log(InputManager.keyPressed[KeyCode.A]+" "+ InputManager.keyHold[KeyCode.A] + " " + InputManager.keyReleased[KeyCode.A]);

    }


    void ApplyMovement(Vector2 _velocity)
    {
        transform.Translate(_velocity);
    }



}
