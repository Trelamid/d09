using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator _animator;
    private Camera _camera;
    private bool _shot;
    [SerializeField]private float _rateOfFire;
    private float _time;

    [SerializeField] private GameObject _bullet;
    public float speedBul = 30;
    public int hitDamage;

    public GameObject effectHit;
    public GameObject effectHitEnemy;

    public enum type
    {
        one, several
    }

    public type _type;
    public bool thisWeapon;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if(!thisWeapon)
            return;
        
        if (Input.GetMouseButtonDown(0))
            _shot = true;
        if (Input.GetMouseButtonUp(0))
            _shot = false;
        
        if(_shot)
            Shot();
        else
            _animator.SetBool("Shoot", false);
    }

    void Shot()
    {
        _animator.SetBool("Shoot", true);
    }

    public void SpawnBullet()
    {
        if (_type == type.one)
            one();
        else
            several();
        
    }

    void one()
    {
        Vector3 centr = new Vector3(960, 510, 0);
        centr.z = 0.1f;
        var auf = _camera.ScreenToWorldPoint(centr);
        centr.z = 0.2f;
        var auf2 = _camera.ScreenToWorldPoint(centr);

        Ray _ray = new Ray(auf, auf2 - auf);
        RaycastHit _raycastHit;
        if (Physics.Raycast(_ray, out _raycastHit))
        {
            var bul = Instantiate(_bullet, transform.position + transform.forward*0.04f, Quaternion.LookRotation(auf2 - auf));
            if(_raycastHit.collider && _raycastHit.collider.tag == "Enemy") 
                StartCoroutine(cor(bul, bul.transform.position, _raycastHit.point, _raycastHit.collider.gameObject.GetComponent<Enemy>()));
            else
                StartCoroutine(cor(bul, bul.transform.position, _raycastHit.point));
        }
    }

    void several()
    {
        Vector3 centr = new Vector3(960+Random.Range(-300, 300), 510+Random.Range(-300, 300), 0);
        centr.z = 0.1f;
        var auf = _camera.ScreenToWorldPoint(centr);
        centr.z = 0.5f;
        var auf2 = _camera.ScreenToWorldPoint(centr);
        
        Vector3 centr2 = new Vector3(960+Random.Range(-300, 300), 510+Random.Range(-300, 300), 0);
        centr2.z = 0.1f;
        var auf21 = _camera.ScreenToWorldPoint(centr2);
        centr2.z = 0.5f;
        var auf22 = _camera.ScreenToWorldPoint(centr2);
        
        Vector3 centr3 = new Vector3(960+Random.Range(-300, 300), 510+Random.Range(-300, 300), 0);
        centr3.z = 0.1f;
        var auf31 = _camera.ScreenToWorldPoint(centr3);
        centr3.z = 0.5f;
        var auf32 = _camera.ScreenToWorldPoint(centr3);

        Ray _ray = new Ray(auf, auf2 - auf);
        Ray _ray2 = new Ray(auf21, auf22 - auf21);
        // Debug.Log(auf22 +" "+ auf21);
        Ray _ray3 = new Ray(auf31, auf32 - auf31);
        
        // Debug.Log(centr + " "+ centr2 +" "+ centr3);
        // Debug.Log(_ray + " "+ _ray2 +" "+ _ray3);
        
        RaycastHit _raycastHit;
        if (Physics.Raycast(_ray, out _raycastHit))
        {
            var bul = Instantiate(_bullet, transform.position + transform.forward*0.04f, Quaternion.LookRotation(auf2 - auf));
            if(_raycastHit.collider && _raycastHit.collider.tag == "Enemy") 
                StartCoroutine(cor(bul, bul.transform.position, _raycastHit.point, _raycastHit.collider.gameObject.GetComponent<Enemy>()));
            else
                StartCoroutine(cor(bul, bul.transform.position, _raycastHit.point));
        }
        RaycastHit _raycastHit2;
        if (Physics.Raycast(_ray2, out _raycastHit2))
        {
            var bul = Instantiate(_bullet, transform.position + transform.forward*0.04f, Quaternion.LookRotation(auf22 - auf21));
            if(_raycastHit.collider && _raycastHit2.collider.tag == "Enemy") 
                StartCoroutine(cor(bul, bul.transform.position, _raycastHit2.point, _raycastHit2.collider.gameObject.GetComponent<Enemy>()));
            else
                StartCoroutine(cor(bul, bul.transform.position, _raycastHit2.point));
        }
        RaycastHit _raycastHit3;
        if (Physics.Raycast(_ray3, out _raycastHit3))
        {
            var bul = Instantiate(_bullet, transform.position + transform.forward*0.04f, Quaternion.LookRotation(auf32 - auf31));
            if(_raycastHit.collider && _raycastHit.collider.tag == "Enemy") 
                StartCoroutine(cor(bul, bul.transform.position, _raycastHit3.point, _raycastHit3.collider.gameObject.GetComponent<Enemy>()));
            else
                StartCoroutine(cor(bul, bul.transform.position, _raycastHit3.point));
        }
        
        // Debug.Log(_raycastHit.point + " "+ _raycastHit2.point +" "+ _raycastHit3.point);

    }

    IEnumerator cor(GameObject bul, Vector3 start, Vector3 end, Enemy enemy = null)
    {
        // for (int i = 0; i < 100; i++)
        // {
        //     yield return new WaitForFixedUpdate();
        //     bul.transform.position = Vector3.Lerp(start, end, i/100f);
        // }

        while ((end - bul.transform.position).magnitude > 0.1)
        {
            yield return new WaitForFixedUpdate();
            // bul.transform.Translate((end - start) * Time.deltaTime);
            bul.transform.position += (end - start).normalized * Time.deltaTime * speedBul;
        }
        if(enemy)
            enemy.GetDamage(hitDamage);
        var effect = Instantiate(effectHit, end, Quaternion.identity);
        Destroy(effect, 0.5f);
        Destroy(bul);
    }
}
