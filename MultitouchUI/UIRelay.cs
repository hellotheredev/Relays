using UnityEngine;

using HelloThere.InCommon;
using HelloThere.ProgrammableValidation;
using HelloThere.ProgrammableResource;

using Relays;

namespace MultitouchUI
{

	public class UIRelay : Relay<UIPackage>
	{

		public void AddUITransmission(object transmitter, UIEventTypes uiEvent)
		{
			AddUITransmission(transmitter, uiEvent, null, false, RequireReceiver.No);
		}

		public void AddUITransmission(object transmitter, UIEventTypes uiEvent, bool immediateStateChange)
		{
			AddUITransmission(transmitter, uiEvent, null, immediateStateChange, RequireReceiver.No);
		}

		public void AddUITransmission(object transmitter, UIEventTypes uiEvent, RequireReceiver requireReceiver)
		{
			AddUITransmission(transmitter, uiEvent, null, false, requireReceiver);
		}

		public void AddUITransmission(object transmitter, UIEventTypes uiEvent, ScriptableObject option)
		{
			AddUITransmission(transmitter, uiEvent, option, false, RequireReceiver.No);
		}

		public void AddUITransmission(object transmitter, UIEventTypes uiEvent, ScriptableObject option, bool immediateStateChange)
		{
			AddUITransmission(transmitter, uiEvent, option, immediateStateChange, RequireReceiver.No);
		}

		public void AddUITransmission(object transmitter, UIEventTypes uiEvent, ScriptableObject option, RequireReceiver requireReceiver)
		{
			AddUITransmission(transmitter, uiEvent, option, false, requireReceiver);
		}

		public void AddUITransmission(object transmitter, UIEventTypes uiEvent, ScriptableObject option, bool immediateStateChange, RequireReceiver requireReceiver)
		{
			AddTransmission(transmitter, new UIPackage()
			{
				eventType = uiEvent,
				option = option,
				immediateStateChange = immediateStateChange
			}, requireReceiver);
		}

	}

	public struct UIPackage
	{
		public UIEventTypes eventType;
		public ScriptableObject option;
		public bool immediateStateChange;
	}

	public enum UIEventTypes
	{
		Clicked,
		Selected,
		Deselected
	}

}