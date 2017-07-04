namespace UnityEngine
{
	using UnityEngine;
	using System;
	using System.Collections;
	
	public static class MonoBehaviourExtension {
	
	    public static void DelayExecute(this MonoBehaviour mono, Action action, float delayTime)
	    {
	        mono.StartCoroutine(DelayInvokeFunc(action, delayTime));
	    }
	
	    public static void DelayExecuteRepeating(this MonoBehaviour mono, Action action, float delayTime, float repeatTime)
	    {
	        mono.StartCoroutine(DelayInvokeRepeatingFunc(action, delayTime, repeatTime));
	    }
	
	    public static IEnumerator DelayInvokeFunc(Action action, float delayTime)
	    {
	        yield return new WaitForSeconds(delayTime);
	        action();
	    }
	
	    public static IEnumerator DelayInvokeRepeatingFunc(Action action, float delayTime, float repeatTime)
	    {
	        yield return new WaitForSeconds(delayTime);
	        action();
	        while (true)
	        {
	            action();
	            yield return new WaitForSeconds(repeatTime);
	        }
	    }
	}
}