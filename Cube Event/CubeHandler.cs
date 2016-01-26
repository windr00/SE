using UnityEngine;
using System.Collections;
using ProtoBuf;
using CubeEvent;

public class CubeHandler : EventHandler {


	private void PysicalMove(CubeCE ce) {
		var rbody = gameObject.GetComponent<Rigidbody>();
		foreach (var key in ce.key) {
			switch(key) {
			case "W":
			{
				rbody.AddForce(transform.forward * 1.0f);
				break;
			}
			case "S":
			{
				rbody.AddForce(transform.forward * -1.0f);
				break;
			}
			case "A":
			{
				rbody.AddForce(transform.right * -1.0f);
				break;
			}
			case "D":
			{
				rbody.AddForce(transform.right * 1.0f);
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
		throw new System.NotImplementedException ();
	}
}
