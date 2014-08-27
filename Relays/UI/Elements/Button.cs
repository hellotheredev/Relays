using UnityEngine;
using UnityEngine.EventSystems;

using HelloThere.InCommon;
using HelloThere.ProgrammableValidation;

using Relays;

namespace Relays.UI
{

	public class Button : UIElement
	{

		protected override void OnValidateUIElement (OperationResultHandler resultHandler)
		{
		}

		public override void OnPointerClick (PointerEventData eventData)
		{
			relay.AddUITransmission(this, UIRelay.EventTypes.Clicked, Option, requireReceiver);
			relay.Transmit ();

			base.OnPointerClick (eventData);
		}
			
		public ScriptableObject Option;

	}

}