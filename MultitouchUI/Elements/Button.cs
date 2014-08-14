/// TODO: add disabled state

using UnityEngine;

using HelloThere.InCommon;
using HelloThere.ProgrammableValidation;
using HelloThere.MultiTouch;

using Relays;

namespace MultitouchUI
{

	public class Button : UIElement
	{

		override protected void OnReleaseInside(FingerSession fingerSession)
		{
			base.OnReleaseInside(fingerSession);

			Relay.AddUITransmission(this, UIEvents.Clicked, Option, RequireReceiver);
		}

		public ScriptableObject Option;

	}

}