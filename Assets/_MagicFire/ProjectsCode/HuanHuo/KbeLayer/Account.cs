using UnityEngine;
using System.Collections;

namespace KBEngine
{
	public class Account : Entity 
    {
	    public Account()
	    {
	
	    }
	
	    public override void __init__()
	    {
	        base.__init__();
            Event.fireOut("onLoginSuccessfully", new object[] { KBEngineApp.app.entity_uuid, id, this });
	    }

        public override void onDestroy()
        {
            base.onDestroy();
            Event.deregisterIn(this);
        }
	
	}
}
