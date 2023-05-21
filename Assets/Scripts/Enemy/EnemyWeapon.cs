using UnityEngine;
using Weapon;

public class EnemyWeapon : Weapon.Weapon
{
    [SerializeField] private Rocket _rocket;

    private PoolMono<Rocket> _bulletPool;

    public void SetPlayerFacade(PlayerFacade playerFacade)
    {
        _playerFacade = playerFacade;
        CreateBullets();
    }
    
    private void CreateBullets()
    {
        _bulletPool = new PoolMono<Rocket>(_rocket, _bulletCount);

        var allBullet = _bulletPool.GetAllObjects();

        for (int i = 0; i < allBullet.Count; i++)
        {
            allBullet[i].SetDependency(_playerFacade);
            allBullet[i].SetTarget(_playerFacade.transform);
        }
    }
    public virtual void Shot()
    {
        var bullet = _bulletPool.GetFreeElement();
        var bulletTransform = bullet.transform;
        bulletTransform.position = _firePoint.transform.position;
    }
}
