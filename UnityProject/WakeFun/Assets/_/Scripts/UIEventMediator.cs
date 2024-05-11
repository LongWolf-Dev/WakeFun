using UnityEngine;
using Random = UnityEngine.Random;

public class UIEventMediator : MonoBehaviour
{
    [SerializeField] private AddFriendHandler addFriendHandler;

    private void Awake()
    {
        addFriendHandler.OnSearchIDEvent.AddListener(SearchAccountByID);
        addFriendHandler.OnAddFriendEvent.AddListener(AddFriend);
    }

    private void AddFriend(SO_Account targetAccount)
    {
        Debug.Log($">>> AddFriend: {targetAccount.UserName} / {targetAccount.TitleName}");
    }

    private void SearchAccountByID(string accountID, AddFriendHandler target)
    {
        /**
         * ·j´M¸ê®Æ
         * */
        SO_Account result = ScriptableObject.CreateInstance<SO_Account>();
        result._SetupRandomData();

        result = Random.Range(0, 2) == 0 ? result : null;
        Debug.Log($">>> SearchAccountByID: {result}");

        target.AccountSoDataBySearch = result;
    }
}
