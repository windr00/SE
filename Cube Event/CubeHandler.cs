using UnityEngine;
using System.Collections;
using ProtoBuf;
using CubeEvent;
using System.IO;

public class CubeHandler : EventHandler {


	private void PysicalMove(CubeCE ce) {
        //var rbody = gameObject.GetComponent<Rigidbody>();
        Debug.Log("handle cube ce, key count:" + ce.key.Count);
		foreach (var key in ce.key) {
            Debug.Log(key);
			switch(key) {
			case "W":
			{
                Debug.Log("forward");
                transform.Translate(transform.forward);
				break;
			}
			case "S":
			{
                Debug.Log("backward");
                transform.Translate(-1 * transform.forward);
				break;
			}
			case "A":
			{
                Debug.Log("left");
                transform.Translate(-1 * transform.right);
				break;
			}
			case "D":
			{
                Debug.Log("right");
                transform.Translate(transform.right);
				break;
			}
			}
		}
	}

	private void ApplyNewState(CubeSTE st) {
		var state = gameObject.GetComponent<CubeState> ();
		foreach (var content in st.content) {
			switch (content.state) {
			case StateEnum.POS:
			{
				state.position = new UnityEngine.Vector3(content.value.x,
				                                         content.value.y,
				                                         content.value.z);
				transform.position = state.position;
				break;
			}
			case StateEnum.ROT:
			{
				state.rotation = new UnityEngine.Vector3(content.value.x,
				                                         content.value.y,
				                                         content.value.z);
				transform.eulerAngles = state.rotation;
				break;
			}
			}
		}
	}


	public override void Handle (UserEvent e)
	{
		var isSim = gameObject.GetComponent<State> ().isInSimulator;
		switch (e.type) {
		case UserEvent.EventType.CTR:
		{
			if (!isSim) {
				return;
			}
			PysicalMove(e.rawContent as CubeCE);
			break;
		}

		case UserEvent.EventType.ST:
		{
			if (isSim) {
				return ;
			}
			ApplyNewState(e.rawContent as CubeSTE);
			break;
		}
		}
	}

	public override object SelfDeserialize (UserEvent.EventType type, byte[] body)
	{
        object ret = null;

        using (var stream = new MemoryStream(body))
        {
            switch (type)
            {
                case UserEvent.EventType.CTR:
                    {
                        ret = Serializer.Deserialize<CubeCE>(stream);
                        break;
                    }
                case UserEvent.EventType.ST:
                    {
                        ret = Serializer.Deserialize<CubeSTE>(stream);
                        break;
                    }
            
            }

        }

        return ret;
	}
}
