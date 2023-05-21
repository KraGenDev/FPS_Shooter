using UnityEngine;

namespace Weapon
{
    public class Weapon : MonoBehaviour, IGun
    {
        [SerializeField] protected Transform _firePoint;
        [SerializeField] private Transform _container;
        [SerializeField] internal Bullet _bullet;
        [SerializeField] private float _shotForce;
        [SerializeField] protected int _bulletCount = 10;
        [SerializeField] protected PlayerFacade _playerFacade;

        private PoolMono<Bullet> _bulletPool;

        private void Awake() => CreateBullets();


        private void CreateBullets()
        {
            _bulletPool = new PoolMono<Bullet>(_bullet, _bulletCount,_container);

            var allBullet = _bulletPool.GetAllObjects();

            for (int i = 0; i < allBullet.Count; i++)
            {
                allBullet[i].SetDependency(_playerFacade);
            }
        }
        public virtual void Shot()
        {
            var bullet = _bulletPool.GetFreeElement();
            var bulletTransform = bullet.transform;
            bulletTransform.position = _firePoint.transform.position;
            
            var bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = Vector3.zero;
            bulletRigidbody.AddForce(_firePoint.forward * _shotForce,ForceMode.Impulse);
        }        
    }
}