using System;
using UnityEngine;

public class WJY_TestSceneManager : Singleton<WJY_TestSceneManager>
{
    protected override void Start()
    {
        base.Start();

        // 1. 리소스 로드
        ResourceLoad((key, count, total) =>
        {
            Debug.Log($"{key}, {count}, {total}");
            if (count == total)
            {
                // 2. 객체 생성, 초기화
                Debug.Log("리소스 로드 완료. 객체 생성, 초기화 시작");

                var ground = ResourceManager.Instance.GetCache<GameObject>("Ground");
                Instantiate(ground);
                var player = ResourceManager.Instance.GetCache<GameObject>("PlayerCharacter");
                Instantiate(player);
                var weapon = ResourceManager.Instance.GetCache<GameObject>("Weapon");
                Instantiate(weapon, Camera.main.transform.GetChild(0));

                InputManager.Instance.Initialize();
            }
        });
    }

    private void ResourceLoad(Action<string, int, int> callback = null)
    {
        // Resource.LoadAllAsync가 없어서
        // LoadAsync()를 여러번 반복해서 필요한 리소스를 캐싱해야할 것 같습니다.
        // 따라서, LoadAllAsync 메서드 내부에 캐싱이 필요한 오브젝트의 주소를 등록하고,
        // 그 주소들을 반복문을 돌며 LoadAsnyc로 로드하도록 했습니다.

        ResourceManager.Instance.LoadAllAsync<UnityEngine.Object>(callback);
    }
}