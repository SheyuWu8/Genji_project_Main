using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenjiShuriken : MonoBehaviour
{
    [SerializeField] private GameObject shurikenPrefab;
    [SerializeField] private float delayBetweenShurikens;
    [SerializeField] private float shurikenLaunchForce;
    [SerializeField] private List<Transform> projectileSpawnTransforms = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            foreach(Transform spawnLocation in projectileSpawnTransforms)
            {
                GameObject projectileInstance = Instantiate(shurikenPrefab, spawnLocation.position, spawnLocation.rotation);
                projectileInstance.GetComponent<Rigidbody>().AddForce(projectileInstance.transform.forward * shurikenLaunchForce, ForceMode.VelocityChange);
            }
        }
    }

    private IEnumerator LeftClickAttack()
    {
        int shurikensFired = 0;

        while (shurikensFired < 3)
        {
            shurikensFired++;
            GameObject projectileInstance = Instantiate(shurikenPrefab, projectileSpawnTransforms[1].position, projectileSpawnTransforms[1].rotation);
            projectileInstance.GetComponent<Rigidbody>().AddForce(projectileInstance.transform.forward * shurikenLaunchForce, ForceMode.VelocityChange);
            yield return new WaitForSeconds(delayBetweenShurikens);
        }

        yield return null;
    }
}
