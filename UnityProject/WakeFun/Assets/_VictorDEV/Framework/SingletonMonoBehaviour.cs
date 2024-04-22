using UnityEngine;

namespace VictorDev.Common
{
    /// <summary>
    /// ��ҼҦ��A�istatic�I�s�A�i������GameObject�W
    /// <para>����Instance�O�_�s�b�æ۰ʷs�ة�����W</para>
    /// </summary>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : Component, new()
    {
        private static T instance { get; set; }

        protected static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        instance = obj.AddComponent<T>();
                        instance.name = $">>Dynamic<< {instance.GetType().Name}";
                    }
                }
                return instance;
            }
        }

        private void Awake() => instance ??= this as T;
        protected virtual void OnValidate() => name = $"[Singleton] - {typeof(T).Name}";
    }
}
