using System.Collections;
using Enemy;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Systems
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float _timeSpawn = 5f;
        [SerializeField] private float _minTimeSpawn = 2f;
        [SerializeField] private float _minusTimeForSpawn = 0.25f;
        [SerializeField] private int _enemyCount = 20;
        [Space]
        [SerializeField] private BlueEnemy _blueEnemy;
        [SerializeField] private RedEnemy _redEnemy;
        [Space]
        [SerializeField] private float _planeWidth = 10f;
        [SerializeField] private float _planeLength = 10f;
        
        
        private PoolMono<BlueEnemy> _blueEnemies;
        private PoolMono<RedEnemy> _redEnemies;
        [Inject] private PlayerFacade _playerFacade;

        private void Start() => CreateEnemyPool();

        private void CreateEnemyPool()
        {
            var countBlueEnemy = _enemyCount / 4;
            var countRedEnemy = _enemyCount - countBlueEnemy;
            var myTransform = transform;
            
            _blueEnemies = new PoolMono<BlueEnemy>(_blueEnemy,countBlueEnemy,myTransform);
            _redEnemies = new PoolMono<RedEnemy>(_redEnemy, countRedEnemy,myTransform);
            
            GiveRequireComponents();
            StartCoroutine(Spawner());
        }

        private void GiveRequireComponents()
        {
            var allBlueEnemies = _blueEnemies.GetAllObjects();
            var allRedEnemies = _redEnemies.GetAllObjects();
            
            for (int i = 0; i < allBlueEnemies.Count; i++)
            {
                allBlueEnemies[i].SetPlayer(_playerFacade,_playerFacade.transform);
            }

            for (int i = 0; i < allRedEnemies.Count; i++)
            {
                allRedEnemies[i].SetTarget(_playerFacade.transform);    
            }
        }
        IEnumerator Spawner()
        {
            var currentEnemy = 0;
            while (true)
            {
                yield return new WaitForSeconds(_timeSpawn);

                if(currentEnemy < 4)
                {
                    var enemy = _redEnemies.GetFreeElement();
                    Spawn(enemy);
                    enemy.SetHealth();
                    currentEnemy++;
                }
                else
                {
                    var enemy = _blueEnemies.GetFreeElement();
                    Spawn(enemy);
                    enemy.SetHealth();
                    currentEnemy = 0;
                }
                if (_timeSpawn >  _minTimeSpawn) _timeSpawn -= _minusTimeForSpawn;
            }
        }

        private void Spawn(Enemy.Enemy enemy)
        {
            var position = Vector3.zero;
            position.x = Random.Range(-_planeLength,_planeLength);
            position.z = Random.Range(-_planeWidth,_planeWidth);
            position.y = 1f;
            
            enemy.transform.position = position;
        }
    }
}