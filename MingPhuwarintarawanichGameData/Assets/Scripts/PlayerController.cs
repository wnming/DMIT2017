using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    float vInput, hInput, movementSpeed, rotationSpeed;
    [SerializeField]
    GameObject model;
    [SerializeField]
    Image fogPanel;
    [SerializeField]
    bool fade, fadeOn;
    private float direction;
    private float sensitivity = 20.0f;

    private bool isPartical;
    [SerializeField]
    GameObject transitionParticle;

    float initialCamera;

    // Start is called before the first frame update
    void Start()
    {
        initialCamera = Camera.main.fieldOfView;
        anim = GetComponentInChildren<Animator>();
        movementSpeed = 1.0f;
        rotationSpeed = 100.0f;

        direction = 100.0f;

        isPartical = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!fade)
        {
            vInput = Input.GetAxis("Vertical");
            hInput = Input.GetAxis("Horizontal");

            //Camera.main.fieldOfView = initialCamera;
        }
        else
        {
            vInput = 0;
            hInput = 0;
            if (fadeOn && direction > -100)
            {
                if (!isPartical)
                {
                    isPartical = true;
                    GameObject particle = Instantiate(transitionParticle, transform.position, transform.rotation);
                    Destroy(particle, 1.8f);
                }
                float cam = Camera.main.fieldOfView;
                cam += -1 * sensitivity * Time.deltaTime;
                Camera.main.fieldOfView = cam;
                direction -= 1;
            }
            else
            {
                if (!fadeOn && direction < 100)
                {
                    if (!isPartical)
                    {
                        isPartical = true;
                        GameObject particle = Instantiate(transitionParticle, transform.position, transform.rotation);
                        Destroy(particle, 1.8f);
                    }
                    float cam = Camera.main.fieldOfView;
                    cam += 1 * sensitivity * Time.deltaTime;
                    Camera.main.fieldOfView = cam;
                    direction += 1;
                }
                else
                {
                    if(Camera.main.fieldOfView > initialCamera)
                    {
                        Camera.main.fieldOfView = initialCamera;
                    }
                    isPartical = false;
                    fade = false;
                }
            }
            //if (fadeOn && fogPanel.color.a < 1)
            //{
            //    //Debug.Log(fogPanel.color.a);
            //    fogPanel.color = new Color(fogPanel.color.r, fogPanel.color.g, fogPanel.color.b, fogPanel.color.a + Time.deltaTime);
            //}
            //else if (!fadeOn && fogPanel.color.a > 0)
            //{
            //    fogPanel.color = new Color(fogPanel.color.r, fogPanel.color.g, fogPanel.color.b, fogPanel.color.a - Time.deltaTime);
            //}
            //else
            //{
            //    fade = false;
            //}
        }

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        transform.Translate(new Vector3(hInput * movementSpeed * Time.deltaTime, 0, vInput * movementSpeed * Time.deltaTime));
        transform.rotation = Quaternion.identity;


        if (hInput == 0 && vInput == 0)
        {
            anim.SetBool("Walk", false);
        }
        else
        {
            model.transform.LookAt(transform.position + new Vector3(hInput, 0, vInput).normalized);
            anim.SetBool("Walk", true);

            anim.speed = movementSpeed;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 2.0f;
        }
        else
        {
            movementSpeed = 1.0f;
        }
    }

    public void Fade(bool on)
    {
        fade = true;
        fadeOn = on;
    }

    public bool Fading()
    {
        return fade;
    }
}
