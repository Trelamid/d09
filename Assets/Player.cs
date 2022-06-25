using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Image = UnityEngine.UI.Image;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private NavMeshAgent _navMeshAgent;

    private Vector2 lastPos;

    public float health = 0f;

    public float speed;

    public GameObject weaponOne;
    public GameObject weaponSeveral;

    // public AudioSource _normalMusic;
    // public AudioSource _panicMusic;
    // public AudioSource _alarmMusic;
    // public AudioSource _reloadLvlMusic;
    //
    // private int music = 1;

    // public Image _healthUI;
    // public TextMeshProUGUI _healthUIText;

    // public bool haveKey;

    private Camera _camera;

    // public Image _panelUI;

    private bool end;
    
    // private void OnCollisionStay(Collision collisionInfo)
    // {
    //     Debug.Log(collisionInfo.gameObject.name);
    // }
    
    private void OnTriggerStay(Collider collisionInfo)
    {
        

        if (collisionInfo.tag == "Smoke")
        {
            if(health >= 2)
                health -= 2;
        }
        else if (collisionInfo.tag == "CameraLight")
        {
            health += 1;
        }
        else if (collisionInfo.tag == "Light")
        {
            health += 0.4f;
        }
    }
    
    
    

    void Start()
    {
        health = 0;
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _rigidbody = GetComponent<Rigidbody>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponOne.SetActive(true);
            weaponSeveral.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponOne.SetActive(false);
            weaponSeveral.SetActive(true);
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        HealthControl();
        
        // MusicControl();
        
        if (!end && health >= 100f)
            Find();
        
        Move();
        
        RotateIt();
    }

    void HealthControl()
    {
        // _healthUI.fillAmount = health / 100f;
        // _healthUIText.text = ((int)health).ToString();
        //
        if(health > 0)
            health -= 0.005f;
        
        // if(health > 75)
        //     _healthUI.color = Color.red;
        // else
        //     _healthUI.color = Color.gray;
            
    }
    
    // void MusicControl()
    // {
    //     if (music != 1 && health < 75)
    //     {
    //         music = 1;
    //         _alarmMusic.Stop();
    //         _normalMusic.Play();
    //         _panicMusic.Stop();
    //     }
    //     else if (music != 2 && health >= 75)
    //     {
    //         music = 2;
    //         _alarmMusic.Play();
    //         _panicMusic.Play();
    //         _normalMusic.Stop();
    //     }
    // }

    public void Win()
    {
        Debug.Log("Win");
        // StartCoroutine(corScreen());
        // _reloadLvlMusic.Play();
    }

    void Find()
    {
        end = true;
        // StartCoroutine(corScreen());
        // _reloadLvlMusic.Play();
    }

    // IEnumerator corScreen()
    // {
    //     while (_panelUI.color.a < 1)
    //     {
    //         yield return new WaitForSeconds(0.1f);
    //         _panelUI.color = new Color(_panelUI.color.r, _panelUI.color.g, _panelUI.color.b, _panelUI.color.a + 0.02f);
    //     }
    //
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    // }
    
    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // if (horizontal != 0)
        // {
        // _navMeshAgent.Move(transform.position + ((transform.right * horizontal + transform.forward * vertical) * Time.deltaTime));
        _navMeshAgent.Move((transform.right * horizontal + transform.forward * vertical) * Time.deltaTime * speed);
        // _navMeshAgent.Move(transform.position);
        // _rigidbody.velocity = (transform.right * horizontal + transform.forward * vertical) * 5;
        // _rigidbody.velocity = transform.up * vertical;
        // _rigidbody.velocity = new Vector3(horizontal, 0, vertical);

        // }
    }

    void RotateIt()
    {
        Vector2 pos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        transform.Rotate(0,pos.x, 0);
        // _camera.gameObject.transform.Rotate(-pos.y,0f,0f);
        // weapon.gameObject.transform.Rotate(0,pos.y / 10,0);
        // .visible = false;
        return;
    }
}
