using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class BackGround : MonoBehaviour
{

	[SerializeField, Header("視差効果"), Range(0,1)]
    private float _parallaxEffect ;
private GameObject _camera ;
private float _length ;
private float _startPosX ;

void Start ()
{
    _startPosX = transform.position.x ;
    _length = GetComponent<SpriteRenderer>().bounds.size.x ;
    _camera = Camera.main.gameObject ; 
}



private void Updade()
{ 
    _Parallax() ;

}

private void _Parallax ()
{
  float temp = _camera.transform.position.x * (1 - _parallaxEffect) ;
  float dist = _camera.transform.position.x * _parallaxEffect ;

  transform.position = new Vector3(_startPosX + dist, transform.position.y, transform.position.z) ;
   if (temp > _startPosX + _length)
   {
       _startPosX += _length ;
   }
   else if (temp < _startPosX - _length)
   {
       _startPosX -= _length ;
   }
}

}