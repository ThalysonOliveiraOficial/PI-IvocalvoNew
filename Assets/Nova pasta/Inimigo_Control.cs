using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Pool;

public class Inimigo_Control : MonoBehaviour
{
    [SerializeField] GameControl _gameControl;
    public List<GameObject> iniList_1;
    public List<GameObject> iniList_2;
    // Start is called before the first frame update
    void Start()
    {
        _gameControl = Camera.main.GetComponent<GameControl>();
        Invoke("InimigoStart1", 0.5f);
    }

    // Update is called once per frame
    public void InimigoStart1()
    {
        GameObject bullet = Inimigo_tipo1.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            // bullet.transform.position = turret.transform.position;
            // bullet.transform.rotation = turret.transform.rotation;
            bullet.GetComponent<SeguirPlayer>()._player = _gameControl._player;
            bullet.SetActive(true);
        }
    }
    public void InimigoStart2()
    {
        GameObject bullet = Inimigo_tipo2.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            // bullet.transform.position = turret.transform.position;
            // bullet.transform.rotation = turret.transform.rotation;
            bullet.GetComponent<SeguirPlayer>()._player = _gameControl._player;
            bullet.SetActive(true);
        }
    }
}
