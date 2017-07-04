namespace MagicFire.Mmorpg.UI
{
    using UnityEngine;
    using System.Collections;
    using UnityEngine.UI;

    public class FriendsListItem : MonoBehaviour
    {
        public string friendName 
        { 
            get
            {
                return transform.Find("FriendName").GetComponent<Text>().text.ToString();
            }
            set
            {
                // GameObject.Find("FriendName").GetComponent<Text>().text = value;
                transform.Find("FriendName").GetComponent<Text>().text = value;
                //_friendName = value;
            } 
        }

        public static string _friName = null;
        
       public string friName()
        {
            return _friName;
        }
        
        //µã»÷item
        public void ClickFriendsListItem()
        {
            var c = transform.Find("FriendName").GetComponent<Text>().text.ToString();
            _friName = c;
            Debug.Log("enter script ClickFriendsListItem()!");
            //var c = transform.Find("FriendName").GetComponent<Text>().text.ToString();
            Debug.Log("friend's name: " + c);
        }
    }
}
