using UnityEngine;

using HelloThere.InCommon;
using HelloThere.ProgrammableValidation;
using HelloThere.ProgrammableResource;

using Relays;

namespace Relays.UI
{

	public class UIRelay : Relay<UIRelay.Package>
	{

		#region AddUITransmission

		public void AddUITransmission (object transmitter, EventTypes uiEvent)
		{
			AddUITransmission (transmitter, uiEvent, null, false, RequireReceiver.No);
		}

		public void AddUITransmission (object transmitter, EventTypes uiEvent, bool immediateStateChange)
		{
			AddUITransmission (transmitter, uiEvent, null, immediateStateChange, RequireReceiver.No);
		}

		public void AddUITransmission (object transmitter, EventTypes uiEvent, RequireReceiver requireReceiver)
		{
			AddUITransmission (transmitter, uiEvent, null, false, requireReceiver);
		}

		public void AddUITransmission (object transmitter, EventTypes uiEvent, ScriptableObject option)
		{
			AddUITransmission (transmitter, uiEvent, option, false, RequireReceiver.No);
		}

		public void AddUITransmission (object transmitter, EventTypes uiEvent, ScriptableObject option, bool immediateStateChange)
		{
			AddUITransmission (transmitter, uiEvent, option, immediateStateChange, RequireReceiver.No);
		}

		public void AddUITransmission (object transmitter, EventTypes uiEvent, ScriptableObject option, RequireReceiver requireReceiver)
		{
			AddUITransmission (transmitter, uiEvent, option, false, requireReceiver);
		}

		public void AddUITransmission (object transmitter, EventTypes uiEvent, ScriptableObject option, bool immediateStateChange, RequireReceiver requireReceiver)
		{
			AddTransmission (transmitter, new Package () {
				eventType = uiEvent,
				option = option,
				immediateStateChange = immediateStateChange
			}, requireReceiver);
		}

		#endregion

		public enum EventTypes
		{
			Clicked,
			Selected,
			Deselected
		}
		   
		public struct Package
		{
			public EventTypes eventType;
			public ScriptableObject option;
			public bool immediateStateChange;
		}

	}

}