/// TODO: add disabled state

using UnityEngine;

using HelloThere.InCommon;
using HelloThere.ProgrammableValidation;
using HelloThere.MultiTouch;

using Sequences;
using Relays;

namespace MultitouchUI
{

	public class RadioButton : UIElement
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

			Relay.AddUITransmission(this, UIEventTypes.Selected, Option, RequireReceiver);
		}

		void OnTransmission(object transmitter, UIPackage package)
		{
			SequencePlayer sequencePlayer = null;

			if(package.eventType == UIEventTypes.Selected)
			{
				if(transmitter as RadioButton == this)
				{
					sequencePlayer = OnSelected;
				}
				else
				{
					sequencePlayer = OnDeselected;

					Relay.AddUITransmission(this, UIEventTypes.Deselected, Option);
				}

				if(package.immediateStateChange)
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

		public ScriptableObject Option;

	}

}