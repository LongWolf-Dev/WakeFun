using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VictorDev.Common;
using VictorDev.EditorTool;

/// <summary>
/// [Singleton] [static]  ������޲z��
/// <para> + �HDictionary�޲z�P�ʺA�ͦ��h�����O�������</para>
/// <para> + �Y�ؼЪ��������ä��s�b�󪫥���A�h�H�ؼЪ���Prefab�۰ʰʺA�ͦ���Ҥ�</para>
/// </summary>
public class ObjectPoolManager : SingletonMonoBehaviour<ObjectPoolManager>
{
    [Header(">>> �C�����O��������̤j�ƶq<<< ")]
    [Range(30, 200)]
    [SerializeField] private int maxSizeOfEachQueue = 60;

    /// <summary>
    /// ��Dictionary�޲z�ʺA�ƶq�������
    /// </summary>
    private Dictionary<string, PoolContainer> poolDict { get; set; } = new Dictionary<string, PoolContainer>();

    /// <summary>
    /// �N�ؼЪ��󲾰ʦܪ����
    /// <para>+ �ݥΪx�����T���w���O���A</para>
    /// <para>+ ����P�_Dictionary�̦��L�ؼйﹳ��Queue</para>
    /// <para>+ �Y�L�h�s��Queue�PTransforContainer</para>
    /// <para>+ �H�ؼйﹳ.GetType()��Key�A�x�s��Dictionary</para>
    /// </summary>
    public static void PushToPool<T>(Transform container, Action actionOnAddedToPool = null) where T : Component
    {
        List<T> childList = container.GetComponentsInChildren<T>().ToList<T>();
        for (int i = 0; i < childList.Count; i++)
        {
            PushToPool<T>(childList[i].GetComponent<T>(), actionOnAddedToPool);
        }
    }

    /// <summary>
    /// �N�ؼЪ��󲾰ʦܪ����
    /// <para>+ �ݥΪx�����T���w���O���A</para>
    /// <para>+ ����P�_Dictionary�̦��L�ؼйﹳ��Queue</para>
    /// <para>+ �Y�L�h�s��Queue�PTransforContainer</para>
    /// <para>+ �H�ؼйﹳ.GetType()��Key�A�x�s��Dictionary</para>
    /// </summary>
    public static void PushToPool<T>(T target, Action actionOnAddedToPool = null) where T : Component
    {
        string className = typeof(T).Name;
        PoolContainer poolContainer;

        if (Instance.poolDict.ContainsKey(className) == false)
            Instance.poolDict[className] = new PoolContainer(className);
        poolContainer = Instance.poolDict[className];

        // �Y�����������W�L�̤j�ƶq�A�h�����R��
        if (poolContainer.PoolCount < Instance.maxSizeOfEachQueue)
        {
            actionOnAddedToPool?.Invoke();
            poolContainer.AddToQueuePool(target);
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log($"[{Instance.GetType().Name}] >>>  [ {className} ] ����W�L�̤j�ƶq: {Instance.maxSizeOfEachQueue}\t�����i��R��: {target.name}");
            DestroyImmediate(target.gameObject);
#else
            Destroy(target.gameObject);
#endif
        }
    }

    /// <summary>
    /// �q������̸��^���ؼ����O����
    /// <para>+ �Ϊx���ӫ��w���O���A</para>
    /// <para>+ �Y�S���ؼЪ���ɡA�h�Hprefab�ʺA��Ҥƨæ^��</para>
    /// </summary>
    public static T GetInstanceFromQueuePool<T>(T prefab) where T : Component
    {
        string className = typeof(T).Name;
        if (Instance.poolDict.ContainsKey(className) == false)
            Instance.poolDict[className] = new PoolContainer(className);

        return Instance.poolDict[className].GetInstanceFromQueuePool<T>(prefab);
    }

    protected override void OnValidateAfter() => name = $"{objName} [ MaxSize: {maxSizeOfEachQueue}]";

    /*-------------------------------------------------------------------------------------------------------------------------------------------*/

    /// <summary>
    /// �����Container�PQueue
    /// </summary>
    [Serializable]
    private class PoolContainer
    {
        private Queue<Component> queuePool { get; set; } = new Queue<Component>();
        private Transform container { get; set; }

        public string ClassName { get; private set; }
        public int PoolCount => queuePool.Count;

        public PoolContainer(string typeName)
        {
            ClassName = typeName;
            queuePool = new Queue<Component>();
            container = new GameObject().transform;
            container.parent = Instance.transform;
            RefreshContainerName();
        }

        /// <summary>
        /// �s�W�ؼйﹳ��Queue��
        /// </summary>
        public void AddToQueuePool(Component target)
        {
            queuePool.Enqueue(target);
            target.transform.SetParent(container);
            target.name = ClassName;
            target.gameObject.SetActive(false);
            ResetTarget(target);
            RefreshContainerName();
        }

        /// <summary>
        /// �qQueue���^������
        /// <para>+ �Y�S���ؼЪ��󫬺A�ɡA�h�Hprefab�ʺA��Ҥƨæ^��</para>
        /// </summary>
        public T GetInstanceFromQueuePool<T>(T prefab) where T : Component
        {
            Component target =
                (queuePool.Count > 0) ? queuePool.Dequeue() : Instantiate(prefab);

            target.gameObject.SetActive(true);
            RefreshContainerName();
            return target.GetComponent<T>();
        }

        /// <summary>
        /// ���m�ؼйﹳTransforme��0
        /// </summary>
        private void ResetTarget(Component target)
        {
            target.transform.position = Vector3.zero;
            target.transform.rotation = Quaternion.identity;
            target.transform.localScale = Vector3.one;
        }

        private void RefreshContainerName() => container.name = $"{ClassName} [Count:{PoolCount}]";
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ObjectPoolManager))]
    private class ObjectPoolManager_Inspector : InspectorEditor<ObjectPoolManager>
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (instance.poolDict.Count == 0) return;

            _CreateSpacer();
            _CreateLabelFiled($"[Queue ClassName]\t[Length]");
            _DrawHorizontalLine();

            foreach (PoolContainer poolContainer in instance.poolDict.Values)
            {
                _CreateLabelFiled($"{poolContainer.ClassName}\t{poolContainer.PoolCount}");
            }
        }
    }
#endif
}