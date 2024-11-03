using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHp : MonoBehaviour
{
    [SerializeField, Header("HPicon")]
    private GameObject _playerIcon;

        private Player _player;
        private int _beforeHP;

    // Start is called before the first frame update
    void Start()
    {

        _player = FindObjectOfType<Player>();
        _beforeHP = _player.GetHP();
        _CreateHPIcon();


    }


    public void _CreateHPIcon()
    {
        for(int i = 0; i < _player.GetHP(); i++)
        {
            GameObject _playerHPObj = Instantiate(_playerIcon);
            _playerHPObj.transform.parent = transform;
        }
    }
    // Update is called once per frame
    void Update()
    {
        _ShowHPIcon();
    }

    public void _ShowHPIcon()
    {
        if (_beforeHP == _player.GetHP()) return;

        Image[] Icons = transform.GetComponentsInChildren<Image>();
        for(int i = 0 ; i < Icons.Length; i++)
        {
            Icons[i].gameObject.SetActive(i < _player.GetHP());
            
        }
        _beforeHP = _player.GetHP();
    }
}
