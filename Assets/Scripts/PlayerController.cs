using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // FSM
    // State Library

    [SerializeField] Transform bobber;
    Vector3 defaultBobberPos;

    [SerializeField] Slider powerBar;
    [SerializeField] TMP_Text text;
    bool held = false;
    float power = 0.0f;
    float maxPower = 20.0f;

    // Start is called before the first frame update
    void Start()
        => defaultBobberPos = bobber.transform.position;

    // Update is called once per frame
    void Update()
    {

        // Active trigger
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MB Down!");
            held = true;
        }

        // Accumulator
        if (held)
        {
            power += 2.5f * Time.deltaTime;
            power = Mathf.Clamp(power, 1.0f, maxPower);
            //Debug.Log("Accumulating power: " + power.ToString());
            powerBar.value = power / maxPower;
            text.text = power.ToString() + " | " + powerBar.value.ToString();
        }

        // Reset working vars
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("MB Up!");
            held = false;
            bobber.GetComponent<Rigidbody2D>().AddForce(new Vector2(power, power / 2.0f));
            power = 0.0f;
        }

        // Reset
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Resetting!");
            bobber.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            bobber.transform.position = defaultBobberPos;
        }
    }

    void Raycast()
    {

    }
}
