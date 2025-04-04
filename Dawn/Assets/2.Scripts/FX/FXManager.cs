using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public static FXManager Instance { get; private set; }

    private Dictionary<FXType, GameObject> fxPrefabDict = new Dictionary<FXType, GameObject>();
    private Dictionary<FXType, Queue<GameObject>> fxPools = new Dictionary<FXType, Queue<GameObject>>();
    public int poolSize = 10;

    public GameObject playerAttackFxPrefab;
    public GameObject playerDamagedFxPrefab;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        fxPrefabDict.Add(FXType.FX_Player_NormalAttack, playerAttackFxPrefab);
        fxPrefabDict.Add(FXType.FX_Player_Damaged, playerDamagedFxPrefab);

        foreach (var type in fxPrefabDict.Keys)
        {
            Queue<GameObject> pool = new Queue<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(fxPrefabDict[type]);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }

            fxPools.Add(type, pool);
        }
    }

    public void FXPlay(FXType type, Vector3 position, Vector3 scale)
    {
        if(fxPools.ContainsKey(type) == true)
        {
            GameObject fxObj = fxPools[type].Dequeue();

            if(fxObj != null)
            {
                fxObj.transform.position = position;
                fxObj.transform.localScale = scale;
                fxObj.SetActive(true);
            }

            Animator anim = fxObj.GetComponent<Animator>();
            if(anim != null)
            {
                anim.Play(0);
                StartCoroutine(AnimationAndEndCoroutine(type, fxObj, anim));
            }
        }
    }

    IEnumerator AnimationAndEndCoroutine(FXType type, GameObject obj, Animator anim)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);
        obj.SetActive(false);
        fxPools[type].Enqueue(obj);
    }
}
