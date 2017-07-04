using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KBEngine
{
	public class Avatar : CombatModel
	{
        private List<object> _avatarBag = new List<object>();

        public List<object> AvatarBag
        {
            get
            {
                if (_avatarBag.Count == 0)
                {
                    object avatarBagObject = getDefinedProperty("avatarBag");
                    _avatarBag = ((Dictionary<string, object>)avatarBagObject)["values"] as List<object>;
                    return _avatarBag;
                }
                return _avatarBag;
            }
        }

	    public override void __init__()
	    {
	        base.__init__();
            if (isPlayer())
            {
                KBEngine.Event.registerIn("updatePlayer", this, "updatePlayer");
                KBEngine.Event.registerIn("RequestMove", this, "RequestMove");
                KBEngine.Event.registerIn("RequestDialog", this, "RequestDialog");
                KBEngine.Event.registerIn("RequestBuyGoods", this, "RequestBuyGoods");
                KBEngine.Event.registerIn("RequestDoSkillQ", this, "RequestDoSkillQ");
                KBEngine.Event.registerIn("RequestDoSkillW", this, "RequestDoSkillW");
                KBEngine.Event.registerIn("StopMove", this, "StopMove");
                KBEngine.Event.registerIn("OnLeaveSpaceClientInputInValid", this, "OnLeaveSpaceClientInputInValid");
                //郑晓飞--注册发送信息函数
                KBEngine.Event.registerIn("SendChatMessage", this, "SendChatMessage");
                //郑晓飞--注册寻找朋友函数
                KBEngine.Event.registerIn("FindFriends", this, "FindFriends");
                //郑晓飞--添加朋友函数
                KBEngine.Event.registerIn("AddFriends", this, "AddFriends");
                //郑晓飞--删除朋友函数
                KBEngine.Event.registerIn("DeleteFriends", this, "DeleteFriends");
            }
	    }

        public override void onDestroy()
        {
            base.onDestroy();
            Event.deregisterIn(this);
        }

        public void set_avatarBag(object old)
        {
            object avatarBagObject = getDefinedProperty("avatarBag");
            _avatarBag = ((Dictionary<string, object>)avatarBagObject)["values"] as List<object>;
        }

        #region 暴露给服务端调用的方法代码块

	    // ReSharper disable once InconsistentNaming
        public void onMainAvatarEnterSpace(int spaceId, string spaceName)
        {
            Event.fireOut("onMainAvatarEnterSpace", spaceId, spaceName);
        }
        // ReSharper disable once InconsistentNaming
        public void onMainAvatarLeaveSpace()
	    {
	        Event.fireOut("onMainAvatarLeaveSpace");
	    }

        public void DoDialog(System.String npcName, System.String dialog)
        {
            Event.fireOut("DoDialog", new object[] { this, npcName, dialog });
        }

        public void BuyResult(int result)
        {
            Event.fireOut("BuyResult", new object[] { this, System.Convert.ToBoolean(result) });
        }

        public void DoStore(Dictionary<string, object> storeGoodsIdListObject)
        {
            List<System.Int32> storeGoodsIdList = (List<System.Int32>)storeGoodsIdListObject["values"];
            foreach (var item in storeGoodsIdList)
	        {
                Debug.Log(item);
	        }
            Event.fireOut("DoStore", new object[] { this });
        }

        #endregion 

        #region 处理u3d表现层的抛入事件

	    // ReSharper disable once InconsistentNaming
        public virtual void updatePlayer(float x, float y, float z, float dir_y, float dir_z)
        {
            position.x = x;
            position.y = y;
            position.z = z;

            direction.z = dir_y;
            direction.y = dir_z;
        }

	    public void OnLeaveSpaceClientInputInValid()
	    {
            cellCall("onLeaveSpaceClientInputInValid");
        }

        public void RequestMove(Vector3 point)
        {
            cellCall("requestMove", new object[] { point });
        }

        public void StopMove()
        {
            cellCall("stopMove");
        }

        public void RequestDialog(uint spaceId, string npcName)
        {
            cellCall("requestDialog", new object[] { spaceId, npcName });
        }

        public void RequestBuyGoods(uint spaceId, string npcName, int goodsId)
        {
            cellCall("requestBuyGoods", new object[] { spaceId, npcName, goodsId });
        }

        public void RequestDoSkillQ(Vector3 point, float yaw)
        {
            cellCall("requestDoSkillQ", new object[] { point, yaw });
        }

        public void RequestDoSkillW(Vector3 point)
        {
            cellCall("requestDoSkillW", new object[] { point });
        }

        //郑晓飞---发送聊天信息
        public void SendChatMessage(string message)
        {
            cellCall("onSendChatMessage", new object[] { getDefinedProperty("entityName"), message});
        }

        //郑晓飞--寻找朋友--返回所有注册玩家
        public void FindFriends()
        {
            Debug.Log("Avatar:FindFriends");
            cellCall("FindFriends");
        }

        //郑晓飞--添加玩家
        public void AddFriends(string goldxFriendsName)
        {
            Debug.Log("Avatar:AddFriends");
            cellCall("AddFriends", new object[] {goldxFriendsName});
        }

        //郑晓飞--删除朋友
        public void DeleteFriends()
        {
            Debug.Log("Avatar:DeleteFriends");
            cellCall("DeleteFriends");
        } 
         
        //郑晓飞--显示全部好友
        public void ShowAllFriends()
        {
            Debug.Log("Avatar:ShowAllFriends");
            cellCall("ShowAllFriends");
        }
        #endregion
    }
}