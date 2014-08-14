using UnityEngine;

using HelloThere.InCommon;
using HelloThere.ProgrammableValidation;
using HelloThere.MultiTouch;

using Sequences;
using Relays;

namespace MultitouchUI
{

	public class Checkbox : UIElement
	{

		protected override void OnValidation(OperationResultHandler operationResultHandler)
		{
			base.OnValidation(operationResultHandler);

			OnSelected = GetElement<SequencePlayer>("Sequences/Input/OnSelected");
			OnDeselected = GetElement<SequencePlayer>("Sequences/Input/OnDeselected");
		}

		void OnEnable()
		{
			Relay.AddReceiver(OnTransmission);

			selected = false;

			OnDeselected.SampleEnd();
			OnSelected.SampleStart();
		}

		void OnDisable()
		{
			Relay.RemoveReceiver(OnTransmission);
		}

		override protected void OnReleaseInside(FingerSession fingerSession)
		{
			base.OnReleaseInside(fingerSession);

			if(selected)
			{
				Relay.AddUITransmission(this, UIEvents.Deselected);
			}
			else
			{
				Relay.AddUITransmission(this, UIEvents.Selected);
			}
		}

		void OnTransmission(object transmitter, UIPackage package)
		{
			SequencePlayer sequencePlayer = null;

			switch(package.Event)
			{
				case UIEvents.Selected:
					sequencePlayer = OnSelected;
					break;

				case UIEvents.Deselected:
					sequencePlayer = OnDeselected;
					break;
			}

			if(sequencePlayer != null)
			{
				if(package.ImmediateStateChange)
				{
					sequencePlayer.SampleEnd();
				}
				else
				{
					sequencePlayer.Play();
				}
			}
		}

		private SequencePlayer OnSelected;
		private SequencePlayer OnDeselected;
		private bool selected;

		public ScriptableObject Option;

	}

}