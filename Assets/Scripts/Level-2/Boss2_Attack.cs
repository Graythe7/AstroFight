using System.Collections;
using UnityEngine;

public class Boss2_Attack : MonoBehaviour
{
    public Animator bossAnimator;
    public Boss bossMovement;
    public Transform firePoint;
    public Bullet bulletPrefab;
    public Bomb bombPrefab;
    public GameManager gameManager;

    public float bulletSpace = 1f; // Adjust bullet spacing
    private float minFireRate = 2f;
    private float maxFireRate = 4f;

    private void Start()
    {
        StartCoroutine(StartShootingDelay());
    }

    private IEnumerator StartShootingDelay()
    {
        yield return new WaitForSeconds(3.5f); // Initial delay
        StartShoot(); // Start firing after delay
    }

    private void StartShoot()
    {
        InvokeRepeating(nameof(ShootRoutine), 2.5f, Random.Range(minFireRate, maxFireRate));
    }

    private void ShootRoutine()
    {
        if (!gameManager.WinState) // Stop shooting if the game is won
        {
            if(gameManager.bossHealth >= 50)
            {
                StartCoroutine(Shoot());
            }
            else
            {
                StartCoroutine(Bombing());
            }

            
        }
        else
        {
            CancelInvoke(nameof(ShootRoutine)); // Stop shooting if the boss is defeated
        }
    }

    public IEnumerator Shoot()
    {
        bossMovement.MovementPause(false);
        bossAnimator.SetBool("isBossShooting", true);

        yield return new WaitForSeconds(0.6f);// wait before shooting 

        for (int i = -2; i <= 2; i++) // Spread bullets vertically
        {
            Vector2 spawnPosition = new Vector2(firePoint.position.x + (i * bulletSpace), firePoint.position.y);
            Bullet bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            bullet.Project(Vector2.left);
        }

        bossMovement.MovementPause(true);
        bossAnimator.SetBool("isBossShooting", false);

        yield return new WaitForSeconds(Random.Range(minFireRate, maxFireRate));
    }

    public IEnumerator Bombing()
    {
        bossMovement.MovementPause(false);
        bossAnimator.SetBool("isBossShooting", true);

        yield return new WaitForSeconds(0.6f);

        Instantiate(bombPrefab, firePoint.transform.position, Quaternion.identity);

        bossMovement.MovementPause(true);
        bossAnimator.SetBool("isBossShooting", false);
        yield return new WaitForSeconds(3f);
    }
}
