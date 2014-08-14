using UnityEngine;

using HelloThere.InCommon;
using HelloThere.ProgrammableValidation;
using HelloThere.ProgrammableResource;

using Relays;

namespace MultitouchUI
{

	public class UIRelay : Relay<UIPackage>
	{

		public void AddUITransmission(object transmitter, UIEvents uiEvent)
		{
			AddUITransmission(transmitter, uiEvent, null, false, RequireReceiver.No);
		}

		public void AddUITransmission(object transmitter, UIEvents uiEvent, bool immediateStateChange)
		{
			AddUITransmission(transmitter, uiEvent, null, immediateStateChange, RequireReceiver.No);
		}

		public void AddUITransmission(object transmitter, UIEvents uiEvent, RequireReceiver requireReceiver)
		{
			AddUITransmission(transmitter, uiEvent, null, false, requireReceiver);
		}

		public void AddUITransmission(object transmitter, UIEvents uiEvent, ScriptableObject option)
		{
			AddUITransmission(transmitter, uiEvent, option, false, RequireReceiver.No);
		}

		public void AddUITransmission(object transmitter, UIEvents uiEvent, ScriptableObject option, bool immediateStateChange)
		{
			AddUITransmission(transmitter, uiEvent, option, immediateStateChange, RequireReceiver.No);
		}

		public void AddUITransmission(object transmitter, UIEvents uiEvent, ScriptableObject option, RequireReceiver requireReceiver)
		{
			AddUITransmission(transmitter, uiEvent, option, false, requireReceiver);
		}

		public void AddUITransmission(object transmitter, UIEvents uiEvent, ScriptableObject option, bool immediateStateChange, RequireReceiver requireReceiver)
		{
			AddTransmission(transmitter, new UIPackage()
			{
				Event = uiEvent,
				Option = option,
				ImmediateStateChange = immediateStateChange
			}, requireReceiver);
		}

	}

	public struct UIPackage
	{
		public UIEvents Event;
		public ScriptableObject Option;
		public bool ImmediateStateChange;
	}

	public enum UIEvents
	{
		Clicked,
		Selected,
		Deselected
	}

}